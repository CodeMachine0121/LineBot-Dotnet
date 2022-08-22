using Microsoft.AspNetCore.Mvc;
using LineBotDemo.Filters;
namespace LineBotDemo;

public class LineVerifySignatureAttributes : TypeFilterAttribute
{
    public LineVerifySignatureAttributes() : base(typeof(LineVerifySignatureFilter))
    {
        
    }
}