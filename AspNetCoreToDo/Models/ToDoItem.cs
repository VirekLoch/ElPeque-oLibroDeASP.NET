using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreToDo.Models
{
    public class ToDoItem
    {
        public Guid Id { get; set; }

        public bool IsDone { get; set; }
        public string? UserId { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }

        
    }
}