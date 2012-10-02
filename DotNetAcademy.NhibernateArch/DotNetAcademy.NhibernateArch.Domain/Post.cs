using System;
using Iesi.Collections.Generic;

namespace DotNetAcademy.NhibernateArch.Domain
{
    public class Post
    {
        private readonly Guid _id;
        private readonly ISet<Comment> _comments;
        private readonly ISet<Tag> _tags; 

        public Post()
        {
            _id = Guid.Empty;
            _comments = new HashedSet<Comment>();
            _tags = new HashedSet<Tag>();
        }

        public virtual Guid Id { get { return _id; } }
        public virtual String Description { get; set; }
        public virtual String Message { get; set; }
        public virtual DateTime PostedOn { get; set; }
        public virtual User WrittenBy { get; set; }
        public virtual ISet<Comment> Comments { get { return _comments; } }
        public virtual ISet<Tag> Tags { get { return _tags; } }
    }
}