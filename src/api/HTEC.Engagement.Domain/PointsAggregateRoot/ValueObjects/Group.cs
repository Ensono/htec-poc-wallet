using System;
using Amido.Stacks.Domain;

namespace HTEC.Engagement.Domain.ValueObjects
{
    public class Group : IValueObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Group(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
