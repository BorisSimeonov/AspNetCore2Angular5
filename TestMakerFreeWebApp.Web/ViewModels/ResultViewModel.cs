using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Web.ViewModels
{
    public class ResultViewModel : IMapFrom<ResultDetailsServiceModel>
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Text { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }
    }
}
