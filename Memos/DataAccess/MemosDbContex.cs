using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Memos.Models;

namespace Memos.DataAccess;

public class MemosDbContex : DbContext
{
    public DbSet<Memo> Memos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").SetBasePath(Directory.GetCurrentDirectory()).Build();

        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
}
