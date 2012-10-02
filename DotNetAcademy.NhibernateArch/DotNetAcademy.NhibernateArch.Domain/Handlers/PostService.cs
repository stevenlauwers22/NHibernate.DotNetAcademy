using DotNetAcademy.NhibernateArch.Contracts;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser;
using DotNetAcademy.NhibernateArch.Contracts.PopulateDatabase;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers;

namespace DotNetAcademy.NhibernateArch.Domain.Handlers
{
    public class PostService : IPostService
    {
        private readonly IDispatcher _dispatcher;

        public PostService(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void PopulateDatabase(PopulateDatabaseCommand command)
        {
            _dispatcher.Dispatch(command);    
        }

        public GetPostsByDescriptionResult GetPostsByDescription(GetPostsByDescriptionRequest request)
        {
            return _dispatcher.Dispatch<GetPostsByDescriptionRequest, GetPostsByDescriptionResult>(request);
        }

        public GetPostsPerUserResult GetPostsPerUser(GetPostsPerUserRequest request)
        {
            return _dispatcher.Dispatch<GetPostsPerUserRequest, GetPostsPerUserResult>(request);
        }
    }
}
