using System.ComponentModel;
using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Web.ViewModels
{
    public class AnswerViewModel : IMapFrom<AnswerDetailsServiceModel>
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        [DefaultValue(0)]
        public int Value { get; set; }
    }
}
