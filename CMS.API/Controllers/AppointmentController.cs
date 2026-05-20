using CMS.BLL;
using CMS.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    // 🔥 Admin + Doctor
    // الدكتور يشوف المواعيد الخاصة بالمرضى
    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Query query)
    {
        var result = await _service.GetAllAsync(query);
        if(result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // 🔥 Admin + Doctor + Patient
    // المريض يشوف ميعاده
    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // 🔥 Patient + Admin
    // المريض يحجز ميعاد
    [Authorize(Roles = "Admin,Patient")]
    [HttpPost]
    public async Task<IActionResult> Add(CreateAppointmentDto dto)
    {
        var response = await _service.AddAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok("Appointment Created");
    }

    // 🔥 Admin فقط
    // تغيير بيانات المواعيد إدارة
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateAppointmentDto dto)
    {
        var response = await _service.UpdateAsync(id, dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok("Appointment Created");
    }

    // 🔥 Admin + Patient
    // المريض يقدر يلغي ميعاده
    [Authorize(Roles = "Admin,Patient")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _service.DeleteAsync(id);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok("Appointment Created");
    }
}