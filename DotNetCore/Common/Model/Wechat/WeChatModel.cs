using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model.Wechat
{
    public class WeChatModel
    {
        private string _appid = string.Empty;
        /// <summary>
        /// 小程序 APPID
        /// </summary>
        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }


        private string _appsecret = string.Empty;
        /// <summary>
        /// 小程序 APPSECRET
        /// </summary>
        public string appsecret
        {
            get { return _appsecret; }
            set { _appsecret = value; }
        }

    }

    public class WeChatUserModel
    {
        private string _openid = string.Empty;
        /// <summary>
        /// 用户 openid
        /// </summary>
        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }

        private string _session_key = string.Empty;
        /// <summary>
        /// 用户登陆 key
        /// </summary>
        public string session_key
        {
            get { return _session_key; }
            set { _session_key = value; }
        }

    }
    //获取用户手机号
    public class WecharDetailsModel<T>
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 区域手机号
        /// </summary>
        public string purePhoneNumber { get; set; }
        /// <summary>
        /// 区码
        /// </summary>
        public string countryCode { get; set; }

        public T watermark { get; set; }
    }

    public class watermark
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 用户appid
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }
}
