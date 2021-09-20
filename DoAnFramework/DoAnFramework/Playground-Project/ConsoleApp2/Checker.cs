using System;
using System.Reflection;

namespace ConsoleApp2
{
    public abstract class Checker : IChecker
    {

        protected Checker() => this.IgnoreCharCasing = false;
        public const string ClassLibraryFileExtension = ".dll";
        protected bool IgnoreCharCasing { get; set; }
        public abstract CheckerResult Check(
    string inputData,
    string receivedOutput,
    string expectedOutput,
    bool isTrialTest);

        public virtual void SetParameter(string parameter)
            => throw new InvalidOperationException("This checker doesn't support parameters");

        public static IChecker CreateChecker(string assemblyName, string typeName, string parameter)
        {
            var assemblyFilePath = FilerHelpers.BuildPath(
                AppDomain.CurrentDomain.BaseDirectory,
                $"{assemblyName}{ClassLibraryFileExtension}");

            var assembly = Assembly.LoadFile(assemblyFilePath);
            var type = assembly.GetType($"{assemblyName}.{typeName}");
            var checker = (IChecker)Activator.CreateInstance(type);

            if (!string.IsNullOrEmpty(parameter))
            {
                checker.SetParameter(parameter);
            }

            return checker;
        }

    }
}
