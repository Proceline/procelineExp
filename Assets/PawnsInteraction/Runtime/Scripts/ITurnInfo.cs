namespace PawnsInteraction.Runtime.Scripts
{
    public interface ITurnInfo
    {
        int GetTurnBeforeStart { get; }
        int GetCurrentTurn { get; }
    }
}