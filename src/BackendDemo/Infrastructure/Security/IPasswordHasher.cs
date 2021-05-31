using System.Threading.Tasks;

namespace BackendDemo.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        Task<byte[]> Hash(string password, byte[] salt);
    }
}