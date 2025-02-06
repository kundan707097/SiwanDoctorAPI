using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.AppointmentAppServices
{
    public class AppointmentAppServices: IAppointmentAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public AppointmentAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            
            _userManager = userManager;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AppointmentResponse> CreateAppointment(AppointmentRequest request)
        {
            try
            {
                var appointment = new Appointment
                {
                    FK_PatientId = request.patient_id,
                    Status = request.status,
                    Date = request.date,
                    TimeSlots = request.time_slots,
                    FK_DoctId = request.doct_id,
                    DeptId = request.dept_id,
                    Type = request.type,
                    MeetingId = request.meeting_id,
                    MeetingLink = request.meeting_link,
                    Fee = request.fee,
                    ServiceCharge = request.service_charge,
                    TotalAmount = request.total_amount,
                    InvoiceDescription = request.invoice_description,
                    PaymentMethod = request.payment_method,
                    UserId = request.user_id,
                    PaymentTransactionId = request.payment_transaction_id,
                    PaymentStatus = request.payment_status,
                    CouponId = request.coupon_id,
                    CouponTitle = request.coupon_title,
                    CouponValue = request.coupon_value,
                    CouponOffAmount = request.coupon_off_amount,
                    UnitTaxAmount = request.unit_tax_amount,
                    Tax = request.tax,
                    UnitTotalAmount = request.unit_total_amount,
                    Source = request.source
                };

                _applicationDbContext.appointments.Add(appointment);
                await _applicationDbContext.SaveChangesAsync();

                return new AppointmentResponse
                {
                    response = 200,
                    status = true,
                    message = "Appointment created successfully.",
                    id = appointment.Id
                };
            }
            catch (Exception ex)
            {
                return new AppointmentResponse
                {
                    response = 500,
                    status = false,
                    message = "An error occurred while creating the appointment: " + ex.Message
                };
            }
        }

        public async Task<List<AppointmentDate>> GetAppointmentsByUserIdAsync(int userId)
        {
            // Get appointments from the database
            var appointments = await _applicationDbContext.appointments
                .Where(x => x.FK_PatientId == userId)
                .Select(a => new AppointmentDate
                {
                    id = a.Id,
                    patient_id = a.Id,
                    status = a.Status,
                    date = a.Date,
                    time_slots = a.TimeSlots,
                    doct_id = a.FK_DoctId,
                    dept_id = a.DeptId,
                    type = a.Type,
                    meeting_id = a.MeetingId,
                    meeting_link = a.MeetingLink,
                    payment_status = a.PaymentStatus,
                    //current_cancel_req_status = a.cu,
                    source = a.Source,
                    created_at = a.CreationTime.ToString(),
                    updated_at = a.LastModificationTime.ToString(),
                    user_id = a.UserId,
                    //patient_f_name = a.,
                    //patient_l_name = a.PatientLastName,
                    //dept_title = a.DepartmentTitle,
                    //doct_f_name = a.DoctorFirstName,
                    //doct_l_name = a.DoctorLastName,
                    //doct_image = a.DoctorImage,
                    //doct_specialization = a.DoctorSpecialization
                })
                .ToListAsync();

            return appointments;
        }

    }
}
