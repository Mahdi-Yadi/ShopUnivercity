using System.Security.Claims;
using System.Security.Principal;
namespace ShopUnivercity.Web.Tools;
public static class IdentityExtentions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal != null)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (data != null)
            {
                if (ulong.TryParse(data.Value, out var userId))
                {
                    return (int)userId;
                }
                else
                {
                    throw new OverflowException("Value was either too large or too small for a UInt64.");
                }
            }
        }

        return default;
    }

    public static int GetUserId(this IPrincipal principal)
    {
        var user = (ClaimsPrincipal)principal;
        return user.GetUserId();
    }
}