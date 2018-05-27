using System;
using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Services.Models
{
    public class AnswerDetailsServiceModel : IMapFrom<Answer>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }

        public int Type { get; set; }

        public int Flags { get; set; }

        public int Value { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
