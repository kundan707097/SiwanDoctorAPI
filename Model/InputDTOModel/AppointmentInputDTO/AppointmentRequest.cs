namespace SiwanDoctorAPI.Model.InputDTOModel.AppointmentInputDTO
{
    public class AppointmentRequest
    {
        public int patient_id { get; set; }
        public string? status { get; set; }
        public string? date { get; set; }
        public string? time_slots { get; set; }
        public int doct_id { get; set; }
        public int dept_id { get; set; }
        public string? type { get; set; }
        public string? meeting_id { get; set; }
        public string? meeting_link { get; set; }
        public decimal? fee { get; set; }
        public decimal? service_charge { get; set; }
        public decimal? total_amount { get; set; }
        public string? invoice_description { get; set; }
        public string? payment_method { get; set; }
        public int user_id { get; set; }
        public string? payment_transaction_id { get; set; }
        public string? payment_status { get; set; }
        public string? coupon_id { get; set; }
        public string? coupon_title { get; set; }
        public string? coupon_value { get; set; }
        public string? coupon_off_amount { get; set; }
        public string? unit_tax_amount { get; set; }
        public string? tax { get; set; }
        public decimal unit_total_amount { get; set; }
        public string? source { get; set; }
    }
}
