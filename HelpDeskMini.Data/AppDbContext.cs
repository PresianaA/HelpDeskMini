using HelpDeskMini.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMini.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<HelpRequest> HelpRequests => Set<HelpRequest>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Technician> Technicians => Set<Technician>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HelpRequest>(entity =>
        {
            entity.ToTable("HelpRequests");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Cabinet).IsRequired().HasMaxLength(20);
            entity.Property(x => x.Description).IsRequired().HasMaxLength(500);
            entity.Property(x => x.Priority).IsRequired().HasMaxLength(10);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(20);

            entity.HasOne(x => x.Department)
                .WithMany(x => x.HelpRequests)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(x => x.Technician)
                .WithMany(x => x.AssignedRequests)
                .HasForeignKey(x => x.TechnicianId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Departments");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(80);
            entity.Property(x => x.Description).HasMaxLength(200);
            entity.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.ToTable("Technicians");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).HasMaxLength(100);
        });

        // Примерни записи за избор във формуляра. Не представляват потребителски профили.
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Информационни технологии", Description = "Техническа поддръжка на компютри и мрежа" },
            new Department { Id = 2, Name = "Компютърни кабинети", Description = "Поддръжка на оборудването в кабинетите" },
            new Department { Id = 3, Name = "Мултимедия", Description = "Проектори, екрани и аудио техника" }
        );

        modelBuilder.Entity<Technician>().HasData(
            new Technician { Id = 1, FullName = "Иван Петров", Email = "ivan.petrov@example.com" },
            new Technician { Id = 2, FullName = "Мария Иванова", Email = "maria.ivanova@example.com" },
            new Technician { Id = 3, FullName = "Георги Димитров", Email = "georgi.dimitrov@example.com" }
        );
    }
}
