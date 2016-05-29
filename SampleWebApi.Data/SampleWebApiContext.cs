using System.Data.Entity;
using SampleWebApi.Domain;

namespace SampleWebApi.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SampleWebApiContext:DbContext, ISampleWebApiContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);
            modelBuilder.Entity<Todo>().HasRequired(t => t.User);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            //modelBuilder.Entity<User>().HasOptional(u => u.Todos);
        }
    }

    public interface ISampleWebApiContext
    {
        DbSet<Todo> Todos { get; set; }
        DbSet<User> Users { get; set; }
    }
}
