using DomainModel;
using InfoTrack.DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.UrlPosition;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchUrlController : ControllerBase
{
    private readonly ILogger<SearchUrlController> _logger;
    private readonly IUrlPosition _urlPosition;

    public SearchUrlController(ILogger<SearchUrlController> logger, IUrlPosition urlPosition)
    {
        _logger = logger;
        _urlPosition = urlPosition;
    }

    [HttpGet(Name = "searchurl")]
    public async Task<IActionResult> Get([FromQuery]SearchUrlRequest request) 
    {
        TryValidateModel(request);
        var result = await _urlPosition.CalculatePositionUrl(request);
        return Ok(new SearchUrlResponse
        {
            Data = result,
            Success = true
        });
    }
}
