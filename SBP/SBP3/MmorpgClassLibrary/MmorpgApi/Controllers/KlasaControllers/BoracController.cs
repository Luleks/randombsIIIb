namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class BoracController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiBorca/{boracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int boracId) {
        (bool isError, var borac, string? error, int code) = (await DataProvider.VratiBorcaAsync(boracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(borac);
    }
    
    [HttpPut]
    [Route("AzurirajBorca")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajBorca([FromBody] BoracView bw) {
        var data = await DataProvider.AzurirajBorcaAsync(bw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran borac sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajBorca/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajBorca(int likId, [FromBody]BoracView bw) {
        var data = await DataProvider.DodajKlasuBoracLikuAsync(likId, bw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat borac sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiBorca/{boracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int boracId) {
        var data = await DataProvider.ObrisiBorcaAsync(boracId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan borac.");
    }
}