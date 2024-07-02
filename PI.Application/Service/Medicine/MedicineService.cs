using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Medicine;
using PI.Domain.Infrastructure.Api;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class MedicineService : BaseService, IMedicineService
    {
        private readonly IApiHelper _apiHelper;

        public MedicineService(IUnitOfWork unitOfWork, IApiHelper apiHelper) : base(unitOfWork)
        {
            _apiHelper = apiHelper;
        }

        //fetch external api to post medicine list

        private async Task<MedicineInfoSystemResponse> GetMedicineListFromExternalApi()
        {
            var url = $"https://dichvucong.dav.gov.vn/api/services/app/soDangKy/GetAllPublicServerPaging";
            //create paylod json

            var payload = new MedicinePayload();
            var response = await _apiHelper.PostAsync<MedicineInfoSystemResponse>(url, payload);
            return response;
        }

        public async Task CreateMedicine()
        {
            try
            {
                var response = await GetMedicineListFromExternalApi();
                await _unitOfWork.BeginTransactionAsync();
                //handle save data to db
                foreach (var medicine in response.Result.Items)
                {
                    //check if medicine exist in db
                    var medicineInDb = await _unitOfWork.Resolve<IMedicineRepository>()
                        .FindByRegistrationNo(medicine.SoDangKy);

                    if (medicineInDb != null)
                    {
                        //check manufacturer exist
                        var manufacturerInDb = await _unitOfWork.Resolve<Manufacturer>()
                            .FindAsync(x => x.Name == medicine.CongTySanXuat.TenCongTySanXuat);
                        if (manufacturerInDb == null)
                        {
                            //insert manufacturer
                            var newManufacturer = new Manufacturer()
                            {
                                Name = medicine.CongTySanXuat.TenCongTySanXuat,
                                Nation = medicine.CongTySanXuat.NuocSanXuat,
                                Address = medicine.CongTySanXuat.DiaChiSanXuat
                            };
                            await _unitOfWork.Resolve<Manufacturer>().CreateAsync(newManufacturer);
                            await _unitOfWork.SaveChangesAsync();
                            manufacturerInDb = newManufacturer;
                        }
                        medicineInDb.ManufacturerId = manufacturerInDb.ManufacturerId;
                        
                        
                        //update medicine
                        medicineInDb.Name = medicine.TenThuoc;
                        
                        //update ingredients with many to many relationship
                        var ingredients = medicine.ThongTinThuocCoBan.HoatChatChinh.Split(";");

                        //remove all ingredient of medicine
                        medicineInDb.Ingredients.Clear();
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var ingredient in ingredients)
                        {
                            var ingredientInDb = await _unitOfWork.Resolve<Ingredient>()
                                .FindAsync(x => x.FullName == ingredient);

                            if (ingredientInDb == null)
                            {
                                var newIngredient = new Ingredient()
                                {
                                    FullName = ingredient
                                };

                                await _unitOfWork.Resolve<Ingredient>().CreateAsync(newIngredient);
                                await _unitOfWork.SaveChangesAsync();
                                //save many to many relationship
                                medicineInDb.Ingredients.Add(newIngredient);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            else
                            {
                                //save many to many relationship
                                medicineInDb.Ingredients.Add(ingredientInDb);
                                await _unitOfWork.SaveChangesAsync();
                            }
                        }

                        // await _unitOfWork.Resolve<Medicine>().UpdateAsync(medicineInDb);

                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        //check manufacturer exist
                        var manufacturerInDb = await _unitOfWork.Resolve<Manufacturer>()
                            .FindAsync(x => x.Name == medicine.CongTySanXuat.TenCongTySanXuat);
                        if (manufacturerInDb == null)
                        {
                            //insert manufacturer
                            var newManufacturer = new Manufacturer()
                            {
                                Name = medicine.CongTySanXuat.TenCongTySanXuat,
                                Nation = medicine.CongTySanXuat.NuocSanXuat,
                                Address = medicine.CongTySanXuat.DiaChiSanXuat
                            };
                            await _unitOfWork.Resolve<Manufacturer>().CreateAsync(newManufacturer);
                            await _unitOfWork.SaveChangesAsync();
                            manufacturerInDb = newManufacturer;
                        }
                        
                        //check unit
                        var unitInDb = await _unitOfWork.Resolve<Unit>()
                            .FindAsync(x =>  medicine.ThongTinThuocCoBan.DongGoi.Contains(x.Name));
                        if (unitInDb == null)
                        {
                            //insert unit
                            var newUnit = new Unit()
                            {
                                Name = medicine.ThongTinThuocCoBan.DongGoi
                            };
                            await _unitOfWork.Resolve<Unit>().CreateAsync(newUnit);
                            await _unitOfWork.SaveChangesAsync();
                            unitInDb = newUnit;
                        }
                            
                        //insert medicine
                        var newMedicine = new Medicine()
                        {
                            Name = medicine.TenThuoc,
                            RegistrationNo = medicine.SoDangKy,
                            PackingSize = medicine.ThongTinThuocCoBan.DongGoi,
                            ManufacturerId = manufacturerInDb.ManufacturerId,
                            UnitId = unitInDb.UnitId
                        };
                        await _unitOfWork.Resolve<Medicine>().CreateAsync(newMedicine);
                        await _unitOfWork.SaveChangesAsync();
                        //save many to many relationship
                        var ingredients = medicine.ThongTinThuocCoBan.HoatChatChinh.Split(";");
                        foreach (var ingredient in ingredients)
                        {
                            var ingredientInDb = await _unitOfWork.Resolve<Ingredient>()
                                .FindAsync(x => x.FullName == ingredient);
                            if (ingredientInDb == null)
                            {
                                var newIngredient = new Ingredient()
                                {
                                    FullName = ingredient
                                };

                                await _unitOfWork.Resolve<Ingredient>().CreateAsync(newIngredient);
                                await _unitOfWork.SaveChangesAsync();
                                //save many to many relationship
                                newMedicine.Ingredients.Add(newIngredient);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            else
                            {
                                //save many to many relationship
                                newMedicine.Ingredients.Add(ingredientInDb);
                                await _unitOfWork.SaveChangesAsync();
                            }
                        }
                    }
                }

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagingApiResponse<MedicineResponse>> SearchMedicineAsync(string keySearch,
            PagingQuery pagingQuery, string orderBy)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Medicine>()
                    .SearchAsync<MedicineResponse>(keySearch, pagingQuery, orderBy);
                return Success(result);
            }
            catch (Exception ex)
            {
                return PagingFailed<MedicineResponse>(ex.GetExceptionMessage());
            }
        }
        
    }
}