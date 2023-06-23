namespace AuthReadyAPI.Exceptions
{
    public class NotFound : ApplicationException
    {
        public NotFound(string name, object key) : base($"{name} {key} was not found")
        {
        }
    }
}
