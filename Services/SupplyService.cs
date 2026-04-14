using MedicalSupplies.Api.Models;

namespace MedicalSupplies.Api.Services;

public class SupplyService
{
    // Tạo sẵn 1 list 4 món vật tư y tế mẫu
    private readonly List<Supply> _supplies =
    [
        new Supply { Id = 1, Name = "Khẩu trang y tế", Category = "Bảo hộ", ExpiryYear = 2026, Quantity = 500 },
        new Supply { Id = 2, Name = "Găng tay vô trùng", Category = "Bảo hộ", ExpiryYear = 2025, Quantity = 2 },
        new Supply { Id = 3, Name = "Bơm kim tiêm 5ml", Category = "Dụng cụ", ExpiryYear = 2027, Quantity = 0 },
        new Supply { Id = 4, Name = "Cồn sát trùng 90 độ", Category = "Hóa chất", ExpiryYear = 2026, Quantity = 15 }
    ];

    // [TÍNH NĂNG ĐIỂM CỘNG 1] Hàm lấy danh sách có hỗ trợ tìm kiếm và lọc
    public List<Supply> GetAll(string? searchName = null, string? category = null)
    {
        var result = _supplies.AsQueryable();

        if (!string.IsNullOrEmpty(searchName))
        {
            result = result.Where(s => s.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(category))
        {
            result = result.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        return result.ToList();
    }

    // [TÍNH NĂNG ĐIỂM CỘNG 2] Hàm tìm 1 vật tư theo ID
    public Supply? GetById(int id)
    {
        return _supplies.FirstOrDefault(s => s.Id == id);
    }

    // Hàm tính toán thống kê tổng quan (Giữ nguyên)
    public object GetStats()
    {
        var totalTypes = _supplies.Count;
        var totalQuantity = _supplies.Sum(x => x.Quantity);
        var availableSupplies = _supplies.Count(x => x.Quantity > 0);

        return new
        {
            TotalTypes = totalTypes,
            TotalQuantity = totalQuantity,
            AvailableSupplies = availableSupplies
        };
    }

    // Hàm phân loại trạng thái theo điều kiện số lượng (Giữ nguyên)
    public string GetStatus(int quantity)
    {
        if (quantity <= 0) return "Out of stock";
        if (quantity <= 2) return "Low stock";
        return "Available";
    }
}