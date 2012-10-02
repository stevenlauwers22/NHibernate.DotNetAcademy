using DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers;
using NHibernate;
using NHibernate.Transform;

namespace DotNetAcademy.NhibernateArch.Domain.Handlers.GetPostsPerUser
{
    public class GetPostsPerUserHandler : IQueryHandler<GetPostsPerUserRequest, GetPostsPerUserResult>
    {
        private readonly ISession _session;

        public GetPostsPerUserHandler(ISession session)
        {
            _session = session;
        }

        public GetPostsPerUserResult Handle(GetPostsPerUserRequest request)
        {
            User userAlias = null;
            PostsPerUserDTO postPerUserAlias = null;
            var postsPerUser = _session
                .QueryOver<Post>()
                .Left.JoinAlias(p => p.WrittenBy, () => userAlias)
                .SelectList(s => s
                    .SelectGroup(() => userAlias.Id).WithAlias(() => postPerUserAlias.Id)
                    .SelectGroup(() => userAlias.Firstname).WithAlias(() => postPerUserAlias.Firstname)
                    .SelectGroup(() => userAlias.Lastname).WithAlias(() => postPerUserAlias.Lastname)
                    .SelectCount(p => p.Id).WithAlias(() => postPerUserAlias.NbrOfPosts))
                .TransformUsing(Transformers.AliasToBean<PostsPerUserDTO>())
                .List<PostsPerUserDTO>();

            var result = new GetPostsPerUserResult { PostsPerUser = postsPerUser };
            return result;
        }
    }
}
