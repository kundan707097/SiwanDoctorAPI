using Abp.Application.Services;
using SiwanDoctorAPI.Model.InputDTOModel.PrescribedMedicine;

namespace SiwanDoctorAPI.AppServices.DoctorPrescribeMdicinesAppServices
{
    public interface IDoctorPrescribeMdicinesAppServices : IApplicationService
    {
        Task<int> AddPrescribedMedicineAsync(PrescribedMedicineRequest request);
        Task<List<Medicine>> GetMedicinesByDoctorId(int doct_id);
        Task<bool> UpdatePrescribedMedicine(int id, string title, string notes);
        Task<bool> DeletePrescribedMedicine(int id);
    }
}
