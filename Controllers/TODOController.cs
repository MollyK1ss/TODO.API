using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODO.API.Context;
using TODO.API.Controllers.ViewModels;
using TODO.API.Models;
using TODO.API.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TODO.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TODOController : ControllerBase
    {
        private readonly TODO_Context _context;
        private readonly ToDoValidator _validator;

        public TODOController(TODO_Context context, ToDoValidator validator)
        {
            _context = context;
            _validator = validator;
        }


        // GET: api/<TODOController>
        /// <summary>
        /// Возвращает все задачи
        /// </summary>
        /// <returns>Returns TODOModel</returns>
        /// <response code="200">Успешно</response>
        [HttpGet]
        public async Task<IEnumerable<TODOModel>> Get()
        {
            return await _context.TODOTable.AsNoTracking().ToListAsync();
        }

        // GET api/<TODOController>/5
        /// <summary>
        /// Возвращает запись задачи по коду
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns TODOModel</returns>
        /// <response code="200">Успешно</response>
        /// <response code="404">Задача с таким кодом не найдена</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public async Task<ActionResult<TODOModel>> GetById([FromQuery]int id)
        {
            var model = await _context.TODOTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (model == null) 
            {
                return NotFound($"Entity with key: {id} not found");
            }

            return Ok(model);
        }

        // POST api/<TODOController>
        /// <summary>
        /// Создаёт запись задачи
        /// </summary>
        /// <param name="tODOModel"></param>
        /// <returns>Returns TODOModel</returns>
        /// <response code="200">Успешно создан</response>
        [HttpPost]
        public async Task<ActionResult<TODOModel>> AddPost([FromBody] ToDoVm tODOModel)
        {
            var validationResult = _validator.Validate(tODOModel);
            if(!validationResult.IsValid)
            {
                return BadRequest(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }
            var personId = (await _context.Person.FirstOrDefaultAsync(x => x.Surname == tODOModel.Person.Surname))?.Id;

            if (personId == null)
            {
                var person = new PersonModel
                {
                    Surname = tODOModel.Person.Surname,
                    Name = tODOModel.Person.Name,
                    Patronymic = tODOModel.Person.Patronymic,
                    DateBirth = tODOModel.Person.DateBirth,
                    Gender = tODOModel.Person.Gender
                };

                _context.Person.Add(person);

                await _context.SaveChangesAsync();
                personId = person.Id;
            }

            var model = new TODOModel
            {
                PersonId = personId.GetValueOrDefault(),
                TaskName = tODOModel.TaskName,
                Description = tODOModel.Description,
                CreatedTask = DateTime.Now,
                UpdateTask = DateTime.Now,
                EndTask = DateTime.Now.AddDays(7)
            };

            _context.TODOTable.Add(model);
            await _context.SaveChangesAsync();

            return Created("",model.Id);
        }

        // PUT api/<TODOController>/5
        /// <summary>
        /// Обновляет запись задачи по коду
        /// </summary>
        /// <param name="tODOModel"></param>
        /// <param name="Id"></param>
        /// <returns>Returns TODOModel</returns>
        /// <response code="200">Успешно обновлено</response>
        /// <response code="404">Задача с таким кодом не найдена</response>
        [HttpPut()]
        public async Task<ActionResult<TODOModel>> Put(int Id, [FromBody] ToDoVm tODOModel)
        {
            var model = new TODOModel
            {
                Id = Id,
                TaskName = tODOModel.TaskName,
                Description = tODOModel.Description,
                CreatedTask = DateTime.Now,
                UpdateTask = DateTime.Now,
                EndTask = DateTime.Now.AddDays(7)
            };

            if (!(await _context.TODOTable.AnyAsync(x => x.Id == model.Id)))
            {
                return NotFound($"Entity with key: {model.Id} not found");
            }

            _context.TODOTable.Update(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // DELETE api/<TODOController>/5
        /// <summary>
        /// Удаление записи задачи по коду
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Задача с таким кодом не найдена</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.TODOTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
            {
                return NotFound($"Entity with key: {id} not found");
            }

            _context.Remove(model);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
