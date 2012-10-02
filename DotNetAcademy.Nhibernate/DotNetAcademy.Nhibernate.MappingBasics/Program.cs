using System;
using DotNetAcademy.Nhibernate.MappingBasics.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DotNetAcademy.Nhibernate.MappingBasics
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("App started");

            var configuration = new Configuration().Configure();
            configuration.AddAssembly(typeof (Program).Assembly);

            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(true, true);

            Console.WriteLine();
            Console.WriteLine("Database created");

            using (var sessionFactory = configuration.BuildSessionFactory())
            {
                var postId = SavePost(sessionFactory);
                GetPost(sessionFactory, postId);
            }

            Console.WriteLine("Press any key to quit the application");
            Console.ReadLine();
        }

        private static Guid SavePost(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to save a post to the database");
                Console.ReadLine();

                var post = new Post
                {
                    Description = "NH vs EF",
                    Message = "Wow, NHibernate is so much cooler than the Entity Framework",
                    PostedOn = DateTime.Now
                };
                
                session.Save(post);
                tx.Commit();

                Console.WriteLine("Post with ID '{0}' saved to the database", post.Id);
                return post.Id;
            }
        }

        private static void GetPost(ISessionFactory sessionFactory, Guid postId)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to retrieve post with ID '{0}' from the database", postId);
                Console.ReadLine();
                
                var post = session.Get<Post>(postId);
                Console.WriteLine("Post from DB");
                Console.WriteLine("------------");
                Console.WriteLine("Id: " + post.Id);
                Console.WriteLine("Description: " + post.Description);
                Console.WriteLine("Message: " + post.Message);
                Console.WriteLine("PostedOn: " + post.PostedOn);

                post.Description = "New description";
                tx.Commit();
            }
        }
    }
}