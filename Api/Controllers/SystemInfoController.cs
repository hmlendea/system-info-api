using Microsoft.AspNetCore.Mvc;
using SystemInfoApi.Service;
using NuciAPI.Controllers;
using SystemInfoApi.Configuration;
using SystemInfoApi.Api.Requests;
using SystemInfoApi.Api.Responses;
using SystemInfoApi.Api.Mapping;

namespace SystemInfoApi.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemInfoController(
        ISystemInfoService service,
        SecuritySettings securitySettings) : NuciApiController
    {
        [HttpGet]
        public ActionResult Get()
            => ProcessRequest(
                new GetSystemInfoRequest(),
                () =>
                {
                    GetSystemInfoResponse response = new()
                    {
                        SystemInfo = service.GetSystemInfo().ToApiObject(),
                        NetworkInfo = service.GetNetworkInfo().ToApiObject(),
                        RegionalInfo = service.GetRegionalInfo().ToApiObject()
                    };

                    response.SignHMAC(securitySettings.HmacSigningKey);

                    return response;
                },
                NuciApiAuthorisation.ApiKey(securitySettings.ApiKey));
    }
}
