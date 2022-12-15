using Microsoft.AspNetCore.Mvc;

namespace Tsi.Erp.FirstApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IRepository<Gouvernorat> _repo;

        public HomeController(IRepository<Gouvernorat> repo)
        {
            _repo = repo;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {


            var Gouvernorat = new Gouvernorat()
            {
                Libelle = "Hello Adzo",
                Code = 190,
                LibelleAr = "Hello Adzo",
                LibelleFr = "Hello Adzo"
            };

            await _repo.AddAsync(Gouvernorat);

            return Ok("Hello World " + Guid.NewGuid().ToString());
        }
    }
}
