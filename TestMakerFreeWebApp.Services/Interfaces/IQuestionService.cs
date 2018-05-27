using System.Collections.Generic;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<QuestionDetailsServiceModel> Get(int id);
        Task<List<QuestionDetailsServiceModel>> All(int quizId);
        Task<QuestionDetailsServiceModel> Create(string text, string notes, int quizId);
        Task<QuestionDetailsServiceModel> Update(int id, string text, string notes, int quizId);
        Task<bool> Exists(int id);
        Task Delete(int id);
    }
}
