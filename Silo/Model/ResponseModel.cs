using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 响应数据模型
    /// </summary>
    public class ResponseModel<T>
    {
        /// <summary>
        /// 请求结束时产生的新数据的唯一标识符
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
       
        /// <summary>
        ///请求返回的数据项
        /// </summary>
        public List<T> Items { get; set; }
        /// <summary>
        /// 请求返回的消息文本
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 请求返回的消息状态码
        /// </summary>
        public int Code { get; set; }
    }
}
