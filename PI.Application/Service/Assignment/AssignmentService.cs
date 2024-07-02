using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Assignment;
using PI.Domain.Models;
using PI.Domain.Repositories;

namespace PI.Application.Service.Assignments
{
    internal class AssignmentService : BaseService, IAssignmentService
    {
        public AssignmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> CreateAssignment(CreateAssignmentRequest request)
        {
            var (isValid, message) = await ValidateCreateAssignment(request);
            ValidateException.ThrowIf(isValid == false, message);

            var assignment = request.Adapt<Assignment>();
            ValidateException.ThrowIfNull(assignment, "Can not parse request to entity");

            await _unitOfWork.Resolve<IAssignmentRepository>().CreateAsync(assignment);

            var result = await _unitOfWork.SaveChangesAsync();

            return Success(result > 0);
        }

        private async Task<(bool, string)> ValidateCreateAssignment(CreateAssignmentRequest request)
        {
            var isExist = await _unitOfWork.Resolve<Account>().IsExist(request.ReporterId);
            if (!isExist)
                return (false, "Reporter not found");

            if (request.AssigneeId != null)
            {
                isExist = await _unitOfWork.Resolve<Account>().IsExist(request.AssigneeId ?? 0);
                if (!isExist)
                    return (false, "Assignee not found");
            }

            return (true, string.Empty);
        }

        public async Task<ApiResponse<bool>> AssignAssignment(AssignAssignmentRequest request)
        {
            var assigment = await _unitOfWork.Resolve<IAssignmentRepository>().FindAsync(request.AssignmentId);
            ValidateException.ThrowIfNull(assigment, "Assignment not found");

            if (request.AssigneeId != null)
            {
                var isAssigneeExist = await _unitOfWork.Resolve<Account>().IsExist(request.AssigneeId ?? -1);
                ValidateException.ThrowIf(isAssigneeExist == false, "Assignee not found");
            }

            assigment.AssigneeId = request.AssigneeId;

            await _unitOfWork.Resolve<IAssignmentRepository>().UpdateAsync(assigment);
            var res = await _unitOfWork.SaveChangesAsync();
            return Success(res > 0);
        }

        public async Task<ApiResponse<bool>> DeleteAssignment(int assignmentId)
        {
            var isExistAssignment = await _unitOfWork.Resolve<IAssignmentRepository>().IsExist(assignmentId);
            ValidateException.ThrowIf(isExistAssignment == false, "Assignment not found");

            await _unitOfWork.Resolve<IAssignmentRepository>().DeleteAsync(assignmentId);
            var res = await _unitOfWork.SaveChangesAsync();
            return Success(res > 0);
        }

        public async Task<ApiResponse<bool>> UpdateAssignment(UpdateAssignmentRequest request)
        {
            var assignment = await _unitOfWork.Resolve<IAssignmentRepository>().FindAsync(request.AssignmentId);
            ValidateException.ThrowIfNull(assignment, "Assignment not found");

            var (isValid, message) = await ValidateUpdateAssignment(request);
            ValidateException.ThrowIf(isValid == false, message);

            request.Adapt(assignment);

            await _unitOfWork.Resolve<IAssignmentRepository>().UpdateAsync(assignment);

            var res = await _unitOfWork.SaveChangesAsync();

            return Success(res > 0);
        }

        private async Task<(bool, string)> ValidateUpdateAssignment(UpdateAssignmentRequest request)
        {
            var isExist = await _unitOfWork.Resolve<Account>().IsExist(request.ReporterId);
            if (!isExist)
                return (false, "Reporter not found");

            if (request.AssigneeId != null)
            {
                isExist = await _unitOfWork.Resolve<Account>().IsExist(request.AssigneeId ?? 0);
                if (!isExist)
                    return (false, "Assignee not found");
            }

            return (true, string.Empty);
        }

        public async Task<ApiResponse<AssignmentResponse>> GetAssignment(int assignmentId)
        {
            var assignment = await _unitOfWork.Resolve<IAssignmentRepository>().FindAsync(assignmentId);
            ValidateException.ThrowIfNull(assignment, "Assignment not found");

            var res = assignment.Adapt<AssignmentResponse>();
            ValidateException.ThrowIfNull(res, "Can not parse entity to response");

            return Success(res);
        }

        public async Task<PagingApiResponse<AssignmentResponse>> SearchMyAssigment(SearchMyAssignmentRequest request)
        {
            var isExist = await _unitOfWork.Resolve<Account>().IsExist(request.AccountId);
            ValidateException.ThrowIf(isExist == false, "Account not found");

            var assignments = await _unitOfWork.Resolve<IAssignmentRepository>().SearchMyAssignment(request);

            return Success(assignments);
        }
    }

}
