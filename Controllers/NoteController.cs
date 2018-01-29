using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorLight;

namespace ModernNotes.Controllers
{
    [Route("/api/notes")]
    public class NoteController : Controller
    {
        // GET /api/notes
        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return new Note[] {};
        }

        // GET /api/notes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "note id 1";
       }

    }
}
