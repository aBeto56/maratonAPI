using maratonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static maratonAPI.Models.Dtos;

namespace maraton.Controllers
{
    [Route("api/futok")]
    [ApiController]
    public class FutokController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Futok> Post(CreateFutoDto createFutoDto)
        {
            using (var context = new MaratonContext())
            {
                var Futo = new Futok()
                {
                    Fid = createFutoDto.id,
                    Fnev = createFutoDto.Fnev,
                    Szulev = createFutoDto.Szulev,
                    Szulho = createFutoDto.szulho,
                    Csapat = createFutoDto.csapat,
                    Ffi = createFutoDto.ffi,
                };
                if (createFutoDto.Fnev != null)
                {
                    context.Add(Futo);
                    context.SaveChanges();
                    return StatusCode(201, Futo);
                }
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public ActionResult<Futok> GetAll()
        {
            using (var context = new MaratonContext())
            {
                return Ok(context.Futoks.ToList());
            }
        }

        [HttpGet("by/id")]
        public ActionResult<Futok> GetById(int id)
        {
            using (var context = new MaratonContext())
            {
                var Futo = context.Futoks.FirstOrDefault(x => x.Fid == id);

                if (Futo != null)
                {
                    return StatusCode(200, Futo);
                }
                return NotFound();
            }
        }

        [HttpGet("woman")]
        public ActionResult<Futok> GetWoman()
        {
            using (var context = new MaratonContext())
            {
                var futoks = context.Futoks.Where(item => item.Ffi == false)
                                           .OrderBy(item => item.Fnev)
                                           .ToList();

                if (futoks.Any())
                {
                    return Ok(futoks);
                }
                else
                {
                    return NotFound("");
                }
            }
        }

        [HttpPut]
        public ActionResult<Futok> Put(int id, UpdateFutoDto updateFutoDto)
        {
            using (var context = new MaratonContext())
            {
                var existingFuto = context.Futoks.FirstOrDefault(x => x.Fid == id);
                if (existingFuto != null)
                {
                    existingFuto.Csapat = updateFutoDto.csapat;
                    existingFuto.Ffi = updateFutoDto.ffi;
                    context.SaveChanges();
                    return StatusCode(200, existingFuto);
                }
                return NotFound();
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            using (var context = new MaratonContext())
            {
                var delFuto = context.Futoks.FirstOrDefault(x => x.Fid == id);
                if (delFuto.Fid != null)
                {
                    context.Remove(delFuto);
                    context.SaveChanges();
                    return StatusCode(200, "User deleted!");
                }
                return NotFound();
            }
        }
    }
}