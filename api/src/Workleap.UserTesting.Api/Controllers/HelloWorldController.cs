using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Workleap.UserTesting.Api.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
public sealed class HelloWorldController : ControllerBase
{
    private readonly ILogger<HelloWorldController> _logger;
    private readonly IConfiguration _configuration;

    public HelloWorldController(ILogger<HelloWorldController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("helloworld")]
    public ActionResult HelloWorld()
    {
        this._logger.LogInformation($"HelloWorld called {DateTime.UtcNow} - {GetIpAddress(HttpContext)}");

        return this.Ok(@$"
                                    Hello from Workleap.UserTesting.Api
                  -----                                                               -----
                1 | H |                                                               |He |
                  |---+----                                       --------------------+---|
                2 |Li |Be |                                       | B | C | N | O | F |Ne |
                  |---+---|                                       |---+---+---+---+---+---|
                3 |Na |Mg |3B  4B  5B  6B  7B |    8B     |1B  2B |Al |Si | P | S |Cl |Ar |
                  |---+---+---------------------------------------+---+---+---+---+---+---|
                4 | K |Ca |Sc |Ti | V |Cr |Mn |Fe |Co |Ni |Cu |Zn |Ga |Ge |As |Se |Br |Kr |
                  |---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---|
                5 |Rb |Sr | Y |Zr |Nb |Mo |Tc |Ru |Rh |Pd |Ag |Cd |In |Sn |Sb |Te | I |Xe |
                  |---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---|
                6 |Cs |Ba |LAN|Hf |Ta | W |Re |Os |Ir |Pt |Au |Hg |Tl |Pb |Bi |Po |At |Rn |
                  |---+---+---+------------------------------------------------------------
                7 |Fr |Ra |ACT|
                  -------------
                              -------------------------------------------------------------
                  Lanthanide |La |Ce |Pr |Nd |Pm |Sm |Eu |Gd |Tb |Dy |Ho |Er |Tm |Yb |Lu |
                              |---+---+---+---+---+---+---+---+---+---+---+---+---+---+---|
                  Actinide   |Ac |Th |Pa | U |Np |Pu |Am |Cm |Bk |Cf |Es |Fm |Md |No |Lw |
                              -------------------------------------------------------------
                              
                Sentinel key: {_configuration["Sentinel"]}");
    }

    private static string GetIpAddress(HttpContext context)
    {
        // An AppGateway will set the original IP address in the "x-forwarded-for" header
        // https://docs.microsoft.com/en-us/azure/application-gateway/how-application-gateway-works
        if (context.Request.Headers.ContainsKey("x-forwarded-for"))
        {
            return context.Request.Headers["x-forwarded-for"];
        }

        return context.Connection.RemoteIpAddress.ToString();
    }
}
