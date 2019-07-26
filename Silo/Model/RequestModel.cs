using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 请求数据模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestModel<T>
    {
        /// <summary>
        /// 原数据
        /// </summary>
        public T Origin { get; set; }
    }
}
