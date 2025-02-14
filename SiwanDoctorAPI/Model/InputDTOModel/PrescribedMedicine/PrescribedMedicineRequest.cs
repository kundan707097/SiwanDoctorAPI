namespace SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine
{
    public class PrescribedMedicineRequest
    {
        public string? title { get; set; }
        public string? notes { get; set; }
        public int doct_id { get; set; }
    }

    public class MedicineApiResponse
    {
        public int response { get; set; }
        public List<Medicine> data { get; set; }
    }

    public class Medicine
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? notes { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
    }
}
