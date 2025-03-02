using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.PaymentInputDTO;

namespace SiwanDoctorAPI.AppServices.PaymentAppServices
{
    public interface IPaymentAppServices: IApplicationService
    {
        Task<object> CreateOrderAsync(PaymentRequestModel model);
        Task<object> VerifyPaymentAsync(PaymentVerificationModel model);
    }
}
