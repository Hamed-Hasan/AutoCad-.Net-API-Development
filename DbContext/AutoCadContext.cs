namespace AutoCADApi.DbContext
{
    using AutoCADApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class AutoCadContext : DbContext
    {
        public AutoCadContext(DbContextOptions<AutoCadContext> options) : base(options) { }

        public DbSet<AutoCADFile> AutoCADFiles { get; set; }
        public DbSet<Pin> Pins { get; set; }
        public DbSet<ModalContent> ModalContents { get; set; }
    }
}