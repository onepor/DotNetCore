using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helper;
using DotNetCore.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeChatAuthController : ControllerBase
    {
        private const string SECRET = "404a9e0b4d9180654ee41ebe46abe7dd";
        private const string APPID = "wx596a1de6fc1ff171";

        private readonly ILogger<WeChatAuthController> _logger;

        public WeChatAuthController(ILogger<WeChatAuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult WeChatAuth(string JSCODE)
        {
            string RetJson = HttpHelper.Http("https://api.weixin.qq.com/sns/jscode2session?appid="+ APPID + "&secret="+ SECRET + "&js_code="+ JSCODE + "&grant_type=authorization_code");
            
            return Ok(new
            {
                data = RetJson
            });
        }

        [HttpGet]
        public IActionResult WeChatGetPhoneNumber(string iv,string encryptedData,string code)
        {
            var loginData = WeChatHelper.WeChatLogin(APPID, SECRET,code);
            if (loginData != null )
            {
                var RetJson = WeChatHelper.GetPhoneNumber(loginData.openid, loginData.session_key, iv, encryptedData);

                return Ok(new
                {
                    data = RetJson
                });
            }
            else
            {
                return Ok(new
                {
                    data = "500"
                });
            }
        }
    }
}
