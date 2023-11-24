using Medical_Center;
using Medical_Center_Business.Business;
using Medical_Center_Data.Data;
using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository;
using Medical_Center_Data.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddScoped<IRepo<Appointment>, AppointmentRepository>(); 
builder.Services.AddScoped<IRepo<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IRepo<Patient>, PatientRepository>();
builder.Services.AddScoped<IRepo<Payment>, BookingRepository>();
/*builder.Services.AddScoped<IUserRepository, UserRepository>();*/
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookings, Bookings>(); 
builder.Services.AddScoped<IAppointmentBusiness, AppointmentBusiness>();
builder.Services.AddScoped<IPatientBusiness, PatientBusiness>();
builder.Services.AddScoped<IDoctorBusiness, DoctorBusiness>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers().AddNewtonsoftJson(
    options => {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
