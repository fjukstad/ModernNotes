using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace ModernNotes.Models
{
    public class Note {
        [Required]
        public int Id {get; set; }

        [Required]
        public string Title {get; set; }

        [Required]
        public string Content {get;set;}
    }

    public class NoteContext : DbContext {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options){ 
        }
        public DbSet<Note> Notes { get; set; }
    }
}