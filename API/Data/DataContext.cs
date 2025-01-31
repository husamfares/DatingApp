using System.Data.Common;
using API.Entities;
using Microsoft.EntityFrameworkCore;//this packge to derive DbContext
using Microsoft.EntityFrameworkCore.Metadata.Internal; 

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    
}