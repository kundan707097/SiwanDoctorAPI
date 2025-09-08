using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.DoctorReview;

namespace SiwanDoctorAPI.AppServices.DoctorReviewAppServices
{
    public class DoctorReviewAppServices: IDoctorReviewAppServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DoctorReviewAppServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AddDoctorReviewResponse> AddDoctorReviewAsync(AddDoctorReviewRequest request)
        {
            var review = new DoctorReview
            {
                FK_DoctorId = request.doctor_id,
                Points = request.points,
                Description = request.description,
                FK_UserId = request.user_id,
                AppointmentId = request.appointment_id
            };

            _applicationDbContext.doctorReviews.Add(review);
            await _applicationDbContext.SaveChangesAsync();

            return new AddDoctorReviewResponse
            {
                response = 200,
                status = true,
                message = "successfully",
                id = review.Id
            };
        }

        public async Task<DoctorReviewResponse> GetDoctorReviewsAsync(int doctorId)
        {
            var reviews = await _applicationDbContext.doctorReviews
                .Where(dr => dr.FK_DoctorId == doctorId && dr.IsDeleted == false)
                .Select(dr => new DoctorReviewData
                {
                    id = dr.Id,
                    doctor_id = dr.FK_DoctorId,
                    user_id = dr.FK_UserId,
                    appointment_id = dr.AppointmentId,
                    points = dr.Points,
                    description = dr.Description,
                    created_at = dr.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = dr.LastModificationTime.ToString(),
                    //f_name = dr.User.F_Name,
                    //l_name = dr.User.L_Name,
                    doct_f_name = dr.doctor_Details.FirstName,
                    doct_l_name = dr.doctor_Details.LastName,
                }).ToListAsync();

            int totalPoints = (int)reviews.Sum(r => r.points);
            int totalReviews = reviews.Count;
            string averageRating = totalReviews > 0 ? (totalPoints / (double)totalReviews).ToString("0.00") : "0.00";

            return new DoctorReviewResponse
            {
                totalReviewPoints = totalPoints,
                numberOfReviews = totalReviews,
                averageRating = averageRating,
                response = 200,
                data = reviews
            };
        }


        public async Task<IEnumerable<DoctorReviewData>> GetAllReviewsAsync()
        {
            var reviews = await _applicationDbContext.doctorReviews.Where(x=>x.IsDeleted ==false).ToListAsync();
            return reviews.Select(r => new DoctorReviewData
            {
                id = r.Id,
                doctor_id = r.FK_DoctorId,
                user_id = r.FK_UserId,
                appointment_id = r.AppointmentId,
                points = r.Points,
                description = r.Description,
                created_at = r.CreationTime.ToString(),
                updated_at = r.LastModificationTime.ToString(),
                //f_name = r.FName,
                //l_name = r.LName,
                doct_f_name = r.doctor_Details.FirstName,
                doct_l_name = r.doctor_Details.LastName
            });
        }
        //public async Task<IEnumerable<DoctorReviewData>> GetByIdReviews(int id)
        //{
        //    var reviews = await _applicationDbContext.doctorReviews.Where(x => x.IsDeleted == false && x.Id == id).ToListAsync();
        //    return reviews.Select(r => new DoctorReviewData
        //    {
        //        id = r.Id,
        //        doctor_id = r.FK_DoctorId,
        //        user_id = r.FK_UserId,
        //        appointment_id = r.AppointmentId,
        //        points = r.Points,
        //        description = r.Description,
        //        created_at = r.CreationTime.ToString(),
        //        updated_at = r.LastModificationTime.ToString(),
        //        //f_name = r.FName,
        //        //l_name = r.LName,
        //        doct_f_name = r.doctor_Details.FirstName,
        //        doct_l_name = r.doctor_Details.LastName
        //    });
        //}

        //public async Task<IEnumerable<DoctorReviewData>> GetByIdReviewsAsync(int id)
        //{
        //    var reviews = await _applicationDbContext.doctorReviews.Where(x => x.IsDeleted == false && x.FK_DoctorId == id).ToListAsync();
        //    return reviews.Select(r => new DoctorReviewData
        //    {
        //        id = r.Id,
        //        doctor_id = r.FK_DoctorId,
        //        user_id = r.FK_UserId,
        //        appointment_id = r.AppointmentId,
        //        points = r.Points,
        //        description = r.Description,
        //        created_at = r.CreationTime.ToString(),
        //        updated_at = r.LastModificationTime.ToString(),
        //        //f_name = r.FName,
        //        //l_name = r.LName,
        //        doct_f_name = r.doctor_Details.FirstName,
        //        doct_l_name = r.doctor_Details.LastName
        //    });
        //  }
        //public async Task<IEnumerable<DoctorReviewData>> GetByIdReviewsAsync(int id)
        //{
        //    var reviews = await _applicationDbContext.doctorReviews
        //        .Include(r => r.doctor_Details)
        //        .Where(x => !x.IsDeleted && x.FK_DoctorId == id)
        //        .ToListAsync();

        //    return reviews.Select(r => new DoctorReviewData
        //    {
        //        id = r.Id,
        //        doctor_id = r.FK_DoctorId,
        //        user_id = r.FK_UserId,
        //        appointment_id = r.AppointmentId,
        //        points = r.Points,
        //        description = r.Description,
        //        created_at = r.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
        //        updated_at = r.LastModificationTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
        //        doct_f_name = r.doctor_Details?.FirstName ?? "",
        //        doct_l_name = r.doctor_Details?.LastName ?? ""
        //    });
        //}
        public async Task<IEnumerable<DoctorReviewData>> GetByIdReviewsAsync(int id)
        {
            var reviews = await _applicationDbContext.doctorReviews
                .Include(r => r.doctor_Details)
                .Where(x => !x.IsDeleted && x.FK_DoctorId == id)
                .ToListAsync();

            var result = new List<DoctorReviewData>();

            foreach (var r in reviews)
            {
                var user = await _applicationDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == r.FK_UserId);

                result.Add(new DoctorReviewData
                {
                    id = r.Id,
                    doctor_id = r.FK_DoctorId,
                    user_id = r.FK_UserId,
                    appointment_id = r.AppointmentId,
                    points = r.Points,
                    description = r.Description,
                    created_at = r.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = r.LastModificationTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    doct_f_name = r.doctor_Details?.FirstName ?? "",
                    doct_l_name = r.doctor_Details?.LastName ?? "",
                    f_name = user?.FirstName ?? "",
                    l_name = user?.LastName ?? ""
                });
            }

            return result;
        }


    }
}
