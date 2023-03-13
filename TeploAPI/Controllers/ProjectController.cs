using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TeploAPI.Data;
using TeploAPI.Migrations;
using TeploAPI.Models;

namespace TeploAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private TeploDBContext _context;
        public ProjectController(TeploDBContext context)
        {
            _context = context;
        }

        // TODO: Refactoring.
        [HttpPost]
        public async Task<IActionResult> PostAsync(int? inputDataId)
        {
            var basePeriodFurnaceData = await _context.Furnaces.FirstOrDefaultAsync(d => d.Id == inputDataId);

            // Из базы
            var cokeCoefficients = await _context.Сoefficients.FirstOrDefaultAsync(i => i.Id == 1);
            var furnanceCapacityCoefficients = await _context.Сoefficients.FirstOrDefaultAsync(i => i.Id == 2);

            Reference reference = new Reference { CokeCunsumptionCoefficents = cokeCoefficients, FurnanceCapacityCoefficents = furnanceCapacityCoefficients };

            //ProjectCalculateModel projectResult = new ProjectCalculateModel(inputData, projectView.Project, reference);

            //inputData.BlastTemperature = projectResult.Project.BlastTemperature;
            //inputData.BlastHumidity = projectResult.Project.BlastHumidity;
            //inputData.OxygenContentInBlast = projectResult.Project.OxygenContentInBlast;
            //inputData.ColoshGasPressure = projectResult.Project.ColoshGasPressure;
            //inputData.NaturalGasConsumption = projectResult.Project.NaturalGasConsumption;
            //inputData.Chugun_SI = projectResult.Project.Chugun_SI;
            //inputData.Chugun_MN = projectResult.Project.Chugun_MN;
            //inputData.Chugun_P = projectResult.Project.Chugun_P;
            //inputData.Chugun_S = projectResult.Project.Chugun_S;
            //inputData.AshContentInCoke = projectResult.Project.AshContentInCoke;
            //inputData.SulfurContentInCoke = projectResult.Project.SulfurContentInCoke;
            return Ok("test");
        }
    }
}
