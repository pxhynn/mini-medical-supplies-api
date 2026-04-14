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

    // Hàm trả về toàn bộ danh sách
    public List<Supply> GetAll() => _supplies;

    // Hàm tính toán thống kê tổng quan
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

    // Hàm phân loại trạng thái theo điều kiện số lượng
    public string GetStatus(int quantity)
    {
        if (quantity <= 0) return "Out of stock";
        if (quantity <= 2) return "Low stock";
        return "Available";
    }
}