namespace Finance.Domain.Core
{
    using System;

    public abstract class BaseEntity
    {
    }

    public abstract class Entity<TId> : BaseEntity, IEquatable<Entity<TId>>
    {
        private int? _requestedHashCode;

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
                throw new ArgumentException("The Id can not be the type's default value.", "id");

            this.Id = id;
        }

        public TId Id { get; protected set; }

        //TODO: need to be refacotry
        public bool IsTransient()
        {
            return object.Equals(this.Id, default(TId));
        }

        public override bool Equals(object other)
        {
            var entity = other as Entity<TId>;

            if (entity != null)
                return this.Equals(entity);

            return base.Equals(other);
        }

        public bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            return this.Id.Equals(other.Id);
        }

        protected virtual void OnIdChanged()
        {
        }
    }
}