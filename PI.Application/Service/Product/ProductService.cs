using PI.Domain.Dto.Product;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ICurrentAccount _currentAccount;

        public ProductService(IUnitOfWork unitOfWork, ICurrentAccount currentAccount) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
        }

        //get product detail by productId
        public async Task<ApiResponse<ProductResponse>> GetProductDetail(int productId)
        {
            var product = await _unitOfWork.Resolve<IProductRepository>().GetProductDetail
                (productId);

            if (product == null)
            {
                return Failed<ProductResponse>("Product not found", HttpStatusCode.NotFound);
            }

            return Success(product);
        }

        public async Task<PagingApiResponse<ProductResponse>> SearchProduct(SearchProductRequest request)
        {
            try
            {
                var response = await _unitOfWork.Resolve<IProductRepository>()
                    .SearchAsync(request);

                foreach (var item in response)
                {
                    foreach (var unit in item.ProductUnits)
                    {
                        var stockQuantity = await _unitOfWork.Resolve<ProductStock>()
                            .FindAsync(p => p.ProductUnitId == unit.ProductUnitId);
                        //unit.StockQuantity = stockQuantity?.StockQuantity ?? 0;
                    }
                    //convert double to long
                    //item.TotalQuantity =  (long) item.ProductUnits.Sum(p => p.StockQuantity);
                    //check is low on stock
                    //item.IsLowOnStock = await IsLowOnStock(item.Category.CategoryId, item.TotalQuantity);
                }

                return Success(response);
            }
            catch (Exception ex)
            {
                return PagingFailed<ProductResponse>(ex.GetExceptionMessage());
            }
        }


        public async Task<ApiResponse<string>> CreateProduct(CreateProductRequest request)
        {
            try
            {
                // Validate request
                //validate catergory
                await _unitOfWork.BeginTransactionAsync();

                var isMedicineCategory = await CheckValidCategory(request.CategoryId);
                if (isMedicineCategory)
                {
                    throw new ArgumentException("Category is not valid");
                }

                await CheckValidManufacturer(request.ManufacturerId);

                var product = request.Adapt<Product>();
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.CreatedBy = _currentAccount.GetAccountId();
                product.UpdatedBy = _currentAccount.GetAccountId();

                await _unitOfWork.Resolve<Product>().CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                //save image
                await SaveImage(request.ImageUrls, product.ProductId);

                //save product unit
                await SaveProductUnit(request.Units, product.ProductId, product.Name, request.MasterUnitId);

                //save product attribute
                foreach (var attribute in request.ProductAttributes)
                {
                    var productAttribute = attribute.Adapt<ProductAttributeMapping>();
                    productAttribute.ProductId = product.ProductId;
                    await _unitOfWork.Resolve<ProductAttributeMapping>().CreateAsync(productAttribute);
                }

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();
                return Success<string>("Create product success");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(ex.GetExceptionMessage(), HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponse<string>> CreateMedicine(CreateProductMedicineRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var isMedicineCategory = await CheckValidCategory(request.CategoryId);
                if (!isMedicineCategory)
                {
                    throw new ArgumentException("Category is not valid");
                }

                var medicine = await _unitOfWork.Resolve<Medicine>().FindAsync(p => p.MedicineId == request.MedicineId);
                if (medicine == null)
                {
                    throw new ArgumentException("Medicine not found");
                }


                var product = request.Adapt<Product>();
                product.Name = medicine.Name;
                product.PackingSize = medicine.PackingSize ?? null;
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.MedicineId = medicine.MedicineId;
                product.CreatedBy = _currentAccount.GetAccountId();
                product.UpdatedBy = _currentAccount.GetAccountId();

                await _unitOfWork.Resolve<Product>().CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                //save image
                await SaveImage(request.ImageUrls, product.ProductId);

                //save product unit
                await SaveProductUnit(request.Units, product.ProductId, product.Name, medicine.UnitId);
                await _unitOfWork.CommitTransactionAsync();
                return Success<string>("Create product success");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(ex.GetExceptionMessage(), HttpStatusCode.BadRequest);
            }
        }

        //update product
        public async Task<ApiResponse<string>> UpdateProduct(UpdateProductRequest request, int productId)
        {
            try
            {
                var product = await _unitOfWork.Resolve<Product>().FindAsync(p => p.ProductId == productId);
                if (product == null)
                {
                    throw new ArgumentException("Product not found");
                }

                await _unitOfWork.BeginTransactionAsync();
                //validate catergory
                var isMedicineCategory = await CheckValidCategory(request.CategoryId);
                if (isMedicineCategory)
                {
                    throw new ArgumentException("Category is not valid");
                }

                //validate manufacturer
                await CheckValidManufacturer(request.ManufacturerId);
                await UpdateProductUnit(request.Units, productId, product.Name, request.MasterUnitId);
                await UpdateProductImage(request.ImageUrls, productId);
                product = request.Adapt(product);
                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = _currentAccount.GetAccountId();
                await _unitOfWork.Resolve<Product>().UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Success<string>("Update product success");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<string>> UpdateMedicine(UpdateMedicineRequest request, int productId)
        {
            try
            {
                var product = await _unitOfWork.Resolve<Product>().FindAsync(p => p.ProductId == productId);
                if (product == null)
                {
                    throw new ArgumentException("Product not found");
                }
                var medicine = await _unitOfWork.Resolve<Medicine>().FindAsync(p => p.MedicineId == request.MedicineId);
                if (medicine == null)
                {
                    throw new ArgumentException("Medicine not found");
                }
                await _unitOfWork.BeginTransactionAsync();
                //validate catergory
                var isMedicineCategory = await CheckValidCategory(request.CategoryId);
                if (!isMedicineCategory)
                {
                    throw new ArgumentException("Category is not valid");
                }

                await UpdateProductUnit(request.Units, productId, product.Name, medicine.UnitId);
                await UpdateProductImage(request.ImageUrls, productId);

                product = request.Adapt(product);
                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = _currentAccount.GetAccountId();
                await _unitOfWork.Resolve<Product>().UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Update product success");
            }
            catch (Exception ex)
            {
                return Failed<string>("Update product failed");
            }
        }

        //save image
        private async Task SaveImage(List<string> imageUrls, int productId)
        {
            foreach (var imageUrl in imageUrls)
            {
                var image = new ProductImage()
                {
                    ProductId = productId,
                    ImageUrl = imageUrl
                };
                await _unitOfWork.Resolve<ProductImage>().CreateAsync(image);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        //save product unit
        private async Task SaveProductUnit(List<CreateProductUnitRequest> units, int productId, string productName,
            int masterUnitId)
        {
            // Save master product unit
            await FindUnitById(masterUnitId);


            var masterProductUnit = await CreateProductUnit(productId, productName, masterUnitId, null, null);

            foreach (var item in units)
            {
                var unit = await FindUnitById(item.UnitId);
                if (unit == null)
                {
                    throw new ArgumentException("Unit not found");
                }

                await CreateProductUnit(productId, productName, item.UnitId, masterProductUnit.ProductUnitId,
                    item.ConversionValue);
            }
        }

        private async Task<Unit> FindUnitById(int unitId)
        {
            var unit = await _unitOfWork.Resolve<Unit>().FindAsync(x => x.UnitId == unitId);
            if (unit == null)
                throw new ArgumentException("Unit not found");
            return unit;
        }

        private async Task<ProductUnit> CreateProductUnit(int productId, string productName, int unitId, int? parentId,
            int? conversionValue)
        {
            var skuCode = GenerateSkuCode(productId, unitId);
            var unit = await FindUnitById(unitId);


            var productUnit = new ProductUnit()
            {
                ProductId = productId,
                UnitId = unitId,
                ParentId = parentId ?? null,
                ConversionValue = conversionValue ?? null,
                SkuCode = skuCode,
                Name = $"{productName} - {unit.Name}"
            };

            await _unitOfWork.Resolve<ProductUnit>().CreateAsync(productUnit);
            await _unitOfWork.SaveChangesAsync();

            return productUnit;
        }

        private string GenerateSkuCode(int productId, int unitId)
        {
            return $"SP{Math.Abs(productId).ToString().PadLeft(2, '0')}{Math.Abs(unitId).ToString().PadLeft(2, '0')}";
        }


        public async Task<ApiResponse<bool>> DeleteProduct(int[] ids)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Resolve<Product>().DeleteAsync(ids);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return Success(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<bool>(ex.GetExceptionMessage(), HttpStatusCode.BadRequest);
            }
        }

        private async Task UpdateProductUnit(List<CreateProductUnitRequest> units, int productId, string productName,
            int masterUnitId)
        {
            //update product unit
            //compare old unit with new unit
            var unitIds = units.Select(p => p.UnitId).ToList();
            var oldProductUnits = await _unitOfWork.Resolve<ProductUnit>().FindListAsync(p => p.ProductId == productId);
            var oldUnitIds = oldProductUnits.Select(p => p.UnitId).ToList();
            var deleteUnitIds = oldUnitIds.Except(unitIds).ToList();
            var addUnitIds = unitIds.Except(oldUnitIds).ToList();

            //delete product unit
            foreach (var deleteUnitId in deleteUnitIds)
            {
                var productUnit = oldProductUnits.FirstOrDefault(p => p.UnitId == deleteUnitId);
                if (productUnit != null)
                {
                    productUnit.IsDeleted = true;
                    await _unitOfWork.Resolve<ProductUnit>().UpdateAsync(productUnit);
                }
            }

            var addUnits = units.Where(p => addUnitIds.Contains(p.UnitId)).ToList();

            //add product unit
            await SaveProductUnit(addUnits, productId, productName, masterUnitId);
        }

        private async Task UpdateProductImage(List<string> newImageUrls, int productId)
        {
            //update product image
            var oldProductImages =
                await _unitOfWork.Resolve<ProductImage>().FindListAsync(p => p.ProductId == productId);
            var oldImageUrls = oldProductImages.Select(p => p.ImageUrl).ToList();
            var deleteImageUrls = oldImageUrls.Except(newImageUrls).ToList();
            var addImageUrls = newImageUrls.Except(oldImageUrls).ToList();

            //delete product image
            foreach (var deleteImageUrl in deleteImageUrls)
            {
                var productImage = oldProductImages.FirstOrDefault(p => p.ImageUrl == deleteImageUrl);
                if (productImage != null)
                {
                    productImage.IsDeleted = true;
                    await _unitOfWork.Resolve<ProductImage>().UpdateAsync(productImage);
                }
            }

            //add product image
            await SaveImage(addImageUrls, productId);
        }

        //check valid category for product is not thuoc
        public async Task<bool> CheckValidCategory(int categoryId)
        {
            var category = await _unitOfWork.Resolve<ICategoryRepository>().FindById(categoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found");
            }

            if (category.HasChildren || category.ParentId == null)
            {
                throw new ArgumentException("Category is not valid");
            }

            var parentCategory = await _unitOfWork.Resolve< ICategoryRepository>()
                .FindById(category.ParentId.Value);
            if (parentCategory is { Name: "Thuốc" })
            {
                return true;
            }

            return false;
        }

        //check valid manufacturer
        public async Task CheckValidManufacturer(int manufacturerId)
        {
            var manufacturer =
                await _unitOfWork.Resolve<Manufacturer>().FindAsync(p => p.ManufacturerId == manufacturerId);
            if (manufacturer == null)
            {
                throw new ArgumentException("Manufacturer not found");
            }
        }

        //check is low on stock
        private async Task<bool> IsLowOnStock(int categoryId, long? totalQuantity)
        {
            if (totalQuantity == null)
                return true;

            var categoryLimit = await _unitOfWork.Resolve<CategorySetting>().FindAsync(p => p.CategoryId == categoryId);

            if (categoryLimit == null)
            {
                return false;
            }

            return totalQuantity < categoryLimit.MinQuantity;
        }
    }
}