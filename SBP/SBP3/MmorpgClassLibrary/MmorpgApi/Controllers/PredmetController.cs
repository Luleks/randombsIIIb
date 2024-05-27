namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PredmetController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiPredmete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var predmeti, string? error, int code) = (await DataProvider.VratiSvePredmeteAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(predmeti);
    }
    
    [HttpPut]
    [Route("AzurirajPredmet")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] PredmetView pw) {
        var data = await DataProvider.AzurirajPredmetAsync(pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran predmet sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajPredmet")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]PredmetView pw) {
        var data = await DataProvider.DodajPredmet(pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat predmet sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisPredmet/{predmetId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int predmetId) {
        var data = await DataProvider.ObrisiPredmetAsync(predmetId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan predmet.");
    }
}