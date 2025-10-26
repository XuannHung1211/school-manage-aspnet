using backend_asp_school.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// 🔹 1. Kết nối Database
// ==============================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// ==============================
// 🔹 2. Cấu hình CORS (cho phép FE truy cập API)
// ==============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ==============================
// 🔹 3. Cấu hình Controllers + JSON (tránh vòng lặp)
// ==============================
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    })
    .AddJsonOptions(options =>
    {
        // Tránh vòng lặp JSON khi có quan hệ navigation (Class -> Student -> Class)
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// ==============================
// 🔹 4. Cấu hình Session (tùy chọn)
// ==============================
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ==============================
// 🔹 5. Build ứng dụng
// ==============================
var app = builder.Build();

// ==============================
// 🔹 6. Tự tạo DB khi khởi động
// ==============================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var connString = context.Database.GetConnectionString();
    Console.WriteLine("🔗 Connection string EF đang dùng: " + connString);

    context.Database.EnsureCreated();

    Console.WriteLine("✅ Tạo database và bảng thành công!");
}

// ==============================
// 🔹 7. Middleware pipeline
// ==============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// 👉 Chuyển hướng HTTP → HTTPS (bắt buộc để tránh lỗi axios "Network Error")
app.UseHttpsRedirection();

// Dùng static files (nếu cần phục vụ file tĩnh)
app.UseStaticFiles();

// Bật session
app.UseSession();

// Routing
app.UseRouting();

// Cho phép CORS
app.UseCors("AllowAll");

// Authorization (nếu có)
app.UseAuthorization();

// Map các controller API
app.MapControllers();

// Chạy ứng dụng
app.Run();
