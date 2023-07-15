using Business.Layer.DataContext;
using Business.Layer.Repositories;
using Business.Layer.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orchestration.Layer.Orchestrations;
using Types.Layer.Configuration;
using Types.Layer.Contracts;
using Types.Layer.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IAuthenticationService, AuthenticationOrchestration>();
builder.Services.AddScoped<IUserOrchestration, UserManagement>();
builder.Services.AddScoped<ITokenService,TokenOrchestration>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericOrchestration<>), typeof(GenericOrchestration<>));
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ISharedRepository, SharedRepository>();
builder.Services.AddScoped<ISharedLikeRepository, SharedLikeRepository>();
builder.Services.AddScoped<ICommentOrchestration,CommentOrchestration>();
builder.Services.AddScoped<ISharedOrchestration, SharedOrchestration>();
builder.Services.AddScoped<ISharedLikeOrchestration, SharedLikeOrchestration>();
builder.Services.AddScoped<IUnitOfWork, UOW>();
builder.Services.AddScoped<IMailOrchestration, MailOrchestration>();
builder.Services.AddScoped<IFollowOrchestration, FollowOrchestration>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddIdentity<AppUserContract, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();


builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlServer(Configuration.GetConnectionString("SqlConStr"));
});



builder.Services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));

builder.Services.Configure<List<Client>>(Configuration.GetSection("Clients"));

var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignOrchestration.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateIssuerSigningKey =true,
        ValidateAudience=true,
        ValidateIssuer=true,
        ValidateLifetime=true,
        ClockSkew=TimeSpan.Zero
    };

});

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
