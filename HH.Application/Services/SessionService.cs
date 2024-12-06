using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Enums;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class SessionService : BaseService, ISessionService
    {
        public SessionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Close(int id)
        {
            var Session = await _unitOfWork.Resolve<ISessionRepository>().FindSingleAsync(id);
            if (Session == null)
                return Failed<bool>("Không tìm thấy");

            if (Session.EndDate != null)
                return Failed<bool>("Phiên đã đóng");

            // Update volume_used and add fuelImportSession for each pump
            var importDetails = new List<FuelImportSession>();
            var fuelImport = await _unitOfWork.Resolve<FuelImport>().GetAllAsync();
            if (fuelImport == null)
                return Failed<bool>("Không thấy đợt nhập nào");

            // with each pump in a session
            foreach (var pump in Session.PetrolPumps)
            {
                var tankId = pump.TankId;
                var totalVolumeUsed = pump.TotalVolume;

                // For from oldest to newest fuel import
                foreach (var import in fuelImport.OrderBy(x => x.ImportDate))
                {
                    if (totalVolumeUsed <= 0) break;
                    if (import.Status == "Closed")                continue;
                    if (import.VolumeUsed >= import.ImportVolume) continue;
                    if (import.TankId != tankId)                  continue;

                    // Calculate volume used for this import
                    var volumeUsed = Math.Min((import?.ImportVolume ?? 0) - (import?.VolumeUsed ?? 0), 
                                              (totalVolumeUsed ?? 0));

                    if (volumeUsed <= 0) break;

                    import.VolumeUsed = (import.VolumeUsed ?? 0) + volumeUsed;
                    import.TotalSalePrice = (import.TotalSalePrice ?? 0) + volumeUsed * pump.Price;
                    if (import.VolumeUsed >= import.ImportVolume)
                        import.Status = "Closed";

                    //add new fuelImportSession if volumeUsed > 0
                    var fuelImportSession = new FuelImportSession
                    {
                        SessionId = Session.Id,
                        FuelImportId = import.Id,
                        VolumeUsed = volumeUsed,
                        SalePrice = volumeUsed * pump.Price, 
                    };
                    importDetails.Add(fuelImportSession);

                    totalVolumeUsed -= (int) volumeUsed;
                }
                if (totalVolumeUsed > 0)
                    return Failed<bool>("Không đủ dữ liệu nhập nhiên liệu để xuất");
            }

            Session.EndDate = DateTime.Now;
            Session.Status = SessionStatus.Closed.ToString();

            await _unitOfWork.Resolve<Session>().UpdateAsync(Session);
            await _unitOfWork.Resolve<FuelImport>().UpdateAsync(fuelImport.ToArray());
            await _unitOfWork.Resolve<FuelImportSession>().UpdateAsync(importDetails.ToArray());
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>(true, "Đóng phiên thành công");

        }

        public async Task<ApiResponse<int>> Create(SessionCreateDto request)
        {
            var Session = request.Adapt<Session>();

            Session.StartDate = DateTime.Now;
            Session.TotalExpense = 0;

            var lastPump = (await _unitOfWork.Resolve<PetrolPump>().GetAllAsync());
            Session.PetrolPumps.Select(pump =>
            {
                pump.Price = lastPump?.OrderBy(p => -p.SessionId)
                                      .Where(p => p.IsDeleted == false)
                                      .FirstOrDefault(p => p.TankId == pump.TankId && p.Price.HasValue && p.Price != 0)?
                                      .Price ?? 0;
                if (pump.StartVolume == 0)
                    pump.StartVolume = lastPump?.OrderBy(p => -p.SessionId)
                                            .Where(p => p.IsDeleted == false)
                                            .FirstOrDefault(p => p.TankId == pump.TankId && p.EndVolume != 0)?
                                            .EndVolume ?? 0;
                return pump;
            }).ToList();

            await _unitOfWork.Resolve<Session>().CreateAsync(Session);
            await _unitOfWork.SaveChangesAsync();

            // Create first cash
            if (Session.CashForChange > 0)
            {
                var firstCash = new ExpenseCreateDto
                {
                    SessionId = Session.Id,
                    ExpenseTypeId = 1,
                    Amount = Session.CashForChange ?? 0
                };
                var newCash = firstCash.Adapt<Expense>();
                await _unitOfWork.Resolve<Expense>().CreateAsync(newCash);
                await _unitOfWork.SaveChangesAsync();
            }



            return Success<int>(Session.Id, "Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<Session>().DeleteAsync(id);

            var FuelImportSession = await _unitOfWork.Resolve<FuelImportSession>().FindListAsync(x => x.SessionId == id);

            // delete all fuelImportSession relate + remove volume from total
            foreach (var item in FuelImportSession)
            {
                item.IsDeleted = true;

                var fuelImport = await _unitOfWork.Resolve<FuelImport>().FindAsync(item.FuelImportId);
                fuelImport.VolumeUsed -= item.VolumeUsed;
                fuelImport.TotalSalePrice -= item.SalePrice;
                if (fuelImport.VolumeUsed < fuelImport.ImportVolume)
                    fuelImport.Status = "Processing";
            }

            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<SessionGetDto>> Get(int id)
        {
            var Session = await _unitOfWork.Resolve<ISessionRepository>().FindAsync(id);

            if (Session == null)
                return Failed<SessionGetDto>("Không tìm thấy");

            Session.Expenses = Session.Expenses.OrderBy(Expense => -Expense.Id).ToList();
            var SessionGetDto = Session.Adapt<SessionGetDto>();

            return Success<SessionGetDto>(SessionGetDto);
        }

        public async Task<ApiResponse<List<SessionGetDto>>> Gets(SearchBaseRequest request)
        {
            var res = await _unitOfWork.Resolve<ISessionRepository>().FindAll();
            var Sessions = res.Adapt<List<SessionGetDto>>().OrderBy(Session => -Session.Id).ToList();

            if (Sessions == null)
                return Failed<List<SessionGetDto>>("Không tìm thấy");
            return Success<List<SessionGetDto>>(Sessions);
        }

        public async Task<ApiResponse<bool>> Update(SessionUpdateDto request)
        {
            var Session = await _unitOfWork.Resolve<ISessionRepository>().FindSingleAsync(request.Id);

            if (Session == null)
                return Failed<bool>("Không tìm thấy");

            var updatedSession = request.Adapt<Session>();

            await _unitOfWork.Resolve<Session>().UpdateAsync(updatedSession);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Cập nhật thành công");
        }
    }
}
