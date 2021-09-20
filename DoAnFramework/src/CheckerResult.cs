using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public class CheckerResult
    {
        public bool IsCorrect { get; set; }

        public CheckerResultType ResultType { get; set; }

        /// <summary>
        /// More detailed information visible only by administrators.
        /// </summary>
        public CheckerDetails CheckerDetails { get; set; }
    }
}
