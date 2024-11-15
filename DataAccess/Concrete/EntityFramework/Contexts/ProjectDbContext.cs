using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
                                                                                : base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
                                                                        : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<Bolgeler> Bolgelers { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<RotaDetayi> RotaDetayis { get; set; }
        public DbSet<RotaGaleri> RotaGaleris { get; set; }
        public DbSet<Sehir> Sehirs { get; set; }
        public DbSet<Ulke> Ulkes { get; set; }
        public DbSet<Yorumlar> Yorumlars { get; set; }
        public DbSet<ResimTipi> ResimTipis { get; set; }
        public DbSet<Gunler> Gunlers { get; set; }
        public DbSet<Puan> Puans { get; set; }
        public DbSet<RotaAnasayifa> RotaAnasayifas { get; set; }
        public DbSet<Destek> Desteks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<MediaFoto> MediaFotoes { get; set; }

   
 

        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext")).EnableSensitiveDataLogging());

            }
        }

    }
}
