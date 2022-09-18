using System;

namespace ScoreSpace
{
    public interface IFinder
    {
        public event Action<LiveObject> OnLiveObjectEnters;
        public event Action<LiveObject> OnLiveObjectExits;
    }
}
