namespace SiwanDoctorAPI.Model.InputDTOModel.CouponInputDTO
{
    public class CouponResponse
    {
        public int response { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
        public int id { get; set; }
    }


    public class CouponValidationResponse
    {
        public int response { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }

        public CouponData? data { get; set; }
    }
}