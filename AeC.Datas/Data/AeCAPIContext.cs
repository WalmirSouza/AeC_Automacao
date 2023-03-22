using Microsoft.EntityFrameworkCore;

namespace AeC.Datas.Data
{
    public class AeCAPIContext : DbContext
    {
        public AeCAPIContext(DbContextOptions<AeCAPIContext> options)
            : base(options)
        {
        }
        public AeCAPIContext() { }

        public DbSet<Domains.Pesquisa> Pesquisas { get; set; } = default!;

    }
}
