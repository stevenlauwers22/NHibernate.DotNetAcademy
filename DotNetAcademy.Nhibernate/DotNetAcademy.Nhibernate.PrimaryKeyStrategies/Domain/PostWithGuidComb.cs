using System;

namespace DotNetAcademy.Nhibernate.PrimaryKeyStrategies.Domain
{
    public class PostWithGuidComb
    {
        private readonly Guid _id;

        public PostWithGuidComb()
        {
            _id = Guid.Empty;
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Description { get; set; }
        public virtual String Message { get; set; }
        public virtual DateTime PostedOn { get; set; }
    }
}