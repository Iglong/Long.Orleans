using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface IStorageHealthCheckGrain : IGrainWithGuidKey
    {
        Task CheckAsync();
    }
}
