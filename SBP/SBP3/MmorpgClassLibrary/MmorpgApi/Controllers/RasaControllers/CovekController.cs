namespace MmorpgApi.Controllers.RasaControllers;

[ApiController]
[Route("[controller]")]
public class CovekController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiCoveka/{covekId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int covekId) {
        (bool isError, var covek, string? error, int code) = (await DataProvider.VratiCovekaAsync(covekId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(covek);
    }
    
    [HttpPost]
    [Route("DodajCoveka/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajCoveka(int likId, [FromBody] CovekView cw) {
        var data = await DataProvider.DodajRasuCovekLikuAsync(likId, cw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat covek sa Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajCoveka")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajCoveka([FromBody] CovekView cw) {
        var data = await DataProvider.AzurirajCovekaAsync(cw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran covek sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiCoveka/{covekId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiCoveka(int covekId) {
        var data = await DataProvider.ObrisiCovekaAsync(covekId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan covek.");
    }
}