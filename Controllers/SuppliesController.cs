using MedicalSupplies.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSupplies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliesController : ControllerBase
{
    private readonly SupplyService _supplyService;

    // Tiêm (Inject) SupplyService vào Controller để sử dụng
    public SuppliesController(SupplyService supplyService)
    {
        _supplyService = supplyService;
    }

    // Endpoint 1: Trả về danh sách vật tư kèm trạng thái (GET /api/supplies)
    [HttpGet]
    public IActionResult GetAll()
    {
        var supplies = _supplyService.GetAll()
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.Category,
                s.ExpiryYear,
                s.Quantity,
                Status = _supplyService.GetStatus(s.Quantity) // Gọi hàm kiểm tra trạng thái từ Service
            });
            
        return Ok(supplies);
    }

    // Endpoint 2: Trả về thống kê tổng quan (GET /api/supplies/stats)
    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_supplyService.GetStats());
    }
}