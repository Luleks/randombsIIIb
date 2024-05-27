namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SesijaController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiSesije/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int igracId) {
        (bool isError, var sesije, string? error, int code) = (await DataProvider.VratiSveSesijeIgracaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(sesije);
    }
    
    [HttpPut]
    [Route("AzurirajSesiju")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] SesijaView sw) {
        var data = await DataProvider.AzurirajSesijuAsync(sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirana sesija sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajSesiju/{igracId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajSesiju(int igracId, [FromBody]SesijaView sw) {
        var data = await DataProvider.DodajSesiju(igracId, sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodata sesija sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiSesiju/{sesijaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiSesiju(int sesijaId) {
        var data = await DataProvider.ObrisiSesijuAsync(sesijaId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisana sesija.");
    }
}