using System.Web.Mvc;
using DotNetAcademy.NhibernateArch.Contracts;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser;
using DotNetAcademy.NhibernateArch.Contracts.PopulateDatabase;

namespace DotNetAcademy.NhibernateArch.UI.Mvc.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopulateDatabase()
        {
            _postService.PopulateDatabase(new PopulateDatabaseCommand());
            return View();
        }

        public ActionResult PostsByDescription(string description)
        {
            var request = new GetPostsByDescriptionRequest {Description = description};
            var result = _postService.GetPostsByDescription(request);
            return View(result.Posts);
        }

        public ActionResult PostsPerUser()
        {
            var request = new GetPostsPerUserRequest();
            var result = _postService.GetPostsPerUser(request);
            return View(result.PostsPerUser);
        }
    }
}