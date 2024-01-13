using System.Reflection;
using Kata08ConflictingObjectives.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kata08ConflictingObjectives.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public DbSet<Word> Words => Set<Word>();  
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}