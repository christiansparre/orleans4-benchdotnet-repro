using Orleans;

namespace OrleansPlayground.Grains.Interfaces
{
    public interface IHelloWorldGrain : IGrainWithStringKey
    {
        Task<HelloMessage> Hello(string name);
    }
}