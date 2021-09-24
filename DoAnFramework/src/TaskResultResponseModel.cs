using System.Collections.Generic;

namespace DoAnFramework
{
    public class TaskResultResponseModel
    {
        public int Points { get; set; }

        public IEnumerable<TestResultResponseModel> TestResults { get; set; }
    }
}
