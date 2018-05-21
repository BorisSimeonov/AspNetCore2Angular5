using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Domain.DomainModels
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string Notes { get; set; }

        [DefaultValue(0)]
        public int Type { get; set; }

        [DefaultValue(0)]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        [Required]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual HashSet<Answer> Answers { get; set; }
    }
}
