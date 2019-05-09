
namespace PacManLibrary
{
    public enum Direction
    {
        Right = -2,
        Down = -1,
        Up = 1,
        Left = 2
    };

    public enum GameStat
    {
        Await,
        AwaitAfterDeath,
        Playing,
        Lose,
        Win
    }
}

