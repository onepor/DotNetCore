using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost]
        //public async Task<NewtonsoftJsonResult> LoginIn(string userName, string userPassword, string code)
        //{
        //    AjaxResult objAjaxResult = new AjaxResult();
        //    var user = _userBll.GetUser(userName, userPassword);
        //    if (user == null)
        //    {
        //        objAjaxResult.Result = DoResult.NoAuthorization;
        //        objAjaxResult.PromptMsg = "用户名或密码错误";
        //    }
        //    else
        //    {
        //        var claims = new List<Claim>
        //{
        //    new Claim("userName", userName),
        //    new Claim("userID",user.Id.ToString()),
        //};
        //        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
        //        objAjaxResult.Result = DoResult.Success;
        //        objAjaxResult.PromptMsg = "登录成功";
        //    }
        //    return new NewtonsoftJsonResult(objAjaxResult);
        //}
    }
}
