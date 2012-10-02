using System;

namespace DotNetAcademy.Nhibernate.MappingRelations.Domain
{
    public class User
    {
        private readonly Guid _id;

        public User()
        {
            _id = Guid.Empty;
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Firstname { get; set; }
        public virtual String Lastname { get; set; }
    }
}