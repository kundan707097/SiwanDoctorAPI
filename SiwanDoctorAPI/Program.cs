using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiwanDoctorAPI.AppServices.AdminAppServices;
using SiwanDoctorAPI.AppServices.AppointmentAppServices;
using SiwanDoctorAPI.AppServices.CouponAppService;
using SiwanDoctorAPI.AppServices.DashboardAppService;
using SiwanDoctorAPI.AppServices.DepartmentAppServices;
using SiwanDoctorAPI.AppServices.DoctorAppServices;
using SiwanDoctorAPI.AppServices.DoctorPrescribeMdicinesAppServices;
using SiwanDoctorAPI.AppServices.DoctorReviewAppServices;
using SiwanDoctorAPI.AppServices.FamilyVitalsAppServices;
using SiwanDoctorAPI.AppServices.LoginAppServices;
using SiwanDoctorAPI.AppServices.PatientAppServices;
using SiwanDoctorAPI.AppServices.PatientPrescriptionAppServices;
using SiwanDoctorAPI.AppServices.PaymentAppServices;
using SiwanDoctorAPI.AppServices.PublicDoctorAppServices;
using SiwanDoctorAPI.AppServices.RegistrationAppServices;
using SiwanDoctorAPI.AppServices.SocialMediaAppservices;
using SiwanDoctorAPI.AppServices.SpecializationAppServices;
using SiwanDoctorAPI.AppServices.TimeSlotAppServices;
using SiwanDoctorAPI.AppServices.VideoMettingAppServices;
using SiwanDoctorAPI.AppServices.WebPageAppServices;
using SiwanDoctorAPI.DbConnection;
using System.Text;
using static SiwanDoctorAPI.DbConnection.ApplicationDbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationDbContext.ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddHttpClient();
builder.Services.AddTransient<ILoginAppServices, LoginAppServices>();
builder.Services.AddTransient<IRegistrationAppService, RegistrationAppService>();
builder.Services.AddTransient<IPatientAppServices , PatientAppServices>();
builder.Services.AddTransient<IDoctorAppServices, DoctorAppServices>();
builder.Services.AddTransient<ITimeSlotAppServices, TimeSlotAppServices>();
builder.Services.AddTransient<IPublicDoctorAppServices , PublicDoctorAppServices>();
builder.Services.AddTransient<IFamilyVitalsAppServices  , FamilyVitalsAppServices>();
builder.Services.AddTransient<ISpecializationAppServices ,SpecializationAppServices>();
builder.Services.AddTransient<IDepartmentAppServices, DepartmentAppServices>();
builder.Services.AddTransient<IDoctorReviewAppServices, DoctorReviewAppServices>();
builder.Services.AddTransient<IAppointmentAppServices , AppointmentAppServices>();
builder.Services.AddTransient<ISocialMediaAppservices, SocialMediaAppservices>();
builder.Services.AddTransient<ISettingAppServices, SettingAppServices>();
builder.Services.AddTransient<IDoctorPrescribeMdicinesAppServices , DoctorPrescribeMdicinesAppServices>();
builder.Services.AddTransient<IVideoMettingAppServices, VideoMettingAppServices>();
builder.Services.AddTransient<IPatientPrescriptionAppServices, PatientPrescriptionAppServices>();
builder.Services.AddTransient<ICouponAppService, CouponAppService>();
builder.Services.AddTransient<IDashboardAppService, DashboardAppService>();
builder.Services.AddTransient<IPaymentAppServices, PaymentAppServices>();
builder.Services.AddTransient<IAdminAppServices, AdminAppServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader();
//    });
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowVercel",
//        policy =>
//        {
//            policy.WithOrigins("https://siwan-doctor-frontend.vercel.app") // Allow Vercel
//                  .AllowAnyMethod()
//                  .AllowAnyHeader();
//        });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any domain
              .AllowAnyMethod()   // Allow GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader();  // Allow all headers
    });
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await EnsureRolesExist(roleManager);
}
// Configure the HTTP request pipeline.
app.UseSwagger(); // Enable Swagger generation
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; // Makes Swagger UI accessible at the root URL
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("AllowVercel");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
async Task EnsureRolesExist(RoleManager<IdentityRole<int>> roleManager)
{
    string[] roles = { "Admin", "Doctor", "Patient" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}