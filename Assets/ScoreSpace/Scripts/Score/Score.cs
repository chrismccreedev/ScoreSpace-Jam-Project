using UnityEngine;
using LootLocker.Requests;
using System;

namespace ScoreSpace
{
    public class Score : MonoBehaviour
    {
        private static Score Instance;

        [SerializeField] private int _leaderboardId = 7196;
        [SerializeField] private int _leaderboardTopCount = 10;
        [SerializeField] private int _after = 0;

        private string _playerId;

        private int _score = 0;

        private void Start()
        {
            Instance = this;

            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log(response.Error);
                    return;
                }

                Debug.Log("successfully started LootLocker");
                _playerId = response.player_id.ToString();
            });
        }

        public static void Reset()
        {
            Instance._score = 0;
        }

        public static void Add(int value)
        {
            Instance._score += value;
        }

        public static void Remove(int value)
        {
            Instance._score -= value;

            if (Instance._score < 0)
                Instance._score = 0;
        }

        public static void SubmitScore()
        {
            LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("Name"), Instance._score, Instance._leaderboardId, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Score submitted");
                    Reset();
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                }
            });
        }

        public static void GetLeaderboard(Action<LootLockerLeaderboardMember[]> onComplete)
        {
            LootLockerSDKManager.GetScoreList(Instance._leaderboardId, Instance._leaderboardTopCount, Instance._after, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("ScoreList getted");
                    onComplete?.Invoke(response.items);
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                }
            });
        }
    }
}
