using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodosController(AppDbContext db) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Todo>>> Get()
				=> await db.Todos.AsNoTracking().ToListAsync();

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Todo>> GetOne(int id)
				=> await db.Todos.FindAsync(id) is { } t ? t : NotFound();

		[HttpPost]
		public async Task<ActionResult<Todo>> Create(Todo todo)
		{
			db.Todos.Add(todo);
			await db.SaveChangesAsync();
			return CreatedAtAction(nameof(GetOne), new { id = todo.Id }, todo);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, Todo todo)
		{
			if (id != todo.Id) return BadRequest();
			db.Entry(todo).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var t = await db.Todos.FindAsync(id);
			if (t is null) return NotFound();
			db.Todos.Remove(t);
			await db.SaveChangesAsync();
			return NoContent();
		}
	}
}
