
namespace PacManLibrary
{
    public static class GameControllExtension
    {
        public static void MoveDown(this Game game)
        {
            if (game.GameStat == GameStat.Await || game.GameStat == GameStat.AwaitAfterDeath)
            {
                game.StartLoop();
            }
            game.PacMan.SetDirection(Direction.Down);
        }
        public static void MoveRight(this Game game)
        {
            if (game.GameStat == GameStat.Await || game.GameStat == GameStat.AwaitAfterDeath)
            {
                game.StartLoop();
            }
            game.PacMan.SetDirection(Direction.Right);
        }
        public static void MoveLeft(this Game game)
        {
            if (game.GameStat == GameStat.Await || game.GameStat == GameStat.AwaitAfterDeath)
            {
                game.StartLoop();
            }
            game.PacMan.SetDirection(Direction.Left);
        }
        public static void MoveUp(this Game game)
        {
            if (game.GameStat == GameStat.Await || game.GameStat == GameStat.AwaitAfterDeath)
            {
                game.StartLoop();
            }
            game.PacMan.SetDirection(Direction.Up);
        }
    }
}
