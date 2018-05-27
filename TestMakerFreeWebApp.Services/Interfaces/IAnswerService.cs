using System.Collections.Generic;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<AnswerDetailsServiceModel> Get(int id);
        Task<List<AnswerDetailsServiceModel>> All(int questionId);
        Task<AnswerDetailsServiceModel> Create(string text, string notes, int questionId);
        Task<AnswerDetailsServiceModel> Update(int id, string text, string notes, int questionId);
        Task<bool> AnswerExists(int id);
        Task Delete(int id);
    }
}
