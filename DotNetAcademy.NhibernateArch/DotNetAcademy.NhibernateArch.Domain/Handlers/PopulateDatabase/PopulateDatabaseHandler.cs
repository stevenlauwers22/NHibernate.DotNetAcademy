using System;
using DotNetAcademy.NhibernateArch.Contracts.PopulateDatabase;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers;
using NHibernate;

namespace DotNetAcademy.NhibernateArch.Domain.Handlers.PopulateDatabase
{
    public class PopulateDatabaseHandler : ICommandHandler<PopulateDatabaseCommand>
    {
        private readonly ISession _session;

        public PopulateDatabaseHandler(ISession session)
        {
            _session = session;
        }

        public void Handle(PopulateDatabaseCommand command)
        {
            var user1 = new User { Firstname = "Steven", Lastname = "Lauwers" };
            var user2 = new User { Firstname = "Gitte", Lastname = "Vermeiren" };
            var user3 = new User { Firstname = "Johan", Lastname = "Ven" };
            _session.Save(user1);
            _session.Save(user2);
            _session.Save(user3);

            var tag1 = new Tag { Description = "Tag1" };
            var tag2 = new Tag { Description = "Tag2" };
            var tag3 = new Tag { Description = "Tag3" };
            _session.Save(tag1);
            _session.Save(tag2);
            _session.Save(tag3);

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
            _session.Save(post1);

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
            _session.Save(post2);

            var post3 = new Post
            {
                Description = "EF rules",
                Message = "...",
                PostedOn = DateTime.Now,
                WrittenBy = user2
            };

            post3.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
            post3.Tags.Add(tag1);
            _session.Save(post3);

            var post4 = new Post
            {
                Description = "Why EF is better than NH",
                Message = "...",
                PostedOn = DateTime.Now.AddHours(-5),
                WrittenBy = user2
            };

            post4.Comments.Add(new Comment { Message = "Comment1", PostedOn = DateTime.Now });
            post4.Tags.Add(tag1);
            _session.Save(post4);

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
            _session.Save(post5);
        }
    }
}
