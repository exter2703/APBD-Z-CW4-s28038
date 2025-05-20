using APBD_Z_CW4_s28038.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z_CW4_s28038.Controllers;

[ApiController]
[Route ("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatients(int id)
    {
        var patients = await service.GetPatientDetails (id);
        if (patients == null)
            return NotFound();
        return Ok(await service.GetPatientDetails(id));
    }
}