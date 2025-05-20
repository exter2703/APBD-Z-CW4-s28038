using APBD_Z_CW4_s28038.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z_CW4_s28038.Controllers;

[ApiController]
[Route ("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var patient = await service.GetPatientDetails(id);
            if (patient == null)
                throw new Exception($"Patient with id {id} not found.");
            return Ok(patient);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}