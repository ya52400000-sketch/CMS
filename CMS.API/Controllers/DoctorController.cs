using CMS.BLL;
using CMS.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _service;

    public DoctorController(IDoctorService service)
    {
        _service = service;
    }

    // 🔥 أي User مسجل يقدر يشوف الدكاترة
    [Authorize(Roles = "Admin,Doctor,Patient")]
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

    // 🔥 أي User مسجل يقدر يشوف دكتور
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
    // إضافة دكتور عملية إدارية
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add(CreateDoctorDto dto)
    {
        var response = await _service.AddAsync(dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Admin فقط
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDoctorDto dto)
    {
        var response = await _service.UpdateAsync(id, dto);
        if (!response.IsSucceded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    // 🔥 Admin فقط
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