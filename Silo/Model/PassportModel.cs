using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PassportModel : BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public new Guid   Id { get; set; }
        /// <summary>
        /// 用户登录名/通行证号
        /// </summary>
        public string Passport { get; set; }
        /// <summary>
        /// 用户登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户首次设置的登陆密码
        /// </summary>
        public string OriginPassword { get; set; }
        /// <summary>
        /// 用户初始化使用的密码
        /// </summary>
        public string InitPassword { get; set; }
        /// <summary>
        /// 是否兼容电子邮件
        /// </summary>
        public bool IsEmail { get; set; }
        /// <summary>
        /// 电子邮件域名 比如: iglong.com
        /// </summary>
        public string Email { get; set; }
    }
}
