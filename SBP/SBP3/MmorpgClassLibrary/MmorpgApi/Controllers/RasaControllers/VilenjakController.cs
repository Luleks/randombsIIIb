namespace MmorpgApi.Controllers.RasaControllers;

[ApiController]
[Route("[controller]")]
public class VilenjakController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiVilenjaka/{vilenjakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int vilenjakId) {
        (bool isError, var vilenjak, string? error, int code) = (await DataProvider.VratiVilenjakaAsync(vilenjakId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(vilenjak);
    }
    
    [HttpPost]
    [Route("DodajVilenjaka/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajVilenjaka(int likId, [FromBody] VilenjakView vw) {
        var data = await DataProvider.DodajRasuVilenjakLikuAsync(likId, vw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat vilenjak sa Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajVilenjaka")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajVilenjaka([FromBody] VilenjakView vw) {
        var data = await DataProvider.AzurirajVilenjakaAsync(vw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran vilenjak sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiVilenjaka/{vilenjakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiVilenjaka(int vilenjakId) {
        var data = await DataProvider.ObrisiVilenjakaAsync(vilenjakId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan vilenjak.");
    }
}