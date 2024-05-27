namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TimController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiTimove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var timovi, string? error, int code) = (await DataProvider.VratiSveTimoveAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(timovi);
    }
    
    [HttpPut]
    [Route("AzurirajTim")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] TimView tw) {
        var data = await DataProvider.AzurirajTimAsync(tw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran tim sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajTim")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]TimView tw) {
        var data = await DataProvider.DodajTim(tw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat tim sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiTim/{timId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int timId) {
        var data = await DataProvider.ObrisiTimAsync(timId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan tim.");
    }
}