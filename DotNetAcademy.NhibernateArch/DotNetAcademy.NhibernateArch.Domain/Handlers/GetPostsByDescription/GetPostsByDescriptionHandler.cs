using System.Linq;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers;
using NHibernate;
using NHibernate.Criterion;

namespace DotNetAcademy.NhibernateArch.Domain.Handlers.GetPostsByDescription
{
    public class GetPostsByDescriptionHandler : IQueryHandler<GetPostsByDescriptionRequest, GetPostsByDescriptionResult>
    {
        private readonly ISession _session;

        public GetPostsByDescriptionHandler(ISession session)
        {
            _session = session;
        }

        public GetPostsByDescriptionResult Handle(GetPostsByDescriptionRequest request)
        {
            var posts = _session
                .QueryOver<Post>()
                .Where(p => p.Description.IsLike(request.Description, MatchMode.Anywhere))
                .List();

            var postDtos = posts
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    Message = p.Message,
                    PostedOn = p.PostedOn
                })
                .ToList();

            var result = new GetPostsByDescriptionResult { Posts = postDtos };
            return result;
        }
    }
}
