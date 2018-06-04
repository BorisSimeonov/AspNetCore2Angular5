using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Interfaces;
using TestMakerFreeWebApp.Services.Models;
using TestMakerFreeWebApp.Web.Controllers.AbstractControllers;
using TestMakerFreeWebApp.Web.ViewModels;

namespace TestMakerFreeWebApp.Web.Controllers
{
    public class ResultController : BaseApiController
    {
        private IQuizService QuizService { get; }

        private IResultService ResultService { get; }

        public ResultController(IQuizService quizService, IResultService resultService, IMapper mapper) 
            : base(mapper)
        {
            QuizService = quizService;
            ResultService = resultService;
        }

        /// <summary>
        /// Retrieves the Result with the given {id}
        /// </summary>
        /// &lt;param name="id">The ID of an existing Result</param>
        /// <returns>the Result with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await ResultService.Get(id);

            if (result == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Result ID {0} has not been found", id)
                });
            }

            return new JsonResult(Mapper.Map<ResultDetailsServiceModel, ResultViewModel>(result), JsonSettings);
        }

        /// <summary>
        /// Adds a new Result to the Database
        /// </summary>
        /// <param name="m">The ResultViewModel containing the data to insert</param>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] ResultViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var result = await ResultService.Create(
                model.Text,
                model.MinValue,
                model.MaxValue,
                model.QuizId);

            return new JsonResult(Mapper.Map<ResultDetailsServiceModel, ResultViewModel>(result), JsonSettings);
        }

        /// <summary>
        /// Edit the Result with the given {id}
        /// </summary>
        /// <param name="m">The ResultViewModel containing the data to update</param>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ResultViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            if (!await ResultService.Exists(model.QuizId))
            {
                return NotFound(new { Error = String.Format("Result ID {0} has not been found", model.Id) });
            }

            if (!await QuizService.Exists(model.QuizId))
            {
                return NotFound(new { Error = String.Format("Quiz ID {0} has not been found", model.QuizId) });
            }

            ResultDetailsServiceModel result = await ResultService.Update(
                model.Id,
                model.Text,
                model.MinValue,
                model.MaxValue,
                model.QuizId);

            return new JsonResult(Mapper.Map<ResultDetailsServiceModel, ResultViewModel>(result), JsonSettings);
        }

        /// <summary>
        /// Deletes the Result with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Result</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (!await ResultService.Exists(id))
            {
                return NotFound(new { Error = String.Format("Result ID {0} has not been found", id) });
            }

            await ResultService.Delete(id);

            return new OkResult();
        }

        [HttpGet("All/{quizId}")]
        public async Task<IActionResult> AllAsync(int quizId)
        {
            var answers = await ResultService.All(quizId);

            return new JsonResult(answers.Select(r => Mapper.Map<ResultDetailsServiceModel, ResultViewModel>(r)), JsonSettings);
        }
    }
}
