
namespace ScoreSpace
{
    public class ShieldBonus : Bonus
    {
        protected override void StartBonus()
        {
            Player.CanBeDestroyed = false;
        }

        protected override void EndBonus()
        {
            Player.CanBeDestroyed = true;
        }
    }
}
