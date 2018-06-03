using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Web.ViewModels
{
    public class QuestionViewModel : IMapFrom<QuestionDetailsServiceModel>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Text { get; set; }
    }
}
