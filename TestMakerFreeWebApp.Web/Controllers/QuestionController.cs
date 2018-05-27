using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Interfaces;
using TestMakerFreeWebApp.Services.Models;
using TestMakerFreeWebApp.Web.Controllers.AbstractControllers;
using TestMakerFreeWebApp.Web.ViewModels;

namespace TestMakerFreeWebApp.Web.Controllers
{
    public class QuestionController : BaseApiController
    {
        private IQuestionService QuestionService { get; }

        private IQuizService QuizService { get; }

        public QuestionController(IQuestionService questionService, IQuizService quizService, IMapper mapper) 
            : base(mapper)
        {
            QuestionService = questionService;
            QuizService = quizService;
        }

        /// <summary>
        /// Retrieves the Question with the given {id}
        /// </summary>
        /// &lt;param name="id">The ID of an existing Question</param>
        /// <returns>the Question with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var question = await QuestionService.Get(id);

            if (question == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Question ID {0} has not been found", id)
                });
            }

            return new JsonResult(Mapper.Map<QuestionDetailsServiceModel, QuestionViewModel>(question), JsonSettings);
        }

        /// <summary>
        /// Adds a new Question to the Database
        /// </summary>
        /// <param name="m">The QuestionViewModel containing the data to insert</param>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] QuestionViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            QuestionDetailsServiceModel question = await QuestionService.Create(
                model.Text,
                model.Notes,
                model.QuizId);

            return new JsonResult(Mapper.Map<QuestionDetailsServiceModel, QuestionViewModel>(question), JsonSettings);
        }

        /// <summary>
        /// Edit the Question with the given {id}
        /// </summary>
        /// <param name="m">The QuestionViewModel containing the data to update</param>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] QuestionViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            if (!await QuestionService.Exists(model.Id))
            {
                return NotFound(new { Error = String.Format("Question ID {0} has not been found", model.Id) });
            }

            if (!await QuizService.Exists(model.QuizId))
            {
                return NotFound(new { Error = String.Format("Quiz ID {0} has not been found", model.QuizId) });
            }

            QuestionDetailsServiceModel question = await QuestionService.Update(
                model.Id,
                model.Text,
                model.Notes,
                model.QuizId);

            return new JsonResult(Mapper.Map<QuestionDetailsServiceModel, QuestionViewModel>(question), JsonSettings);
        }

        /// <summary>
        /// Deletes the Question with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Question</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await QuestionService.Exists(id))
            {
                return NotFound(new { Error = String.Format("Question ID {0} has not been found", id) });
            }

            await QuestionService.Delete(id);

            return new OkResult();
        }

        [HttpGet("all/{quizId}")]
        public async Task<IActionResult> AllAsync(int quizId)
        {
            var questions = await QuestionService.All(quizId);

            return new JsonResult(questions.Select(q => Mapper.Map<QuestionDetailsServiceModel, QuestionViewModel>(q)), JsonSettings);
        }
    }
}
