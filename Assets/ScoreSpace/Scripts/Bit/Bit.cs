using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Bit : MonoBehaviour
    {
        [SerializeField] private GameObject _bitSignal;

        [SerializeField] private float _bitDelay = 0.5f;
        [SerializeField] private float _bitLength = 0.1f;

        private bool _inBit = false;
        public bool InBit => _inBit;

        private void Start()
        {
            StartBit();
        }

        public void StartBit()
        {
            StartCoroutine(WaitForBit());
        }

        public void StopBit()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitForBit()
        {
            while (true)
            {
                _inBit = false;
                _bitSignal.SetActive(false);
                yield return new WaitForSeconds(_bitDelay);

                _inBit = true;
                _bitSignal.SetActive(true);
                yield return new WaitForSeconds(_bitLength);
            }
        }
    }
}
