using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using PacManLibrary.Model;
using PacManLibrary.Interfaces;
using PacManLibrary.Events;
using PacManLibrary.GameData;

namespace PacManLibrary
{
    public class Game
    {
        private const int iteratinAppearSpeedy = 10;
        private const int n = 36;
        private const int m = 28;
        private const int ghostapearX = 14;
        private const int ghostapearY = 14;
        private IEventSink eventSink;
        private int iteration;
        private BlinkiGhost blinkiGhost;
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;
        private List<IDisappearable> disappearables;
        private bool failed = false;
        private FoodEventHandler foodHandler;

        public Game()
        {
            Initialize(true);
        }

        public GameStat GameStat { get; private set; } = GameStat.Await;
        public PacMan PacMan { get; private set; }
        public IPoint[,] Border { get; private set; }
        public GameContext GameContext { get; set; }
        public List<IEnemy> Enemies { get; private set; }

        public void Restart()
        {
            cancelTokenSource.Cancel();
            PacMan.IsLive = false;
            Initialize(true);
        }

        public void StartLoop()
        {
            GameStat = GameStat.Playing;
            GameLoop(token);
        }
        private async Task GameLoop(CancellationToken token)
        {
            while (PacMan.IsLive)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                iteration++;
                AddGhost(failed);
                try
                {
                    PacMan.Move();
                }
                catch (Exception e)
                {
                    Logger(e.ToString());
                }
                foreach (GhostAbstract item in Enemies)
                {
                    try
                    {
                        item.Move();
                    }
                    catch (Exception e)
                    {
                        Logger(e.ToString());
                    }
                }

                foreach (IDisappearable item in disappearables)
                {
                    item.Disapper();
                }
                if (token.IsCancellationRequested)
                {
                    return;
                }
                if (PacMan.Life == 0)
                {
                    GameStat = GameStat.Lose;
                    return;
                }
                if (GameContext.ISWin)
                {
                    GameStat = GameStat.Await;
                    Initialize(true, PacMan.Score, PacMan.Life);
                    return;
                }
                await Task.Delay(10);
            }
            Initialize(false);
        }

        private void AddGhost(bool failed)
        {
            if (iteration == 100)
            {
                PinkiGhost pinkiGhost = new PinkiGhost(GameContext) { X = ghostapearX, Y = ghostapearX };
                Enemies.Add(pinkiGhost);
            }
            /*if (failed && iteration == 200)
            {
                InkiGhost inkiGhost = new InkiGhost(GameContext) { X = ghostapearX, Y = ghostapearX };
                Enemies.Add(inkiGhost);
            }
            if (failed && iteration == 300)
            {
                ClaydGhost claydGhost = new ClaydGhost(GameContext) { X = ghostapearX, Y = ghostapearX };
                Enemies.Add(claydGhost);
            }*/
        }

        private void Initialize(bool initializing, float score = 0, int lifes = 3)
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
            eventSink = new EventSink();
            iteration = 0;
            if (initializing)
            {
                Border = new MapData().InitializateMap();
                PacMan = new PacMan(Border, eventSink) { X = 26, Y = 13, Life = lifes, Score = score };
            }
            else
            {
                GameStat = GameStat.AwaitAfterDeath;
                failed = true;
                Border[PacMan.X, PacMan.Y] = new EatedFood() { IsChoosable = PacMan.OnWayChosser };
                PacMan.EventSink = eventSink;
                foreach (GhostAbstract item in Enemies)
                {
                    Border[item.X, item.Y] = item.OnPoint;
                    if (item.OnPoint == PacMan)
                    {
                        Border[item.X, item.Y] = new EatedFood() { IsChoosable = PacMan.OnWayChosser };
                    }
                }
                PacMan.IsLive = true;
                PacMan.X = 26;
                PacMan.Y = 13;
                PacMan.Xpixel = 0;
                PacMan.Ypixel = 0;
                Border[26, 13] = PacMan;
            }
            disappearables = new List<IDisappearable>();
            Enemies = new List<IEnemy>();
            GameContext = new GameContext { Map = Border, PacMan = PacMan, Enemies = Enemies, EventSink = eventSink, Disappearables = disappearables };
            foodHandler = new FoodEventHandler(GameContext);
            blinkiGhost = new BlinkiGhost(GameContext) { X = ghostapearX, Y = ghostapearY };
            eventSink.Subscribe<FoodEated>(foodHandler.Execute);
            PacMan.SetDirection(Direction.Left);
            Border[14, 14] = blinkiGhost;
            Enemies.Add(blinkiGhost);
        }

        
        public void Logger(string lines)
        {
            Console.Beep();
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\deep\source\repos\PacMan\PacManLibrary\Stuff\logs.txt", true);
            file.WriteLine(DateTime.Now.ToString() + " " + lines);
            file.Close();
        }
    }
}
