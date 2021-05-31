namespace BackendDemo.Infrastructure
{
    public interface ICurrentUserAccessor
    {
        string GetCurrentUsername();
    }
}