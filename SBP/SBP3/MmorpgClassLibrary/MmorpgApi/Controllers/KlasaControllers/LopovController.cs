namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class LopovController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiLopova/{lopovId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int lopovId) {
        (bool isError, var lopov, string? error, int code) = (await DataProvider.VratiLopovaAsync(lopovId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(lopov);
    }
    
    [HttpPut]
    [Route("AzurirajLopova")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajLopova([FromBody] LopovView lw) {
        var data = await DataProvider.AzurirajLopovaAsync(lw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran lopov sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajLopova/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajLopova(int likId, [FromBody]LopovView lw) {
        var data = await DataProvider.DodajKlasuLopovLikuAsync(likId, lw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat lopov sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiLopova/{lopovId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int lopovId) {
        var data = await DataProvider.ObrisiLopovaAsync(lopovId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan lopov.");
    }
}