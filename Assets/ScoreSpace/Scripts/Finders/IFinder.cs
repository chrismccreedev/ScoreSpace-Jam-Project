using System;
using UnityEngine;

namespace ScoreSpace
{
    public interface IFinder
    {
        public event Action<LiveObject> OnLiveObjectEnters;
        public event Action<LiveObject> OnLiveObjectExits;

        public event Action<Rigidbody2D> OnRigidbodyEnters;
        public event Action<Rigidbody2D> OnRigidbodyExits;
    }
}
