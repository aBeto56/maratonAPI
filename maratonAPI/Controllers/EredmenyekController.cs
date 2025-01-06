using maratonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static maratonAPI.Models.Dtos;

namespace maraton.Controllers
{
    [Route("api/Eredmenyek")]
    [ApiController]
    public class EredmenyekController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Eredmenyek> Post(CreateEredmenyDto createEredmenyDto)
        {
            using (var context = new MaratonContext())
            {
                var Eredmeny = new Eredmenyek()
                {
                    Futo = createEredmenyDto.futo,
                    Kor = createEredmenyDto.kor,
                    Ido = createEredmenyDto.ido,
                };
                if (createEredmenyDto.futo != null)
                {
                    context.Add(Eredmeny);
                    context.SaveChanges();
                    return StatusCode(201, Eredmeny);
                }
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public ActionResult<Eredmenyek> GetAll()
        {
            using (var context = new MaratonContext())
            {
                return Ok(context.Eredmenyeks.ToList());
            }
        }

        [HttpGet("by/id")]
        public ActionResult<Eredmenyek> GetById(int id)
        {
            using (var context = new MaratonContext())
            {
                var Eredmeny = context.Eredmenyeks.FirstOrDefault(x => x.Futo == id);

                if (Eredmeny != null)
                {
                    return StatusCode(200, Eredmeny);
                }
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<Eredmenyek> Put(int id, UpdateEredmenyDto updateEredmenyDto)
        {
            using (var context = new MaratonContext())
            {
                var existingEredmeny = context.Eredmenyeks.FirstOrDefault(x => x.Futo == id);
                if (existingEredmeny != null)
                {
                    existingEredmeny.Kor = updateEredmenyDto.kor;
                    existingEredmeny.Ido = updateEredmenyDto.ido;
                    context.SaveChanges();
                    return StatusCode(200, existingEredmeny);
                }
                return NotFound();
            }
        }
    }
}