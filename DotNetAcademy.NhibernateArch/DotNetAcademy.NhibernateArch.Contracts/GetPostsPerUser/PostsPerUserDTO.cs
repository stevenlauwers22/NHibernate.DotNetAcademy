using System;

namespace DotNetAcademy.NhibernateArch.Contracts.GetPostsPerUser
{
    public class PostsPerUserDTO
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public int NbrOfPosts { get; set; }
    }
}