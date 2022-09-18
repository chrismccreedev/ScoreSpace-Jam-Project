using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Bit : MonoBehaviour
    {
        [SerializeField] private GameObject _bitSignal;

        private bool _inBit = false;
        public bool InBit => _inBit;

        private void Awake()
        {
            SoundManager.Instance.onPlayMusic += StartBit;
            SoundManager.Instance.onStopMusic += StopBit;
        }

        public void StartBit(MusicData musicData)
        {
            StartCoroutine(WaitForBit(musicData.BitDelay, musicData.BitLength, musicData.Offset));
        }

        public void StopBit()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitForBit(float bitDelay, float bitLength, float offset)
        {
            yield return new WaitForSeconds(offset);

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
