namespace Mego.Database.Models.Exception
{
    /// <summary>
    /// Not found exception for business logic. Status code 404 NotFound
    /// </summary>
    public class ItemNotFoundException : System.Exception
    {
        public ItemNotFoundException(string message) : base(message)
        {

        }
    }
}