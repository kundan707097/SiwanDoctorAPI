using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.PaymentAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.PaymentInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentAppServices _paymentAppServices;
        public PaymentController(IPaymentAppServices paymentAppServices)
        {
            _paymentAppServices = paymentAppServices;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromForm] PaymentRequestModel model)
        {
            var order = await _paymentAppServices.CreateOrderAsync(model);
            return Ok(order);
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromForm] PaymentVerificationModel model)
        {
            var result = await _paymentAppServices.VerifyPaymentAsync(model);
            return Ok(result);
        }
    }
}
