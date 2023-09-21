using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto2.Models;

namespace Proyecto2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Carrera>? Carrera { get; set; }
    public DbSet<Alumno>? Alumno { get; set; }
    public DbSet<Profesor>? Profesor { get; set; }
}
