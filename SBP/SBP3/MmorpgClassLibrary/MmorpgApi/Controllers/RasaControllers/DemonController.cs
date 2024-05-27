namespace MmorpgApi.Controllers.RasaControllers;

[ApiController]
[Route("[controller]")]
public class DemonController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiDemona/{demonId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int demonId) {
        (bool isError, var demon, string? error, int code) = (await DataProvider.VratiDemonaAsync(demonId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(demon);
    }
    
    [HttpPost]
    [Route("DodajDemona/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajDemona(int likId, [FromBody] DemonView dw) {
        var data = await DataProvider.DodajRasuDemonLikuAsync(likId, dw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat demon sa Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajDemona")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajDemona([FromBody] DemonView dw) {
        var data = await DataProvider.AzurirajDemonaAsync(dw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran demon sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiDemona/{demonId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiDemona(int demonId) {
        var data = await DataProvider.ObrisiDemonaAsync(demonId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan demon.");
    }
}