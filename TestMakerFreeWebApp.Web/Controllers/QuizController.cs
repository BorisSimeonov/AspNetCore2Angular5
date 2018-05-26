using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        private IMapper Mapper { get; }

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            QuizService = quizService;
            Mapper = mapper;
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
                return NotFound();
            }

            return new JsonResult(Mapper.Map<QuizDetailsServiceModel, QuizViewModel>(quiz), new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        /// <summary>
        /// Adds a new Quiz to the Database
        /// </summary>
        /// <param name="m">The QuizViewModel containing the data to insert</param>
        [HttpPut]
        public IActionResult Put(QuizViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit the Quiz with the given {id}
        /// </summary>
        /// <param name="m">The QuizViewModel containing the data to update</param>
        [HttpPost]
        public IActionResult Post(QuizViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the Quiz with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Quiz</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GET: api/quiz/latest
        /// Retrieves the {num} latest Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>the {num} latest Quizzes</returns>
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();

            sampleQuizzes.Add(new QuizViewModel()
            {
                Id = 1,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Anime-related personality test",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = String.Format("Sample Quiz {0}", i),
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(sampleQuizzes, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => t.Title),
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;

            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
