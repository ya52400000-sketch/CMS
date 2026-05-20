using System.Text;
using CMS.BLL;
using CMS.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PublicMonsterServer");
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(connectionString).UseLazyLoadingProxies());

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
builder.Services.AddScoped<IMedicalRecordRepo, MedicalRecordRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequiredLength = 5;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#region register authorization service
builder.Services.AddAuthentication(options =>
{
    //> to know data of user from the token, Bearer Token Authenticatio
    //> Authorization: Bearer eyJhbGciOi..
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //> if user reached the endpoint and doesn't have token, return 401 unauthorized
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //> tell .NET use JWT in Authentication Method
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true; //> save token in authentication properties

    options.RequireHttpsMetadata = false; //> useful when work with HTTP (localhost)

    //> how ensure the token is valid
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //> prevent to use token genrated from another servers

        ValidateAudience = true, //> prevent to use token generated to another audiences

        ValidateLifetime = true, //> enable life time for token

        ValidateIssuerSigningKey = true, //> enable key to create signature for (header + payload)

        ValidIssuer = builder.Configuration["Jwt:Issuer"], //> expected issuer

        ValidAudience = builder.Configuration["Jwt:Audience"], //> expected audience

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!)) //> expected key
    };
});

#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Fix: Removed "../" to ensure correct routing on IIS
    c.SwaggerEndpoint("swagger/v1/swagger.json", "V1");
    c.RoutePrefix = string.Empty; // Set Swagger as the default home page
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

