﻿using System;
using TestMakerFreeWebApp.Common.Mapping;
using TestMakerFreeWebApp.Domain.DomainModels;

namespace TestMakerFreeWebApp.Services.Models
{
    public class QuizDetailsServiceModel : IMapFrom<Quiz>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Notes { get; set; }

        public int Type { get; set; }

        public int Flags { get; set; }

        public string UserId { get; set; }

        public int ViewCount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
