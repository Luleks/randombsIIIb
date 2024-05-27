namespace MmorpgApi.Controllers.KlasaControllers;

[ApiController]
[Route("[controller]")]
public class CarobnjakController : ControllerBase {
    
    [HttpGet]
    [Route("PreuzmiCarobnjaka/{carobnjakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIgrace(int carobnjakId) {
        (bool isError, var carobnjak, string? error, int code) = (await DataProvider.VratiCarobnjakaAsync(carobnjakId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(carobnjak);
    }
    
    [HttpPut]
    [Route("AzurirajCarobnjaka")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AzurirajCarobnjaka([FromBody] CarobnjakView cw) {
        var data = await DataProvider.AzurirajCarobnjakaAsync(cw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azuriran carobnjak sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajCarobnjaka/{likId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DodajCarobnjaka(int likId, [FromBody]CarobnjakView cw) {
        var data = await DataProvider.DodajKlasuCarobnjakLikuAsync(likId, cw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodat carobnjak sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiCarobnjaka/{carobnjakId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ObrisiIgracac(int carobnjakId) {
        var data = await DataProvider.ObrisiCarobnjakaAsync(carobnjakId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisan carobnjak.");
    }
}