namespace Entities
{
    public interface IEntity
    {

    }
    public abstract class BaseEntity<Tkey>:IEntity
    {
        public Tkey Id { get; set; }
    }

    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
