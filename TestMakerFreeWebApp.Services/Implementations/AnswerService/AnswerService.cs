﻿using AutoMapper;
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

namespace TestMakerFreeWebApp.Services.Implementations.AnswerService
{
    public class AnswerService : BaseService, IAnswerService
    {
        private IQuestionService QuestionService { get; }

        public AnswerService(ApplicationDbContext dbContext, IMapper mapper, IQuestionService questionService)
            : base(dbContext, mapper)
        {
            QuestionService = questionService;
        }

        public async Task<List<AnswerDetailsServiceModel>> All(int questionId)
            => await DbContext.Answers
                .Where(a => a.QuestionId == questionId)
                .OrderBy(a => a.CreatedDate)
                .ProjectTo<AnswerDetailsServiceModel>()
                .ToListAsync();

        public async Task<AnswerDetailsServiceModel> Create(string text, int questionId, int value)
        {
            var now = DateTime.Now;

            Answer newAnswer = null;
            if (await QuestionService.Exists(questionId))
            {
                newAnswer = new Answer
                {
                    Text = text,
                    Value = value,
                    CreatedDate = now,
                    LastModifiedDate = now,
                    QuestionId = questionId
                };

                await DbContext.Answers.AddAsync(newAnswer);
                await DbContext.SaveChangesAsync();
            }

            return Mapper.Map<Answer, AnswerDetailsServiceModel>(newAnswer);
        }

        public async Task Delete(int id)
        {
            var answer = await DbContext.Answers
                .FirstOrDefaultAsync(a => a.Id == id);

            if (answer != null)
            {
                DbContext.Answers.Remove(answer);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<AnswerDetailsServiceModel> Get(int id)
            => await DbContext.Answers
                .Where(a => a.Id == id)
                .ProjectTo<AnswerDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> Exists(int id)
            => await DbContext.Answers.AnyAsync(a => a.Id == id);

        public async Task<AnswerDetailsServiceModel> Update(int id, string text, int questionId, int value)
        {
            var answer = await DbContext.Answers.FirstAsync(a => a.Id == id);
            answer.Text = text;
            answer.QuestionId = questionId;
            answer.Value = value;
            answer.LastModifiedDate = DateTime.Now;

            await DbContext.SaveChangesAsync();

            return Mapper.Map<Answer, AnswerDetailsServiceModel>(answer);
        }
    }
}
