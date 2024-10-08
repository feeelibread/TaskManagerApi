using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Context;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController(TaskContext context) : ControllerBase
    {
        private readonly TaskContext _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(TaskManager task)
        {
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(FindById), new { id = task.Id }, task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var taskFind = await _context.Tasks.FindAsync(id);
            if (taskFind == null)
            {
                return NotFound();
            }
            return Ok(taskFind);
        }

        [HttpGet("AllTasks")]
        public async Task<IActionResult> FindAll()
        {
            var taskList = await _context.Tasks.ToListAsync();
            return Ok(taskList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskManager task)
        {
            var taskDb = await _context.Tasks.FindAsync(id);
            if (taskDb == null)
            {
                return NotFound();
            }
            taskDb.TaskName = task.TaskName;
            taskDb.TaskDate = task.TaskDate;
            taskDb.TaskStatus = task.TaskStatus;
            _context.Tasks.Update(taskDb);
            await _context.SaveChangesAsync();
            return Ok(taskDb);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}