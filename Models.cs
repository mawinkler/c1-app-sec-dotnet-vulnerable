using Microsoft.EntityFrameworkCore;

namespace app
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                b =>
                    {
                        b.Property("_id");
                        b.HasKey("_id");
                        b.Property(e => e.Name);
                    });
        }
    }

    public class User
    {
        private int _id;
        public string Name { get; set; }

        private User(int id, string name)
        {
            _id = id;
            Name = name;
        }

        public int Id
        {
            get { return _id; }
        }

        public User(string name)
        {
            Name = name;
        }
    }
}
