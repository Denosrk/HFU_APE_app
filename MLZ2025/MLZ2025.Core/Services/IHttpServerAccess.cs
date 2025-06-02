using MLZ2025.Core.Model;

namespace MLZ2025.Core.Services
{
    public abstract class IHttpServerAccess
    {
        public abstract Task<IList<ServerAddress>> GetAddressesAsync();
    }
}
