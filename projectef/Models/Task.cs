using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectef.Models;

public class TaskM
{
    //[Key]// Data Annotation or attributes: They are restrictions to our data to ensure the quality of this one
    public Guid TaskId { get; set; }

    //[ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }

    //[Required]
    //[MaxLength(200)]
    public string Title { get; set; }

    public string Description { get; set; }

    public Priority TaskPriority { get; set; }

    public DateTime CreationDate { get; set; } 

    public virtual Category Category { get; set; }

    //[NotMapped]
    public string Summarize {get;set;}//it's not mapped in the dbcontext to recreate the NotMapped notation
}

public enum Priority 
{
    Low, 
    Medium,
    High
}