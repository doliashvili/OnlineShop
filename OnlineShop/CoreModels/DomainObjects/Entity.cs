using System;
using System.Collections.Generic;

namespace CoreModels.DomainObjects
{
    public abstract class Entity : IEntity
    {
        public abstract long Id { get; protected set; }

        protected Entity() { }
        protected Entity(long id) => Id = id;
        
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            
            return obj is Entity entity &&
                   GetType() == entity.GetType() &&
                   entity.Id == Id;
        }
        
        public override int GetHashCode() 
            => HashCode.Combine(GetType(), Id);

        public static bool operator ==(Entity e1, Entity e2)
              => EqualityComparer<Entity>.Default.Equals(e1, e2);

        public static bool operator !=(Entity e1, Entity e2)
            => !(e1 == e2);
    }
}