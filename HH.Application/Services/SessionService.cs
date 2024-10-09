using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
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

        public async Task<ApiResponse<int>> Create(SessionCreateDto request)
        {
            var Session = request.Adapt<Session>();
            Session.StartDate = DateTime.Now;

            await _unitOfWork.Resolve<Session>().CreateAsync(Session);
            await _unitOfWork.SaveChangesAsync();

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
            var SessionGetDto = Session.Adapt<SessionGetDto>();

            if (Session == null)
                return Failed<SessionGetDto>("Không tìm thấy");

            return Success<SessionGetDto>(SessionGetDto);
        }

        public async Task<ApiResponse<List<Session>>> Gets(SearchBaseRequest request)
        {
            var Sessions = await _unitOfWork.Resolve<Session>().GetAllAsync();
            Sessions = Sessions.OrderBy(Session => -Session.Id);

            if (Sessions == null)
                return Failed<List<Session>>("Không tìm thấy");
            return Success<List<Session>>(Sessions.ToList());
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
