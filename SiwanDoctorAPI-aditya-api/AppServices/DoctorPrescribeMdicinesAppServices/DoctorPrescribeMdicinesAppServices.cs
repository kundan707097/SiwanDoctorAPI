using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SiwanDoctorAPI.DbConnection;
using SiwanDoctorAPI.Model.EntityModel.DoctorEntity;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.AppServices.DoctorPrescribeMdicinesAppServices
{
    public class DoctorPrescribeMdicinesAppServices: IDoctorPrescribeMdicinesAppServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DoctorPrescribeMdicinesAppServices(ApplicationDbContext applicationDbContext)
        {
            
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> AddPrescribedMedicineAsync(PrescribedMedicineRequest request)
        {
            // Create a new DoctorPrescribeMedicines entity
            var medicine = new DoctorPrescribeMdicines
            {
                Title = request.title,
                Notes = request.notes,
                FK_DoctId = request.doct_id // Assuming this is the foreign key for the doctor
            };

            await _applicationDbContext.doctorPrescribeMdicines.AddAsync(medicine);

            await _applicationDbContext.SaveChangesAsync();

            return medicine.Id;
        }

        public async Task<List<Medicine>> GetMedicinesByDoctorId(int doct_id)
        {
            var medicines = await _applicationDbContext.doctorPrescribeMdicines
                .Where(x => x.FK_DoctId == doct_id && x.IsDeleted ==false)
                .Select(x => new Medicine
                {
                    id = x.Id,
                    title = x.Title,
                    notes = x.Notes,
                    createdAt = x.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    updatedAt = x.LastModificationTime.ToString()
                }).ToListAsync();

            return medicines;
        }


        public async Task<bool> UpdatePrescribedMedicine(int id, string title, string notes)
        {
            // Find the existing prescribed medicine by id
            var existingMedicine = await _applicationDbContext.doctorPrescribeMdicines
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingMedicine == null)
            {
                return false; // Medicine not found
            }

            // Update the fields with the new data
            existingMedicine.Title = title;
            existingMedicine.Notes = notes;
            await _applicationDbContext.SaveChangesAsync();

            return true; // Successfully updated
        }

        public async Task<bool> DeletePrescribedMedicine(int id)
        {
            var medicine = await _applicationDbContext.doctorPrescribeMdicines
                .FirstOrDefaultAsync(x => x.Id == id);

            if (medicine == null)
            {
                return false; // Medicine not found
            }
            medicine.IsDeleted = true;

            // Save the changes to the database
            await _applicationDbContext.SaveChangesAsync();

            return true; 
        }
    }
}
