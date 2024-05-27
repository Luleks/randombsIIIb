namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OruzjeController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiOruzja")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var oruzja, string? error, int code) = (await DataProvider.VratiSvaOruzjaAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(oruzja);
    }
    
    [HttpPut]
    [Route("AzurirajOruzje")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] OruzjeView ow) {
        var data = await DataProvider.AzurirajOruzjeAsync(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirano oruzje sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajOruzje")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]OruzjeView ow) {
        var data = await DataProvider.DodajOruzje(ow);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodato oruzje sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiOruzje/{oruzjeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int oruzjeId) {
        var data = await DataProvider.ObrisiOruzjeAsync(oruzjeId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisano oruzje.");
    }
}