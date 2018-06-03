using System.Collections.Generic;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Interfaces
{
    public interface IResultService
    {
        Task<ResultDetailsServiceModel> Get(int id);
        Task<List<ResultDetailsServiceModel>> All(int quizId);
        Task<ResultDetailsServiceModel> Create(string text, int? minValue, int? maxValue, int quizId);
        Task<ResultDetailsServiceModel> Update(int id, string text, int? minValue, int? maxValue, int quizId);
        Task<bool> Exists(int id);
        Task Delete(int id);
    }
}
