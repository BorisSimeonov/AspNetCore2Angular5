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

namespace TestMakerFreeWebApp.Services.Implementations.QuestionService
{
    public class QuestionService : BaseService, IQuestionService
    {
        private IQuizService QuizService { get; }

        public QuestionService(ApplicationDbContext dbContext, IMapper mapper, IQuizService quizService)
            : base(dbContext, mapper)
        {
            QuizService = quizService;
        }

        public async Task<QuestionDetailsServiceModel> Get(int id)
            => await DbContext.Questions
                .Where(x => x.Id == id)
                .ProjectTo<QuestionDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<List<QuestionDetailsServiceModel>> All(int quizId)
            => await DbContext.Questions
                .Where(q => q.QuizId == quizId)
                .OrderBy(q => q.CreatedDate)
                .ProjectTo<QuestionDetailsServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await DbContext.Questions.AnyAsync(q => q.Id == id);

        public async Task<QuestionDetailsServiceModel> Create(
            string text,
            int quizId)
        {
            var now = DateTime.Now;

            Question newQuestion = null;
            if (await QuizService.Exists(quizId))
            {
                newQuestion = new Question
                {
                    Text = text,
                    CreatedDate = now,
                    LastModifiedDate = now,
                    QuizId = quizId
                };

                await DbContext.Questions.AddAsync(newQuestion);
                await DbContext.SaveChangesAsync();
            }
            
            return Mapper.Map<Question, QuestionDetailsServiceModel>(newQuestion);
        }

        public async Task<QuestionDetailsServiceModel> Update(
            int id,
            string text,
            int quizId)
        {
            var question = await DbContext.Questions.FirstAsync(q => q.Id == id);
            question.Text = text;
            question.QuizId = quizId;
            question.LastModifiedDate = DateTime.Now;

            await DbContext.SaveChangesAsync();

            return Mapper.Map<Question, QuestionDetailsServiceModel>(question);
        }

        public async Task Delete(int id)
        {
            var question = await DbContext.Questions
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question != null)
            {
                DbContext.Questions.Remove(question);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
