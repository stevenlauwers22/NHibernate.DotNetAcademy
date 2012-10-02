using System;
using DotNetAcademy.Nhibernate.MappingRelations.Domain;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;
using log4net.Config;

namespace DotNetAcademy.Nhibernate.MappingRelations
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
                //var postId = SavePost(sessionFactory);
                //GetPost(sessionFactory, postId);

                InsertData(sessionFactory);

                //// Enable profiling
                //XmlConfigurator.Configure();
                NHibernateProfiler.Initialize();

                RunQueries(sessionFactory);
            }

            Console.WriteLine();
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

                var user = new User
                {
                    Firstname = "Steven",
                    Lastname = "Lauwers"
                };

                session.Save(user);

                var tag1 = new Tag { Description = "Tag1" };
                var tag2 = new Tag { Description = "Tag2" };
                var tag3 = new Tag { Description = "Tag3" };
                session.Save(tag1);
                session.Save(tag2);
                session.Save(tag3);

                var post = new Post
                {
                    Description = "NH vs EF",
                    Message = "Wow, NHibernate is so much cooler than the Entity Framework",
                    PostedOn = DateTime.Now,
                    WrittenBy = user
                };

                post.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post.Comments.Add(new Comment { Message = "Comment2", PostedOn = DateTime.Now });
                post.Tags.Add(tag1);
                post.Tags.Add(tag2);

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
                PrintPost(post);
                tx.Rollback();
            }
        }

        private static void InsertData(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to insert a bunch of data in the database");
                Console.ReadLine();

                var user1 = new User {Firstname = "Steven", Lastname = "Lauwers"};
                var user2 = new User { Firstname = "Gitte", Lastname = "Vermeiren" };
                var user3 = new User { Firstname = "Johan", Lastname = "Ven" };
                session.Save(user1);
                session.Save(user2);
                session.Save(user3);

                var tag1 = new Tag { Description = "Tag1" };
                var tag2 = new Tag { Description = "Tag2" };
                var tag3 = new Tag { Description = "Tag3" };
                session.Save(tag1);
                session.Save(tag2);
                session.Save(tag3);

                var post1 = new Post
                {
                    Description = "NH vs EF",
                    Message = "Wow, NHibernate is so much cooler than the Entity Framework",
                    PostedOn = DateTime.Now.AddDays(-3),
                    WrittenBy = user1
                };

                post1.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post1.Comments.Add(new Comment { Message = "Comment2", PostedOn = DateTime.Now });
                post1.Tags.Add(tag1);
                post1.Tags.Add(tag2);
                session.Save(post1);

                var post2 = new Post
                {
                    Description = "Querying with NHibernate",
                    Message = "...",
                    PostedOn = DateTime.Now.AddHours(-20),
                    WrittenBy = user1
                };

                post2.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post2.Comments.Add(new Comment { Message = "Comment2", PostedOn = DateTime.Now });
                post2.Tags.Add(tag3);
                session.Save(post2);

                var post3 = new Post
                {
                    Description = "EF rules",
                    Message = "...",
                    PostedOn = DateTime.Now,
                    WrittenBy = user2
                };

                post3.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post3.Tags.Add(tag1);
                session.Save(post3);

                var post4 = new Post
                {
                    Description = "Why EF is better than NH",
                    Message = "...",
                    PostedOn = DateTime.Now.AddHours(-5),
                    WrittenBy = user2
                };

                post4.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post4.Tags.Add(tag1);
                session.Save(post4);

                var post5 = new Post
                {
                    Description = "The NH cookbook",
                    Message = "...",
                    PostedOn = DateTime.Now.AddHours(-55),
                    WrittenBy = user3
                };

                post5.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
                post5.Comments.Add(new Comment { Message = "Comment2", PostedOn = DateTime.Now });
                post5.Tags.Add(tag2);
                post5.Tags.Add(tag3);
                session.Save(post5);
                tx.Commit();

                Console.WriteLine("Database has been populated");
            }
        }

        private static void RunQueries(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Press any key to retrieve a list of posts containing 'NH' in their description");
                Console.ReadLine();

                var postsContainingNhQuery = session.QueryOver<Post>();
                postsContainingNhQuery.Left.JoinQueryOver(p => p.WrittenBy);
                postsContainingNhQuery.Left.JoinQueryOver(p => p.Comments);
                postsContainingNhQuery.Left.JoinQueryOver(p => p.Tags);
                postsContainingNhQuery
                    .Where(p => p.Description.IsLike("NH", MatchMode.Anywhere))
                    .TransformUsing(Transformers.DistinctRootEntity);

                var postsContainingNh = postsContainingNhQuery.List();
                foreach (var post in postsContainingNh)
                {
                    PrintPost(post);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to retrieve a list of posts ordered by the date they were posted");
                Console.ReadLine();

                PostInfo postInfoAlias = null;
                var postsOrderByPostedOn = session
                    .QueryOver<Post>()
                    .OrderBy(p => p.PostedOn).Asc()
                    .SelectList(s => s
                        .Select(p => p.Id).WithAlias(() => postInfoAlias.Id)
                        .Select(p => p.Description).WithAlias(() => postInfoAlias.Description)
                        .Select(p => p.PostedOn).WithAlias(() => postInfoAlias.PostedOn))
                    .TransformUsing(Transformers.AliasToBean<PostInfo>())
                    .List<PostInfo>();

                foreach (var post in postsOrderByPostedOn)
                {
                    PrintPost(post);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to retrieve a list of posts that have a tag with description 'Tag1'");
                Console.ReadLine();

                var postWithSpecificTag = session
                    .QueryOver<Post>()
                    .Left.JoinQueryOver<Tag>(p => p.Tags)
                    .Where(t => t.Description == "Tag1")
                    .List();

                foreach (var post in postWithSpecificTag)
                {
                    PrintPost(post);
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to retrieve a list of users and the number of posts they wrote");
                Console.ReadLine();

                User userAlias = null;
                PostsPerUserInfo postPerUserInfoAlias = null;
                var postsPerUser = session
                    .QueryOver<Post>()
                    .Left.JoinAlias(p => p.WrittenBy, () => userAlias)
                    .SelectList(s => s
                        .SelectGroup(() => userAlias.Id).WithAlias(() => postPerUserInfoAlias.Id)
                        .SelectGroup(() => userAlias.Firstname).WithAlias(() => postPerUserInfoAlias.Firstname)
                        .SelectGroup(() => userAlias.Lastname).WithAlias(() => postPerUserInfoAlias.Lastname)
                        .SelectCount(p => p.Id).WithAlias(() => postPerUserInfoAlias.NbrOfPosts))
                    .TransformUsing(Transformers.AliasToBean<PostsPerUserInfo>())
                    .List<PostsPerUserInfo>();

                foreach (var post in postsPerUser)
                {
                    PrintPost(post);
                }

                tx.Rollback();
            }
        }

        private static void PrintPost(Post post)
        {
            Console.WriteLine("Post from DB");
            Console.WriteLine("------------");
            Console.WriteLine("Id: " + post.Id);
            Console.WriteLine("Description: " + post.Description);
            Console.WriteLine("Message: " + post.Message);
            Console.WriteLine("PostedOn: " + post.PostedOn);
            Console.WriteLine("WrittenBy: " + post.WrittenBy.Firstname + " " + post.WrittenBy.Lastname);
            post.Comments.ForEach(c => Console.WriteLine("Comment: " + c.Message + " - PostedOn: " + c.PostedOn));
            post.Tags.ForEach(c => Console.WriteLine("Tag: " + c.Description));
        }

        private static void PrintPost(PostInfo post)
        {
            Console.WriteLine("Post from DB");
            Console.WriteLine("------------");
            Console.WriteLine("Id: " + post.Id);
            Console.WriteLine("Description: " + post.Description);
            Console.WriteLine("PostedOn: " + post.PostedOn);
        }

        private static void PrintPost(PostsPerUserInfo postPerUserInfo)
        {
            Console.WriteLine("Post from DB");
            Console.WriteLine("------------");
            Console.WriteLine("User: " + postPerUserInfo.Firstname + " " + postPerUserInfo.Lastname);
            Console.WriteLine("NbrOfPosts: " + postPerUserInfo.NbrOfPosts);
        }

        public class PostInfo
        {
            public virtual Guid Id { get; set; }
            public virtual String Description { get; set; }
            public virtual DateTime PostedOn { get; set; }
        }

        public class PostsPerUserInfo
        {
            public virtual Guid Id { get; set; }
            public virtual String Firstname { get; set; }
            public virtual String Lastname { get; set; }
            public virtual int NbrOfPosts { get; set; }
        }
    }
}