using Orleans;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrainInterfaces
{
    /// <summary>
    /// 接收服务端推送消息的客户端接收消息接口
    /// </summary>
    public interface IChatCallback : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}
