using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<QuizDetailsServiceModel>> GetLatest(int num)
            => await DbContext.Quizzes
                .OrderBy(q => q.CreatedDate)
                .Take(num)
                .ProjectTo<QuizDetailsServiceModel>()
                .ToListAsync();

        public async Task<List<QuizDetailsServiceModel>> GetByTitle(int num)
            => await DbContext.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ProjectTo<QuizDetailsServiceModel>()
                .ToListAsync();

        public async Task<List<QuizDetailsServiceModel>> GetRandom(int num)
            => await DbContext.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ProjectTo<QuizDetailsServiceModel>()
                .ToListAsync();
    }
}
