using System;
using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Services.Models
{
    public class ResultDetailsServiceModel : IMapFrom<Result>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

        public int Type { get; set; }

        public int Flags { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
