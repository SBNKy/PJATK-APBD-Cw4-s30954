using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure;

public class DatabaseContext(DbContextOptions opt) : DbContext(opt)
{
    
}