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
            {
                bot.Destroy();
                bot.GetComponent<SpriteRenderer>().color = new Color32(155, 155, 155, 255);
            }
            
        }

        protected override void EndBonus()
        {
            
        }
    }
}
