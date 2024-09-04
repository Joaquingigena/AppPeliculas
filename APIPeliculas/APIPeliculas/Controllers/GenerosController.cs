using APIPeliculas.DTOs;
using APIPeliculas.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenerosController(ApplicationDbContext context,IMapper mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generos = await _context.Generos.ToListAsync();

            var generosDTO= _mapper.Map<List<GeneroDTO>>(generos);

            return generosDTO;
        }

        [HttpGet("{id:int}",Name ="ObtenerGeneroPorId")]
        public async Task<ActionResult<GeneroDTO>> get(int id)
        {
            var genero= await _context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            var generoDTO= _mapper.Map<GeneroDTO>(genero);

            if (generoDTO == null)
            {
                return NotFound();
            }

            return generoDTO;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            _context.Add(genero);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerGeneroPorId", new {id=genero.Id},genero);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]GeneroCreacionDTO generoCreacionDTO)
        {
            var generoExiste= await _context.Generos.AnyAsync(g => g.Id == id);

            if (!generoExiste)
            {
                return NotFound();
            }

            var genero = _mapper.Map<Genero>(generoCreacionDTO);

            _context.Update(genero);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registrosBorrados= await _context.Generos.Where(g => g.Id==id).ExecuteDeleteAsync();

            if(registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
