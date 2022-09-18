using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpace
{
    [CreateAssetMenu(fileName = "Music",menuName = "MusicData")]
    public class MusicData : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        public AudioClip AudioClip => _audioClip;

        [SerializeField] private float _bitDelay;
        public float BitDelay => _bitDelay;

        [SerializeField] private float _bitLength;
        public float BitLength => _bitLength;

        [SerializeField] private string _name;
        public string Name => _name;
    }
}
