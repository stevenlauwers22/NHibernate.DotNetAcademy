using System.Collections.Generic;

namespace DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser
{
    public class GetPostsPerUserResult
    {
        public IEnumerable<PostsPerUserDTO> PostsPerUser { get; set; }
    }
}