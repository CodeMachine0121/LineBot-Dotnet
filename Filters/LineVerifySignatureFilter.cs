using System.Security.Cryptography;
using System.Text;
using LineBotDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace  LineBotDemo.Filters;

public class LineVerifySignatureFilter: IAuthorizationFilter
{
    private readonly IOptions<LineSetting> lineSetting;
    public LineVerifySignatureFilter(IOptions<LineSetting> _lineSetting)
    {
        lineSetting = _lineSetting;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.EnableBuffering();

        string requestBody = new StreamReader(context.HttpContext.Request.Body).ReadToEnd(); 
        context.HttpContext.Request.Body.Position = 0;

        var xLineSignature = context.HttpContext.Request.Headers["X-Line-Signature"].ToString();
        var channelSecret = Encoding.UTF8.GetBytes(lineSetting.Value.ChannelSecret);
        var body = Encoding.UTF8.GetBytes(requestBody);

        using (HMACSHA256 hmac = new HMACSHA256(channelSecret))
        {
            var hash = hmac.ComputeHash(body, 0, body.Length);
            var xLineBytes = Convert.FromBase64String(xLineSignature);

            if (SlowEquals(xLineBytes, hash) == false)
                context.Result = new ForbidResult();
        }
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff  =(uint)a.Length^(uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint) (a[i] ^ b[i]);
        }
        
        return diff == 0;
    }
}