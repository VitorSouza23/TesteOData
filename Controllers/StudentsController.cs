using System;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using TesteOData.EF;
using TesteOData.Models;

namespace TesteOData.Controllers
{
    [ODataRoutePrefix("Students")]
    public class StudentsController : ODataController
    {
        private readonly ApiContext _apiContext;
        public StudentsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [ODataRoute]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok(_apiContext.Students.AsQueryable()));
        }
        
        [ODataRoute("{id}")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] Guid id)
        {
            Student student = await _apiContext.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound($"Não foi possível encontrar um objeto do tipo 'Student' através do id {id.ToString()}");
            }
            return Ok(student);
        }

        [ODataRoute]
        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if(student == null)
            {
                return BadRequest("O parâmetro 'student' não pode ser nulo.");
            }

            await _apiContext.Students.AddAsync(student);
            await _apiContext.SaveChangesAsync();
            return Created(student);
        }

        [ODataRoute("{id}")]
        [HttpPut]
        [EnableQuery]
        public async Task<IActionResult> Put([FromODataUri] Guid id, [FromBody] Student student)
        {
            Student currentStudent = await _apiContext.Students.FindAsync(id);
            if(currentStudent == null)
            {
                return NotFound($"Não foi possível encontrar um objeto do tipo 'Student' através do id {id.ToString()}");
            }

            currentStudent.Name = student.Name;
            currentStudent.Score = student.Score;
            _apiContext.Students.Update(currentStudent);
            await _apiContext.SaveChangesAsync();
            return Updated(currentStudent);
        }

        [ODataRoute("{id}")]
        [HttpDelete]
        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] Guid id)
        {
            Student studentToRemove = await _apiContext.Students.FindAsync(id);
            if(studentToRemove == null)
            {
                return NotFound($"Não foi possível encontrar um objeto do tipo 'Student' através do id {id.ToString()}");
            }
            
            _apiContext.Students.Remove(studentToRemove);
            await _apiContext.SaveChangesAsync();

            return Ok(studentToRemove);
        }
    }
}