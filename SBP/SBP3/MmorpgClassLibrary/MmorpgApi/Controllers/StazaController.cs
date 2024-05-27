namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StazaController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiStaze")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var staze, string? error, int code) = (await DataProvider.VratiSveStazeAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(staze);
    }
    
    [HttpPut]
    [Route("AzurirajStazu")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] StazaView sw) {
        var data = await DataProvider.AzurirajStazuAsync(sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirana staza sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajStazu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]StazaView sw) {
        var data = await DataProvider.DodajStazu(sw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodata staza sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiStazu/{stazaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int stazaId) {
        var data = await DataProvider.ObrisiStazuAsync(stazaId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisana staza.");
    }
}