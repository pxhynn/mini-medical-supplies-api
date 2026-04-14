using MedicalSupplies.Api.Models;
using MedicalSupplies.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký các dịch vụ (Services)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký SupplyService để Controller có thể xài được
builder.Services.AddSingleton<SupplyService>(); 

// Đọc cấu hình từ appsettings.json
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// 2. Cấu hình Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Kích hoạt đọc file tĩnh (hello.txt)
app.UseAuthorization();

// 3. Các Minimal API endpoints theo yêu cầu Lab
app.MapGet("/", () => Results.Ok(new { message = "Mini Medical Supplies API is running" }));

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/env", () => Results.Ok(new 
{ 
    Environment = app.Environment.EnvironmentName 
}));

app.MapGet("/config", (IConfiguration config) => Results.Ok(new
{
    AppName = config["AppSettings:AppName"],
    BaseUrl = config["AppSettings:BaseUrl"]
}));

// 4. Kích hoạt Controller endpoints
app.MapControllers();

app.Run();