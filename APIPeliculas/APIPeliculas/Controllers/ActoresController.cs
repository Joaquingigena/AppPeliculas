using APIPeliculas.DTOs;
using APIPeliculas.Entidades;
using APIPeliculas.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlmacenadorArchivos almacenador;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context,IMapper mapper,IAlmacenadorArchivos almacenador)
        {
            _context = context;
            _mapper = mapper;
            this.almacenador = almacenador;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto is not null) 
            {
                var url = await almacenador.Almacenar(contenedor, actorCreacionDTO.Foto);
                actor.Foto = url;
            }

            _context.Add(actor);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerActorPorId",new { id = actor.Id },actor);
        }

        [HttpGet("{id:int}",Name = "ObtenerActorPorId")]
        public async Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }


        //Modificar con la foto
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put (int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = await _context.Actores.FirstOrDefaultAsync( a => a.Id == id);

            if(actor == null)
            {
                return NotFound();
            }

            actor = _mapper.Map(actorCreacionDTO, actor);

            if( actorCreacionDTO.Foto  is not null)
            {
                actor.Foto= await almacenador.Editar(actor.Foto,contenedor,actorCreacionDTO.Foto);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registrosBorrados= await _context.Actores.Where( a => a.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
