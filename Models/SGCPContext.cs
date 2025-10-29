using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace SGCP_POO.Models;

public partial class SGCPContext : DbContext
{
    public SGCPContext()
    {
    }

    public SGCPContext(DbContextOptions<SGCPContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Informacion> Informacions { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }
    public virtual DbSet<Recurso> Recursos { get; set; }
    public virtual DbSet<TarjetaConocimiento> TarjetasConocimiento { get; set; }
    public virtual DbSet<TarjetaRecurso> TarjetasRecursos { get; set; }


    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DANIELVALENCIA\\SQLEXPRESS;Initial Catalog=SGCP_POO;Integrated Security=True;Encrypt=False");
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.IdAdministrador).HasName("PK__Administ__0FE822AA226B2011");

            entity.ToTable("Administrador");

            entity.HasIndex(e => e.CorreoInstitucional, "UQ__Administ__107A1DDA40A45E23").IsUnique();

            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.CorreoInstitucional)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_institucional");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__E0B2763CD343FFC2");

            entity.ToTable("Estudiante");

            entity.HasIndex(e => e.CorreoInstitucional, "UQ__Estudian__107A1DDABF1DAFB4").IsUnique();

            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.CorreoInstitucional)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_institucional");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Informacion>(entity =>
        {
            entity.HasKey(e => e.IdInformacion).HasName("PK__Informac__5E0F3B9E69DBAC86");

            entity.ToTable("Informacion");

            entity.Property(e => e.IdInformacion).HasColumnName("id_informacion");
            entity.Property(e => e.ContraseñaNueva)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña_nueva");
            entity.Property(e => e.CorreoPersonal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo_personal");
            entity.Property(e => e.Deficiencias)
                .HasColumnType("text")
                .HasColumnName("deficiencias");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Habilidades)
                .HasColumnType("text")
                .HasColumnName("habilidades");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TiempoDedicacion).HasColumnName("tiempo_dedicacion");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Informacions)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Informaci__id_es__3D5E1FD2");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__Registro__48155C1F45DC0223");

            entity.ToTable("Registro");

            entity.Property(e => e.IdRegistro).HasColumnName("id_registro");
            entity.Property(e => e.CodigoAdministrador)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo_administrador");
            entity.Property(e => e.ConfirmacionContraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("confirmacion_contraseña");
            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_usuario");

            entity.HasOne(d => d.IdAdministradorNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.IdAdministrador)
                .HasConstraintName("FK__Registro__id_adm__4222D4EF");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK__Registro__id_est__412EB0B6");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK__Sesion__8D3F9DFE18A95D3A");

            entity.ToTable("Sesion");

            entity.Property(e => e.IdSesion).HasColumnName("id_sesion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.HasOne(d => d.IdAdministradorNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.IdAdministrador)
                .HasConstraintName("FK__Sesion__id_admin__46E78A0C");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK__Sesion__id_estud__45F365D3");

            });
        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.ToTable("Recurso");
            entity.HasKey(e => e.IdRecurso);

            entity.Property(e => e.IdRecurso).HasColumnName("id_recurso");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");

            entity.Property(e => e.Titulo).HasColumnName("titulo").HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(700);
            entity.Property(e => e.PalabrasClave).HasColumnName("palabras_clave").HasMaxLength(100);
            entity.Property(e => e.Tema).HasColumnName("tema").HasMaxLength(40);
            entity.Property(e => e.Dificultad).HasColumnName("dificultad").HasMaxLength(5);
            entity.Property(e => e.Formato).HasColumnName("formato").HasMaxLength(20);
            entity.Property(e => e.Enlace).HasColumnName("enlace").HasMaxLength(1000);

            entity.HasOne(e => e.IdEstudianteNavigation)
                .WithMany(p => p.Recursos)
                .HasForeignKey(e => e.IdEstudiante);
        });

        modelBuilder.Entity<TarjetaConocimiento>(entity =>
        {
            entity.ToTable("TarjetaConocimiento");
            entity.HasKey(e => e.IdTarjeta);

            entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.NombreTarjeta).HasColumnName("nombre_tarjeta");
            entity.Property(e => e.FechaCreacion)
                  .HasColumnName("fecha_creacion")
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(e => e.Estudiante)
                  .WithMany(e => e.Tarjetas)
                  .HasForeignKey(e => e.IdEstudiante);
        });

        modelBuilder.Entity<TarjetaRecurso>(entity =>
        {
            entity.ToTable("TarjetaRecurso");
            entity.HasKey(e => e.IdTarjetaRecurso);

            entity.Property(e => e.IdTarjetaRecurso).HasColumnName("id_tarjeta_recurso");
            entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");
            entity.Property(e => e.IdRecurso).HasColumnName("id_recurso");
            entity.Property(e => e.Retroalimentacion).HasColumnName("retroalimentacion");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.FechaRegistro)
                  .HasColumnName("fecha_registro")
                  .HasDefaultValueSql("GETDATE()");

            entity.HasOne(e => e.Tarjeta)
                  .WithMany(t => t.TarjetasRecursos)
                  .HasForeignKey(e => e.IdTarjeta);

            entity.HasOne(e => e.Recurso)
                  .WithMany(r => r.TarjetaRecursos)
                  .HasForeignKey(e => e.IdRecurso);
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
