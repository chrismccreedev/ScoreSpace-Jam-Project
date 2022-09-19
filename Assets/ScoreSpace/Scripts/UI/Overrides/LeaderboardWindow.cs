using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

namespace ScoreSpace
{
    public class LeaderboardWindow : Window
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text[] _scores;
        [SerializeField] private TMP_Text[] _members;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        public override void OpenWindow()
        {
            Score.GetLeaderboard(OnScoreCompleted);
        }

        private void OnScoreCompleted(LootLockerLeaderboardMember[] members)
        {
            base.OpenWindow();

            for (int i = 0; i < _scores.Length; i++)
            {
                if (members.Length > i)
                {
                    _scores[i].text = members[i].score.ToString("N0");
                    _members[i].text = members[i].player.name == string.Empty ? members[i].player.id.ToString("N0") : members[i].player.name;
                }
                else
                {
                    _scores[i].text = string.Empty;
                    _members[i].text = string.Empty;
                }
            }
        }

        private void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick()
        {
            CloseWindow();
            UI.GetWindow<MenuWindow>().OpenWindow();
        }
    }
}
