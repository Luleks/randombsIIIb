namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JeKupioController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiKupovine/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int igracId) {
        (bool isError, var kupovine, string? error, int code) = (await DataProvider.VratiSveKupovineIgracaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(kupovine);
    }
    
    [HttpPost]
    [Route("DodajKupovinu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody] JeKupioView jk) {
        var data = await DataProvider.DodajKupovinu(jk);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodata kupovina sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiKupovinu/{kupovinaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int kupovinaId) {
        var data = await DataProvider.ObrisiKupovinuAsync(kupovinaId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisana kupovina.");
    }
}