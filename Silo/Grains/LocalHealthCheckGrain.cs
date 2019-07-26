using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;
using Orleans.Concurrency;

namespace Grains
{
    [StatelessWorker(1)]
    public class LocalHealthCheckGrain : Grain, ILocalHealthCheckGrain
    {
        public Task PingAsync() => Task.CompletedTask;
    }
}
