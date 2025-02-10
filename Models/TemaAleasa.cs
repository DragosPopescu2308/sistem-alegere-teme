using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class TemaAleasa
    {
        public int Id { get; set; }
        public int IdTema {  get; set; }

        public string UserEmail { get; set; }

        [NotMapped]
        public Item Tema { get; set; }
    }
   
}
