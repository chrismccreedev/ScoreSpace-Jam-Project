using System;

namespace ScoreSpace
{
    public interface IWeapon
    {
        public event Action<LiveObject> OnEnemyEnters;
        public event Action<LiveObject> OnEnemyExits;
    }
}
