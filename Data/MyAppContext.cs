using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : IdentityDbContext<Users>
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }
        

        // Configurarea relatiei TemaAleasa-Tema
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemaAleasa>()
                .HasOne(ta => ta.Tema)
                .WithMany()
                .HasForeignKey(ta => ta.IdTema)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Item> Items { get; set; }
        public DbSet<DificultyLevel> DificultyLevels { get; set; }
        public DbSet<TemaAleasa> TemeAlese { get; set; }
    }
}
