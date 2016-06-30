using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
    public class Program
    {

        // TODO create new IServiceCollection and IServiceProvider.  
        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection()
                                    .AddDbContext<TodoDbContext>(options =>
                                    {
                                        options.UseInMemoryDatabase();
                                        //options.UseSqlite("Filename=./Todo.db");
                                    });

            serviceCollection.AddTransient<TodoRepository, TodoRepository>();
            return serviceCollection.BuildServiceProvider();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core /w with DI container !");

            var provider = ConfigureServices();

            // TODO get DbContext instance from DIContainer.
            var db = provider.GetService<TodoDbContext>();


            db.Todos.Add(new Todo() { IsDone = false, Text = "First Commit" });

            db.SaveChanges();

            foreach (var item in db.Todos)
            {
                Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
            }


            // TODO repository get by DIContainer.
            using (var repos = provider.GetService<TodoRepository>())
            {
                repos.Add(new Todo() { IsDone = false, Text = "Added by repostitory" });
                repos.Save();

                foreach(var item in repos.ListAll()){
                    
                    Console.WriteLine($"[{item.Id}] {item.Text} : {item.IsDone}");
                }
            }

        }
    }

    public class TodoDbContext : DbContext
    {

        public TodoDbContext()
        {
        }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=./Todo.db");
            }
        }
        public DbSet<Todo> Todos { get; set; }
    }

    [Table("Todos")]
    public class Todo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Text { get; set; }
    }

    public class TodoRepository : Repository<TodoDbContext, Todo>
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }
    }

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Find(params object[] keyValues);

        IList<TEntity> ListAll();

        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);

        void Save();
    }

    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
            where TContext : DbContext, new()
            where TEntity : class
    {

        protected Repository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        protected Repository() : this(new TContext())
        {
        }

        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public TEntity Find(params object[] keyValues)
        {
            // Find method not implementd EF Core rc2
            throw new NotImplementedException();
            // return dbSet.Find(keyValues);
        }

        public IList<TEntity> ListAll()
        {
            return dbSet.ToList();
        }

        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }

}

