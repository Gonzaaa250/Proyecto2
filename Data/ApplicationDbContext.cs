﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto2.Models;

namespace Proyecto2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Carrera> Carrera { get; set; }
    public DbSet<Alumno> Alumno { get; set; }
    public DbSet<Profesor> Profesor { get; set; }
    public DbSet<Asignatura> Asignatura { get; set; }
    public DbSet<ProfesorAsignatura> ProfesorAsignatura {get; set;}
    public DbSet<Tarea> Tarea {get; set;}
}
