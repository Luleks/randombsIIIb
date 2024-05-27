namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class SvestenikController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiSvestenika/{svestenikId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int svestenikId) {
        (bool isError, var svestenik, string? error, int code) = (await DataProvider.VratiSvestenikaAsync(svestenikId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(svestenik);
    }
    
    [HttpPut]
    [Route("AzurirajSvestenika")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajSvestenika([FromBody] SvestenikView sw) {
        var data = await DataProvider.AzurirajSvestenikaAsync(sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran svestenik sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajSvestenika/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajSvestenika(int likId, [FromBody]SvestenikView sw) {
        var data = await DataProvider.DodajKlasuSvestenikLikuAsync(likId, sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat svestenik sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiSvestenika/{svestenikId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int svestenikId) {
        var data = await DataProvider.ObrisiSvestenikaAsync(svestenikId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan svestenik.");
    }
}