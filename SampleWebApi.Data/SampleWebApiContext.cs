using System.Data.Entity;
using SampleWebApi.Domain;

namespace SampleWebApi.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SampleWebApiContext:DbContext, ISampleWebApiContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public interface ISampleWebApiContext
    {
        DbSet<Todo> Todos { get; set; }
        DbSet<User> Users { get; set; }
    }
}
