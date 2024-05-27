namespace MmorpgApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrudjeController : ControllerBase {
    [HttpGet]
    [Route("PreuzmiInventory/{igracId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int igracId) {
        (bool isError, var inventory, string? error, int code) = (await DataProvider.VratiIgracevInventoryAsync(igracId));
        if (isError) {
            return StatusCode(code, error);
        }
        return Ok(inventory);
    }
}