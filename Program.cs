
using AsyncAwaitTutorial.Benchmarks;
using BenchmarkDotNet.Running;
using System;

namespace AsyncAwaitTutorial
{
    /// <summary>
    /// Asynchronous programming with async and await
    ///
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
    /// </summary>
    ///

    // Notes
    // 1) 4/17 - Why are we using Task.Delay(3000).Wait() vs normal Thread sleeping?
    // 2) 4/17 - Previous experience tells me using Console.Writeline will cause merged output on the console, is that resolved with Tasks?
    // 3)

    class Program
    {
        static void Main(String[] args)
        {
#if DEBUG
            //SimpleBreakfast simpleBreakfast = new SimpleBreakfast();
            //simpleBreakfast.MakeBreakfast();

            //AwaitedBreakfast awaitedBreakfast = new AwaitedBreakfast();
            //awaitedBreakfast.MakeBreakfast().Wait();

            //ConcurrentAwaitedBreakfast concurrentAwaitedBreakfast = new ConcurrentAwaitedBreakfast();
            //concurrentAwaitedBreakfast.MakeBreakfast().Wait();

            //CompositionAwaitedBreakfast compositionAwaitedBreakfast = new CompositionAwaitedBreakfast();
            //compositionAwaitedBreakfast.MakeBreakfast().Wait();

            SharedPanBreakfast sharedPanBreakfast = new SharedPanBreakfast();
            sharedPanBreakfast.MakeBreakfast().Wait();
#else
            BenchmarkRunner.Run<SimpleBreakfast>();
            BenchmarkRunner.Run<AwaitedBreakfast>();
            BenchmarkRunner.Run<ConcurrentAwaitedBreakfast>();
            BenchmarkRunner.Run<CompositionAwaitedBreakfast>();
            BenchmarkRunner.Run<SharedPanBreakfast>();
#endif
        }
    }
}
