using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces
{
    public interface ILocalHealthCheckGrain : IGrainWithGuidKey
    {
        Task PingAsync();
    }
}
