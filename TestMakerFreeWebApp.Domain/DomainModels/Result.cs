using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestMakerFreeWebApp.Domain.DomainModels
{
    public class Result
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public virtual Quiz Quiz { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public string Text { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

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
