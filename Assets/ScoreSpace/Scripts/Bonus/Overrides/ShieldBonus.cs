using UnityEngine;
namespace ScoreSpace
{
    public class ShieldBonus : Bonus
    {
        protected override void StartBonus()
        {
            Player.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;
            Player.CanBeDestroyed = false;
        }

        protected override void EndBonus()
        {
            Player.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            Player.CanBeDestroyed = true;
        }
    }
}
