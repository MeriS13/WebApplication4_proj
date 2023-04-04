using Board.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Host.DbMigrator;

/// <summary>
/// Контекст БД для мигратора
/// </summary>
public class MigrationDbContext : BoardDbContext
{
    public MigrationDbContext(DbContextOptions options) : base(options)
    {

    }


}
