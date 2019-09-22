using PacManLibrary.Model;

namespace PacManLibrary.Events
{
    public class FoodEventHandler
    {
        private const int cherryApearX = 20;
        private const int cherryApearY = 14;
        private const int ghostapearX = 14;
        private const int ghostapearY = 14;
        private const int poinToApearInki = 30;
        private const int poinToApearChery = 70;
        private const int poinToApearClayd = 80;
        private int pointEated = 0;

        private readonly GameContext context;

        public FoodEventHandler(GameContext context)
        {
            this.context = context;
        }

        public void Execute(FoodEated foodEated)
        {
            pointEated++;
            CheckEffect();
        }

        void CheckEffect()
        {
            if (pointEated == poinToApearInki)
            {
                InvokeInki();
            }
            if (pointEated == poinToApearChery)
            {
                AddCherry();
            }
            if (pointEated == poinToApearClayd)
            {
                InvokeClayd();
            }
            CheckForFinal();   
        }

        private void AddCherry()
        {
            var cherry = new CherryFood(context) { IsChoosable = false, X = cherryApearX, Y = cherryApearY };
            context.Map[cherryApearX, cherryApearY] = cherry;
            context.Disappearables.Add(cherry);
        }
        private void InvokeInki()
        {
            InkiGhost inkiGhost = new InkiGhost(context) { X = ghostapearX, Y = ghostapearX };
            context.Enemies.Add(inkiGhost);
        }
        private void InvokeClayd()
        {
            ClaydGhost claydGhost = new ClaydGhost(context) { X = ghostapearX, Y = ghostapearX };
            context.Enemies.Add(claydGhost);
        }
        private void CheckForFinal()
        {
            var map = context.Map;
            int foodNotEated = 0;
            foreach (var item in map)
            {
                if ((item is BaseFood) || (item is Energizer))
                {
                    foodNotEated++;
                }
            }
            if (foodNotEated - 1  == 0)
            {
                context.ISWin = true;
            }
        }

    }
}
