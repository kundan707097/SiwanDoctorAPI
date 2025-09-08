using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiwanDoctorAPI.AppServices.DepartmentAppServices;
using SiwanDoctorAPI.Model.InputDTOModel.DepartmentInputDTO;

namespace SiwanDoctorAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentAppServices _departmentAppServices;
        public DepartmentController(IDepartmentAppServices departmentAppServices)
        {
            _departmentAppServices = departmentAppServices;
        }

        [HttpPost("add_department")]
        public async Task<IActionResult> AddDepartment([FromForm] AddDepartmentRequest request)
        {
            var response = await _departmentAppServices.AddDepartmentAsync(request);

            if (response.status)
            {
                return Ok(new
                {
                    response = response.response,
                    status = response.status,
                    message = response.message,
                    id = response.id
                });
            }
            else
            {
                return BadRequest(new
                {
                    response = response.response,
                    status = response.status,
                    message = response.message
                });
            }
        }

        [HttpPost("update_department")]
        public async Task<IActionResult> UpdateDepartment([FromForm] UpdateDepartmentRequest request)
        {
            var response = await _departmentAppServices.UpdateDepartmentAsync(request);

            if (response.status)
            {
                return Ok(new
                {
                    response = response.response,
                    status = response.status,
                    message = response.message
                });
            }
            else
            {
                return BadRequest(new
                {
                    response = response.response,
                    status = response.status,
                    message = response.message
                });
            }
        }

        [HttpPost("remove_department_image")]
        public async Task<IActionResult> RemoveDepartmentImage([FromForm] int id)
        {
            var response = await _departmentAppServices.RemoveDepartmentImageAsync(id);

            return response.response == 200 ? Ok(response) : StatusCode(response.response, response);
        }

        [HttpGet("get_department")]
        public async Task<IActionResult> GetDepartments()
        {
            var response = await _departmentAppServices.GetDepartmentsAsync();
            return Ok(response);
        }
      
        [HttpGet("get_department/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await _departmentAppServices.GetDepartmentByIdAsync(id);
            if (response.Data == null)
            {
                return NotFound(new { response = 404, message = "Department not found" });
            }
            return Ok(response);
        }

        [HttpPost("delete_department")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var response = await _departmentAppServices.DeleteDepartmentAsync(id);
            return Ok(response);
        }
        [HttpGet("get_department_active")]
        public async Task<IActionResult> GetDepartmentsActive()
        {
            var response = await _departmentAppServices.GetActiveDepartmentsAsync();
            return Ok(response);
        }


    }
}
