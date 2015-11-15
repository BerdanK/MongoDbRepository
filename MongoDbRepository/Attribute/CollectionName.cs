using System;

namespace MongoDbRepository
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionName : Attribute
    {
        public CollectionName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CollectionName cannot be empty!", "value");

            this.Name = value;
        }

        public virtual string Name { get; private set; }
    }
}
