namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrudjeRestrictionKlasaController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiOgranicenjaOrudja/{orudjeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int orudjeId) {
        (bool isError, var ogranicienja, string? error, int code) =
            (await DataProvider.VratiSveOgranicenjaKlaseZaOrudjeAsync(orudjeId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(ogranicienja);
    }
    
    [HttpPut]
    [Route("AzurirajOgranicenjeOrudja")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] OrudjeRestrictionKlasaView orkw) {
        var data = await DataProvider.AzurirajOgranicenjeKlaseZaOrudjeAsync(orkw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirano ogranicejne orudja sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajOgranicenjeOrudja/{orudjeId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj(int orudjeId, [FromBody]OrudjeRestrictionKlasaView orkw) {
        var data = await DataProvider.DodajOgranicenjeKlaseZaOrudje(orudjeId, orkw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodato ogranicenje orudja sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOgranicenjeOrudja/{ogranicenjeOrudjaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int ogranicenjeOrudjaId) {
        var data = await DataProvider.ObrisiOgranjicenjeKlaseZaOrudjeAsync(ogranicenjeOrudjaId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisano ogranicenje orudja.");
    }
}