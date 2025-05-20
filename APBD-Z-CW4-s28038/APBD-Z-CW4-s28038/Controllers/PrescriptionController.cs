using APBD_Z_CW4_s28038.DTOs;
using APBD_Z_CW4_s28038.Models;
using APBD_Z_CW4_s28038.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Z_CW4_s28038.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController (IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionAddDTO prescription)
    {
        try
        {
            var result = await service.AddPrescription(prescription);
            if (result.StartsWith("Recepta dla"))
            {
                return Ok(result);
            }

            throw new Exception("Bad request.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}