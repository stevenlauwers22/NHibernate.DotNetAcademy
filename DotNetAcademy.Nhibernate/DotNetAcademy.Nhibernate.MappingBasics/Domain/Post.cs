using System;

namespace DotNetAcademy.Nhibernate.MappingBasics.Domain
{
    public class Post
    {
        private readonly Guid _id;

        public Post()
        {
            _id = Guid.Empty;
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Description { get; set; }
        public virtual String Message { get; set; }
        public virtual DateTime PostedOn { get; set; }
    }
}