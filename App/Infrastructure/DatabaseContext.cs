using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public class DatabaseContext(DbContextOptions opt) : DbContext(opt)
{
    public DbSet<Pc> Pcs { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
}