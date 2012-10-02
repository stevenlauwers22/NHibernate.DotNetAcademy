using System;
using DotNetAcademy.Nhibernate.PrimaryKeyStrategies.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DotNetAcademy.Nhibernate.PrimaryKeyStrategies
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("App started");

            var configuration = new Configuration().Configure();
            configuration.AddAssembly(typeof(Program).Assembly);

            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(true, true);

            Console.WriteLine();
            Console.WriteLine("Database created");

            using (var sessionFactory = configuration.BuildSessionFactory())
            {
                SavePostsWithGuidComb(sessionFactory, 10);
                SavePostsWithHilo(sessionFactory, 10);
            }

            Console.WriteLine("Press any key to quit the application");
            Console.ReadLine();
        }

        private static void SavePostsWithGuidComb(ISessionFactory sessionFactory, int number)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to save some posts to the database");
                Console.ReadLine();

                for (var i = 0; i < number; i++)
                {
                    var post = new PostWithGuidComb
                    {
                        Description = "NH vs EF",
                        Message = "Wow, NHibernate is so much cooler than the Entity Framework",
                        PostedOn = DateTime.Now
                    };

                    session.Save(post);
                }

                tx.Commit();

                Console.WriteLine("Posts saved, press any key to continue");
                Console.ReadLine();
            }
        }

        private static void SavePostsWithHilo(ISessionFactory sessionFactory, int number)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to save some posts to the database");
                Console.ReadLine();

                for (var i = 0; i < number; i++)
                {
                    var post = new PostWithHilo
                    {
                        Description = "NH vs EF",
                        Message = "Wow, NHibernate is so much cooler than the Entity Framework",
                        PostedOn = DateTime.Now
                    };

                    session.Save(post);
                }

                tx.Commit();

                Console.WriteLine("Posts saved");
            }
        }
    }
}
