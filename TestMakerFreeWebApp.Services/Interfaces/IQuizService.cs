using System.Collections.Generic;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IQuizService
    {
        Task<QuizDetailsServiceModel> Get(int id);
        Task<List<QuizDetailsServiceModel>> GetLatest(int num);
        Task<List<QuizDetailsServiceModel>> GetByTitle(int num);
        Task<List<QuizDetailsServiceModel>> GetRandom(int num);
    }
}
