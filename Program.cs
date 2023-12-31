using FindALawyer.Data;
using FindALawyer.Services.AppointmentService;
using FindALawyer.Services.ClientAuthService;
using FindALawyer.Services.ClientService;
using FindALawyer.Services.JwtService;
using FindALawyer.Services.LawyerAuthService;
using FindALawyer.Services.LawyerService;
using FindALawyer.Services.PasswordService;
using FindALawyer.Services.PaymentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FindALawyer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FindALawyerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FindALawyerContext") ?? throw new InvalidOperationException("Connection string 'FindALawyerContext' not found.")));


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:JwtSecret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IJwtService, JwtServiceImpl>();
            builder.Services.AddScoped<IPasswordService, PasswordServiceImpl>();
            builder.Services.AddScoped<IClientAuthService, ClientAuthServiceImpl>();
            builder.Services.AddScoped<ILawyerAuthService, LawyerAuthServiceImpl>();
            builder.Services.AddScoped<ILawyerService, LawyerServiceImpl>();
            builder.Services.AddScoped<IPaymentService, PaymentServiceImpl>();
            builder.Services.AddScoped<IAppointmentService, AppointmentServiceImpl>();
            builder.Services.AddScoped<IClientService, ClientServiceImpl>();


            // Add services to the container.

            builder.Services.AddControllers();
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

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}