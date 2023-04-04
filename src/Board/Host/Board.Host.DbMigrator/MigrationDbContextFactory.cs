using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Host.DbMigrator;

/// <summary>
/// Фабрика контекста БД для мигратора.
/// </summary>
public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
{
    /// <inheritdoc/>
    public MigrationDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("PostgresBoardDb");

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString);
        return new MigrationDbContext(dbContextOptionsBuilder.Options);
    }
}
