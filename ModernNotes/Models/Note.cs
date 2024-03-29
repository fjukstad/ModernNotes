using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json; 

namespace ModernNotes.Models
{
    ///<summary>Note model.</summary>
    public class Note {
        
        ///<summary>Note id.</summary>
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int Id {get; set; }

        ///<summary>Note title.</summary>
        [Required]
        [JsonProperty(PropertyName = "title")]
        public string Title {get; set; }

        ///<summary>Note title.</summary>
        [Required]
        [JsonProperty(PropertyName = "content")]
        public string Content {get;set;}
    }

    ///<summary>Note context.</summary>
    public class NoteContext : DbContext {
        
        ///<summary>Note context.</summary>
        public NoteContext(DbContextOptions<NoteContext> options) : base(options){ 
        }

        ///<summary>Note database.</summary>
        public DbSet<Note> Notes { get; set; }
    }
}