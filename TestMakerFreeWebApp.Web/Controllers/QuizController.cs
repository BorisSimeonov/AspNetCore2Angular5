using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Services.Interfaces;
using TestMakerFreeWebApp.Services.Models;
using TestMakerFreeWebApp.Web.Controllers.AbstractControllers;
using TestMakerFreeWebApp.Web.ViewModels;

namespace TestMakerFreeWebApp.Web.Controllers
{
    public class QuizController : BaseApiController
    {
        private IQuizService QuizService { get; }

        public QuizController(IQuizService quizService, IMapper mapper) : base(mapper)
        {
            QuizService = quizService;
        }

        /// <summary>
        /// GET: api/quiz/{id}
        /// Retrieves the Quiz with the given {id}
        /// </summary>
        /// <param name="id">The ID of an existing Quiz</param>
        /// <returns>the Quiz with the given {id}</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var quiz = await QuizService.Get(id);

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Quiz ID {0} has not been found", id)
                });
            }

            return new JsonResult(Mapper.Map<QuizDetailsServiceModel, QuizViewModel>(quiz), JsonSettings);
        }

        /// <summary>
        /// Adds a new Quiz to the Database
        /// </summary>
        /// <param name="m">The QuizViewModel containing the data to insert</param>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] QuizViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            QuizDetailsServiceModel quiz = await QuizService.Create(
                model.Title,
                model.Description,
                model.Text);

            return new JsonResult(Mapper.Map<QuizDetailsServiceModel, QuizViewModel>(quiz), JsonSettings);
        }

        /// <summary>
        /// Edit the Quiz with the given {id}
        /// </summary>
        /// <param name="m">The QuizViewModel containing the data to update</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuizViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            if (!await QuizService.Exists(model.Id))
            {
                return NotFound();
            }

            QuizDetailsServiceModel quiz = await QuizService.Update(
                model.Id,
                model.Title,
                model.Description,
                model.Text);

            return new JsonResult(Mapper.Map<QuizDetailsServiceModel, QuizViewModel>(quiz), JsonSettings);
        }

        /// <summary>
        /// Deletes the Quiz with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Quiz</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await QuizService.Exists(id))
            {
                return NotFound(new { Error = String.Format("Quiz ID {0} has not been found", id) });
            }

            await QuizService.Delete(id);

            return new OkResult();
        }

        /// <summary>
        /// GET: api/quiz/latest
        /// Retrieves the {num} latest Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>the {num} latest Quizzes</returns>
        [HttpGet("Latest/{num:int?}")]
        public async Task<IActionResult> Latest(int num = 10)
        {
            return MapAndSerializeResultList(await QuizService.GetLatest(num));
        }

        [HttpGet("ByTitle/{num:int?}")]
        public async Task<IActionResult> ByTitle(int num = 10)
        {
            return MapAndSerializeResultList(await QuizService.GetByTitle(num));
        }

        [HttpGet("Random/{num:int?}")]
        public async Task<IActionResult> Random(int num = 10)
        {
            return MapAndSerializeResultList(await QuizService.GetRandom(num));
        }

        private JsonResult MapAndSerializeResultList(List<QuizDetailsServiceModel> results)
            => new JsonResult(results.Select(q => Mapper.Map<QuizDetailsServiceModel, QuizViewModel>(q)), JsonSettings);
    }
}
