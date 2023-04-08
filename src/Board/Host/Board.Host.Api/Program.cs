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
using Board.Application.AppData.Contexts.Favorites;
//using Board.Application.AppData.Contexts.Comments;
using Board.Application.AppData.Contexts.Categories.Services;
using Board.Infrastructure.DataAccess.Contexts.Categories.Repository;
using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Infrastructure.DataAccess.Contexts.Posts.Repositories;

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
builder.Services.AddScoped<IPostRepository, PostRepository>();

//Добавление сервисов 
builder.Services.AddScoped<IForbiddenWordsService, ForbiddenWordsService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICommentsService, CommentsService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//для документации

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Board Api", Version = "V1" });
        options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
            $"{typeof(CreatePostDto).Assembly.GetName().Name}.xml")));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Documentation.xml"));
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
