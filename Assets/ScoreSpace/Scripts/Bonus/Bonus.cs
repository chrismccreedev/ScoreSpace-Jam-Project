using System;
using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public abstract class Bonus : MonoBehaviour
    {
        public event Action<Bonus> OnBonusUsed;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Collider2D _collider2D;

        [SerializeField] private TriggerFinder _finder;
        [SerializeField] private float _duration;

        protected Player Player;

        private void Awake()
        {
            _finder.OnLiveObjectEnters += UseBonus;
        }

        private void OnDestroy()
        {
            if (_finder)
            _finder.OnLiveObjectEnters -= UseBonus;
        }

        private void UseBonus(LiveObject liveObject)
        {
            if (liveObject.TryGetComponent(out Player player))
                StartCoroutine(WaitForBonus(player));
        }

        private IEnumerator WaitForBonus(Player player)
        {
            OnBonusUsed?.Invoke(this);

            Player = player;

            _renderer.enabled = false;
            _collider2D.enabled = false;

            StartBonus();
            yield return new WaitForSeconds(_duration);
            EndBonus();

            Destroy(gameObject);
        }

        protected abstract void StartBonus();

        protected abstract void EndBonus();
    }
}
