using System;

namespace DotNetAcademy.NhibernateArch.Domain
{
    public class Tag
    {
        private readonly Guid _id;
        
        public Tag()
        {
            _id = Guid.Empty;
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Description { get; set; }
    }
}