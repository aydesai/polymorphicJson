using Microsoft.AspNetCore.Mvc;

namespace PolymorphicJson.Controllers;

[ApiController]
[Route("[controller]")]
public class TriggerDetailsController : ControllerBase
{
    private readonly ILogger<TriggerDetailsController> _logger;

    public TriggerDetailsController(ILogger<TriggerDetailsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetTriggerDetails")]
    public IEnumerable<TriggerDetails> Get()
    {
        return new List<TriggerDetails>
        {
            new UploadBegin
            {
                Id = 100, Name = "abc",
                FileName = "myFile", TimeOfBeginArrival = DateTime.Now
            },
            new UploadComplete
            {
                Id = 200, Name = "xyz",
                UploadBeginId = 205, TimeOfFinishArrival = DateTime.Now
            }
        };
    }

    [HttpPost(Name = "AddTriggerDetails")]
    public TriggerDetails Post([FromBody] TriggerDetails details)
    {
        if (details is UploadBegin uploadBegin)
        {
            return uploadBegin;
        }
        else if (details is UploadComplete uploadComplete)
        {
            return uploadComplete;
        }
        return details;
    }
}
