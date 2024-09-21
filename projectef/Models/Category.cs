using System.ComponentModel.DataAnnotations;

namespace projectef.Models;

public class Category 
{
    //[Key]Attributes and data notations
    public Guid CategoryId { get; set; }
    
    //[Required]
    //[MaxLength(150)]
    public string Name { get; set; }

    public string Description { get; set; }

    public int Weight { get; set; }

    public virtual ICollection<TaskM> Tasks {get; set;}
}