using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using DM = ServiceLayer.Contracts.DomainModels;
using SeedWebApi.ViewModels;
using System.Threading.Tasks;

namespace SeedWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondsController : BaseController
    {
        private IDiamondService _diamondService;
        private readonly ILogger<DiamondsController> _logger;

        public DiamondsController(ILogger<DiamondsController> logger, IHostingEnvironment hostingEnvironment, IDiamondService diamondService): base(hostingEnvironment)
        {
            if (diamondService == null) throw new ArgumentException("Invalid argument DiamondService");
            if (logger == null) throw new ArgumentException("Invalid argument Logger");

            _diamondService = diamondService;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug($"DEBUG Message :=> {_hostingEnvironment.EnvironmentName}");
            _logger.LogInformation($"INFORMATION Message :=> {_hostingEnvironment.EnvironmentName}");

            return await ExecuteAsync(() => _diamondService.GetDiamonds(), "");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] ViewModelDiamond diamond)
        {
            var newDiamond = new DM.Diamond
            {
                Name = diamond.Name,
                Country = diamond.Country
            };

            await _diamondService.AddDiamond(newDiamond);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
