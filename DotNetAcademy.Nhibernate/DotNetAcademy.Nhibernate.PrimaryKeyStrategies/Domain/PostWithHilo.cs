using System;

namespace DotNetAcademy.Nhibernate.PrimaryKeyStrategies.Domain
{
    public class PostWithHilo
    {
        private readonly int _id;

        public PostWithHilo()
        {
            _id = 0;
        }

        public virtual int Id { get { return _id; } }
        public virtual String Description { get; set; }
        public virtual String Message { get; set; }
        public virtual DateTime PostedOn { get; set; }
    }
}