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
    public class AnswerController : BaseApiController
    {
        private IQuestionService QuestionService { get; }

        private IAnswerService AnswerService { get; }

        public AnswerController(IQuestionService questionService, IAnswerService answerService, IMapper mapper) 
            : base(mapper)
        {
            QuestionService = questionService;
            AnswerService = answerService;
        }

        /// <summary>
        /// Retrieves the Answer with the given {id}
        /// </summary>
        /// &lt;param name="id">The ID of an existing Answer</param>
        /// <returns>the Answer with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var answer = await AnswerService.Get(id);

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Answer ID {0} has not been found", id)
                });
            }

            return new JsonResult(Mapper.Map<AnswerDetailsServiceModel, AnswerViewModel>(answer), JsonSettings);
        }

        /// <summary>
        /// Adds a new Answer to the Database
        /// </summary>
        /// <param name="m">The AnswerViewModel containing the data to insert</param>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] AnswerViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var answer = await AnswerService.Create(
                model.Text,
                model.Notes,
                model.QuestionId);

            return new JsonResult(Mapper.Map<AnswerDetailsServiceModel, AnswerViewModel>(answer), JsonSettings);
        }

        /// <summary>
        /// Edit the Answer with the given {id}
        /// </summary>
        /// <param name="m">The AnswerViewModel containing the data to update</param>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AnswerViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            if (!await AnswerService.Exists(model.Id))
            {
                return NotFound(new { Error = String.Format("Answer ID {0} has not been found", model.Id) });
            }

            if (!await QuestionService.Exists(model.QuestionId))
            {
                return NotFound(new { Error = String.Format("Question ID {0} has not been found", model.QuestionId) });
            }

            AnswerDetailsServiceModel answer = await AnswerService.Update(
                model.Id,
                model.Text,
                model.Notes,
                model.QuestionId);

            return new JsonResult(Mapper.Map<AnswerDetailsServiceModel, AnswerViewModel>(answer), JsonSettings);
        }

        /// <summary>
        /// Deletes the Answer with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Answer</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await AnswerService.Exists(id))
            {
                return NotFound(new { Error = String.Format("Answer ID {0} has not been found", id) });
            }

            await AnswerService.Delete(id);

            return new OkResult();
        }

        [HttpGet("All/{questionId}")]
        public async Task<IActionResult> AllAsync(int questionId)
        {
            var answers = await AnswerService.All(questionId);

            return new JsonResult(answers.Select(a => Mapper.Map<AnswerDetailsServiceModel, AnswerViewModel>(a)), JsonSettings);
        }
    }
}
