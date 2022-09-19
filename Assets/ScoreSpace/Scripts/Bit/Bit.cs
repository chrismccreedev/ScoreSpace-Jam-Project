using System;
using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class Bit : MonoBehaviour
    {
        public event Action OnBitStarted;
        public event Action OnBitEnded;

        public float BitDelay { get; private set; }
        public float BitLength { get; private set; }
        public float SumLength => BitDelay + BitLength;

        private bool _inBit = false;
        public bool InBit => _inBit;

        private void Awake()
        {
            SoundManager.Instance.onPlayMusic += StartBit;
            SoundManager.Instance.onStopMusic += StopBit;
        }

        public void StartBit(MusicData musicData)
        {
            BitDelay = musicData.BitDelay;
            BitLength = musicData.BitLength;

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
                OnBitEnded?.Invoke();
                yield return new WaitForSeconds(bitDelay);

                _inBit = true;
                OnBitStarted?.Invoke();
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
