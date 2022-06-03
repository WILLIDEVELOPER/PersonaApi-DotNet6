using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilPersonalController : ControllerBase
    {
        private readonly firstDBContext context;

        //Models.firstDBContext db = new Models.firstDBContext();


        //[HttpGet]
        //public IEnumerable<Models.Persona> Get()
        //{

        //    IEnumerable<Models.Persona> personas = db.Personas.ToList();
        //    return personas;

        //}

        //[HttpGet("obterneruser/{id}")]
        //public IEnumerable<Models.Persona> Getuser(int id)
        //{
        //    IEnumerable<Models.Persona> personas = db.Personas.ToList();

        //    var person = personas.Where(x => x.Id == id);

        //    if(person == null)
        //    {
        //        return (IEnumerable<Models.Persona>)BadRequest("no existe");
        //    }

        //    return person;
        //}

        //[HttpPost("AgregarPersona")]
        //public List<Models.Persona> AddPerson(Models.Persona personita)
        //{
        //    List<Models.Persona> personas = db.Personas.ToList();
        //    personas.Add(personita);
        //    return personas;
        //}

        public PerfilPersonalController(firstDBContext context)
        {
            this.context = context;
        }

        //Mostrar toda la info
        [HttpGet("MostrarInfo")]
        public async Task<ActionResult<List<Persona>>> Get()
        {
            return Ok(await this.context.Personas.ToListAsync());
        }

        //Mostrar el registro dependiendo del id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Persona>>> GetById(int id)
        {
            var person = await this.context.Personas.FindAsync(id);
            if (person == null)
            {
                return BadRequest("Person not found");
            }
            return Ok(person);
        }

        //Agregar info, Metodo Post(Create)
        [HttpPost("AgregarPersona")]
        public async Task<ActionResult<List<Persona>>> AddPerson(Persona personita)
        {
            this.context.Personas.Add(personita);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Personas.ToListAsync());
        }

        //Actualizar datos(Update)
        [HttpPut("UpdateData")]
        public async Task<ActionResult<List<Persona>>> UpdatePerson(Persona suggest)
        {
            var person = await this.context.Personas.FindAsync(suggest.Id);
            if (person == null)
            {
                return BadRequest("Person not found");
            }

            person.Id = suggest.Id;
            person.Name = suggest.Name;
            person.Descripcion = suggest.Descripcion;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.Personas.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Persona>>> DeletePerson(int id)
        {
            var person = await this.context.Personas.FindAsync(id);
            if (person == null)
            {
                return BadRequest("Person not found");
            }
            this.context.Personas.Remove(person);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Personas.ToListAsync());
        }
    }
}
