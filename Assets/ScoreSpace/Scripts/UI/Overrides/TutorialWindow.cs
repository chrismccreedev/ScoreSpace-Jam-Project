using UnityEngine;
using UnityEngine.UI;

namespace ScoreSpace
{
    public class TutorialWindow : Window
    {
        [SerializeField] private Button _menuButton;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            _menuButton.onClick.AddListener(OnMenuButtonClick);
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
