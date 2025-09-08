using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.Patient_DetailsInformation;
using SiwanDoctorAPI.Model.InputDTOModel.FamilyMemberVitalsInoutDTO;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

namespace SiwanDoctorAPI.AppServices.FamilyVitalsAppServices
{
    public class FamilyVitalsAppServices : IFamilyVitalsAppServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;
        public FamilyVitalsAppServices(UserManager<ApplicationUser> userManager, IConfiguration configuration , ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> AddVitalsAsync(AddVitalsInputModel input)
        {
            try
            {
                var vitals = new FamilyMemberVitals
                {
                    FK_patient_Details = input.user_id,
                    FK_userFamilyMember = input.family_member_id,
                    BpSystolic = input.bp_systolic ?? 0,   // If null, assign 0
                    BpDiastolic = input.bp_diastolic ?? 0,
                    Weight = input.weight ?? 0,
                    Spo2 = input.spo2 ?? 0,
                    Temperature = input.temperature ?? 0,
                    SugarRandom = input.sugar_random ?? 0,
                    SugarFasting = input.sugar_fasting ?? 0,
                    Type = input.type,
                    Date = input.date,
                    Time = input.time
                };

                _applicationDbContext.familyMemberVitals.Add(vitals);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the error if needed
                return false;
            }
        }

        public async Task<bool> DeleteVitalAsync(int id)
        {
            if (id <= 0) return false;

            var vital = await _applicationDbContext.familyMemberVitals.FindAsync(id);
            if (vital == null) return false;

            vital.IsDeleted = true;
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<object>> GetAllVitalsAsync()
        {
            var vitals = await _applicationDbContext.familyMemberVitals
                .Where(v => v.IsDeleted == false) // Fetch only non-deleted records
                .OrderByDescending(v => v.Date)   // Sort by latest date
                .Select(v => new
                {
                    v.Id,
                    v.FK_patient_Details,
                    v.FK_userFamilyMember,
                    v.Type,
                    DateTime =v.Date,
                    CreatedAt = v.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
                    UpdatedAt = v.LastModificationTime,
                    v.BpSystolic,
                    v.BpDiastolic,
                    v.Weight,
                    v.Spo2,
                    v.Temperature,
                    v.SugarRandom,
                    v.SugarFasting
                }).ToListAsync();


            return vitals.Cast<object>().ToList();
        }

        public async Task<GetVitalsModel> GetVitalsByIdAsync(int id)
        {
            // Retrieve the record from the database
            var record = await _applicationDbContext.familyMemberVitals
                .Where(v => v.Id == id && v.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return null; // Or throw an exception, based on your error handling strategy
            }

            // Map the database record to the GetVitalsModel
            var vitalsModel = new GetVitalsModel
            {
                id = record.Id,
                user_id = record.FK_patient_Details,
                family_member_id = record.FK_userFamilyMember,
                bp_systolic = record.BpSystolic,
                bp_diastolic = record.BpDiastolic,
                weight = record.Weight,
                spo2 = record.Spo2,
                temperature = record.Temperature,
                sugar_random = record.SugarRandom,
                sugar_fasting = record.SugarFasting,
                type = record.Type,
                date = record.Date, // Assuming Date is a DateTime, format it to string
                time = record.Time, // Assuming Time is a DateTime, format it to string
                created_at = record.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"), // Assuming CreatedAt is a DateTime
                updated_at = record.LastModificationTime?.ToString("yyyy-MM-dd HH:mm:ss") // Assuming UpdatedAt is a DateTime
            };

            return vitalsModel;
        }


        public async Task<List<VitalRecord>> GetVitalsByUserIdAsync(int userId)
        {
            // Fetch the vitals from the database using Entity Framework
            var vitals = await _applicationDbContext.familyMemberVitals
                .Where(v => v.FK_patient_Details == userId && v.IsDeleted==false)
                .ToListAsync();

            // Map to the VitalRecord model, assuming the DB fields are already set in the model.
            var vitalRecords = vitals.Select(v => new VitalRecord
            {
                id = v.Id,
                user_id = v.FK_patient_Details,
                family_member_id = v.FK_userFamilyMember,
                bp_systolic = v.BpSystolic,
                bp_diastolic = v.BpDiastolic,
                weight = v.Weight,
                spo2 = v.Spo2,
                temperature = v.Temperature,
                sugar_random = v.SugarRandom,
                sugar_fasting = v.SugarFasting,
                type = v.Type,
                date = v.Date, // Format the date if needed
                time = v.Time,  // Format time if needed
                created_at = v.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"), // Adjust format
                updated_at = v.LastModificationTime.ToString() // Adjust format
            }).ToList();

            return vitalRecords;
        }


        public async Task<List<VitalRecord>> GetVitalsByFamilyMemberIdAsync(int familyMemberId)
        {
            // Fetch the vitals from the database using Entity Framework
            var vitals = await _applicationDbContext.familyMemberVitals
                .Where(v => v.FK_userFamilyMember == familyMemberId && v.IsDeleted == false)
                .ToListAsync();

            // Map to the VitalRecord model, assuming the DB fields are already set in the model.
            var vitalRecords = vitals.Select(v => new VitalRecord
            {
                id = v.Id,
                user_id = v.FK_patient_Details,
                family_member_id = v.FK_userFamilyMember,
                bp_systolic = v.BpSystolic,
                bp_diastolic = v.BpDiastolic,
                weight = v.Weight,
                spo2 = v.Spo2,
                temperature = v.Temperature,
                sugar_random = v.SugarRandom,
                sugar_fasting = v.SugarFasting,
                type = v.Type,
                date = v.Date, // Format the date if needed
                time = v.Time,  // Format time if needed
                created_at = v.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"), // Adjust format
                updated_at = v.LastModificationTime.ToString() // Adjust format
            }).ToList();

            return vitalRecords;
        }
        public async Task<List<VitalRecord>> GetVitalsByFamilyMemberAndTypeAsync(int familyMemberId, string type, DateTime startDate, DateTime endDate)
        {
            // First, retrieve all records from the database (without filtering by date)
            var records = await _applicationDbContext.familyMemberVitals
                .Where(v => v.FK_userFamilyMember == familyMemberId
                            && v.Type == type)
                .ToListAsync();

            // Filter the records in memory using DateTime parsing and date comparison
            var filteredRecords = records
                .Where(v => DateTime.TryParse(v.Date, out DateTime parsedDate)
                            && parsedDate >= startDate
                            && parsedDate <= endDate)
                .ToList();

            // Map the filtered records to the VitalRecord model
            var vitalRecords = filteredRecords.Select(v => new VitalRecord
            {
                id = v.Id,  // Assuming familyMemberVitals has an Id field
                user_id = v.FK_userFamilyMember, // Adjust according to your model
                family_member_id = v.FK_userFamilyMember, // Adjust according to your model
                bp_systolic = v.BpSystolic, // Adjust according to your model
                bp_diastolic = v.BpDiastolic, // Adjust according to your model
                weight = v.Weight, // Adjust according to your model
                spo2 = v.Spo2, // Adjust according to your model
                temperature = v.Temperature, // Adjust according to your model
                sugar_random = v.SugarRandom, // Adjust according to your model
                sugar_fasting = v.SugarFasting, // Adjust according to your model
                type = v.Type,
                date = v.Date,
                time = v.Time,
                created_at = v.CreationTime.ToString(),
                updated_at = v.LastModificationTime.ToString(),
            }).ToList();

            return vitalRecords;
        }


        public async Task<bool> UpdateVitalsAsync(UpdateVitalsRequestInputDTO request)
        {
            var vitalRecord = await _applicationDbContext.familyMemberVitals
                .FirstOrDefaultAsync(v => v.Id == request.id);

            if (vitalRecord == null)
            {
                return false; // Return false if record not found
            }

            // Update the vital record fields
            vitalRecord.Date = request.date;
            vitalRecord.Time = request.time;
            vitalRecord.BpSystolic = request.bp_systolic ?? 0;
            vitalRecord.BpDiastolic = request.bp_diastolic ?? 0;
            vitalRecord.Weight = request.weight ?? 0;
            vitalRecord.Spo2 = request.spo2 ?? 0;
            vitalRecord.Temperature = request.temperature ?? 0;
            vitalRecord.SugarRandom = request.sugar_random ?? 0;
            vitalRecord.SugarFasting = request.sugar_fasting ?? 0;

            // Save the changes to the database
            _applicationDbContext.familyMemberVitals.Update(vitalRecord);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

    }
}
