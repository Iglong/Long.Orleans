using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class ChatGrain : Grain, IChat
    {
        private readonly ILogger logger;

        public ChatGrain(ILogger<ChatGrain> logger)
        {
            this.logger = logger;
        }

        Task IChat.SayHello(string greeting)
        {
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            _subsManager.Notify(observer => observer.Instance.ReceiveMessage(greeting), GrainReference.ToKeyString(), typeof(ChatGrain));
            return Task.CompletedTask;
        }
        private ObserverSubscriptionManager<IChatCallback> _subsManager;

        public override async Task OnActivateAsync()
        {

            // We created the utility at activation time.
            _subsManager = new ObserverSubscriptionManager<IChatCallback>();
            await base.OnActivateAsync();
        }

        // Clients call this to subscribe.
        public async Task Subscribe(IChatCallback observer)
        {

            _subsManager.Subscribe(new ObserverInstance<IChatCallback> { Key = GrainReference.ToKeyString(), Type = typeof(ChatGrain), Instance = observer });
        }

        //Also clients use this to unsubscribe themselves to no longer receive the messages.
        public async Task UnSubscribe(IChatCallback observer)
        {
            _subsManager.Unsubscribe(new ObserverInstance<IChatCallback> { Key = GrainReference.ToKeyString(), Type = typeof(ChatGrain), Instance = observer });
        }
    }
}
