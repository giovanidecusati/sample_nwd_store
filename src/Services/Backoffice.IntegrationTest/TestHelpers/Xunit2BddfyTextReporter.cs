using System.Threading;
using TestStack.BDDfy;
using Xunit.Abstractions;

namespace Backoffice.Sales.IntegrationTest.TestHelpers
{
    public class Xunit2BddfyTextReporter : ThreadsafeBddfyTextReporter
    {
        public static readonly Xunit2BddfyTextReporter Instance = new Xunit2BddfyTextReporter();
        private static readonly AsyncLocal<ITestOutputHelper> Output = new AsyncLocal<ITestOutputHelper>();

        private Xunit2BddfyTextReporter() {}

        public void RegisterOutput(ITestOutputHelper output)
        {
            Output.Value = output;
        }

        public override void Process(Story story)
        {
            base.Process(story);
            if (Output.Value != null)
                Output.Value.WriteLine(_text.Value.ToString());
        }
    }
}
