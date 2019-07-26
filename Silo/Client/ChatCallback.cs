using GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class ChatCallback : IChatCallback
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"收到服务端推送消息：{message}");
        }
    }
}
