namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PosedujeController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiPosedovanja/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int igracId) {
        (bool isError, var posedovanja, string? error, int code) = (await DataProvider.VratiSvaPosedovanjaIgracaAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(posedovanja);
    }
    
    [HttpPost]
    [Route("DodajPosedovanje")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody] PosedujeView pw) {
        var data = await DataProvider.DodajPosedovanje(pw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodato posedovanje sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiPosedovanje/{posedovanjeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int posedovanjeId) {
        var data = await DataProvider.ObrisiPosedovanjeAsync(posedovanjeId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisano posedovanje.");
    }
}