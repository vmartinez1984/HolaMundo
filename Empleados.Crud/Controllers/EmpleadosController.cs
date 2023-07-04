using Empleados.Crud.Contexts;
using Empleados.Crud.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Empleados.Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmpleadosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/<EmpleadosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Empleado> lista;

            lista = await _appDbContext.Empleado.ToListAsync();

            return Ok(lista);
        }

        // GET api/<EmpleadosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Empleado empleado;

            empleado = await _appDbContext.Empleado.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (empleado == null)
                return NotFound();

            return Ok(empleado);
        }

        // POST api/<EmpleadosController>
        [HttpPost]
        public async Task<IActionResult> Post(Empleado empleado)
        {
            await _appDbContext.Empleado.AddAsync(empleado);
            await _appDbContext.SaveChangesAsync();

            return Created($"Empleado/{empleado.Id}", new { Id = empleado.Id });
        }

        // PUT api/<EmpleadosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Empleado empleado)
        {
            Empleado empleadoOriginal;

            empleadoOriginal = await _appDbContext.Empleado.Where(x => x.Id == id).FirstAsync();
            empleadoOriginal.Nombre = empleado.Nombre;
            empleadoOriginal.Correo = empleado.Correo;
            _appDbContext.Empleado.Update(empleadoOriginal);
            await _appDbContext.SaveChangesAsync();

            return Accepted();
        }

        // DELETE api/<EmpleadosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Empleado empleado;

            empleado = await _appDbContext.Empleado.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.Empleado.Remove(empleado);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
