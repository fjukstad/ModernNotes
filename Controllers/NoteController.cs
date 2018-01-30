using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ModernNotes.Controllers
{
    public class NoteController : Controller
    {
        // GET /api/notes
        [HttpGet("/api/notes")]
        public IEnumerable<Note> GetAll()
        {
            return new Note[] {};
        }

        // GET /api/notes/5
        [HttpGet("/api/notes/{id}")]
        public string Get(int id)
        {
            return "note id 1";
       }

       [Route("/notes")]  
       public IActionResult Notes(){
           ViewData["Note"] = GetAll();
            return View();
        }

    }
}
