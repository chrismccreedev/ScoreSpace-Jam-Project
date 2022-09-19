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
        [SerializeField] private Vector2[] _beatPositions;
        [SerializeField] private Vector2 _beatDestination;
        [SerializeField] private Image _beatPrefab;

        private List<Image> _beats = new(3);

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            _player.OnObjectDestroyed += LeaveGame;
            _player.OnCurrentDashChanged += SetFiller;

            _beat.OnBitEnded += StartBeat;
        }

        private void OnDestroy()
        {
            _player.OnObjectDestroyed -= LeaveGame;
            _player.OnCurrentDashChanged -= SetFiller;
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
            if (_beats.Count > 2)
            {
                RecolorBeat(_beats[0]);
                RecolorBeat(_beats[1]);
            }

            CreateBeat(_beatPositions[0]);
            CreateBeat(_beatPositions[1]);
        }

        private void EndBeat()
        {
            if (_beats.Count > 2)
            {
                RemoveBeat(_beats[0]);
                RemoveBeat(_beats[1]);
            }
        }

        private void RecolorBeat(Image beat)
        {
            beat.DOColor(Color.red, _beat.BitLength / 2);
        }

        private void RemoveBeat(Image beat)
        {
            _beats.Remove(beat);
            Destroy(beat.gameObject);
        }

        private void CreateBeat(Vector3 position)
        {
            _beats.Add(Instantiate(_beatPrefab, position, Quaternion.identity, _beatParent));

            _beats[^1].DOFade(1, _beat.SumLength);
            _beats[^1].transform.DOMove(_beatDestination, _beat.SumLength);
        }
    }
}
