using PersonProject.Model;
using System.Data.Entity;

namespace PersonProject.Model
{
    public class PeopleContext : DbContext
    {
        public PeopleContext() : base("name=People")
        {
            Database.SetInitializer<PeopleContext>(new CreateDatabaseIfNotExists<PeopleContext>());
        }

        public PeopleContext(IDatabaseInitializer<PeopleContext> strategy, System.Data.Common.DbConnection existingConnection, bool contextOwnsConection)
            : base (existingConnection, contextOwnsConection)
        {
            Database.SetInitializer<PeopleContext>(strategy);
        }
        
        public virtual IDbSet<Person> People { get; set; }
    }
}