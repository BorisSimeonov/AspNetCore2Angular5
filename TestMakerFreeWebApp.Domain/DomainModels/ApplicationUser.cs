using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Domain.DomainModels
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            this.Quizzes = new HashSet<Quiz>();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        public virtual HashSet<Quiz> Quizzes { get; set; }
    }
}
