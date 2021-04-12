using System;
using IdGeneration.IdGen;
using _idGen = IdGeneration.IdGen.IdGenerator;

namespace IdGeneration.GeneratorWrapper
{
    public static class IdGenerator
    {
        private static readonly IdGen.IdGenerator Generator;
        
        static IdGenerator()
        {
            if (!IdGenGlobalState.IsCreated)
            {
                throw new InvalidOperationException("IdGenerator is not initialized");
            }

            var structure = new IdStructure(44, 6, 13);
            Generator = new IdGen.IdGenerator(IdGenGlobalState.InstanceId, 
                new IdGeneratorOptions(
                    idStructure: structure,
                    timeSource: new DefaultTimeSource(IdGenGlobalState.Epoch),
                    sequenceOverflowStrategy: SequenceOverflowStrategy.SpinWait));
            
            Console.WriteLine("Max. generators       : {0}", structure.MaxGenerators);
            Console.WriteLine("Id's/ms per generator : {0}", structure.MaxSequenceIds);
            Console.WriteLine("Wraparound date       : {0:O}", structure.WraparoundDate(Generator.Options.TimeSource.Epoch, Generator.Options.TimeSource));
        }

        public static long NewId => Generator.CreateId();
    }
}