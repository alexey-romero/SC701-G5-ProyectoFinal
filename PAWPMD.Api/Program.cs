using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PAWPMD.Architecture.Authentication;
using PAWPMD.Architecture.Factory;
using PAWPMD.Data.Repository;
using PAWPMD.Service.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher , PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
<<<<<<< HEAD
builder.Services.AddScoped<IWidgetService, WidgetService>();
builder.Services.AddScoped<IWidgetRepository, WidgetRepository>();
builder.Services.AddScoped<IWidgetFactory, WidgetFactory>();
builder.Services.AddScoped<IWidgetTableService, WidgetTableService>();
builder.Services.AddScoped<IWidgetImageService, WidgetImageService>();
builder.Services.AddScoped<IWidgetVideoService, WidgetVideoService>();
builder.Services.AddScoped<IWidgetImageRepository, WidgetImageRepository>();
builder.Services.AddScoped<IWidgetTableRepository, WidgetTableRepository>();
builder.Services.AddScoped<IWidgetVideoRepository, WidgetVideoRepository>();
builder.Services.AddScoped<IUserWidgetService, UserWidgetService>();
builder.Services.AddScoped<IUserWidgetRepository, UserWidgetRepository>();
builder.Services.AddScoped<IWidgetSettingService, WidgetSettingService>();
builder.Services.AddScoped<IWidgetSettingRepository, WidgetSettingRepository>();
=======
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IWidgetRepository, WidgetRepository>();
builder.Services.AddScoped<IWidgetService, WidgetService>();
//builder.Services.AddScoped<UserWidgetRepository, UserWidgetRepository>();
//builder.Services.AddScoped<IUserWidgetService, UserWidgetService>();
builder.Services.AddScoped<IWidgetCategoriesRepository, WidgetCategoriesRepository>();
builder.Services.AddScoped<IWidgetCategoriesService, WidgetCategoriesService>();
//builder.Services.AddScoped<WidgetCategoryRepository, WidgetCategoryRepository>();




>>>>>>> 8c6e70435f981e8d694c3ad8b0e5cb5e99ec6bef

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
