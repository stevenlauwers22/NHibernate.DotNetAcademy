using System.Collections.Generic;

namespace DotNetAcademy.NhibernateArch.Contracts.GetPostsByDescription
{
    public class GetPostsByDescriptionResult
    {
        public IEnumerable<PostDTO> Posts { get; set; }
    }
}