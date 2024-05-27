namespace MmorpgApi.Controllers.RasaControllers;

[ApiController]
[Route("[controller]")]
public class PatuljakController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiPatuljka/{patuljakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int patuljakId) {
        (bool isError, var patuljak, string? error, int code) = (await DataProvider.VratiPatuljkaAsync(patuljakId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(patuljak);
    }
    
    [HttpPost]
    [Route("DodajPatuljka/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajPatuljka(int likId, [FromBody] PatuljakView pw) {
        var data = await DataProvider.DodajRasuPatuljakLikuAsync(likId, pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat patuljak sa Id: {data.Data}");
    }
    
    [HttpPut]
    [Route("AzurirajPatuljka")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajPatuljka([FromBody] PatuljakView pw) {
        var data = await DataProvider.AzurirajPatuljkaAsync(pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran patuljak sa Id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiPatuljka/{patuljakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiPatuljka(int patuljakId) {
        var data = await DataProvider.ObrisiPatuljkaAsync(patuljakId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan patuljak.");
    }
}