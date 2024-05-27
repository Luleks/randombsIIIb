namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamMembershipController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiClanoveTima/{timId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int timId) {
        (bool isError, var clanstva, string? error, int code) = (await DataProvider.VratiSveClanoveTimaAsync(timId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(clanstva);
    }
    
    [HttpGet]
    [Route("PreuzmiIstorijuTimovaIgraca/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetHistory(int igracId) {
        (bool isError, var clanstva, string? error, int code) = (await DataProvider.VratiIstorijuTimovaLikaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(clanstva);
    }
    
    [HttpPut]
    [Route("AzurirajClanstvo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] TeamMembershipView tw) {
        var data = await DataProvider.AzurirajClanstvoAsync(tw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirano clanstvo sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajClanstvo")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody] TeamMembershipView tw) {
        var data = await DataProvider.DodajClanstvo(tw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodato clanstvo sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiClanstvo/{clanstvoId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int clanstvoId) {
        var data = await DataProvider.ObrisiClanstvoAsync(clanstvoId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisano clanstvo.");
    }
}