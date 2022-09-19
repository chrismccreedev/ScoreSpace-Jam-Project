using UnityEngine;
using LootLocker.Requests;

namespace ScoreSpace
{
    public class Score : MonoBehaviour
    {
        private static Score Instance;

        public readonly int LeaderboardId = 7196;
        public readonly int LeaderboardTopCount = 10;
        public readonly int After = 0;

        private string _playerId;

        private int _score = 0;

        private void Start()
        {
            Instance = this;

            LootLockerSDKManager.StartGuestSession("DEBUG PLAYER", (response) =>
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
            LootLockerSDKManager.SubmitScore("DEBUG PLAYER", Instance._score, Instance.LeaderboardId, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Score submitted");
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                }
            });
        }

        public void GetLeaderboard()
        {
            LootLockerSDKManager.GetScoreList(Instance.LeaderboardId, Instance.LeaderboardTopCount, Instance.After, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("ScoreList getted");
                }
                else
                {
                    Debug.Log("failed: " + response.Error);
                }
            });
        }
    }
}
