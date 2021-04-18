
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitTutorial.Models
{
    class Pan
    {
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task TryUse()
        {
            await semaphoreSlim.WaitAsync();
        }

        public void StopUsing()
        {
            semaphoreSlim.Release();
        }

    }
}
