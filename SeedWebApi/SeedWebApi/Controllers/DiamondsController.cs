using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using DM = ServiceLayer.Contracts.DomainModels;
using VM = SeedWebApi.ViewModels;

namespace SeedWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondsController : ControllerBase
    {
        private IDiamondService _diamondService;
        private IHostingEnvironment _hostingEnvironment;

        private readonly ILogger<DiamondsController> _logger;

        public DiamondsController(ILogger<DiamondsController> logger, IHostingEnvironment hostingEnvironment, IDiamondService diamondService)
        {
            if (diamondService == null) throw new ArgumentException("Invalid argument DiamondService");
            if (logger == null) throw new ArgumentException("Invalid argument Logger");
            if (hostingEnvironment == null) throw new ArgumentException("Invalid argument HostingEnvironment");

            _diamondService = diamondService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<VM.Diamond>> Get()
        {
            _logger.LogDebug($"DEBUG Message :=> {_hostingEnvironment.EnvironmentName}");
            _logger.LogInformation($"INFORMATION Message :=> {_hostingEnvironment.EnvironmentName}");
            return _diamondService.GetDiamonds().Select(x => new VM.Diamond { Name = x.Name, Country = x.Country }).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] VM.Diamond diamond)
        {
            var newDiamond = new DM.Diamond
            {
                Name = diamond.Name,
                Country = diamond.Country
            };

            _diamondService.AddDiamond(newDiamond);
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
