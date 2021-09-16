using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
namespace ConsoleApp2
{
    public class BaseInputModel
    {
        public byte[] TaskSkeleton { get; set; }

        public string TaskSkeletonAsString => this.TaskSkeleton.Decompress();
    }
}
