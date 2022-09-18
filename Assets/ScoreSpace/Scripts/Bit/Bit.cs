using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Bit : MonoBehaviour
    {
        [SerializeField] private GameObject _bitSignal;

        private bool _inBit = false;
        public bool InBit => _inBit;

        private void Start()
        {
            SoundManager.Instance.onPlayMusic += StartBit;
            SoundManager.Instance.onStopMusic += StopBit;
        }

        public void StartBit(MusicData musicData)
        {
            StopBit();
            StartCoroutine(WaitForBit(musicData.BitDelay, musicData.BitLength));
        }

        public void StopBit()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitForBit(float bitDelay, float bitLength)
        {
            while (true)
            {
                _inBit = false;
                _bitSignal.SetActive(false);
                yield return new WaitForSeconds(bitDelay);

                _inBit = true;
                _bitSignal.SetActive(true);
                yield return new WaitForSeconds(bitLength);
            }
        }

        private void OnDestroy()
        {
            SoundManager.Instance.onPlayMusic -= StartBit;
            SoundManager.Instance.onStopMusic -= StopBit;
        }

        public void UseBit()
        {
            _inBit = false;
        }
    }
}
