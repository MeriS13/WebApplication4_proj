using Board.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess;

public class BoardDbContextConfiguration : IDbContextOptionsConfigurator<BoardDbContext>
{
    private const string PostgresConnectionStringName = "PostgresBoardDb";
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    public BoardDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }

    public void Configure(DbContextOptionsBuilder<BoardDbContext> options)
    {
        var connectionString = _configuration.GetConnectionString(PostgresConnectionStringName);
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Не найдена строка подключения с именем '(PostgresConnectionStringName)'");
        }
        options.UseNpgsql(connectionString);
        options.UseLoggerFactory(_loggerFactory);
    }

    

}
