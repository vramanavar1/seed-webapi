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

        // GET api/diamonds
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug($"DEBUG Message :=> {_hostingEnvironment.EnvironmentName}");
            _logger.LogInformation($"INFORMATION Message :=> {_hostingEnvironment.EnvironmentName}");

            return await ExecuteAsync(() => _diamondService.GetDiamonds(), "");
        }

        // GET api/diamonds/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAsync(() => _diamondService.GetDiamond(id), "");
        }

        // POST api/diamonds
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

        // PUT api/diamonds/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ViewModelDiamond diamond)
        {
            var domainDiamond = new DM.Diamond
            {
                Id = id,
                Name = diamond.Name,
                Country = diamond.Country
            };
            await _diamondService.UpdateDiamond(domainDiamond);
        }

        // DELETE api/diamonds/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _diamondService.DeleteDiamond(id);
        }
    }
}
