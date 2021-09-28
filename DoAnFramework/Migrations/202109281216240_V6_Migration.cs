namespace DoAnFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V6_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checkers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        DllFile = c.String(unicode: false),
                        ClassName = c.String(unicode: false),
                        Parameter = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContestCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        OrderBy = c.Int(nullable: false),
                        ParentId = c.Int(),
                        IsVisible = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        IsVisible = c.Boolean(nullable: false),
                        AutoChangeTestsFeedbackVisibility = c.Boolean(nullable: false),
                        CategoryId = c.Int(),
                        Duration = c.Time(precision: 0),
                        StartTime = c.DateTime(precision: 0),
                        EndTime = c.DateTime(precision: 0),
                        ContestPassword = c.String(maxLength: 20, storeType: "nvarchar"),
                        PracticePassword = c.String(maxLength: 20, storeType: "nvarchar"),
                        NewIpPassword = c.String(maxLength: 20, storeType: "nvarchar"),
                        PracticeStartTime = c.DateTime(precision: 0),
                        PracticeEndTime = c.DateTime(precision: 0),
                        LimitBetweenSubmissions = c.Int(nullable: false),
                        OrderBy = c.Int(nullable: false),
                        NumberOfProblemGroups = c.Short(nullable: false),
                        Description = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestCategories", t => t.CategoryId)
                .Index(t => t.IsVisible)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ContestIps",
                c => new
                    {
                        ContestId = c.Int(nullable: false),
                        IpId = c.Int(nullable: false),
                        IsOriginallyAllowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContestId, t.IpId })
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Ips", t => t.IpId, cascadeDelete: true)
                .Index(t => t.ContestId)
                .Index(t => t.IpId);
            
            CreateTable(
                "dbo.Ips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 45, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Value, unique: true);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ParticipationStartTime = c.DateTime(precision: 0),
                        ParticipationEndTime = c.DateTime(precision: 0),
                        IsOfficial = c.Boolean(nullable: false),
                        IsInvalidated = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ContestId)
                .Index(t => t.UserId)
                .Index(t => t.IsOfficial);
            
            CreateTable(
                "dbo.ParticipantAnswers",
                c => new
                    {
                        ParticipantId = c.Int(nullable: false),
                        ContestQuestionId = c.Int(nullable: false),
                        Answer = c.String(unicode: false),
                    })
                .PrimaryKey(t => new { t.ParticipantId, t.ContestQuestionId })
                .ForeignKey("dbo.ContestQuestions", t => t.ContestQuestionId)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.ParticipantId)
                .Index(t => t.ContestQuestionId);
            
            CreateTable(
                "dbo.ContestQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        Text = c.String(maxLength: 100, storeType: "nvarchar"),
                        AskOfficialParticipants = c.Boolean(nullable: false),
                        AskPracticeParticipants = c.Boolean(nullable: false),
                        RegularExpressionValidation = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .Index(t => t.ContestId);
            
            CreateTable(
                "dbo.ContestQuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Text = c.String(maxLength: 100, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestQuestions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProblemGroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        MaximumPoints = c.Short(nullable: false),
                        TimeLimit = c.Int(nullable: false),
                        MemoryLimit = c.Int(nullable: false),
                        SourceCodeSizeLimit = c.Int(),
                        CheckerId = c.Int(),
                        OrderBy = c.Int(nullable: false),
                        SolutionSkeleton = c.Binary(),
                        AdditionalFiles = c.Binary(),
                        ShowResults = c.Boolean(nullable: false),
                        ShowDetailedFeedback = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checkers", t => t.CheckerId)
                .ForeignKey("dbo.ProblemGroups", t => t.ProblemGroupId)
                .Index(t => t.ProblemGroupId)
                .Index(t => t.CheckerId)
                .Index(t => t.ShowResults);
            
            CreateTable(
                "dbo.ParticipantScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProblemId = c.Int(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                        SubmissionId = c.Int(),
                        ParticipantName = c.String(unicode: false),
                        Points = c.Int(nullable: false),
                        IsOfficial = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.ProblemId)
                .ForeignKey("dbo.SubmissionInputs", t => t.SubmissionId)
                .Index(t => t.ProblemId)
                .Index(t => t.ParticipantId)
                .Index(t => t.SubmissionId)
                .Index(t => t.IsOfficial);
            
            CreateTable(
                "dbo.SubmissionInputs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipantId = c.Int(),
                        ProblemId = c.Int(),
                        SubmissionTypeId = c.Int(),
                        Content = c.Binary(),
                        FileExtension = c.String(unicode: false),
                        IpAddress = c.String(unicode: false),
                        IsCompiledSuccessfully = c.Boolean(nullable: false),
                        CompilerComment = c.String(unicode: false),
                        IsPublic = c.Boolean(),
                        TestRunsCache = c.String(unicode: false),
                        Processed = c.Boolean(nullable: false),
                        ProcessingComment = c.String(unicode: false),
                        Points = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.ParticipantId)
                .ForeignKey("dbo.Problems", t => t.ProblemId)
                .ForeignKey("dbo.SubmissionTypes", t => t.SubmissionTypeId)
                .Index(t => t.ParticipantId)
                .Index(t => t.ProblemId)
                .Index(t => t.SubmissionTypeId)
                .Index(t => t.IsPublic);
            
            CreateTable(
                "dbo.SubmissionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IsSelectedByDefault = c.Boolean(nullable: false),
                        ExecutionStrategyType = c.Int(nullable: false),
                        CompilerType = c.Int(nullable: false),
                        AdditionalCompilerArguments = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        AllowBinaryFilesUpload = c.Boolean(nullable: false),
                        AllowedFileExtensions = c.String(unicode: false),
                        Problem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Problems", t => t.Problem_Id)
                .Index(t => t.Problem_Id);
            
            CreateTable(
                "dbo.TestRuns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubmissionId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        TimeUsed = c.Int(nullable: false),
                        MemoryUsed = c.Long(nullable: false),
                        ResultType = c.Int(nullable: false),
                        ExecutionComment = c.String(unicode: false),
                        CheckerComment = c.String(unicode: false),
                        ExpectedOutputFragment = c.String(unicode: false),
                        UserOutputFragment = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubmissionInputs", t => t.SubmissionId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestId)
                .Index(t => t.SubmissionId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProblemId = c.Int(nullable: false),
                        InputData = c.Binary(),
                        OutputData = c.Binary(),
                        IsTrialTest = c.Boolean(nullable: false),
                        IsOpenTest = c.Boolean(nullable: false),
                        HideInput = c.Boolean(nullable: false),
                        OrderBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Problems", t => t.ProblemId, cascadeDelete: true)
                .Index(t => t.ProblemId);
            
            CreateTable(
                "dbo.ProblemGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContestId = c.Int(nullable: false),
                        OrderBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.ContestId, cascadeDelete: true)
                .Index(t => t.ContestId);
            
            CreateTable(
                "dbo.ProblemResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProblemId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        File = c.Binary(),
                        FileExtension = c.String(maxLength: 50, storeType: "nvarchar"),
                        Link = c.String(unicode: false),
                        OrderBy = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Problems", t => t.ProblemId, cascadeDelete: true)
                .Index(t => t.ProblemId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        ForegroundColor = c.String(unicode: false),
                        BackgroundColor = c.String(unicode: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        FirstName = c.String(maxLength: 30, storeType: "nvarchar"),
                        LastName = c.String(maxLength: 30, storeType: "nvarchar"),
                        City = c.String(maxLength: 200, storeType: "nvarchar"),
                        EducationalInstitution = c.String(unicode: false),
                        FacultyNumber = c.String(maxLength: 30, storeType: "nvarchar"),
                        DateOfBirth = c.DateTime(precision: 0),
                        Company = c.String(maxLength: 200, storeType: "nvarchar"),
                        JobTitle = c.String(maxLength: 100, storeType: "nvarchar"),
                        ForgottenPasswordToken = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        ModifiedOn = c.DateTime(precision: 0),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Value = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.SourceCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ProblemId = c.Int(),
                        Content = c.Binary(),
                        IsPublic = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(precision: 0),
                        CreatedOn = c.DateTime(nullable: false, precision: 0),
                        PreserveCreatedOn = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Problems", t => t.ProblemId)
                .Index(t => t.AuthorId)
                .Index(t => t.ProblemId);
            
            CreateTable(
                "dbo.TagProblems",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Problem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Problem_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.Problem_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Problem_Id);
            
            CreateTable(
                "dbo.ProblemsForParticipants",
                c => new
                    {
                        Participant_Id = c.Int(nullable: false),
                        Problem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Participant_Id, t.Problem_Id })
                .ForeignKey("dbo.Participants", t => t.Participant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Problems", t => t.Problem_Id, cascadeDelete: true)
                .Index(t => t.Participant_Id)
                .Index(t => t.Problem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SourceCodes", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.SourceCodes", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Participants", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProblemsForParticipants", "Problem_Id", "dbo.Problems");
            DropForeignKey("dbo.ProblemsForParticipants", "Participant_Id", "dbo.Participants");
            DropForeignKey("dbo.TagProblems", "Problem_Id", "dbo.Problems");
            DropForeignKey("dbo.TagProblems", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.SubmissionTypes", "Problem_Id", "dbo.Problems");
            DropForeignKey("dbo.ProblemResources", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.Problems", "ProblemGroupId", "dbo.ProblemGroups");
            DropForeignKey("dbo.ProblemGroups", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ParticipantScores", "SubmissionId", "dbo.SubmissionInputs");
            DropForeignKey("dbo.TestRuns", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.TestRuns", "SubmissionId", "dbo.SubmissionInputs");
            DropForeignKey("dbo.SubmissionInputs", "SubmissionTypeId", "dbo.SubmissionTypes");
            DropForeignKey("dbo.SubmissionInputs", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.SubmissionInputs", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ParticipantScores", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.ParticipantScores", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Problems", "CheckerId", "dbo.Checkers");
            DropForeignKey("dbo.Participants", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ParticipantAnswers", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.ParticipantAnswers", "ContestQuestionId", "dbo.ContestQuestions");
            DropForeignKey("dbo.ContestQuestions", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ContestQuestionAnswers", "QuestionId", "dbo.ContestQuestions");
            DropForeignKey("dbo.Contests", "CategoryId", "dbo.ContestCategories");
            DropForeignKey("dbo.ContestIps", "IpId", "dbo.Ips");
            DropForeignKey("dbo.ContestIps", "ContestId", "dbo.Contests");
            DropForeignKey("dbo.ContestCategories", "ParentId", "dbo.ContestCategories");
            DropIndex("dbo.ProblemsForParticipants", new[] { "Problem_Id" });
            DropIndex("dbo.ProblemsForParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.TagProblems", new[] { "Problem_Id" });
            DropIndex("dbo.TagProblems", new[] { "Tag_Id" });
            DropIndex("dbo.SourceCodes", new[] { "ProblemId" });
            DropIndex("dbo.SourceCodes", new[] { "AuthorId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProblemResources", new[] { "ProblemId" });
            DropIndex("dbo.ProblemGroups", new[] { "ContestId" });
            DropIndex("dbo.Tests", new[] { "ProblemId" });
            DropIndex("dbo.TestRuns", new[] { "TestId" });
            DropIndex("dbo.TestRuns", new[] { "SubmissionId" });
            DropIndex("dbo.SubmissionTypes", new[] { "Problem_Id" });
            DropIndex("dbo.SubmissionInputs", new[] { "IsPublic" });
            DropIndex("dbo.SubmissionInputs", new[] { "SubmissionTypeId" });
            DropIndex("dbo.SubmissionInputs", new[] { "ProblemId" });
            DropIndex("dbo.SubmissionInputs", new[] { "ParticipantId" });
            DropIndex("dbo.ParticipantScores", new[] { "IsOfficial" });
            DropIndex("dbo.ParticipantScores", new[] { "SubmissionId" });
            DropIndex("dbo.ParticipantScores", new[] { "ParticipantId" });
            DropIndex("dbo.ParticipantScores", new[] { "ProblemId" });
            DropIndex("dbo.Problems", new[] { "ShowResults" });
            DropIndex("dbo.Problems", new[] { "CheckerId" });
            DropIndex("dbo.Problems", new[] { "ProblemGroupId" });
            DropIndex("dbo.ContestQuestionAnswers", new[] { "QuestionId" });
            DropIndex("dbo.ContestQuestions", new[] { "ContestId" });
            DropIndex("dbo.ParticipantAnswers", new[] { "ContestQuestionId" });
            DropIndex("dbo.ParticipantAnswers", new[] { "ParticipantId" });
            DropIndex("dbo.Participants", new[] { "IsOfficial" });
            DropIndex("dbo.Participants", new[] { "UserId" });
            DropIndex("dbo.Participants", new[] { "ContestId" });
            DropIndex("dbo.Ips", new[] { "Value" });
            DropIndex("dbo.ContestIps", new[] { "IpId" });
            DropIndex("dbo.ContestIps", new[] { "ContestId" });
            DropIndex("dbo.Contests", new[] { "CategoryId" });
            DropIndex("dbo.Contests", new[] { "IsVisible" });
            DropIndex("dbo.ContestCategories", new[] { "ParentId" });
            DropTable("dbo.ProblemsForParticipants");
            DropTable("dbo.TagProblems");
            DropTable("dbo.SourceCodes");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.ProblemResources");
            DropTable("dbo.ProblemGroups");
            DropTable("dbo.Tests");
            DropTable("dbo.TestRuns");
            DropTable("dbo.SubmissionTypes");
            DropTable("dbo.SubmissionInputs");
            DropTable("dbo.ParticipantScores");
            DropTable("dbo.Problems");
            DropTable("dbo.ContestQuestionAnswers");
            DropTable("dbo.ContestQuestions");
            DropTable("dbo.ParticipantAnswers");
            DropTable("dbo.Participants");
            DropTable("dbo.Ips");
            DropTable("dbo.ContestIps");
            DropTable("dbo.Contests");
            DropTable("dbo.ContestCategories");
            DropTable("dbo.Checkers");
        }
    }
}
