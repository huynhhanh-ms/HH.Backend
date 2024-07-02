using PI.Domain.Dto.Assignment;

namespace PI.Application.Service.Assignments
{
    public interface IAssignmentService
    {
        Task<ApiResponse<bool>> CreateAssignment(CreateAssignmentRequest request);
        Task<ApiResponse<bool>> UpdateAssignment(UpdateAssignmentRequest request);
        Task<ApiResponse<bool>> DeleteAssignment(int assignmentId);
        Task<ApiResponse<bool>> AssignAssignment(AssignAssignmentRequest request);
        Task<ApiResponse<AssignmentResponse>> GetAssignment(int assignmentId);
        Task<PagingApiResponse<AssignmentResponse>> SearchMyAssigment(SearchMyAssignmentRequest request);

    }
}
