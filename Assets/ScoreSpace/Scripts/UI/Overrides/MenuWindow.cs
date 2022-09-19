using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpace
{
    public class MenuWindow : Window
    {
        [SerializeField] private BonusSpawner _bonusSpawner;
        [SerializeField] private BonusManager _bonusManager;
        [SerializeField] private BotSpawner _botSpawner;
        [SerializeField] private BotManager _botManager;
        [SerializeField] private Player _player;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _tutorialButton;

        private bool _gameIsStopped = true;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            AddButtonListeners();

            _player.gameObject.SetActive(false);

            if (!PlayerPrefs.HasKey("Name"))
            {
                CloseWindow();
                UI.GetWindow<NameWindow>().OpenWindow();
            }
        }

        private void OnDestroy()
        {
            RemoveButtonListeners();
        }

        private void AddButtonListeners()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            _leaderboardButton.onClick.AddListener(OnLeaderboardButtonClick);
            _tutorialButton.onClick.AddListener(OnTutorialButtonClick);
        }

        private void RemoveButtonListeners()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonClick);
            _tutorialButton.onClick.RemoveListener(OnTutorialButtonClick);
        }

        public void StopGame()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            _gameIsStopped = true;

            _player.gameObject.SetActive(false);

            KillAllBots();
            DestroyAllBonuses();

            _bonusSpawner.StopSpawn();
            _botSpawner.StopSpawn();

            Score.SubmitScore();
        }

        private void StartGame()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _gameIsStopped = false;

            RefreshPlayer();

            _bonusSpawner.StartSpawn();
            _botSpawner.StartSpawn();
        }

        private void OnPlayButtonClick()
        {
            CloseWindow();
            UI.GetWindow<GameWindow>().OpenWindow();
            StartGame();
        }

        private void KillAllBots()
        {
            Bot[] bots = _botManager.Bots.ToArray();

            foreach (Bot bot in bots)
                bot.Destroy();
        }

        private void DestroyAllBonuses()
        {
            Bonus[] bonuses = _bonusManager.Bonuses.ToArray();

            foreach (Bonus bonus in bonuses)
                Destroy(bonus.gameObject);
        }

        private void RefreshPlayer()
        {
            _player.gameObject.SetActive(true);
            _player.transform.position = Vector2.zero;
            _player.RefreshDash();
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }

        private void OnLeaderboardButtonClick()
        {
            CloseWindow();
            UI.GetWindow<LeaderboardWindow>().OpenWindow();
        }

        private void OnTutorialButtonClick()
        {
            CloseWindow();
            UI.GetWindow<TutorialWindow>().OpenWindow();
        }
    }
}
