using Common.Model.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helper
{
    public class WeChatHelper
    {

        #region 小程序登陆

        /// <summary>
        /// 小程序登陆
        /// </summary>
        /// <param name="AppId">微信小程序AppID</param>
        /// <param name="Secret">微信小程序Secret </param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WeChatUserModel WeChatLogin(string AppId, string Secret, string code)
        {
            try
            {

                #region 获取Session_Key

                string serviceAddress = "https://api.weixin.qq.com/sns/jscode2session?appid=" + AppId + "&secret="
                + Secret + "&js_code=" + code + "&grant_type=authorization_code";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.Method = "GET";
                request.ContentType = "text/html;charset=utf-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                var obj = new
                {
                    data = retString,
                    Success = true
                };
                Formatting microsoftDataFormatSettings = default(Formatting);
                string result = JsonConvert.SerializeObject(obj, microsoftDataFormatSettings);
                //序列化获取session_key
                WeChatUserModel item = JsonConvert.DeserializeObject<WeChatUserModel>(retString);

                return item;

                #endregion
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 获取微信手机号
        /// <summary>
        /// 获取微信手机号
        /// </summary>
        /// <param name="openid">openid</param>
        /// <param name="session_key">session_key </param>
        /// <param name="aesIv">向量</param>
        /// <param name="encryptedData">encryptedData</param>
        /// <param name="code">加密数据</param>
        /// <returns></returns>
        public static WecharDetailsModel<watermark> GetPhoneNumber(string openid, string session_key, string aesIv, string encryptedData)
        {
            try
            {
                #region 获取微信绑定手机号

                //判断是否是16位 如果不够补0
                //text = tests(text);
                //16进制数据转换成byte
                byte[] encryptedDatas = Convert.FromBase64String(encryptedData); // strToToHexByte(text);
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Key = Convert.FromBase64String(session_key); // Encoding.UTF8.GetBytes(AesKey);
                rijndaelCipher.IV = Convert.FromBase64String(aesIv);// Encoding.UTF8.GetBytes(AesIV);
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedDatas, 0, encryptedDatas.Length);
                string results = Encoding.Default.GetString(plainText);

                //序列化获取手机号码
                WecharDetailsModel<watermark> getPhone = JsonConvert.DeserializeObject<WecharDetailsModel<watermark>>(results);
                getPhone.watermark.openid = openid; //获取用户OpenID
                return getPhone;

                #endregion

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
