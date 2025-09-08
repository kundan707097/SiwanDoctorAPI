using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.TimeSlotInputDTO;
using System.Threading.Tasks;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.AppointmentAppServices
{
    public class AppointmentAppServices : IAppointmentAppServices
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
                    UserFamilyMemberId = request.patient_id,
                    Status = request.status,
                    Date = request.date,
                    TimeSlots = request.time_slots,
                    FK_DoctId = request.doct_id,
                    DeptId = request.dept_id,
                    Type = request.type,
                    MeetingId = request.meeting_id,
                    MeetingLink = request.meeting_link,
                    Fee = request.fee ?? 0,
                    ServiceCharge = request.service_charge ?? 0,
                    TotalAmount = request.total_amount ?? 0,
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
                .Where(x => x.UserId == userId)
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
                    patient_f_name = a.userFamilyMember.F_Name,
                    patient_l_name = a.userFamilyMember.L_Name,
                    //dept_title = a.DepartmentTitle,
                    doct_f_name = a.doctor_Details.FirstName,
                    doct_l_name = a.doctor_Details.LastName,
                    doct_image = a.doctor_Details.ProfileImagePath,
                    doct_specialization = a.doctor_Details.Specialization,
                })
                .ToListAsync();

            return appointments;
        }

        public async Task<AppointmentResponseByappointmentId> GetAppointmentById(int appointmentId)
        {
            var appointment = await _applicationDbContext.appointments
                .Where(x => x.Id == appointmentId)
                .Select(x => new AppointmentDataByappointmentId
                {
                    id = x.Id,
                    patient_id = x.UserFamilyMemberId,
                    status = x.Status,
                    date = x.Date,
                    time_slots = x.TimeSlots,
                    doct_id = x.FK_DoctId,
                    dept_id = x.DeptId,
                    type = x.Type,
                    meeting_id = x.MeetingId,
                    meeting_link = x.MeetingLink,
                    payment_status = x.PaymentStatus,
                    //CurrentCancelReqStatus = x.,
                    source = x.Source,
                    created_at = x.CreationTime.ToString(),
                    updated_at = x.LastModificationTime.ToString(),
                    user_id = x.UserId,
                    patient_f_name = x.userFamilyMember.F_Name,
                    patient_l_name = x.userFamilyMember.L_Name,
                    patient_gender = x.userFamilyMember.Gender,
                    patient_phone = x.userFamilyMember.Phone,
                    //DeptTitle = x.,
                    doct_f_name = x.doctor_Details.FirstName,
                    doct_l_name = x.doctor_Details.LastName,
                    doct_image = x.doctor_Details.ProfileImagePath,
                    doct_specialization = x.doctor_Details.Specialization,
                    //TotalReviewPoints = x.TotalReviewPoints,
                    //NumberOfReviews = x.NumberOfReviews,
                    //AverageRating = x.AverageRating,
                    //TotalAppointmentDone = x.TotalAppointmentDone
                }).FirstOrDefaultAsync();

            return new AppointmentResponseByappointmentId
            {
                response = appointment != null ? 200 : 401,
                data = appointment
            };
        }

        public async Task<UpdateStatusResponse> UpdateAppointmentStatusById(UpdateAppointmentStatus request)
        {
            var result = await _applicationDbContext.appointments
                .Where(x => x.Id == request.id)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return new UpdateStatusResponse
                {
                    response = 401,
                    status = false,
                    message = "Appointment not found."
                };
            }

            // Update the status if it exists
            result.Status = request.status;

            try
            {
                // Save changes to the database
                await _applicationDbContext.SaveChangesAsync();

                return new UpdateStatusResponse
                {
                    response = 200,
                    status = true,
                    message = "Appointment status updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateStatusResponse
                {
                    response = 500,
                    status = false,
                    message = $"An error occurred while updating the appointment status: {ex.Message}"
                };
            }
        }


        public async Task<AppointmentsByDateRange> GetAppointmentsByDateRange(DateTime startDate, DateTime endDate)
        {
            var appointments = await _applicationDbContext.appointments
                .Where(a => !string.IsNullOrEmpty(a.Date)) // Filter out null or empty date strings
                .ToListAsync();

            var filteredAppointments = appointments
                .Where(a => DateTime.TryParse(a.Date, out DateTime appointmentDate)
                            && appointmentDate >= startDate && appointmentDate <= endDate)
                .ToList();

            var appointmentData = filteredAppointments.Select(x => new GetAppointmentsByDateRange
            {
                id = x.Id,
                patient_id = x.UserFamilyMemberId,
                status = x.Status,
                date = x.Date,
                time_slots = x.TimeSlots,
                doct_id = x.FK_DoctId,
                dept_id = x.DeptId,
                type = x.Type,
                meeting_id = x.MeetingId,
                meeting_link = x.MeetingLink,
                payment_status = x.PaymentStatus,
                //current_cancel_req_status = x.CurrentCancelReqStatus, // If this is null, it can be nullable
                source = x.Source,
                created_at = x.CreationTime.ToString(), // Assuming CreationTime is DateTime?, convert it to string
                updated_at = x.LastModificationTime?.ToString(), // Assuming LastModificationTime is DateTime?, convert it to string
                user_id = x.UserId,
                patient_f_name = x.userFamilyMember.F_Name,
                patient_l_name = x.userFamilyMember.L_Name,
                patient_phone = x.userFamilyMember.Phone,
                patient_gender = x.userFamilyMember.Gender,
                //dept_title = x.DeptTitle,
                doct_f_name = x.doctor_Details?.FirstName, // Make sure doctor_Details is properly included
                doct_l_name = x.doctor_Details?.LastName,
                doct_image = x.doctor_Details?.ProfileImagePath,
                doct_specialization = x.doctor_Details?.Specialization,
                //total_review_points = x.TotalReviewPoints,
                //number_of_reviews = x.NumberOfReviews,
                //average_rating = x.AverageRating,
                //total_appointment_done = x.TotalAppointmentDone
            }).ToList();

            return new AppointmentsByDateRange
            {
                response = 200,
                data = appointmentData
            };
        }

        public async Task<List<AppointmentDate>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            var appointments = await _applicationDbContext.appointments
                .Where(x => x.FK_DoctId == doctorId)
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
                    patient_f_name = a.userFamilyMember.F_Name,
                    patient_l_name = a.userFamilyMember.L_Name,
                    //dept_title = a.DepartmentTitle,
                    doct_f_name = a.doctor_Details.FirstName,
                    doct_l_name = a.doctor_Details.LastName,
                    doct_image = a.doctor_Details.ProfileImagePath,
                    doct_specialization = a.doctor_Details.Specialization,
                })
                .ToListAsync();

            return appointments;
        }
        public async Task<PagedResultDto<AppointmentDate>> GetAppointmentsByPaginationRecordss(int doctorId, int start, int end)
        {
            var query = _applicationDbContext.appointments.Where(x => x.FK_DoctId == doctorId);

            var totalRecords = await query.CountAsync();

            var appointments = await query
                .OrderBy(x => x.Date)
                .Skip(start)
                .Take(end - start)
                .Select(x => new AppointmentDate
                {
                    id = x.Id,
                    patient_id = x.UserFamilyMemberId,
                    status = x.Status,
                    date = x.Date,
                    time_slots = x.TimeSlots,
                    doct_id = x.FK_DoctId,
                    dept_id = x.DeptId,
                    type = x.Type,
                    meeting_id = x.MeetingId,
                    meeting_link = x.MeetingLink,
                    payment_status = x.PaymentStatus,
                    //current_cancel_req_status = x.CurrentCancelReqStatus,
                    source = x.Source,
                    created_at = x.CreationTime.ToString(),
                    updated_at = x.LastModificationTime.ToString(),
                    user_id = x.UserId,
                    patient_f_name = x.userFamilyMember.F_Name,
                    patient_l_name = x.userFamilyMember.L_Name,
                    //dept_title = x.doctor_Details.Department,
                    doct_f_name = x.doctor_Details.FirstName,
                    doct_l_name = x.doctor_Details.LastName,
                    doct_image = x.doctor_Details.ProfileImagePath,
                    doct_specialization = x.doctor_Details.Specialization
                })
                .ToListAsync();

            return new PagedResultDto<AppointmentDate>
            {
                TotalCount = totalRecords,
                Items = appointments
            };
        }

        public async Task<ServiceResponse> UpdateAppointmentStatus(int id, string status)
        {
            var appointment = await _applicationDbContext.appointments
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return new ServiceResponse
                {
                    response = 201, // It seems response 201 is intended for "Appointment not found", consider using a different status code
                    status = false,
                    message = "Appointment not found"
                };
            }

            // Logic for updating appointment status
            appointment.Status = status;

            try
            {
                _applicationDbContext.appointments.Update(appointment);
                var saveResult = await _applicationDbContext.SaveChangesAsync();

                if (saveResult > 0)
                {
                    return new ServiceResponse
                    {
                        response = 200,
                        status = true,
                        message = "Status updated successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    response = 500,
                    status = false,
                    message = $"Error occurred while updating status: {ex.Message}"
                };
            }

            return new ServiceResponse
            {
                response = 400,
                status = false,
                message = "Cannot update status"
            };
        }


        public async Task<ServiceResponse> CancelAppointmentStatus(int id, string status)
        {
            var appointment = await _applicationDbContext.appointments
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return new ServiceResponse
                {
                    response = 201, // It seems response 201 is intended for "Appointment not found", consider using a different status code
                    status = false,
                    message = "Appointment not found"
                };
            }

            // Logic for updating appointment status
            appointment.Status = status;

            try
            {
                _applicationDbContext.appointments.Update(appointment);
                var saveResult = await _applicationDbContext.SaveChangesAsync();

                if (saveResult > 0)
                {
                    return new ServiceResponse
                    {
                        response = 200,
                        status = true,
                        message = "Status updated successfully"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    response = 500,
                    status = false,
                    message = $"Error occurred while updating status: {ex.Message}"
                };
            }

            return new ServiceResponse
            {
                response = 400,
                status = false,
                message = "Cannot update status"
            };
        }

        //public async Task<List<AppointmentDate>> GetAppointmentsByPaginationRecords(int doctorId, int start, int end)
        //{
        //    return await _applicationDbContext.appointments
        //        .Where(x => x.FK_DoctId == doctorId)
        //        .Skip(start)
        //        .Take(end - start)
        //        .Select(x => new AppointmentDate
        //        {
        //            id = x.Id,
        //            patient_id = x.FK_PatientId,
        //            status = x.Status,
        //            date = x.Date,
        //            time_slots = x.TimeSlots,
        //            doct_id = x.FK_DoctId,
        //            dept_id = x.DeptId,
        //            type = x.Type,
        //            meeting_id = x.MeetingId,
        //            meeting_link = x.MeetingLink,
        //            payment_status = x.PaymentStatus,
        //            //current_cancel_req_status = x.CurrentCancelReqStatus,
        //            source = x.Source,
        //            created_at = x.CreationTime.ToString(),
        //            updated_at = x.LastModificationTime.ToString(),
        //            user_id = x.UserId,
        //            patient_f_name = x.patient_Details.FirstName,
        //            patient_l_name = x.patient_Details.LastName,
        //            //dept_title = x.doctor_Details.Department,
        //            doct_f_name = x.doctor_Details.FirstName,
        //            doct_l_name = x.doctor_Details.LastName,
        //            doct_image = x.doctor_Details.ProfileImagePath,
        //            doct_specialization = x.doctor_Details.Specialization
        //        })
        //        .ToListAsync();
        //}
    }
}
