namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IgracController : ControllerBase {

    [HttpGet]
    [Route("PreuzmiIgrace")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace() {
        (bool isError, var igraci, string? error, int code) = (await DataProvider.VratiSveIgraceAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(igraci);
    }
    
    [HttpGet]
    [Route("PreuzmiIgraca/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrac(int igracId) {
        (bool isError, var igrac, string? error, int code) = (await DataProvider.VratiIgracaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(igrac);
    }
    
    [HttpPost]
    [Route("DodajIgraca")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajIgraca([FromBody] IgracView iw) {
        var data = await DataProvider.DodajIgracaAsync(iw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return StatusCode(StatusCodes.Status201Created, $"Uspesno dodat igrac. Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajIgraca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajIgraca([FromBody] IgracView iw) {
        var data = await DataProvider.AzurirajIgracaAsync(iw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran igrac sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiIgraca/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
    public async Task<ActionResult> ObrisiIgracac(int igracId) {
        var data = await DataProvider.ObrisiIgracaAsync(igracId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan igrac.");
    }
}