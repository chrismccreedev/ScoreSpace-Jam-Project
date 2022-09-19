using System.Linq;
using UnityEngine;

namespace ScoreSpace
{
    public class KillAllBonus : Bonus
    {
        protected override void StartBonus()
        {
            BotManager botManager = FindObjectOfType<BotManager>();
            Bot[] bots = botManager.Bots.ToArray();

            foreach (Bot bot in bots)
                bot.Destroy();
            
        }

        protected override void EndBonus()
        {
            
        }
    }
}
