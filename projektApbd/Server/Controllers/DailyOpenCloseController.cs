using Microsoft.AspNetCore.Mvc;
using projektApbd.Server.Services;

namespace projektApbd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyOpenCloseController : Controller
    {
        private readonly IDailyOpenCloseService _service;

        public DailyOpenCloseController(IDailyOpenCloseService service)
        {
            _service = service;
        }
    }
}
