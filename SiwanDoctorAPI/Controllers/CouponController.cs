using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.CouponAppService;
using SiwanDoctorAPI.Model.InputDTOModel.CouponInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponAppService _couponAppService;
        public CouponController(ICouponAppService couponAppService)
        {
            _couponAppService = couponAppService;
        }


        [HttpPost("add_coupon")]
        public async Task<IActionResult> AddCoupon([FromForm] CouponRequest couponDto)
        {
            var result = await _couponAppService.AddCouponAsync(couponDto);
            return Ok(result);
        }

        [HttpPost("update_coupon")]
        public async Task<IActionResult> UpdateCoupon([FromForm] UpdateCouponDto updateCouponDto)
        {
            var result = await _couponAppService.UpdateCouponAsync(updateCouponDto);
            return Ok(result);
        }
        [HttpGet("get_coupon")]
        public async Task<IActionResult> GetCoupons()
        {
            var result = await _couponAppService.GetCouponsAsync();
            return Ok(result);
        }
        [HttpGet("get_coupon_active")]
        public async Task<IActionResult> GetActiveCoupons()
        {
            var result = await _couponAppService.GetActiveCouponsAsync();
            return Ok(result);
        }

        [HttpGet("get_coupon/{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var result = await _couponAppService.GetCouponByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { response = 404, message = "Coupon not found" });
            }
            return Ok(result);
        }
        [HttpPost("get_validate")]
        public async Task<IActionResult> ValidateCoupon(string title, int user_id)
        {
            var result = await _couponAppService.ValidateCouponAsync(title, user_id);
            return Ok(result);
        }
        [HttpPost("delete_coupon")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var result = await _couponAppService.DeleteCouponAsync(id);
            return Ok(result);
        }
    }
}
