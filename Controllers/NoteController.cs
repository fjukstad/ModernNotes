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
            /* 
            if(_context.Notes.Count()==0){
                _context.Notes.Add(new Note{Title="note #1",
                                            Content="This is some content.",
                                            Id=1});
                _context.SaveChanges();
            }
            */
        }

        // GET /api/notes
        [HttpGet("/api/notes")]
        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.ToList(); 
        }

        // GET /api/notes/5
        [HttpGet("/api/notes/{id}", Name = "Get")]
        public Note Get(long id)
        {
            var item = _context.Notes.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return null;
            }
            return item;
       }

       [HttpPost("/api/new")]
        public IActionResult New([FromBody] Note note){
            if(note==null){
                return BadRequest();
            }
            _context.Notes.Add(note);
            _context.SaveChanges();
            return CreatedAtRoute("View", new { id = note.Id}, note);
        }

        [HttpPost("/api/delete/{id}")]
        public IActionResult Delete(int id) { 
            var note = Get(id);
            if(note == null) { 
                Console.WriteLine("damn");
            }
            _context.Notes.Remove(note);
            return new NoContentResult();
        }

        [HttpPut("/api/update/{id}")]
        public IActionResult Update (int id, [FromBody] Note updateNote) { 

            Console.WriteLine("shitbucket");

            if(updateNote == null || updateNote.Id != id) { 
                Console.WriteLine(updateNote.Id);
                Console.WriteLine(id);
                return BadRequest();
            }
            var oldNote = Get(id); 
            if (oldNote == null) {
                return NotFound();
            }

            oldNote.Title = updateNote.Title;
            oldNote.Content = updateNote.Content; 
            
            _context.Notes.Update(oldNote);
            _context.SaveChanges();

            return new NoContentResult();

        }

       [Route("/notes")]  
       public IActionResult Notes(){
           ViewData["Notes"] = GetAll();
            return View();
        }

       [Route("/note/{id}", Name="View")]  
       public IActionResult Note(int id){
           ViewData["Note"] = Get(id);
           var note = Get(id);
           if(note == null) { 
               return NotFound();
           }
            return View();
        }


       [Route("/edit/{id}", Name="Edit")]  
       public IActionResult Edit(int id){
           var note = Get(id);
           if(note == null) { 
               return NotFound();
           }
           ViewData["Note"] = note;
            return View();
        }}
}
