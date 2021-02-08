using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using diodocs.Models;
using diodocs.Services;
using System.Threading;

namespace diodocs.Controllers
{
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ILogger<ConvertController> _logger;
        private readonly IConverter _converter;

        public ConvertController(ILogger<ConvertController> logger, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
        }

        [HttpPost]
        [Route("api/v1/[controller]/pdf")]
        public ConvertResponse Post(ConvertRequest[] req)
        {
            ConvertResponse res = new ConvertResponse();
            res.OutputData = _converter.ConvertExcelToPDF(req);
            return res;
        }
    }
}