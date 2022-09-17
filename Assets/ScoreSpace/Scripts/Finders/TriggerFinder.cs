using System;
using UnityEngine;

namespace ScoreSpace
{
    public class TriggerFinder : MonoBehaviour, IFinder
    {
        public event Action<LiveObject> OnLiveObjectEnters;
        public event Action<LiveObject> OnLiveObjectExits;

        [SerializeField] private Team _requiredTeam;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject) && liveObject.GetTeam() == _requiredTeam)
                OnLiveObjectEnters?.Invoke(liveObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject) && liveObject.GetTeam() == _requiredTeam)
                OnLiveObjectExits?.Invoke(liveObject);
        }
    }
}
