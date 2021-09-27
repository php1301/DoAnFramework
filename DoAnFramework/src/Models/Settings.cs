using System;
using System.ComponentModel.DataAnnotations;

namespace DoAnFramework.src.Models
{
    public class Setting
    {


            [Key]
            public string Name { get; set; }

            public string Value { get; set; }
    }
}
