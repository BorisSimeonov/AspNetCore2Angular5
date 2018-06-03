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

namespace TestMakerFreeWebApp.Services.Implementations.ResultService
{
    public class ResultService : BaseService, IResultService
    {
        private IQuizService QuizService { get; }

        public ResultService(ApplicationDbContext dbContext, IMapper mapper, IQuizService quizService)
            : base(dbContext, mapper)
        {
            QuizService = quizService;
        }

        public async Task<ResultDetailsServiceModel> Get(int id)
            => await DbContext.Answers
                .Where(r => r.Id == id)
                .ProjectTo<ResultDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<ResultDetailsServiceModel> Create(string text, int? minValue, int? maxValue, int quizId)
        {
            var now = DateTime.Now;

            Result newResult = null;
            if (await QuizService.Exists(quizId))
            {
                newResult = new Result
                {
                    Text = text,
                    CreatedDate = now,
                    MinValue = minValue,
                    MaxValue = maxValue,
                    LastModifiedDate = now,
                    QuizId = quizId
                };

                await DbContext.Results.AddAsync(newResult);
                await DbContext.SaveChangesAsync();
            }

            return Mapper.Map<Result, ResultDetailsServiceModel>(newResult);

        }

        public async Task<ResultDetailsServiceModel> Update(int id, string text, int? minValue, int? maxValue, int quizId)
        {
            var result = await DbContext.Results.FirstAsync(r => r.Id == id);
            result.Text = text;
            result.QuizId = quizId;
            result.MinValue = minValue;
            result.MaxValue = maxValue;
            result.LastModifiedDate = DateTime.Now;

            await DbContext.SaveChangesAsync();

            return Mapper.Map<Result, ResultDetailsServiceModel>(result);
        }

        public async Task Delete(int id)
        {
            var result = await DbContext.Results
                .FirstOrDefaultAsync(r => r.Id == id);

            if (result != null)
            {
                DbContext.Results.Remove(result);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ResultDetailsServiceModel>> All(int quizId)
            => await DbContext.Results
                .Where(a => a.QuizId == quizId)
                .OrderBy(a => a.CreatedDate)
                .ProjectTo<ResultDetailsServiceModel>()
                .ToListAsync();

        public async Task<bool> Exists(int id)
            => await DbContext.Results.AnyAsync(r => r.Id == id);
    }
}
