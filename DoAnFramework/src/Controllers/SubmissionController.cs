using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoAnFramework.src.Models;
namespace DoAnFramework.src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly CreateSubmissionExecutor executor;
        private Submission<TestsInputModel> GetSubmissionModel(SubmissionInput submissionToHandle)
        {
            {
                return new Submission<TestsInputModel>
                {
                    Id = submissionToHandle.Id,
                    AdditionalCompilerArguments = submissionToHandle.SubmissionType.AdditionalCompilerArguments,
                    AllowedFileExtensions = submissionToHandle.SubmissionType.AllowedFileExtensions,
                    FileContent = submissionToHandle.Content,
                    CompilerType = submissionToHandle.SubmissionType.CompilerType,
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
            };
        }

        [HttpGet]
        public string submissionPing()
        {
            return "ok";

        }
        [HttpPost]
        public void submissionSubmit(SubmissionModel participantSubmission)
        {
            var newSubmission = new SubmissionInput
            {
                ContentAsString = participantSubmission.Content,
                ProblemId = 1,
                SubmissionTypeId = participantSubmission.SubmissionTypeId, // ExecutuionStrategyType
                ParticipantId = 1,
                IpAddress = "192.168.100.1",
                IsPublic = false,
            };
            var submissionToProcess = GetSubmissionModel(newSubmission);
            this.executor.RunSubmission<TestsInputModel, TestResult>(submissionToProcess);
        }
    }
}