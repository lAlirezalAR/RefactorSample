namespace Utilities.Framework.Contracts
{
    public interface IEntity<Tkey>
    {
        Tkey Id
        {
            get;
        }
    }
}
