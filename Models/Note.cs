using System;
using Microsoft.EntityFrameworkCore;

namespace ModernNotes.Models
{
    public class Note {
        public int Id {get; set; }
        public string Title {get; set; }
        public string Content {get;set;}
        public string Author {get;set;}
    }

    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options){ 
        }

        public DbSet<Note> Notes { get; set; }

    }
}