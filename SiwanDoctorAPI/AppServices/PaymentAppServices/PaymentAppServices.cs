using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Payment_Entity;
using SiwanDoctorAPI.Model.InputDTOModel.PaymentInputDTO;
using System.Threading.Tasks;

namespace SiwanDoctorAPI.AppServices.PaymentAppServices
{
    public class PaymentAppServices : IPaymentAppServices
    {
        
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly string _razorpayKey;
        private readonly string _razorpaySecret;
        public PaymentAppServices(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _razorpayKey = configuration["Razorpay:KeyId"];
            _razorpaySecret = configuration["Razorpay:KeySecret"];

        }

        public async Task<object> CreateOrderAsync(PaymentRequestModel model)
        {
            var client = new RazorpayClient(_razorpayKey, _razorpaySecret);
            var options = new Dictionary<string, object>
            {
                { "amount", model.Amount * 100 }, // Razorpay expects amount in paise
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString() },
                { "payment_capture", 1 }
            };

            Order order = client.Order.Create(options);

            var payment = new PaymentDetails
            {
                AppointmentId = model.AppointmentId,
                RazorpayOrderId = order["id"].ToString(),
                Amount = model.Amount,
                Currency = "INR",
                Status = "Created"
            };

            _applicationDbContext.paymentDetails.Add(payment);
            await _applicationDbContext.SaveChangesAsync();

            return new { orderId = order["id"].ToString(), amount = model.Amount, currency = "INR" };
        }

        public async Task<object> VerifyPaymentAsync(PaymentVerificationModel model)
        {
            try
            {
                var attributes = new Dictionary<string, string>
                {
                    { "razorpay_order_id", model.OrderId },
                    { "razorpay_payment_id", model.PaymentId },
                    { "razorpay_signature", model.Signature }
                };

                Utils.verifyPaymentSignature(attributes);

                var payment = await _applicationDbContext.paymentDetails
                    .FirstOrDefaultAsync(p => p.RazorpayOrderId == model.OrderId);

                if (payment != null)
                {
                    payment.Status = "Paid";
                    payment.RazorpayPaymentId = model.PaymentId;
                    payment.RazorpaySignature = model.Signature;

                    var appointment = await _applicationDbContext.appointments.FindAsync(payment.AppointmentId);
                    if (appointment != null)
                    {
                        appointment.PaymentStatus = "Paid";
                    }

                    await _applicationDbContext.SaveChangesAsync();
                }

                return new { status = "Payment Verified Successfully" };
            }
            catch (Exception ex)
            {
                return new { status = "Payment Verification Failed", error = ex.Message };
            }
        }
    }

}
