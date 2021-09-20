using System;
using System.Collections.Generic;
using System.Text;

namespace DoAnFramework
{
    
    public enum CheckerResultType
    {
        Ok = 0,

        WrongAnswer = 1 << 0,

        InvalidOutputFormat = 1 << 1,

        InvalidNumberOfLines = 1 << 2,
    }
}
