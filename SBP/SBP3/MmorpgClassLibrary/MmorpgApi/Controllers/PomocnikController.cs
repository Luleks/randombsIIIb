namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PomocnikController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiPomicnike/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int igracId) {
        (bool isError, var pomocnici, string? error, int code) = (await DataProvider.VratiSvePomocnikeIgracaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(pomocnici);
    }
    
    [HttpPut]
    [Route("AzurirajPomicnika")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody]PomocnikView pw) {
        var data = await DataProvider.AzurirajPomocnikaAsync(pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran pomocnik sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajPomicnika/{igracId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj(int igracId, [FromBody]PomocnikView pw) {
        var data = await DataProvider.DodajPomocnika(igracId, pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat pomocnik sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiPomicnika/{pomocnikId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiPomicnika(int pomocnikId) {
        var data = await DataProvider.ObrisiPomocnikaAsync(pomocnikId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan pomocnik.");
    }
}