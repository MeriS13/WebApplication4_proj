using Board.Application.AppData.Contexts.Posts.Services;
using Board.Contracts;
using Board.Contracts.Posts;
using Board.Application.AppData;
using Board.Infrastructure.DataAccess.Interfaces;
using Board.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations;
using Board.Infrastructure.Repository;
//using Board.Application.AppData.Contexts.Comments;
using Board.Application.AppData.Contexts.Categories.Services;
using Board.Infrastructure.DataAccess.Contexts.Categories.Repository;
using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Infrastructure.DataAccess.Contexts.Posts.Repositories;
using Board.Application.AppData.Contexts.ParentCategories.Services;
using Board.Application.AppData.Contexts.ParentCategories.Repository;
using Board.Infrastructure.DataAccess.Contexts.ParentCategories.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Board.Application.AppData.Contexts.Accounts.Repositories;
using Board.Infrastructure.DataAccess.Contexts.Accounts.Repository;
using Board.Application.AppData.Contexts.Accounts.Services;
using System.Xml.Linq;
using Board.Infrastructure.DataAccess.Contexts.Comments.Repository;
using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Application.AppData.Contexts.Comments.Services;
using Board.Application.AppData.Contexts.Answers.Repositories;
using Board.Infrastructure.DataAccess.Contexts.Answers.Repositories;
using Board.Application.AppData.Contexts.Answers.Services;
using Board.Application.AppData.Contexts.Files.Repositories;
using Board.Infrastructure.DataAccess.Contexts.Files.Repository;
using Board.Application.AppData.Contexts.Files.Services;

var builder = WebApplication.CreateBuilder(args);

//Добавление DbContext
builder.Services.AddSingleton<IDbContextOptionsConfigurator<BoardDbContext>, BoardDbContextConfiguration>();

builder.Services.AddDbContext<BoardDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
    ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BoardDbContext>>()
    .Configure((DbContextOptionsBuilder<BoardDbContext>) dbOptions)));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BoardDbContext>()));

//Добавления репозитория
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IParentCategoryRepository, ParentCategoryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

//Добавление сервисов 
builder.Services.AddScoped<IForbiddenWordsService, ForbiddenWordsService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IParentCategoryService, ParentCategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddControllers();

#region Authentication & Authorization

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
    options =>
    {
        var secretKey = builder.Configuration["Jwt:Key"];
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = false,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        };
    });

// Добавление авторизации с использованием политики 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireUserName("Admin");
    });
});



#endregion



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//для документации авторизации 

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Board Api", Version = "V1" });
        options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
            $"{typeof(CreatePostDto).Assembly.GetName().Name}.xml")));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Documentation.xml"));

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme.  
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer secretKey'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme="oauth2",
                Name= "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
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

// For testing
public partial class Program { }