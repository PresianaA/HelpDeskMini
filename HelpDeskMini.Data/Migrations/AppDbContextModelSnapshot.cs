using HelpDeskMini.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelpDeskMini.Data.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "8.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        modelBuilder.Entity("HelpDeskMini.Data.Entities.Department", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            b.Property<string>("Description").HasMaxLength(200).HasColumnType("nvarchar(200)");
            b.Property<string>("Name").IsRequired().HasMaxLength(80).HasColumnType("nvarchar(80)");
            b.HasKey("Id"); b.HasIndex("Name").IsUnique(); b.ToTable("Departments");
            b.HasData(new { Id = 1, Description = "Техническа поддръжка на компютри и мрежа", Name = "Информационни технологии" },
                        new { Id = 2, Description = "Поддръжка на оборудването в кабинетите", Name = "Компютърни кабинети" },
                        new { Id = 3, Description = "Проектори, екрани и аудио техника", Name = "Мултимедия" });
        });

        modelBuilder.Entity("HelpDeskMini.Data.Entities.Technician", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            b.Property<string>("Email").HasMaxLength(100).HasColumnType("nvarchar(100)");
            b.Property<string>("FullName").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
            b.HasKey("Id"); b.ToTable("Technicians");
            b.HasData(new { Id = 1, Email = "ivan.petrov@example.com", FullName = "Иван Петров" },
                      new { Id = 2, Email = "maria.ivanova@example.com", FullName = "Мария Иванова" },
                      new { Id = 3, Email = "georgi.dimitrov@example.com", FullName = "Георги Димитров" });
        });

        modelBuilder.Entity("HelpDeskMini.Data.Entities.HelpRequest", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            b.Property<string>("Cabinet").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
            b.Property<string>("Description").IsRequired().HasMaxLength(500).HasColumnType("nvarchar(500)");
            b.Property<int?>("DepartmentId").HasColumnType("int");
            b.Property<string>("Priority").IsRequired().HasMaxLength(10).HasColumnType("nvarchar(10)");
            b.Property<string>("Status").IsRequired().HasMaxLength(20).HasColumnType("nvarchar(20)");
            b.Property<string>("Title").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
            b.Property<int?>("TechnicianId").HasColumnType("int");
            b.HasKey("Id"); b.HasIndex("DepartmentId"); b.HasIndex("TechnicianId"); b.ToTable("HelpRequests");
        });

        modelBuilder.Entity("HelpDeskMini.Data.Entities.HelpRequest", b =>
        {
            b.HasOne("HelpDeskMini.Data.Entities.Department", "Department").WithMany("HelpRequests").HasForeignKey("DepartmentId").OnDelete(DeleteBehavior.SetNull);
            b.HasOne("HelpDeskMini.Data.Entities.Technician", "Technician").WithMany("AssignedRequests").HasForeignKey("TechnicianId").OnDelete(DeleteBehavior.SetNull);
            b.Navigation("Department"); b.Navigation("Technician");
        });
        modelBuilder.Entity("HelpDeskMini.Data.Entities.Department", b => b.Navigation("HelpRequests"));
        modelBuilder.Entity("HelpDeskMini.Data.Entities.Technician", b => b.Navigation("AssignedRequests"));
    }
}
