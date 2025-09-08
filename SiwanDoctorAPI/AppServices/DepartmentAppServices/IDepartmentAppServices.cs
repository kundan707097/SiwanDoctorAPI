using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO;

namespace SiwanDoctorAPI.AppServices.DepartmentAppServices
{
    public interface IDepartmentAppServices : IApplicationService
    {
        Task<AddDepartmentResponse> AddDepartmentAsync(AddDepartmentRequest request);
        Task<UpdateDepartmentResponse> UpdateDepartmentAsync(UpdateDepartmentRequest request);

        Task<RemoveDepartmentImageResponse> RemoveDepartmentImageAsync(int id);
        Task<GetDepartmentResponse> GetDepartmentsAsync();
        Task<GetDepartmentResponse> GetActiveDepartmentsAsync();
        Task<GetSingleDepartmentResponse> GetDepartmentByIdAsync(int id);
        Task<DeleteDepartmentResponse> DeleteDepartmentAsync(int id);
    }
}
