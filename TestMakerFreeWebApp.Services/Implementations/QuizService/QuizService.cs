using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Domain.DomainModels;
using TestMakerFreeWebApp.Services.Interfaces;
using TestMakerFreeWebApp.Services.Models;

namespace TestMakerFreeWebApp.Services.Implementations.QuizService
{
    public class QuizService : IQuizService
    {
        private ApplicationDbContext DbContext { get; }

        private IMapper Mapper { get; }

        public QuizService(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
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

        public async Task<bool> QuizExists(int id)
            => await DbContext.Quizzes.AnyAsync(q => q.Id == id);

        public async Task<QuizDetailsServiceModel> Create(
            string title, 
            string description, 
            string text, 
            string notes)
        {
            var userId = DbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            var now = DateTime.Now;

            var newQuiz = new Quiz
            {
                Title = title,
                Description = description,
                Text = text,
                Notes = notes,
                CreatedDate = now,
                LastModifiedDate = now,
                UserId = userId
            };

            await DbContext.SaveChangesAsync();
            return Mapper.Map<Quiz, QuizDetailsServiceModel>(newQuiz);
        }

        public async Task<QuizDetailsServiceModel> Update(
            int id, 
            string title, 
            string description, 
            string text, 
            string notes)
        {
            var quiz = await DbContext.Quizzes.FirstAsync(q => q.Id == id);

            quiz.Title = title;
            quiz.Description = description;
            quiz.Text = text;
            quiz.Notes = notes;
            quiz.LastModifiedDate = DateTime.Now;

            await DbContext.SaveChangesAsync();

            return Mapper.Map<Quiz, QuizDetailsServiceModel>(quiz);
        }

        public async Task Delete(int id)
        {
            var quiz = await DbContext.Quizzes
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                DbContext.Quizzes.Remove(quiz);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
