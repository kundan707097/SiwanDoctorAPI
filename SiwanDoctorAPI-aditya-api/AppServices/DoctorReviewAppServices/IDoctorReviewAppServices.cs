using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorReview;

namespace SiwanDoctorAPI.AppServices.DoctorReviewAppServices
{
    public interface IDoctorReviewAppServices : IApplicationService
    {
        Task<AddDoctorReviewResponse> AddDoctorReviewAsync(AddDoctorReviewRequest request);
        Task<DoctorReviewResponse> GetDoctorReviewsAsync(int doctorId);
        Task<IEnumerable<DoctorReviewData>> GetAllReviewsAsync();
        Task<IEnumerable<DoctorReviewData>> GetByIdReviewsAsync(int id);
    }
}
