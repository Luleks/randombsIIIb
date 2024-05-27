namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OklopController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiOklope")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var oklopi, string? error, int code) = (await DataProvider.VratiSveOklopeAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(oklopi);
    }
    
    [HttpPut]
    [Route("AzurirajOklop")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] OklopView ow) {
        var data = await DataProvider.AzurirajOklopAsync(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran oklop sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajOklop")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]OklopView ow) {
        var data = await DataProvider.DodajOklop(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat oklop sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOklop/{oklopId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int oklopId) {
        var data = await DataProvider.ObrisiOklopAsync(oklopId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan oklop.");
    }
}