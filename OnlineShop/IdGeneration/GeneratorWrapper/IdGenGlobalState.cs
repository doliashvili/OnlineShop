using System;
using System.Threading;

namespace IdGeneration.GeneratorWrapper
{
    public static class IdGenGlobalState
    {
        private static readonly SemaphoreSlim Semaphore = new(1, 1);
        public static int InstanceId { get; private set; }
        public static DateTimeOffset Epoch { get; private set; }
        
        internal static bool IsCreated; 
        
        public static void SetState(int instanceId, DateTimeOffset epoch)
        {
            Semaphore.Wait();
            try
            {
                if (IsCreated)
                    throw new InvalidOperationException("IdGen state already initialized.");

                if (instanceId < 0)
                    throw new InvalidOperationException("Instance ID must be greater than or equal to 0.");

                InstanceId = instanceId;
                Epoch = epoch;
                IsCreated = true;
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}