using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ScoreSpace
{
    public class GameWindow : Window
    {
        [SerializeField] private Bit _beat;
        [SerializeField] private Player _player;

        [SerializeField] private Image _filler;

        [SerializeField] private Transform _beatParent;
        [SerializeField] private Vector2 _beatPosition;
        [SerializeField] private Vector2 _beatDestination;
        [SerializeField] private Image _beatPrefab;

        [SerializeField] private List<Image> _beatSignals = new(2);

        private int _current = 0;

        public override void InitializeWindow()
        {
            base.InitializeWindow();

            _player.OnObjectDestroyed += LeaveGame;
            _player.OnCurrentDashChanged += SetFiller;

            _beat.OnBitStarted += StartBeat;
            _beat.OnBitEnded += EndBeat;
        }

        private void OnDestroy()
        {
            _player.OnObjectDestroyed -= LeaveGame;
            _player.OnCurrentDashChanged -= SetFiller;

            _beat.OnBitStarted -= StartBeat;
            _beat.OnBitEnded -= EndBeat;
        }

        private void Update()
        {
            if (State != WindowState.Opened)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                LeaveGame(_player);
        }

        private void LeaveGame(LiveObject liveObject)
        {
            CloseWindow();
            UI.GetWindow<MenuWindow>().StopGame();
            UI.GetWindow<MenuWindow>().OpenWindow();
        }

        private void SetFiller(int current, int max)
        {
            _filler.fillAmount = current / (float)max;
        }

        private void StartBeat()
        {
            RecolorBeat(_beatSignals[_current]);

            _current++;
            if (_current >= _beatSignals.Count)
                _current = 0;

            CreateBeat(_beatSignals[_current]);
        }

        private void EndBeat()
        {
            if (_current == 0)
                RemoveBeat(_beatSignals[1]);
            else
                RemoveBeat(_beatSignals[0]);
        }

        private void RecolorBeat(Image beat)
        {
            beat.DOColor(Color.red, _beat.BitLength / 10);
        }

        private void RemoveBeat(Image beat)
        {
            beat.gameObject.SetActive(false);
            beat.color = new Color(1, 1, 1, 0.25f);
        }

        private void CreateBeat(Image beat)
        {
            beat.gameObject.SetActive(true);

            beat.transform.localPosition = _beatPosition;
            beat.DOFade(1, _beat.SumLength);
            beat.transform.DOLocalMove(_beatDestination, _beat.SumLength);
        }
    }
}
