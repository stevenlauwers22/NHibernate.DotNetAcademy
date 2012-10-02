using System;

namespace DotNetAcademy.Nhibernate.MappingRelations.Domain
{
    public class Comment
    {
        private readonly Guid _id;

        public Comment()
        {
            _id = Guid.Empty;
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Message { get; set; }
        public virtual DateTime PostedOn { get; set; }
    }
}