using UnityEngine;

namespace ScoreSpace
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private WindowState _state;
        public WindowState State => _state;

        public virtual void InitializeWindow()
        {
            SwitchWindow(_state);
        }

        protected void SwitchWindow(WindowState state)
        {
            if (state == WindowState.Opened)
                OpenWindow();
            else if (state == WindowState.Closed)
                CloseWindow();
        }

        public virtual void OpenWindow()
        {
            _state = WindowState.Opened;
            gameObject.SetActive(true);
        }

        public virtual void CloseWindow()
        {
            _state = WindowState.Closed;
            gameObject.SetActive(false);
        }
    }

    public enum WindowState
    {
        Opened,
        Closed
    }
}
