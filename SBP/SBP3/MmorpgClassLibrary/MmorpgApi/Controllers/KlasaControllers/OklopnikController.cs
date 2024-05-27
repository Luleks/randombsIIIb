namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class OklopnikController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiOklopnika/{oklopnikId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int oklopnikId) {
        (bool isError, var oklopnik, string? error, int code) = (await DataProvider.VratiOklopnikaAsync(oklopnikId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(oklopnik);
    }
    
    [HttpPut]
    [Route("AzurirajOklopnika")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajOklopnika([FromBody] OklopnikView ow) {
        var data = await DataProvider.AzurirajOklopnikaAsync(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran oklopnik sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajOklopnika/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajOklopnika(int likId, [FromBody]OklopnikView ow) {
        var data = await DataProvider.DodajKlasuOklopnikLikuAsync(likId, ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat oklopnik sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOklopnika/{oklopnikId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int oklopnikId) {
        var data = await DataProvider.ObrisiOklopnikaAsync(oklopnikId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan oklopnik.");
    }
}