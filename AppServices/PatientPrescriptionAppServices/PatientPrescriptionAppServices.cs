using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.AppointmentDetails;
using SiwanDoctorAPI.Model.EntityModel.PatientPrescriptionEntity.cs;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.AppServices.PatientPrescriptionAppServices
{
    public class PatientPrescriptionAppServices: IPatientPrescriptionAppServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PatientPrescriptionAppServices(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> AddPrescriptionAsync(PrescriptionDto prescriptionDto)
        {
            try
            {
                var patient = await _applicationDbContext.User_FamilyMembers.FindAsync(prescriptionDto.patient_id);
                var doctor = await _applicationDbContext.Doctor_Details.FindAsync(prescriptionDto.doct_id);
                var appointment = await _applicationDbContext.appointments.FindAsync(prescriptionDto.appointment_id);

                if (patient == null || doctor == null || appointment == null)
                {
                    return 0; // Return 0 if any required entity is missing
                }


                var prescription = new PatientPrescription
                {
                    UserFamilyMemberId = prescriptionDto.patient_id,
                    DoctorId = prescriptionDto.doct_id,
                    AppointmentId = prescriptionDto.appointment_id,
                    Test = prescriptionDto.test,
                    Advice = prescriptionDto.advice,
                    ProblemDesc = prescriptionDto.problem_desc,
                    FoodAllergies = prescriptionDto.food_allergies,
                    TendencyBleed = prescriptionDto.tendency_bleed,
                    HeartDisease = prescriptionDto.heart_disease,
                    BloodPressure = prescriptionDto.blood_pressure,
                    Diabetic = prescriptionDto.diabetic,
                    Surgery = prescriptionDto.surgery,
                    Accident = prescriptionDto.accident,
                    Others = prescriptionDto.others,
                    MedicalHistory = prescriptionDto.medical_history,
                    CurrentMedication = prescriptionDto.current_medication,
                    FemalePregnancy = prescriptionDto.female_pregnancy,
                    BreastFeeding = prescriptionDto.breast_feeding,
                    PulseRate = prescriptionDto.pulse_rate,
                    Temperature = prescriptionDto.temperature,
                    NextVisit = prescriptionDto.next_visit,
                    Medicines = prescriptionDto.Medicines.Select(m => new PatientMedicine
                    {
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Duration = m.Duration,
                        Time = m.Time,
                        DoseInterval = m.DoseInterval,
                        Notes = m.Notes
                    }).ToList()
                };
                _applicationDbContext.patientPrescriptions.Add(prescription);
                await _applicationDbContext.SaveChangesAsync();

                return prescription.Id;
            }
            catch (Exception ex)
            {
                return 0;

            }
        }


        public async Task<List<GetPrescriptionDto>> GetPrescriptionsAsync()
        {
            var prescriptions = await _applicationDbContext.patientPrescriptions.Include(p=>p.userFamilyMember)
                .Include(p=>p.appointment)
                .Include(p=>p.Medicines)
                .Include(p=>p.doctor_Details)
                .Select(p=> new GetPrescriptionDto
                {
                    id = p.Id,
                    appointment_id = p.AppointmentId,
                    patient_id= p.UserFamilyMemberId,
                    date = p.CreationTime.ToString("yyyy-MM-dd"),
                    test = p.Test,
                    advice = p.Advice,
                    problem_desc = p.ProblemDesc,
                    food_allergies = p.FoodAllergies,
                    tendency_bleed = p.TendencyBleed,
                    heart_disease = p.HeartDisease,
                    blood_pressure = p.BloodPressure,
                    diabetic = p.Diabetic,
                    surgery = p.Surgery,
                    accident = p.Accident,
                    others = p.Others,
                    medical_history = p.MedicalHistory,
                    current_medication = p.CurrentMedication,
                    female_pregnancy = p.FemalePregnancy,
                    breast_feeding = p.BreastFeeding,
                    pulse_rate = p.PulseRate,
                    temperature = p.Temperature,
                    next_visit = p.NextVisit.ToString(),
                    created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = p.LastModificationTime.ToString(),
                    patient_f_name = p.userFamilyMember.F_Name,
                    patient_l_name = p.userFamilyMember.L_Name,
                    items = p.Medicines.Select(m => new GetMedicineDto
                    {
                        id = m.Id,
                        prescription_id = p.Id,
                        medicine_name = m.MedicineName,
                        dosage = m.Dosage,
                        duration = m.Duration,
                        time = m.Time,
                        dose_interval = m.DoseInterval,
                        notes = m.Notes,
                        created_at = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = m.LastModificationTime.ToString()
                    }).ToList()
                }).ToListAsync();
            return prescriptions;
        }


        public async Task<GetPrescriptionDto> GetPrescriptionByIdAsync(int prescriptionId)
        {
            var prescription = await _applicationDbContext.patientPrescriptions
                .Include(p => p.userFamilyMember)
                .Include(p => p.appointment)
                .Include(p => p.Medicines)
                .Include(p => p.doctor_Details)
                .Where(p => p.Id == prescriptionId)
                .Select(p => new GetPrescriptionDto
                {
                    id = p.Id,
                    appointment_id = p.AppointmentId,
                    patient_id = p.UserFamilyMemberId,
                    date = p.CreationTime.ToString("yyyy-MM-dd"),
                    test = p.Test,
                    advice = p.Advice,
                    problem_desc = p.ProblemDesc,
                    food_allergies = p.FoodAllergies,
                    tendency_bleed = p.TendencyBleed,
                    heart_disease = p.HeartDisease,
                    blood_pressure = p.BloodPressure,
                    diabetic = p.Diabetic,
                    surgery = p.Surgery,
                    accident = p.Accident,
                    others = p.Others,
                    medical_history = p.MedicalHistory,
                    current_medication = p.CurrentMedication,
                    female_pregnancy = p.FemalePregnancy,
                    breast_feeding = p.BreastFeeding,
                    pulse_rate = p.PulseRate,
                    temperature = p.Temperature,
                    next_visit = p.NextVisit.ToString(),
                    created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    patient_f_name = p.userFamilyMember.F_Name,
                    patient_l_name = p.userFamilyMember.L_Name,
                    items = p.Medicines.Select(m => new GetMedicineDto
                    {
                        id = m.Id,
                        prescription_id = p.Id,
                        medicine_name = m.MedicineName,
                        dosage = m.Dosage,
                        duration = m.Duration,
                        time = m.Time,
                        dose_interval = m.DoseInterval,
                        notes = m.Notes,
                        created_at = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = m.LastModificationTime.HasValue ? m.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return prescription;
        }

        public async Task<List<GetPrescriptionDto>> GetPrescriptionsByAppointmentIdAsync(int appointmentId)
        {
            var prescriptions = await _applicationDbContext.patientPrescriptions
                .Include(p => p.userFamilyMember)
                .Include(p => p.Medicines)
                .Where(p => p.AppointmentId == appointmentId)
                .Select(p => new GetPrescriptionDto
                {
                    id = p.Id,
                    appointment_id = p.AppointmentId,
                    patient_id = p.UserFamilyMemberId,
                    date = p.CreationTime.ToString("yyyy-MM-dd"),
                    test = p.Test,
                    advice = p.Advice,
                    problem_desc = p.ProblemDesc,
                    food_allergies = p.FoodAllergies,
                    tendency_bleed = p.TendencyBleed,
                    heart_disease = p.HeartDisease,
                    blood_pressure = p.BloodPressure,
                    diabetic = p.Diabetic,
                    surgery = p.Surgery,
                    accident = p.Accident,
                    others = p.Others,
                    medical_history = p.MedicalHistory,
                    current_medication = p.CurrentMedication,
                    female_pregnancy = p.FemalePregnancy,
                    breast_feeding = p.BreastFeeding,
                    pulse_rate = p.PulseRate,
                    temperature = p.Temperature,
                    next_visit = p.NextVisit.ToString(),
                    created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    patient_f_name = p.userFamilyMember.F_Name,
                    patient_l_name = p.userFamilyMember.L_Name,
                    items = p.Medicines.Select(m => new GetMedicineDto
                    {
                        id = m.Id,
                        prescription_id = p.Id,
                        medicine_name = m.MedicineName,
                        dosage = m.Dosage,
                        duration = m.Duration,
                        time = m.Time,
                        dose_interval = m.DoseInterval,
                        notes = m.Notes,
                        created_at = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    }).ToList()
                })
                .ToListAsync();

            return prescriptions;
        }

        public async Task<ApiResponse> DeletePrescriptionAsync(int id)
        {
            var prescription = await _applicationDbContext.patientPrescriptions.FindAsync(id);

            if (prescription == null)
            {
                return new ApiResponse { response = 404, status = false, message = "Prescription not found" };
            }

            _applicationDbContext.patientPrescriptions.Remove(prescription);
            await _applicationDbContext.SaveChangesAsync();

            return new ApiResponse { response = 200, status = true, message = "Successfully deleted" };
        }

        public async Task<ApiResponse> UpdatePrescriptionAsync(UpdatePrescriptionDto request)
        {
            var prescription = await _applicationDbContext.patientPrescriptions
                .Include(p => p.Medicines)
                .FirstOrDefaultAsync(p => p.Id == request.id);

            if (prescription == null)
            {
                return new ApiResponse { response = 404, status = false, message = "Prescription not found" };
            }

            // Update prescription details
            prescription.AppointmentId = int.TryParse(request.appointment_id, out int appointmentId) ? appointmentId : 0;
            prescription.UserFamilyMemberId = int.TryParse(request.patient_id, out int patientId) ? patientId : 0;
            prescription.Test = request.test;
            prescription.Advice = request.advice;
            prescription.ProblemDesc = request.problem_desc;
            prescription.FoodAllergies = request.food_allergies;
            prescription.TendencyBleed = request.tendency_bleed;
            prescription.HeartDisease = request.heart_disease;
            prescription.BloodPressure = request.blood_pressure;
            prescription.Diabetic = request.diabetic;
            prescription.Surgery = request.surgery;
            prescription.Accident = request.accident;
            prescription.Others = request.others;
            prescription.MedicalHistory = request.medical_history;
            prescription.CurrentMedication = request.current_medication;
            prescription.FemalePregnancy = request.female_pregnancy;
            prescription.BreastFeeding = request.breast_feeding;
            prescription.PulseRate = request.pulse_rate;
            prescription.Temperature = request.temperature;
            prescription.NextVisit = int.TryParse(request.next_visit, out int nextVisitValue) ? nextVisitValue : 0;

            // Remove old medicines
            _applicationDbContext.patientMedicines.RemoveRange(prescription.Medicines);

            prescription.Medicines = request.medicines.Select(m => new PatientMedicine
            {
                Id = prescription.Id,
                MedicineName = m.MedicineName,
                Dosage = m.Dosage ?? "", // Ensure it doesn't break if null
                Duration = m.Duration,
                Time = m.Time,
                DoseInterval = m.DoseInterval ?? "",
                Notes = m.Notes ?? ""
            }).ToList();

            await _applicationDbContext.SaveChangesAsync();

            return new ApiResponse { response = 200, status = true, message = "Update Successfully" };
        }

        public async Task<List<GetPrescriptionDto>> GetPrescriptionsByDoctorAsync(int doctorId)
        {
            var prescriptions = await _applicationDbContext.patientPrescriptions
            .Where(p => p.DoctorId == doctorId)
            .Select(p => new GetPrescriptionDto
            {
                appointment_id = p.AppointmentId,
                patient_f_name = p.userFamilyMember.F_Name,
                patient_l_name = p.userFamilyMember.L_Name,
                id = p.Id,
                patient_id = p.UserFamilyMemberId,
                date = p.CreationTime.ToString("yyyy-MM-dd"),
                test = p.Test,
                advice = p.Advice,
                problem_desc = p.ProblemDesc,
                food_allergies = p.FoodAllergies,
                tendency_bleed = p.TendencyBleed,
                heart_disease = p.HeartDisease,
                blood_pressure = p.BloodPressure,
                diabetic = p.Diabetic,
                surgery = p.Surgery,
                accident = p.Accident,
                others = p.Others,
                medical_history = p.MedicalHistory,
                current_medication = p.CurrentMedication,
                female_pregnancy = p.FemalePregnancy,
                breast_feeding = p.BreastFeeding,
                pulse_rate = p.PulseRate,
                temperature = p.Temperature,
                next_visit = p.NextVisit.ToString(),
                created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null
            })
            .ToListAsync();
            return prescriptions;
        }

        public async Task<List<GetPrescriptionDto>> GetPatientPrescriptionsByUserIdAsync(int userId)
        {
            var prescriptions = await _applicationDbContext.patientPrescriptions
                .Include(p => p.userFamilyMember)
                .Include(p => p.Medicines)
                .Where(p => p.userFamilyMember.PatientDetails.Id == userId)
                .Select(p => new GetPrescriptionDto
                {
                    id = p.Id,
                    appointment_id = p.AppointmentId,
                    patient_id = p.UserFamilyMemberId,
                    date = p.CreationTime.ToString("yyyy-MM-dd"),
                    test = p.Test,
                    advice = p.Advice,
                    problem_desc = p.ProblemDesc,
                    food_allergies = p.FoodAllergies,
                    tendency_bleed = p.TendencyBleed,
                    heart_disease = p.HeartDisease,
                    blood_pressure = p.BloodPressure,
                    diabetic = p.Diabetic,
                    surgery = p.Surgery,
                    accident = p.Accident,
                    others = p.Others,
                    medical_history = p.MedicalHistory,
                    current_medication = p.CurrentMedication,
                    female_pregnancy = p.FemalePregnancy,
                    breast_feeding = p.BreastFeeding,
                    pulse_rate = p.PulseRate,
                    temperature = p.Temperature,
                    next_visit = p.NextVisit.ToString(),
                    created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    patient_f_name = p.userFamilyMember.F_Name,
                    patient_l_name = p.userFamilyMember.L_Name,
                    items = p.Medicines.Select(m => new GetMedicineDto
                    {
                        id = m.Id,
                        prescription_id = p.Id,
                        medicine_name = m.MedicineName,
                        dosage = m.Dosage,
                        duration = m.Duration,
                        time = m.Time,
                        dose_interval = m.DoseInterval,
                        notes = m.Notes,
                        created_at = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    }).ToList()
                })
                .ToListAsync();
            return prescriptions;
        }

        public async Task<List<GetPrescriptionDto>> GetPatientPrescriptionsByPatientIdAsync(int PatientId)
        {
            var prescriptions = await _applicationDbContext.patientPrescriptions
                .Include(p => p.userFamilyMember)
                .Include(p => p.Medicines)
                .Where(p => p.userFamilyMember.Id == PatientId)
                .Select(p => new GetPrescriptionDto
                {
                    id = p.Id,
                    appointment_id = p.AppointmentId,
                    patient_id = p.UserFamilyMemberId,
                    date = p.CreationTime.ToString("yyyy-MM-dd"),
                    test = p.Test,
                    advice = p.Advice,
                    problem_desc = p.ProblemDesc,
                    food_allergies = p.FoodAllergies,
                    tendency_bleed = p.TendencyBleed,
                    heart_disease = p.HeartDisease,
                    blood_pressure = p.BloodPressure,
                    diabetic = p.Diabetic,
                    surgery = p.Surgery,
                    accident = p.Accident,
                    others = p.Others,
                    medical_history = p.MedicalHistory,
                    current_medication = p.CurrentMedication,
                    female_pregnancy = p.FemalePregnancy,
                    breast_feeding = p.BreastFeeding,
                    pulse_rate = p.PulseRate,
                    temperature = p.Temperature,
                    next_visit = p.NextVisit.ToString(),
                    created_at = p.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    patient_f_name = p.userFamilyMember.F_Name,
                    patient_l_name = p.userFamilyMember.L_Name,
                    items = p.Medicines.Select(m => new GetMedicineDto
                    {
                        id = m.Id,
                        prescription_id = p.Id,
                        medicine_name = m.MedicineName,
                        dosage = m.Dosage,
                        duration = m.Duration,
                        time = m.Time,
                        dose_interval = m.DoseInterval,
                        notes = m.Notes,
                        created_at = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        updated_at = p.LastModificationTime.HasValue ? p.LastModificationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null,
                    }).ToList()
                })
                .ToListAsync();
            return prescriptions;
        }
    }
}
