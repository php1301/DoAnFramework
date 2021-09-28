using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DoAnFramework.src.Models;
namespace DoAnFramework.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private ISubmission GetSubmissionModel(SubmissionInput submissionToHandle) => new Submission<TestsInputModel>()
        {

            Id = submissionToHandle.Id,
            AdditionalCompilerArguments = submissionToHandle.SubmissionType.AdditionalCompilerArguments,
            AllowedFileExtensions = submissionToHandle.SubmissionType.AllowedFileExtensions,
            FileContent = submissionToHandle.Content,
            CompilerType = submissionToHandle.SubmissionType.
            CompilerType,
            TimeLimit = submissionToHandle.Problem.TimeLimit,
            MemoryLimit = submissionToHandle.Problem.MemoryLimit,
            ExecutionStrategyType = submissionToHandle.SubmissionType.ExecutionStrategyType,
            ExecutionType = ExecutionType.TestsExecution,
            MaxPoints = submissionToHandle.Problem.MaximumPoints,
            Input = new TestsInputModel
            {
                TaskSkeleton = submissionToHandle.Problem.SolutionSkeleton,
                CheckerParameter = submissionToHandle.Problem.Checker.Parameter,
                CheckerAssemblyName = submissionToHandle.Problem.Checker.DllFile,
                CheckerTypeName = submissionToHandle.Problem.Checker.ClassName,
                Tests = submissionToHandle.Problem.Tests
                      .AsQueryable()
                      .Select(t => new TestContext
                      {
                          Id = t.Id,
                          Input = t.InputDataAsString,
                          Output = t.OutputDataAsString,
                          IsTrialTest = t.IsTrialTest,
                          OrderBy = t.OrderBy
                      })
                      .ToList()
            }

        };
        [HttpGet]
        public string submissionPing()
        {
            return "ok";

        }
        [HttpPost]
        public void submissionSubmit([FromBody] SubmissionModel participantSubmission)
        {
            var context = new DoAnFramework.src.Models.DbContext();
            var executor = new CreateSubmissionExecutor(3306);
            var newSubmission = new SubmissionInput
            {
                ContentAsString = participantSubmission.Content,
                ProblemId = 1,
                SubmissionTypeId = participantSubmission.SubmissionTypeId, // ExecutuionStrategyType
                ParticipantId = 1,
                IpAddress = "192.168.100.1",
                IsPublic = false,
            };

            var submissionInit = context.Submissions.Add(newSubmission);
            context.SaveChanges();
            var submissionConvert = context.Submissions
                .Include(s => s.Problem)
                .Include(s => s.SubmissionType)
                .Where(s => s.Id == submissionInit.Id)
                .FirstOrDefault();
            var submissionToProcess = (Submission<TestsInputModel>)GetSubmissionModel(submissionConvert);
            executor.RunSubmission<TestsInputModel, TestResult>(submissionToProcess);
        }
    }
}