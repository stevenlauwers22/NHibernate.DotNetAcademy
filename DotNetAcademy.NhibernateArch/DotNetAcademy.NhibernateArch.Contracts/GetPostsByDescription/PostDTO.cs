using System;

namespace DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public String Description { get; set; }
        public String Message { get; set; }
        public DateTime PostedOn { get; set; }
    }
}