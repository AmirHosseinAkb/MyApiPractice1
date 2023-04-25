namespace Entities
{
    public abstract class IEntity
    {

    }
    public class BaseEntity<Tkey>:IEntity
    {
        public Tkey Id { get; set; }
    }

    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
