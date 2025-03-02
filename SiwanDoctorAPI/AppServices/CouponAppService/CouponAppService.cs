using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Doctor_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.CouponInputDTO;
using SiwanDoctorAPI.Model.InputDTOModel.PatientInputDTO;

namespace SiwanDoctorAPI.AppServices.CouponAppService
{
    public class CouponAppService: ICouponAppService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CouponAppService(ApplicationDbContext applicationDbContext)
        {
            
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CouponResponse> AddCouponAsync(CouponRequest couponDto)
        {
            var coupon = new Coupon
            {
                Title = couponDto.title,
                Description = couponDto.description,
                Value = couponDto.value,
                StartDate = couponDto.startDate,
                EndDate = couponDto.endDate,
                //IsDeleted = false
            };

            _applicationDbContext.coupons.Add(coupon);
            await _applicationDbContext.SaveChangesAsync();

            return new CouponResponse
            {
                response = 200,
                status = true,
                message = "successfully",
                id = coupon.Id
            };
        }

        public async Task<CouponResponse> UpdateCouponAsync(UpdateCouponDto couponDto)
        {
            var coupon = await _applicationDbContext.coupons.FindAsync(couponDto.id);
            if (coupon == null)
            {
                return new CouponResponse
                {
                    response = 404,
                    status = false,
                    message = "Coupon not found",
                    id = couponDto.id
                };
            }

            coupon.Title = couponDto.title;
            coupon.Description = couponDto.description;
            coupon.Value = couponDto.value;
            coupon.StartDate = couponDto.startDate;
            coupon.EndDate = couponDto.endDate;
            //coupon.IsDeleted = couponDto.active;

            _applicationDbContext.coupons.Update(coupon);
            await _applicationDbContext.SaveChangesAsync();

            return new CouponResponse
            {
                response = 200,
                status = true,
                message = "Update successfully",
                id = coupon.Id
            };
        }

        public async Task<CouponListResponse> GetCouponsAsync()
        {
            var coupons = await _applicationDbContext.coupons.Where(x=>x.IsDeleted==false).Select(c => new CouponData
            {
                id = c.Id,
                title = c.Title,
                description = c.Description,
                Value = c.Value,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                //Active = c.IsDeleted,
                CreatedAt = c.CreationTime,
                //UpdatedAt = c.LastModificationTime.Value
            }).ToListAsync();

            return new CouponListResponse
            {
                response = 200,
                data = coupons
            };
        }

        public async Task<CouponListResponse> GetActiveCouponsAsync()
        {
            var coupons = await _applicationDbContext.coupons
                .Where(c => c.IsDeleted == false)  // Ensure only non-deleted coupons are fetched
                .Select(c => new CouponData
                {
                    id = c.Id,
                    title = c.Title,
                    description = c.Description,
                    Value = c.Value,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    //Active = c.IsDeleted,  // Since it's active, always return 1
                    CreatedAt = c.CreationTime,
                    //UpdatedAt = c.UpdatedAt
                }).ToListAsync();

            return new CouponListResponse
            {
                response = 200,
                data = coupons
            };
        }

        public async Task<GetCouponResponse> GetCouponByIdAsync(int id)
        {
            var coupon = await _applicationDbContext.coupons
                .Where(c => c.Id == id)
                .Select(c => new GetCouponResponse
                {
                    response = 200,
                    data = new CouponData
                    {
                        id = c.Id,
                        title = c.Title,
                        description = c.Description,
                        Value = c.Value,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        //Active = c.IsDeleted,
                        CreatedAt = c.CreationTime,
                       
                    }
                })
                .FirstOrDefaultAsync();

            return coupon ?? new GetCouponResponse { response = 400};
        }

        public async Task<CouponValidationResponse> ValidateCouponAsync(string title, int userId)
        {
            var coupon = await _applicationDbContext.coupons.FirstOrDefaultAsync(c => c.Title == title && c.IsDeleted == false && c.EndDate >= DateTime.UtcNow);

            if (coupon == null)
            {
                return new CouponValidationResponse
                {
                    response = 201,
                    status = false,
                    message = "Invalid Coupon",
                    data = null
                };
            }

            return new CouponValidationResponse
            {
                response = 200,
                status = true,
                message = "Coupon is valid",
                data= new CouponData
                {
                    id = coupon.Id,
                    title = coupon.Title,
                    description = coupon.Description,
                    Value = coupon.Value,
                    StartDate = coupon.StartDate,
                    EndDate = coupon.EndDate,
                    //Active = coupon.IsDeleted,
                    CreatedAt = coupon.CreationTime,

                }
            };
        }
        public async Task<CouponResponse> DeleteCouponAsync(int id)
        {
            var coupon = await _applicationDbContext.coupons.FindAsync(id);
            if (coupon == null)
            {
                return new CouponResponse
                {
                    response = 400,
                    status = false,
                    message = "Coupon not found",
                    id = id
                };
            }

            _applicationDbContext.coupons.Remove(coupon);
            await _applicationDbContext.SaveChangesAsync();

            return new CouponResponse
            {
                response = 200,
                status = true,
                message = "Coupon deleted successfully",
                id = id
            };
        }
    }
}
