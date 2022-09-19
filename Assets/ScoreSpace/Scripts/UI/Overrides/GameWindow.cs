using UnityEngine;

namespace ScoreSpace
{
    public class GameWindow : Window
    {
        [SerializeField] private Player _player;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            _player.OnObjectDestroyed += LeaveGame;
        }

        private void OnDestroy()
        {
            _player.OnObjectDestroyed -= LeaveGame;
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
    }
}
