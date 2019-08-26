using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskListRevisited.Models;

namespace TaskListRevisited.Controllers
{
    public class TaskInfoesController : Controller
    {
        private readonly TaskDBContext _context;

        public TaskInfoesController(TaskDBContext context)
        {
            _context = context;
        }

        // GET: TaskInfoes
        public async Task<IActionResult> Index()
        {
            var taskDBContext = _context.TaskInfo.Include(t => t.User);
            return View(await taskDBContext.ToListAsync());
        }

        // GET: TaskInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TaskNumber == id);
            if (taskInfo == null)
            {
                return NotFound();
            }

            return View(taskInfo);
        }

        // GET: TaskInfoes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.UserInfo, "UserId", "Email");
            return View();
        }

        // POST: TaskInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskNumber,TaskDesc,TaskDueDate,StartDate,TaskStatus,UserId")] TaskInfo taskInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "UserId", "Email", taskInfo.UserId);
            return View(taskInfo);
        }

        // GET: TaskInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo.FindAsync(id);
            if (taskInfo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "UserId", "Email", taskInfo.UserId);
            return View(taskInfo);
        }

        // POST: TaskInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskNumber,TaskDesc,TaskDueDate,StartDate,TaskStatus,UserId")] TaskInfo taskInfo)
        {
            if (id != taskInfo.TaskNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskInfoExists(taskInfo.TaskNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.UserInfo, "UserId", "Email", taskInfo.UserId);
            return View(taskInfo);
        }

        // GET: TaskInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInfo = await _context.TaskInfo
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TaskNumber == id);
            if (taskInfo == null)
            {
                return NotFound();
            }

            return View(taskInfo);
        }

        // POST: TaskInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskInfo = await _context.TaskInfo.FindAsync(id);
            _context.TaskInfo.Remove(taskInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskInfoExists(int id)
        {
            return _context.TaskInfo.Any(e => e.TaskNumber == id);
        }
    }
}
