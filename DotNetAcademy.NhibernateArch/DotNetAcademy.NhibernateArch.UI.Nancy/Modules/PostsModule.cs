using Castle.Windsor;
using DotNetAcademy.NhibernateArch.Contracts;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser;
using DotNetAcademy.NhibernateArch.Contracts.PopulateDatabase;
using NHibernate;
using Nancy;

namespace DotNetAcademy.NhibernateArch.UI.Nancy.Modules
{
    public class PostsModule : NancyModule
    {
        private readonly IWindsorContainer _container;
        private readonly IPostService _postService;

        public PostsModule(IWindsorContainer container, IPostService postService)
            : base("/Posts")
        {
            _container = container;
            _postService = postService;

            Before += ctx => 
            {
                var session = _container.Resolve<ISession>();
                session.Transaction.Begin();

                return null;
            };

            After += ctx =>
            {
                var session = _container.Resolve<ISession>();
                session.Transaction.Commit();
            };

            Get["/Index"] = parameters => View["Index"];

            Get["/PopulateDatabase"] = parameters =>
            {
                _postService.PopulateDatabase(new PopulateDatabaseCommand());
                return View["PopulateDatabase"];
            };

            Get["/PostsByDescription/{Description}"] = parameters =>
            {
                var request = new GetPostsByDescriptionRequest { Description = parameters.Description };
                var result = _postService.GetPostsByDescription(request);
                return View["PostsByDescription", result.Posts];
            };

            Get["/PostsPerUser"] = parameters =>
            {
                var request = new GetPostsPerUserRequest();
                var result = _postService.GetPostsPerUser(request);
                return View["PostsPerUser", result.PostsPerUser];
            };
        }
    }
}
