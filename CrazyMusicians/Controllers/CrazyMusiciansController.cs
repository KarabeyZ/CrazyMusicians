using CrazyMusicians.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        static List<CrazyMusiciansEntity> _crazyMusicians = new List<CrazyMusiciansEntity>()
        {
             new CrazyMusiciansEntity { Id = 1, Name = "Ahmet Çalgı", Job = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
    new CrazyMusiciansEntity { Id = 2, Name = "Zeynep Melodi", Job = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılır ama çok popüler" },
    new CrazyMusiciansEntity { Id = 3, Name = "Cemil Akor", Job = "Çılgın Akorist", FunFact = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
    new CrazyMusiciansEntity { Id = 4, Name = "Fatma Nota", Job = "Sürpriz Nota Üreticisi", FunFact = "Nota üretirken sürekli sürprizler hazırlar" },
    new CrazyMusiciansEntity { Id = 5, Name = "Hasan Ritim", Job = "Ritim Canavarı", FunFact = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
    new CrazyMusiciansEntity { Id = 6, Name = "Elif Armoni", Job = "Armoni Ustası", FunFact = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
    new CrazyMusiciansEntity { Id = 7, Name = "Ali Perde", Job = "Perde Uygulayıcı", FunFact = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
    new CrazyMusiciansEntity { Id = 8, Name = "Ayşe Rezonans", Job = "Rezonans Uzmanı", FunFact = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
    new CrazyMusiciansEntity { Id = 9, Name = "Murat Ton", Job = "Tonlama Meraklısı", FunFact = "Tonlamalardaki farklılıklar bazen komik, ama oldukça ilginç" },
    new CrazyMusiciansEntity { Id = 10, Name = "Selin Akor", Job = "Akor Sihirbazı", FunFact = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        };


        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(_crazyMusicians);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var crazyMusicians = _crazyMusicians.FirstOrDefault(x => x.Id == id);
            if(crazyMusicians == null)
                return NotFound();
            return Ok(crazyMusicians);
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            var crazyMusicians = _crazyMusicians.Where(x => x.Name.Contains(keyword) || x.FunFact.Contains(keyword) || x.Job.Contains(keyword)).ToList();
            if (crazyMusicians.Count==0)
                return NotFound();
            return Ok(crazyMusicians);

        }
        [HttpPost("add")]
        public IActionResult AddCrazyMusician([FromBody] CrazyMusiciansEntity request)
        {
            var id = _crazyMusicians.Max(x=> x.Id) + 1;
            request.Id = id;
            _crazyMusicians.Add(request);
            return CreatedAtAction(nameof(Get), new {id = request.Id}, request);
        }
        [HttpPatch("{id}")]
        public IActionResult ToggleCrazyMusician(int id)
        {
            var crazyMusician = _crazyMusicians.FirstOrDefault(x => x.Id == id);
            if (crazyMusician == null)
                return NotFound();
            crazyMusician.IsDone = !crazyMusician.IsDone;
            return Ok(crazyMusician);
        }
        [HttpPut("{id}")]
        public IActionResult Put (int id, [FromBody] CrazyMusiciansEntity request)
        {
            if(request is null || id != request.Id)
                return BadRequest();
            var crazyMusician = _crazyMusicians.FirstOrDefault(x=> x.Id == id);
            if (crazyMusician == null)
                return NotFound();
            crazyMusician.Name = request.Name;
            crazyMusician.IsDone = request.IsDone;
            crazyMusician.FunFact = request.FunFact;
            return Ok(crazyMusician);

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var crazyMusician = _crazyMusicians.FirstOrDefault(x => x.Id==id);
            if (crazyMusician == null)
                return NotFound();
            crazyMusician.IsDeleted = true;
            return Ok(crazyMusician);
        }

    }
}
