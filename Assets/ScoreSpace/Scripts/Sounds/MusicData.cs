using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpace
{
    [CreateAssetMenu(fileName = "Music",menuName = "MusicData")]
    public class MusicData : ScriptableObject
    {
        [SerializeField] private MusicType _type;
        public MusicType Type => _type;

        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;

        [SerializeField] private float _bitDelay;
        public float BitDelay => _bitDelay;

        [SerializeField] private float _bitLength;
        public float BitLength => _bitLength;

        [SerializeField] private float _offset;
        public float Offset => _offset;
    }
}
