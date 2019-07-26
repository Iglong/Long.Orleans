using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IChat : IGrainWithIntegerKey
    {
        /// <summary>
        /// 简化版的讲话
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SayHello(string message);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        Task Subscribe(IChatCallback observer);
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        Task UnSubscribe(IChatCallback observer);
    }
}
