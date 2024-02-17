using JKF.Utils;
using Microsoft.AspNetCore.Mvc;
using Web;

namespace JKF.ChatApi
{
    [HandlerLogin(FilterMode.Enforce)]
    public class BaseController : ControllerBase
    {
        
    }
}
