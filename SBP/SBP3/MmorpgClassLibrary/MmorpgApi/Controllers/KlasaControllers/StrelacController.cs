namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class StrelacController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiStrelca/{strelacId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int strelacId) {
        (bool isError, var strelac, string? error, int code) = (await DataProvider.VratiStrelcaAsync(strelacId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(strelac);
    }
    
    [HttpPut]
    [Route("AzurirajStrelca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajStrelca([FromBody] StrelacView sw) {
        var data = await DataProvider.AzurirajStrelcaAsync(sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran strelac sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajStrelca/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajStrelca(int likId, [FromBody]StrelacView sw) {
        var data = await DataProvider.DodajKlasuStrelacLikuAsync(likId, sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat strelac sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiStrelca/{strelacId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int strelacId) {
        var data = await DataProvider.ObrisiStrelcaAsync(strelacId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan strelac.");
    }
}