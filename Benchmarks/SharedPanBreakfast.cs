using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncAwaitTutorial.Models;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace AsyncAwaitTutorial.Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    public class SharedPanBreakfast
    {
        [Benchmark]
        public async Task MakeBreakfast()
        {
            Pan pan = new Pan();

            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Egg> eggsTask = FryEggsAsync(pan, 2);
            Task<Bacon> baconTask = FryBaconAsync(pan, 3);
            Task<Toast> toastTask = MakeToastWithButterAndJamAsync(2);

            List<Task> breakfastTasks = new List<Task>() { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("toast is ready");
                }

                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");

            Console.WriteLine("Breakfast is ready!");
        }

        private async Task<Toast> MakeToastWithButterAndJamAsync(Int32 slices)
        {
            Toast toast = await ToastBreadAsync(slices);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }

        private async Task<Toast> ToastBreadAsync(Int32 slices)
        {
            for (Int32 slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(2000);

            // Exception Handling
            //Console.WriteLine("Fire! Toast is ruined!");
            //throw new InvalidOperationException("The toaster is on fire");

            await Task.Delay(1000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private async Task<Egg> FryEggsAsync(Pan pan, Int32 howMany)
        {
            Console.WriteLine("Waiting to use the pan for eggs...");
            await pan.TryUse();

            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            Console.WriteLine("Put eggs on plate");

            pan.StopUsing();

            return new Egg();
        }

        private async Task<Bacon> FryBaconAsync(Pan pan, Int32 slices)
        {
            Console.WriteLine("Waiting to use the pan for bacon...");
            await pan.TryUse();

            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (Int32 slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            Console.WriteLine("Put bacon on plate");

            pan.StopUsing();

            return new Bacon();
        }

        private Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private void ApplyJam(Toast toast)
        {
            Console.WriteLine("Putting jam on the toast");
        }

        private void ApplyButter(Toast toast)
        {
            Console.WriteLine("Putting butter on the toast");
        }

    }
}
