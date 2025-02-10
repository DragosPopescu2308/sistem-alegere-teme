using MyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public class TemeViewModel
    {
        public List<Item> TemeDisponibile { get; set; }
        public List<TemaAleasa> TemeAlese { get; set; }
    }
}
