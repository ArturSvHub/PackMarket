namespace PackMarket.Services
{
    public class BlazorAppContext
    {
        public string IP { get; set; }
        public BlazorAppContext()
        {
            IP= new HttpContextAccessor().HttpContext.Connection?.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
