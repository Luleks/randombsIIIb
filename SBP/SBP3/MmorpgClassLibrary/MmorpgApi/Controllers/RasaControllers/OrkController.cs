namespace MmorpgApi.Controllers.RasaControllers;

[ApiController]
[Route("[controller]")]
public class OrkController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiOrka/{orkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int orkId) {
        (bool isError, var ork, string? error, int code) = (await DataProvider.VratiOrkaAsync(orkId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(ork);
    }
    
    [HttpPost]
    [Route("DodajOrka/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajOrka(int likId, [FromBody] OrkView ow) {
        var data = await DataProvider.DodajRasuOrkLikuAsync(likId, ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat ork sa Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajOrka")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajOrka([FromBody] OrkView ow) {
        var data = await DataProvider.AzurirajOrkaAsync(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran ork sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOrka/{orkId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiOrka(int orkId) {
        var data = await DataProvider.ObrisiOrkaAsync(orkId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan ork.");
    }
}