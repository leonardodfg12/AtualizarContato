using System.Diagnostics.CodeAnalysis;
using AtualizarContato.Domain.Domain;
using AtualizarContato.Infrastructure.Data.FluentMap;
using Microsoft.EntityFrameworkCore;

namespace AtualizarContato.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class ContactZoneDbContext : DbContext
    {
        public DbSet<ContactDomain> Contatos { get; set; }

        public ContactZoneDbContext(DbContextOptions<ContactZoneDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
        }
    }
}