using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    public interface IChecker
    {
        CheckerResult Check(string inputData, string receivedOutput, string expectedOutput, bool isTrialTest);

        void SetParameter(string parameter);
    }
}
