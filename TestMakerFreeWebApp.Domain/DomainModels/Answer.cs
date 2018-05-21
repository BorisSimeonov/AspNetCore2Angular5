using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Domain.DomainModels
{
    public class Answer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public virtual Question Question { get; set; }  

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Value { get; set; }

        public string Notes { get; set; }

        [DefaultValue(0)]
        public int Type { get; set; }

        [DefaultValue(0)]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}
