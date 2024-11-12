using System;

namespace PawnsInteraction.Runtime.Scripts
{
    public interface IDelegateContainer
    {
        
    }

    public interface IDelegateContainer<out T> : IDelegateContainer where T : Delegate
    {
        T ContainedDelegate { get; }
    }
}