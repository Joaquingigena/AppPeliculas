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
        public async Task<ActionResult<List<Genero>>> get(int id)
        {
            return await _context.Generos.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            _context.Add(genero);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerGeneroPorId", new {id=genero.Id},genero);
        }
    }
}
