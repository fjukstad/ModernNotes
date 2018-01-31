using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModernNotes.Models;

namespace ModernNotes.Controllers
{
    /// <summary>Note controller.</summary>
    public class NoteController : Controller
    {
        private readonly NoteContext _context; 

        /// <summary>Specify DB context.</summary>
        public NoteController(NoteContext context){
            _context = context; 
        }

        /// <summary>Retrieve all notes.</summary>
        /// <returns>A list of all notes.</returns>
        [ProducesResponseType(typeof(IEnumerable<Note>), 200)]
        [HttpGet("/api/notes")]
        public IEnumerable<Note> GetAll()
        {
            return _context.Notes.ToList(); 
        }

        /// <summary>Retrieve a specific note.</summary>
        /// <returns>The note with the given id.</returns>
        /// <response code="200">If the note is found.</response>            
        /// <response code="400">If the note is not found.</response>     
        [ProducesResponseType(typeof(Note), 200)]
        [ProducesResponseType(typeof(void), 400)]       
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

        /// <summary>Create a new note.</summary>
        /// <returns>The newly created note.</returns>
        /// <response code="201">If the note is created successfully.</response>            
        /// <response code="400">If the note is null.</response>     
        [ProducesResponseType(typeof(Note), 201)]
        [ProducesResponseType(typeof(void), 400)]       
        [HttpPost("/api/new")]
        public IActionResult New([FromBody] Note note){
            if(note==null){
                return BadRequest();
            }
            _context.Notes.Add(note);
            _context.SaveChanges();
            return CreatedAtRoute("View", new { id = note.Id}, note);
        }

        /// <summary>Delete a note.</summary>
        /// <returns></returns>
        /// <response code="204">If the note is deleted successfully.</response>            
        /// <response code="400">If the note is not found.</response>     
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]  
        [HttpPost("/api/delete/{id}")]
        public IActionResult Delete(int id) { 
            var note = Get(id);
            if(note == null) { 
                return BadRequest();
            }
            _context.Notes.Remove(note);
            _context.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>Update a note.</summary>
        /// <returns></returns>
        /// <response code="204">If the note is updated successfully.</response>            
        /// <response code="400">If the note is null or the id match the id of the note.</response>     
        /// <response code="404">If the note is not found.</response>     
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]  
        [ProducesResponseType(typeof(void), 404)]  
        [HttpPut("/api/update/{id}")]
        public IActionResult Update (int id, [FromBody] Note updateNote) { 
            if(updateNote == null || updateNote.Id != id) { 
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


        ///<summary>All notes view controller.</summary> 
       [ApiExplorerSettings(IgnoreApi = true)]
       [HttpGet]
       [Route("/notes")]  
       public IActionResult Notes(){
           ViewData["Notes"] = GetAll();
            return View();
        }

        ///<summary>Note view controller.</summary> 
       [ApiExplorerSettings(IgnoreApi = true)]
       [HttpGet]
       [Route("/note/{id}", Name="View")]  
       public IActionResult Note(int id){
           ViewData["Note"] = Get(id);
           var note = Get(id);
           if(note == null) { 
               return NotFound();
           }
            return View();
        }

        ///<summary>Edit note view.</summary> 
       [ApiExplorerSettings(IgnoreApi = true)]
       [HttpGet]
       [Route("/edit/{id}", Name="Edit")]  
       public IActionResult Edit(int id){
           var note = Get(id);
           if(note == null) { 
               return NotFound();
           }
           ViewData["Note"] = note;
            return View();
        }
    }
}
