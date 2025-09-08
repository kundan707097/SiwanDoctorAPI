namespace SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine
{
    public class UpdatePrescriptionDto
    {
        public int id { get; set; }
        public string appointment_id { get; set; }
        public string patient_id { get; set; }
        public string? test { get; set; }
        public string? advice { get; set; }
        public string? problem_desc { get; set; }
        public string? food_allergies { get; set; }
        public string? tendency_bleed { get; set; }
        public string? heart_disease { get; set; }
        public string? blood_pressure { get; set; }
        public string? diabetic { get; set; }
        public string? surgery { get; set; }
        public string? accident { get; set; }
        public string? others { get; set; }
        public string? medical_history { get; set; }
        public string? current_medication { get; set; }
        public string? female_pregnancy { get; set; }
        public string? breast_feeding { get; set; }
        public string? pulse_rate { get; set; }
        public string? temperature { get; set; }
        public string? next_visit { get; set; }
        public List<UpdateMedicineDto> medicines { get; set; }
    }

    public class UpdateMedicineDto
    {
        public string? MedicineName { get; set; }
        public string? Dosage { get; set; }
        public string? Duration { get; set; }
        public string? Time { get; set; }
        public string? DoseInterval { get; set; }
        public string? Notes { get; set; }
    }
}
