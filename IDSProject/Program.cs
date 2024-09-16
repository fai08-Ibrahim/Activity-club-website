using IDSProject.services.Services;
using IDSProject.core.Repositories;
using IDSProject.core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Services.IServices;
using IDSProject.core.Services;
using IDSProject.Repositories;
using IDSProject.Services;
using IDSProject.core.Models;
using IDSProject.Mapping;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Register DbContext with the DI container
builder.Services.AddDbContext<DatabaseServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILookUpRepository, LookUpRepository>();
builder.Services.AddScoped<IGuideRepository, GuideRepository>();
builder.Services.AddScoped<IEventMemberRepository, EventMemberRepository>();
builder.Services.AddScoped<IEventGuideRepository, EventGuideRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILookUpService, LookUpService>();
builder.Services.AddScoped<IGuideService, GuideService>();
builder.Services.AddScoped<IEventMemberService, EventMemberService>();
builder.Services.AddScoped<IEventGuideService, EventGuideService>();

// Configure controllers and JSON options
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<DateOnlySchemaFilter>();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
