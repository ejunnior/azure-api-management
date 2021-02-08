namespace Finance.Domain.Core
{
    using System;

    public abstract class AggregateRoot : Entity<Guid>
    {
        protected AggregateRoot()
        {
        }
    }
}