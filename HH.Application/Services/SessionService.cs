using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Enums;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Domain.Repositories.Common;
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
            var Session = await _unitOfWork.Resolve<Session>().FindAsync(id);
            if (Session == null)
                return Failed<bool>("Không tìm thấy");

            if (Session.EndDate != null)
                return Failed<bool>("Phiên đã đóng");

            Session.EndDate = DateTime.Now;
            Session.Status = SessionStatus.Closed.ToString();

            await _unitOfWork.Resolve<Session>().UpdateAsync(Session);
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
                                      .FirstOrDefault(p => p.TankId == pump.TankId && p.Price.HasValue && p.Price != 0)?
                                      .Price ?? 0;
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
            await _unitOfWork.SaveChangesAsync();
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<SessionGetDto>> Get(int id)
        {
            var Session = await _unitOfWork.Resolve<Session>().FindAsync(id);

            if (Session == null)
                return Failed<SessionGetDto>("Không tìm thấy");

            Session.Expenses = Session.Expenses.OrderBy(Expense => -Expense.Id).ToList();
            var SessionGetDto = Session.Adapt<SessionGetDto>();

            return Success<SessionGetDto>(SessionGetDto);
        }

        public async Task<ApiResponse<List<Session>>> Gets(SearchBaseRequest request)
        {
            var res = await _unitOfWork.Resolve<ISessionRepository>().FindAll();
            var Sessions = res.Adapt<List<Session>>().OrderBy(Session => -Session.Id).ToList();

            if (Sessions == null)
                return Failed<List<Session>>("Không tìm thấy");
            return Success<List<Session>>(Sessions);
        }

        public async Task<ApiResponse<bool>> Update(SessionUpdateDto request)
        {
            var Session = await _unitOfWork.Resolve<Session>().FindAsync(request.Id);

            if (Session == null)
                return Failed<bool>("Không tìm thấy");

            var updatedSession = request.Adapt<Session>();

            await _unitOfWork.Resolve<Session>().UpdateAsync(updatedSession);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Cập nhật thành công");
        }
    }
}
