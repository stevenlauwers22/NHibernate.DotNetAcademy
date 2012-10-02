using DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription;
using DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser;
using DotNetAcademy.NhibernateArch.Contracts.PopulateDatabase;

namespace DotNetAcademy.NhibernateArch.Contracts
{
    public interface IPostService
    {
        void PopulateDatabase(PopulateDatabaseCommand command);
        GetPostsByDescriptionResult GetPostsByDescription(GetPostsByDescriptionRequest request);
        GetPostsPerUserResult GetPostsPerUser(GetPostsPerUserRequest request);
    }
}