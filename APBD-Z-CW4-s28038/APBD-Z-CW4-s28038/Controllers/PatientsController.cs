using APBD_Z_CW4_s28038.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z_CW4_s28038.Controllers;

[ApiController]
[Route ("[controller]")]
public class PatientsController(IDbService service) :ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        return Ok(await service.GetPatientDetails());
    }
}