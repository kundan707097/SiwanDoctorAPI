using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.CouponInputDTO;

namespace SiwanDoctorAPI.AppServices.CouponAppService
{
    public interface ICouponAppService : IApplicationService
    {
        Task<CouponResponse> AddCouponAsync(CouponRequest couponDto);
        Task<CouponResponse> UpdateCouponAsync(UpdateCouponDto couponDto);
        Task<CouponListResponse> GetCouponsAsync();
        Task<CouponListResponse> GetActiveCouponsAsync();
        Task<GetCouponResponse> GetCouponByIdAsync(int id);
        Task<CouponValidationResponse> ValidateCouponAsync(string title, int userId);
        Task<CouponResponse> DeleteCouponAsync(int id);
    }
}
