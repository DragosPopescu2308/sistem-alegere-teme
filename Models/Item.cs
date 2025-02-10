using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titlu { get; set; } = null!;
        public int Termen { get; set; }
        public string Descriere { get; set; }

        public int? DificultyLevelId { get; set; }
        [ForeignKey("DificultyLevelId")]
        public DificultyLevel? DificultyLevel { get; set; }
    }
   
}
