﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyCore.Infrastructure.Entities;
using SurveyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyCore.Infrastructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ISurveyDbContext dbContext;

        public SurveyRepository(ISurveyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Survey GetSurveyByUid(string uid)
        {
            return dbContext.Survey.FirstOrDefault(surveyAnswers => surveyAnswers.UID == uid);
        }

        public bool GetSurveyStatus(string uid)
        {
            return dbContext.Survey.FirstOrDefault(surveyAnswers => surveyAnswers.UID == uid).Submitted;
        }

        public void UpdateSurveyAnswers(SurveyData surveyData, bool isSubmit = false)
        {
            if (surveyData == null) return;
            var surveyToUpdate = GetSurveyByUid(surveyData.UserId);

            surveyToUpdate.Answers = JsonConvert.SerializeObject(surveyData);
            surveyToUpdate.Submitted = isSubmit;

            (dbContext as DbContext).SaveChanges();
        }

        public int DeleteSurveyAnswers(string uid)
        {
            var surveyToUpdate = GetSurveyByUid(uid);
            surveyToUpdate.Answers = " ";
            surveyToUpdate.Submitted = false;

            return (dbContext as DbContext).SaveChanges();
        }
    }
}