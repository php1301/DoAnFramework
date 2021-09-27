using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using DoAnFramework.src.Models;

namespace DoAnFramework.Migrations
{
    internal sealed class Configuration: DbMigrationsConfiguration<DbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            // this.SeedSubmissionsAndTestRuns(context);
            this.SeedSubmissionTypes(context);
            this.SeedRoles(context);
            this.SeedCheckers(context);

            // this.SeedContests(context);
            // this.SeedRandomContests(context);
            // this.SeedProblem(context);
            // this.SeedTest(context);
            // this.SeedCategoryContestProblem(context);
        }

        //// TODO: Add seed with .Any()
        protected void SeedRoles(DbContext context)
        {
            foreach (var entity in context.Roles)
            {
                context.Roles.Remove(entity);
            }

            context.Roles.AddOrUpdate(new IdentityRole(GlobalConstants.AdministratorRoleName));
        }

        protected void SeedCheckers(DbContext context)
        {
            var checkers = new DoAnFramework.src.Models.Checker[]
            {
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Exact",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.ExactChecker",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Trim",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.TrimChecker",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Sort lines",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.SortChecker",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Case-insensitive",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.CaseInsensitiveChecker",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Precision checker - 14",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.PrecisionChecker",
                        Parameter = "14",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Precision checker - 7",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.PrecisionChecker",
                        Parameter = "7",
                    },
                new DoAnFramework.src.Models.Checker
                    {
                        Name = "Precision checker - 3",
                        DllFile = "OJS.Workers.Checkers.dll",
                        ClassName = "OJS.Workers.Checkers.PrecisionChecker",
                        Parameter = "3",
                    }
            };
            context.Checkers.AddOrUpdate(x => x.Name, checkers);
            context.SaveChanges();
        }

        protected void SeedSubmissionTypes(DbContext context)
        {
            // foreach (var entity in context.SubmissionTypes)
            // {
            //    context.SubmissionTypes.Remove(entity);
            // }

            // context.SaveChanges();
            var submissionTypes = new[]
            {
                new SubmissionType
                {
                    Name = "MySQL prepare DB & run queries",
                    CompilerType = CompilerType.None,
                    AdditionalCompilerArguments = string.Empty,
                    ExecutionStrategyType = ExecutionStrategyType.MySqlPrepareDatabaseAndRunQueries,
                    IsSelectedByDefault = false,
                    AllowBinaryFilesUpload = false
                },
                new SubmissionType
                {
                    Name = "MySQL run queries & check DB",
                    CompilerType = CompilerType.None,
                    AdditionalCompilerArguments = string.Empty,
                    ExecutionStrategyType = ExecutionStrategyType.MySqlRunQueriesAndCheckDatabase,
                    IsSelectedByDefault = false,
                    AllowBinaryFilesUpload = false
                },
                new SubmissionType
                {
                    Name = "MySQL run skeleton, run queries & check DB",
                    CompilerType = CompilerType.None,
                    AdditionalCompilerArguments = string.Empty,
                    ExecutionStrategyType = ExecutionStrategyType.MySqlRunSkeletonRunQueriesAndCheckDatabase,
                    IsSelectedByDefault = false,
                    AllowBinaryFilesUpload = false
                },
            };

            context.SubmissionTypes.AddOrUpdate(x => x.Name, submissionTypes);
            context.SaveChanges();
        }

        private void SeedCategoryContestProblem(IDbContext context)
        {
            foreach (var categoryToBeDeleted in context.ContestCategories)
            {
                context.ContestCategories.Remove(categoryToBeDeleted);
            }

            foreach (var contestToBeDeleted in context.Contests)
            {
                context.Contests.Remove(contestToBeDeleted);
            }

            foreach (var problemToBeDeleted in context.Problems)
            {
                context.Problems.Remove(problemToBeDeleted);
            }

            var category = new ContestCategory
            {
                Name = "Category",
                OrderBy = 1,
                IsVisible = true,
                IsDeleted = false,
            };

            var otherCategory = new ContestCategory
            {
                Name = "Other Category",
                OrderBy = 1,
                IsVisible = true,
                IsDeleted = false,
            };

            var contest = new Contest
            {
                Name = "Contest",
                OrderBy = 1,
                PracticeStartTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-2),
                IsVisible = true,
                IsDeleted = false,
                Category = category
            };

            var otherContest = new Contest
            {
                Name = "Other Contest",
                OrderBy = 2,
                PracticeStartTime = DateTime.Now.AddDays(-2),
                StartTime = DateTime.Now.AddDays(-2),
                IsVisible = true,
                IsDeleted = false,
                Category = category
            };

            var problemGroup1 = new ProblemGroup
            {
                OrderBy = 0,
                Contest = contest
            };

            var problemGroup2 = new ProblemGroup
            {
                OrderBy = 1,
                Contest = contest
            };

            var problem = new Problem
            {
                Name = "Problem",
                MaximumPoints = 100,
                TimeLimit = 10,
                MemoryLimit = 10,
                OrderBy = 1,
                ShowResults = true,
                IsDeleted = false,
                ProblemGroup = problemGroup1
            };

            var otherProblem = new Problem
            {
                Name = "Other Problem",
                MaximumPoints = 100,
                TimeLimit = 10,
                MemoryLimit = 10,
                OrderBy = 1,
                ShowResults = true,
                IsDeleted = false,
                ProblemGroup = problemGroup2
            };

            var test = new DoAnFramework.src.Models.Test
            {
                InputDataAsString = "Input",
                OutputDataAsString = "Output",
                OrderBy = 0,
                IsTrialTest = false,
                Problem = problem,
            };

            var user = new UserProfile
            {
                UserName = "Ifaka",
                Email = "Nekav@nekav.com"
            };

            var participant = new Participant
            {
                Contest = contest,
                IsOfficial = false,
                User = user
            };

            var submission = new SubmissionInput
            {
                Problem = problem,
                Participant = participant,
                CreatedOn = DateTime.Now
            };

            for (int i = 0; i < 10; i++)
            {
                test.TestRuns.Add(new TestRun
                {
                    MemoryUsed = 100,
                    TimeUsed = 100,
                    CheckerComment = "Checked!",
                    ExecutionComment = "Executed!",
                    ResultType = TestRunResultType.CorrectAnswer,
                    Submission = submission
                });
            }

            context.Problems.Add(problem);
            context.Problems.Add(otherProblem);
            context.Contests.Add(otherContest);
            context.ContestCategories.Add(otherCategory);
            context.Tests.Add(test);
        }

        private void SeedSubmissionsAndTestRuns(DbContext context)
        {
            foreach (var submission in context.Submissions)
            {
                context.Submissions.Remove(submission);
            }

            foreach (var testRun in context.TestRuns)
            {
                context.TestRuns.Remove(testRun);
            }

            foreach (var participantToDelete in context.Participants)
            {
                context.Participants.Remove(participantToDelete);
            }

            Random random = new Random();

            List<TestRun> testRuns = new List<TestRun>();

            var test = new DoAnFramework.src.Models.Test
            {
                IsTrialTest = false,
                OrderBy = 1
            };

            for (int i = 0; i < 1000; i++)
            {
                testRuns.Add(new TestRun
                {
                    TimeUsed = (random.Next() % 10) + 1,
                    MemoryUsed = (random.Next() % 1500) + 200,
                    ResultType = (TestRunResultType)(random.Next() % 5),
                    Test = test
                });
            }

            var contest = new Contest
            {
                Name = "Contest batka 2",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(2),
                IsDeleted = false,
                IsVisible = true,
                OrderBy = 1
            };

            var problemGroup = new ProblemGroup
            {
                OrderBy = 0,
                Contest = contest
            };

            var problem = new Problem
            {
                ProblemGroup = problemGroup,
                Name = "Problem",
                MaximumPoints = 100,
                MemoryLimit = 100,
                OrderBy = 1
            };

            var user = new UserProfile
            {
                UserName = "Ifaka",
                Email = "Nekav@nekav.com"
            };

            var participant = new Participant
            {
                Contest = contest,
                IsOfficial = false,
                User = user
            };

            for (int i = 0; i < 100; i++)
            {
                var submission = new SubmissionInput
                {
                    Problem = problem,
                    Participant = participant
                };

                for (int j = 0; j < (random.Next() % 20) + 5; j++)
                {
                    submission.TestRuns.Add(testRuns[random.Next() % 1000]);
                }

                context.Submissions.Add(submission);
            }
        }

        private void SeedContest(DbContext context)
        {
            foreach (var entity in context.Contests)
            {
                context.Contests.Remove(entity);
            }

            context.Contests.Add(new Contest
            {
                Name = "Contest",
            });

            context.SaveChanges();
        }

        private void SeedProblem(DbContext context)
        {
            foreach (var problem in context.Problems)
            {
                context.Problems.Remove(problem);
            }

            var contest = context.Contests.FirstOrDefault(x => x.Name == "Contest");

            contest.ProblemGroups.Add(new ProblemGroup
            {
                OrderBy = 0,
                Problems = new List<Problem>
                {
                    new Problem
                    {
                        Name = "Problem"
                    },
                    new Problem
                    {
                        Name = "Other problem"
                    }
                }
            });

            context.SaveChanges();
        }

        private void SeedTest(DbContext context)
        {
            foreach (var entity in context.Tests)
            {
                context.Tests.Remove(entity);
            }

            var selectedProblem = context.Problems.FirstOrDefault(x => x.Name == "Problem" && !x.IsDeleted);

            selectedProblem.Tests.Add(new DoAnFramework.src.Models.Test
            {
                InputDataAsString = "Trial input test 1",
                OutputDataAsString = "Trial output test 1",
                IsTrialTest = true
            });

            selectedProblem.Tests.Add(new DoAnFramework.src.Models.Test
            {
                InputDataAsString = "Trial input test 2",
                OutputDataAsString = "Trial output test 2",
                IsTrialTest = true
            });

            for (int i = 0; i < 10; i++)
            {
                selectedProblem.Tests.Add(new DoAnFramework.src.Models.Test
                {
                    InputDataAsString = i.ToString(),
                    OutputDataAsString = (i + 1).ToString(),
                    IsTrialTest = false
                });
            }

            var otherProblem = context.Problems.FirstOrDefault(x => x.Name == "Other problem" && !x.IsDeleted);

            otherProblem.Tests.Add(new DoAnFramework.src.Models.Test
            {
                InputDataAsString = "Trial input test 1 other",
                OutputDataAsString = "Trial output test 1 other",
                IsTrialTest = true
            });

            otherProblem.Tests.Add(new DoAnFramework.src.Models.Test
            {
                InputDataAsString = "Trial input test 2 other",
                OutputDataAsString = "Trial output test 2 other",
                IsTrialTest = true
            });

            for (int i = 0; i < 10; i++)
            {
                otherProblem.Tests.Add(new DoAnFramework.src.Models.Test
                {
                    InputDataAsString = i.ToString() + "other",
                    OutputDataAsString = (i + 1).ToString() + "other",
                    IsTrialTest = false
                });
            }
        }

        private void SeedRandomContests(DbContext context)
        {
            foreach (var contest in context.Contests)
            {
                context.Contests.Remove(contest);
            }

            var category = new ContestCategory
            {
                Name = "Category",
                OrderBy = 1,
                IsVisible = true,
                IsDeleted = false,
            };

            context.Contests.Add(
                new Contest
                {
                    Name = "DSA future",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(10),
                    EndTime = DateTime.Now.AddHours(19),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "DSA 2 out",
                    IsVisible = false,
                    StartTime = DateTime.Now.AddHours(10),
                    EndTime = DateTime.Now.AddHours(19),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "DSA 3 past",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-10),
                    EndTime = DateTime.Now.AddHours(-8),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "DSA 2 active",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-2),
                    EndTime = DateTime.Now.AddHours(2),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps another active",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-2),
                    EndTime = DateTime.Now.AddHours(2),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps another active 2",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-2),
                    EndTime = DateTime.Now.AddHours(2),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps another active 3",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-2),
                    EndTime = DateTime.Now.AddHours(2),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps 2 past",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-10),
                    EndTime = DateTime.Now.AddHours(-8),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps 3 past",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(-10),
                    EndTime = DateTime.Now.AddHours(-8),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps 3 past",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(10),
                    EndTime = DateTime.Now.AddHours(18),
                    Category = category,
                });
            context.Contests.Add(
                new Contest
                {
                    Name = "JS Apps 3 past",
                    IsVisible = true,
                    StartTime = DateTime.Now.AddHours(10),
                    EndTime = DateTime.Now.AddHours(18),
                    Category = category,
                });
        }
    }
}
