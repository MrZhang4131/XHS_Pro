using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.Text;
using XHS_Pro.Tool;
using XHS_Pro.Data;

//øÁ”Ú«Î«Ûbegin
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//øÁ”Ú«Î«Ûend
var builder = WebApplication.CreateBuilder(args);
//Token begin
var TokenConfig = builder.Configuration.GetSection("TokenOption").Get<TokenOption_Format>();
builder.Services.AddSingleton(TokenConfig);
//Token end
//øÁ”Ú«Î«Û
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8081",
                                              "http://localhost:8080").AllowAnyHeader().AllowAnyMethod(); ;
                      });
});
//øÁ”Ú«Î«Ûend

//Token begin
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = TokenConfig.Issuer,
        ValidateAudience = true,
        ValidAudience = TokenConfig.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfig.IssuerSigningKey))
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});
//Token end
builder.Services.AddDbContext<XHS_ProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("XHS_ProjectContext") ?? throw new InvalidOperationException("Connection string 'XHS_ProjectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Token_Fe, Token_Gen>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//øÁ”Ú∑√Œ 
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
