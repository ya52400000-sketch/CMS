using CMS.BLL;
using CMS.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _service;

    public PatientController(IPatientService service)
    {
        _service = service;
    }

    // 🔥 Admin + Doctor
    // الدكتور والإدمن يقدروا يشوفوا كل المرضى
    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Query query)
    {
        var result = await _service.GetAllAsync(query);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // 🔥 Admin + Doctor + Patient
    // المريض يقدر يشوف بياناته
    // الدكتور يقدر يشوف بيانات المرضى
    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // 🔥 Admin فقط
    // إنشاء مريض جديد مسؤولية الإدارة
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add(CreatePatientDto dto)
    {
        var response = await _service.AddAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Admin فقط
    // تعديل بيانات المرضى صلاحية إدارية
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
    {
        var response = await _service.UpdateAsync(id, dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Admin فقط
    // حذف المرضى صلاحية حساسة جدًا
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _service.DeleteAsync(id);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}