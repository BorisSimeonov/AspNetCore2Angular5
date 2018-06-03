using System.Collections.Generic;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<AnswerDetailsServiceModel> Get(int id);
        Task<List<AnswerDetailsServiceModel>> All(int questionId);
        Task<AnswerDetailsServiceModel> Create(string text, int questionId, int value);
        Task<AnswerDetailsServiceModel> Update(int id, string text, int questionId, int value);
        Task<bool> Exists(int id);
        Task Delete(int id);
    }
}
