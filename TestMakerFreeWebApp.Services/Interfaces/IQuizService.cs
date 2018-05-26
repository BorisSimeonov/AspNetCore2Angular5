using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IQuizService
    {
        Task<QuizDetailsServiceModel> Get(int id);
    }
}
