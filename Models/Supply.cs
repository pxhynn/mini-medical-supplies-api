namespace MedicalSupplies.Api.Models;

public class Supply
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public int ExpiryYear { get; set; }
    public int Quantity { get; set; }
}