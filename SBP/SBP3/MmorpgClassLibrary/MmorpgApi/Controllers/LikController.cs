namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LikController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiLika/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetLik(int igracId) {
        (bool isError, var lik, string? error, int code) = (await DataProvider.VratiIgracevogLikaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(lik);
    }

    [HttpPost]
    [Route("DodajLika/{igracId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajLika(int igracId, [FromBody] LikView iw) {
        var data = await DataProvider.DodajLikaAsync(igracId, iw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat lik. Id = {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajLika")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajLika([FromBody] LikView lw) {
        var data = await DataProvider.AzurirajLikaAsync(lw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran lik sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiLika/{likId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int likId) {
        var data = await DataProvider.ObrisiLikaAsync(likId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan lik.");
    }
}