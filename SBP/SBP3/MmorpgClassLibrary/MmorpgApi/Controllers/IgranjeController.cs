namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IgranjeController : ControllerBase {
    
    [HttpPost]
    [Route("DodajIgranjeStaze")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajIgranje([FromBody]IgraView iw, [FromQuery]int[] igraci) {
        (bool isError, var igranje, string? error, int code) = (await DataProvider.DodajIgranje(iw, [..igraci]));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(igranje);
    }
    
    [HttpGet]
    [Route("PreuzmiIgranjeStaze/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetStaze(int igracId) {
        (bool isError, var igranja, string? error, int code) = (await DataProvider.PreuzmiIgracevaIgranjaStaza(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(igranja);
    }
    
    [HttpGet]
    [Route("PreuzmiGrupu/{grupaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetGrupa(int grupaId) {
        (bool isError, var grupa, string? error, int code) = (await DataProvider.PreuzmiGrupu(grupaId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(grupa);
    }
    
    [HttpDelete]
    [Route("ObrisiIgranje/{igranjeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int igranjeId) {
        var data = await DataProvider.ObrisiIgranje(igranjeId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisao igranje.");
    }
}