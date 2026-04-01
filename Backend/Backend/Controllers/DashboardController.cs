using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserService _service;

        public DashboardController(AppDbContext context)
        {
            _context = context;
            _service = new UserService(context);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var totalUsers = _context.Users.Count();

            return Ok(new
            {
                totalUsers
            });
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Archivo vacío");

            ExcelPackage.License.SetNonCommercialPersonal("Andres Idrovo");

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];

            var rowCount = sheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var user = new User
                {
                    Name = sheet.Cells[row, 1].Text,
                    LastName = sheet.Cells[row, 2].Text,
                    Identification = sheet.Cells[row, 3].Text,
                    Username = sheet.Cells[row, 4].Text,
                    Password = sheet.Cells[row, 5].Text
                };

                _context.Users.Add(user);
            }

            _context.SaveChanges();

            return Ok(new
            {
                message = "Usuarios cargados",
                success = true
            });
        }
    }
}
