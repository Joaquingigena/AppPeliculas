using APIPeliculas.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPeliculas.Controllers
{
    public class CustonBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustonBaseController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<IActionResult<TDTO>> Get<TEntidad,TDTO>(int id)
            where TEntidad : class,IId
        {
            var entidad= await context.Set<TEntidad>().FirstOrDefaultAsync( x => x.Id  == id);

        }
    }
}
