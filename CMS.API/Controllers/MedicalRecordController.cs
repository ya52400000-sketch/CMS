using Castle.Components.DictionaryAdapter;
using CMS.BLL;
using CMS.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _service;

    public MedicalRecordController(IMedicalRecordService service)
    {
        _service = service;
    }

    // 🔥 Admin + Doctor
    // السجلات الطبية بيانات حساسة
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
    // المريض يقدر يشوف سجله الطبي
    [Authorize(Roles = "Admin,Doctor,Patient")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // 🔥 Doctor + Admin
    // الدكتور يكتب التشخيص
    [Authorize(Roles = "Admin,Doctor")]
    [HttpPost]
    public async Task<IActionResult> Add(CreateMedicalRecordDto dto)
    {
        var response = await _service.AddAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Doctor + Admin
    // تعديل التشخيص مسؤولية الدكتور
    [Authorize(Roles = "Admin,Doctor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMedicalRecordDto dto)
    {
        var response = await _service.UpdateAsync(id, dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Admin فقط
    // حذف السجل الطبي صلاحية عالية جدًا
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