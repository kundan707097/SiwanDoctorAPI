namespace SiwanDoctorAPI.Model.InputDTOModel.CouponInputDTO
{
    public class CouponRequest
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public int value { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public bool active { get; set; }
    }


    public class UpdateCouponDto
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int value { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public bool active { get; set; }
    }

    public class CouponListResponse
    {
        public int response { get; set; }
        public List<CouponData> data { get; set; }
    }
    public class CouponData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class GetCouponResponse
    {
        public int response { get; set; }
        public CouponData data { get; set; }
    }
}
