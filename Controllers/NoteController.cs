using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModernNotes.Models;

namespace ModernNotes.Controllers
{
    public class NoteController : Controller
    {

        private readonly NoteContext _context; 

        public NoteController(NoteContext context){
            _context = context; 
            if(_context.Notes.Count()==0){
                _context.Notes.Add(new Note{Title="note #1",
                                            Content="This is some content.",
                                            Id=1});
                _context.SaveChanges();
            }
        }

        // GET /api/notes
        [HttpGet("/api/notes")]
        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.ToList(); 
        }

        // GET /api/notes/5
        [HttpGet("/api/notes/{id}")]
        public Note Get(long id)
        {
            Console.WriteLine(id);
            var item = _context.Notes.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return null;
            }
            return item;
       }

       [Route("/notes")]  
       public IActionResult Notes(){
           ViewData["Notes"] = GetAll();
            return View();
        }

       [Route("/edit/{id}")]  
       public IActionResult Note(int id){
           var note = Get(id);
           if(note == null) { 
               return NotFound();
           }
           ViewData["Note"] = note;
            return View();
        }}
}
