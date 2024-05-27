namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StazaRestrictionRasaController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiOgranicenjaStaze/{stazaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int stazaId) {
        (bool isError, var ogranicienja, string? error, int code) =
            (await DataProvider.VratiSveOgranicenjaRaseZaStazuAsync(stazaId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(ogranicienja);
    }
    
    [HttpPut]
    [Route("AzurirajOgranicenjeStaze")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] StazaRestrictionRasaView srrw) {
        var data = await DataProvider.AzurirajOgranicenjeRaseZaStazuAsync(srrw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirano ogranicejne staze sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajOgranicenjeStaze/{stazaId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj(int stazaId, [FromBody]StazaRestrictionRasaView srrw) {
        var data = await DataProvider.DodajOgranicenjeRaseZaStazu(stazaId, srrw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodato ogranicenje staza sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOgranicenjeStaze/{ogranicenjeStazeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int ogranicenjeStazeId) {
        var data = await DataProvider.ObrisiOgranjicenjeRaseZaStazuAsync(ogranicenjeStazeId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisano ogranicenje staze.");
    }
}