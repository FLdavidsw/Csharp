using Microsoft.EntityFrameworkCore;
using projectef.Models;

namespace projectef;

public class ContextTasks: DbContext//comes from Microsoft.EntityFrameworkCore
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskM> Tasks { get; set; }

    public ContextTasks(DbContextOptions<ContextTasks> options) :base(options) { }

    /*
    Fluent API is a useful tool to add different functions to our models. This one is applied 
    overriding the characteristics set out in the model
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a6204"), Name = "Actividades Pendientes", Weight = 20});
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a62ef"), Name = "Actividades Personales", Weight = 50});

        modelBuilder.Entity<Category>(category => 
        {
            category.ToTable("Category");
            category.HasKey(p => p.CategoryId);

            category.Property(p => p.Name).IsRequired().HasMaxLength(150);
            
            category.Property(p => p.Description);

            category.Property(p => p.Weight);

            category.HasData(categoriesInit);
        });

        List<TaskM> TaskInit = new List<TaskM>();
        TaskInit.Add(new TaskM() { TaskId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a6111"), CategoryId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a6204"), TaskPriority=Priority.Medium, Title = "Pay public services", CreationDate = DateTime.Now});
        TaskInit.Add(new TaskM() { TaskId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a6124"), CategoryId = Guid.Parse("21b8e7ae-1b3e-4f60-bbe5-0dceef8a62ef"), TaskPriority=Priority.Low ,Title = "Finish that The Office", CreationDate = DateTime.Now});

        modelBuilder.Entity<TaskM>(task => 
        {
            task.ToTable("Task");
            task.HasKey(p => p.TaskId);
            
            task.HasOne(p => p.Category).WithMany(p => p.Tasks).HasForeignKey(p => p.CategoryId);
        
            task.Property(p => p.Title).IsRequired().HasMaxLength(200);
        
            task.Property(p => p.Description);

            task.Property(p => p.TaskPriority);

            task.Property(p => p.CreationDate);

            task.Ignore(p => p.Summarize);

            task.HasData(TaskInit);
        });//
    }
}

