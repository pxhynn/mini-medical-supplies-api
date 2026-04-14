using MedicalSupplies.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSupplies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliesController : ControllerBase
{
    private readonly SupplyService _supplyService;

    public SuppliesController(SupplyService supplyService)
    {
        _supplyService = supplyService;
    }

    // [TÍNH NĂNG ĐIỂM CỘNG 1] Endpoint: Trả về danh sách (có hỗ trợ tìm kiếm và lọc)
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? search, [FromQuery] string? category)
    {
        var supplies = _supplyService.GetAll(search, category)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.Category,
                s.ExpiryYear,
                s.Quantity,
                Status = _supplyService.GetStatus(s.Quantity)
            });
            
        return Ok(supplies);
    }

    // [TÍNH NĂNG ĐIỂM CỘNG 2] Endpoint: Lấy chi tiết 1 vật tư theo ID (có bắt lỗi 404)
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var supply = _supplyService.GetById(id);
        if (supply == null)
        {
            return NotFound(new { message = $"Không tìm thấy vật tư y tế với ID = {id}" });
        }

        return Ok(new
        {
            supply.Id,
            supply.Name,
            supply.Category,
            supply.ExpiryYear,
            supply.Quantity,
            Status = _supplyService.GetStatus(supply.Quantity)
        });
    }

    // Endpoint: Trả về thống kê tổng quan (Giữ nguyên)
    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_supplyService.GetStats());
    }
}