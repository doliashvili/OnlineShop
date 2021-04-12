namespace CoreModels.DomainObjects
{
    /// <summary>
    /// eventless aggregate root
    /// </summary>
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(long id) : base(id)
        {
        }

        protected AggregateRoot()
        {
        }
    }
}