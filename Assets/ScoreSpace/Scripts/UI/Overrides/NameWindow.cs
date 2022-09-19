using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

namespace ScoreSpace
{
    public class NameWindow : Window
    {
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private Button _submitButton;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            _submitButton.onClick.AddListener(SetName);
        }

        private void OnDestroy()
        {
            _submitButton.onClick.RemoveListener(SetName);
        }

        private void SetName()
        {
            string name = _nameField.text.Trim();

            if (!string.IsNullOrWhiteSpace(name))
            {
                PlayerPrefs.SetString("Name", name);
                LootLockerSDKManager.SetPlayerName(name, null);
                CloseWindow();
                UI.GetWindow<TutorialWindow>().OpenWindow();
            }
        }
    }
}
