using Besho.BLL.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace Besho.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet ("PdfProduct")]
        public IResult GetProductReport()
        {

            var document = _reportService.CreateDocument();
            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "BeshoProducts.pdf");


        }
    }
}
