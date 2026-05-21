using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models;

[Table("PCs")]
public class Pc
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Column(TypeName = "float(5)")]
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public ICollection<PcComponent> PcComponents { get; set; } = [];
}