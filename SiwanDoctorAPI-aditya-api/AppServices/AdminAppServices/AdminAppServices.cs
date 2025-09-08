using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model;
using SiwanDoctorAPI.Model.InputDTOModel.AdminInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO;

namespace SiwanDoctorAPI.AppServices.AdminAppServices
{
    
    public class AdminAppServices: IAdminAppServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AdminAppServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _applicationDbContext.Patients_Details
                .Where(x => !x.IsDeleted)
                .Select(c => new UserDto
                {
                    id = c.Id,
                    //wallet_amount = c.wa,
                    f_name = c.FirstName,
                    l_name = c.LastName,
                    phone = c.Phone,
                    isd_code = c.ISDCode,
                    gender = c.Gender,
                    dob = c.DateOfBirth,
                    email = c.Email,
                    image = c.ProfileImagePath,
                    address = c.patient_Address,
                    city = c.city,
                    state = c.State,
                    postal_code = c.PinCode,
                    //isd_code_sec = c.ISDCode,
                    //phone_sec = c.PhoneSecondary,
                    //email_verified_at = c.EmailVerifiedAt,
                    //remember_token = c.RememberToken,
                    created_at = c.CreationTime,
                    //updated_at = c.LastModificationTime
                })
            .ToListAsync();
        }

        public async Task<List<AppointmentDate>> GetAppointmentsAsync(string? search, int start, int end, string? status)
        {
            var query = _applicationDbContext.appointments
                .Where(a => !a.IsDeleted)
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
                });

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.patient_f_name.Contains(search) || a.patient_l_name.Contains(search));
            }

            // Apply status filter
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.status == status);
            }

            return await query.Skip(start).Take(end - start + 1).ToListAsync();
        }
        public async Task<List<configurations>> GetSettingsAsync()
        {
            return await _applicationDbContext.configurations
                .ToListAsync();
        }
    }
}
