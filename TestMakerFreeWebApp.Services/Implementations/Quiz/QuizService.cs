using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Services.Interfaces;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Implementations.Quiz
{
    public class QuizService : IQuizService
    {
        private ApplicationDbContext DbContext { get; }

        public QuizService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<QuizDetailsServiceModel> Get(int id)
            => await DbContext.Quizzes
                .Where(x => x.Id == id)
                .ProjectTo<QuizDetailsServiceModel>()
                .FirstOrDefaultAsync();
    }
}
