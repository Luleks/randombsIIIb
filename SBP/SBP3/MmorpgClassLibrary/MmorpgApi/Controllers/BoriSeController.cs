namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BoriSeController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiBorbe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get() {
        (bool isError, var borbe, string? error, int code) = (await DataProvider.VratiSveBorbeAsync());
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(borbe);
    }
    
    [HttpPut]
    [Route("AzurirajBorbu")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Azuriraj([FromBody] BoriSeView bsw) {
        var data = await DataProvider.AzurirajBorbuAsync(bsw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno azurirana borba sa Id = {data.Data}");
    }
    
    [HttpPost]
    [Route("DodajBorbu")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Dodaj([FromBody]BoriSeView bsw) {
        var data = await DataProvider.DodajBorbu(bsw);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno dodata borba sa id = {data.Data}");
    }
    
    [HttpDelete]
    [Route("ObrisiBorbu/{borbaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Obrisi(int borbaId) {
        var data = await DataProvider.ObrisiBorbuAsync(borbaId);
        if (data.IsError)
            return StatusCode(data.StatusCode, data.Error);
        return Ok($"Uspesno obrisana borba");
    }
}