using UnityEngine;
using System.Linq;

namespace ScoreSpace
{
    public class UI : MonoBehaviour
    {
        private static UI Instance;

        [SerializeField] private Window[] _windows;

        private void Awake()
        {
            Instance = this;

            foreach (Window window in _windows)
                window.InitializeWindow();
        }

        public static T GetWindow<T>() where T : Window
        {
            return Instance._windows.OfType<T>().FirstOrDefault();
        }
    }
}
