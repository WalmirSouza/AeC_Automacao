using Microsoft.EntityFrameworkCore;

namespace Upd8.Datas.Data
{
    public class Upd8APIContext : DbContext
    {
        public Upd8APIContext(DbContextOptions<Upd8APIContext> options)
            : base(options)
        {
        }
        public Upd8APIContext() { }

        public DbSet<Domains.Cliente> Cliente { get; set; } = default!;
        public DbSet<Domains.Municipios> Municipio { get; set; } = default!;
    }
}
