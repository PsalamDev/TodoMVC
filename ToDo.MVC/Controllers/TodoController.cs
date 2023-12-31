﻿using Microsoft.AspNetCore.Mvc;
using ToDo.MVC.Models;

namespace ToDo.MVC.Controllers
{
    public class TodoController : Controller
    {

        private readonly TodoDbContext _dbContext;
        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var todos = _dbContext.Todos.ToList();
            return View(todos);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            var todoId = _dbContext.Todos.Select(x => x.Id).Max() + 1;
            todo.Id = todoId;
            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var todo = _dbContext.Todos.Find(id);
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo todo)
        {
            _dbContext.Todos.Update(todo);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            var todo = _dbContext.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = _dbContext.Todos.Find(id);
            _dbContext.Todos.Remove(todo);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
      

