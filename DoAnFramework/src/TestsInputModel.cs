using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public class TestsInputModel: BaseInputModel
    {
        public const string DefaultCheckerAssemblyName = "OJS.Workers.Checkers";
        public string CheckerAssemblyName { get; set; } = DefaultCheckerAssemblyName;

        public string CheckerTypeName { get; set; }

        public string CheckerParameter { get; set; }

        public IEnumerable<TestContext> Tests { get; set; }

        public IChecker GetChecker() => Checker.CreateChecker(
            this.CheckerAssemblyName,
            this.CheckerTypeName,
            this.CheckerParameter);
    }
}
