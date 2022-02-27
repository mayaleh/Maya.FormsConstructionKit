using System.Reflection;
using Maya.FormsConstructionKit.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Maya.FormsConstructionKit.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> logger;

        public InfoController(ILogger<InfoController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var a = Assembly.GetExecutingAssembly();
                var v = a.GetName().Version;
                return this.Ok(new AppInfo
                {
                    Version = v,
                    Name = a.GetName().Name,
                    Content = "1N73LL1G3NC3 15 7H3 4B1L17Y 70 4D4P7 70 CH4NG3. - 573PH3N H4WK1NG",
                    Author = "Salim Mayaleh"
                });
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}