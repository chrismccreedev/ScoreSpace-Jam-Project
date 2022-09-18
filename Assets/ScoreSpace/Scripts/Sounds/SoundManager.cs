using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace ScoreSpace
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _soundSource,_musicSource;
        [SerializeField] private SoundAudioClip[] _soundAudioClip;
        [SerializeField] private MusicData[] _musicDatas;

        public event Action<MusicData> onPlayMusic;
        public event Action onStopMusic;

        public static SoundManager Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            PlayMusicWithBit("Dark01");
        }

        public void PlaySoundOfType(SoundType soundType)
        {
            _soundSource.PlayOneShot(GetSoundAudioClip(soundType).audioClip);
        }

        private SoundAudioClip GetSoundAudioClip(SoundType soundType) => _soundAudioClip.Where(a => a.soundType == soundType).FirstOrDefault();

        private MusicData GetMusicData(string name) => _musicDatas.Where(a => a.Name == name).FirstOrDefault();  

        public void PlayMusic(string name)
        {
            MusicData musicData = GetMusicData(name);

            _musicSource.clip = musicData.AudioClip;
            _musicSource.Play();
        }

        public void PlayMusicWithBit(string name)
        {
            MusicData musicData = GetMusicData(name);

            onPlayMusic?.Invoke(musicData);
            _musicSource.clip = musicData.AudioClip;
            _musicSource.Play();

        }
        public void StopMusic()
        {
            onStopMusic?.Invoke();
            _musicSource.Stop();
        }

        public void ChangeVolume(AudioSource audioSource, float volume)
        {
            audioSource.volume = volume;
        }
    }
}

[System.Serializable]
public class SoundAudioClip
{
    public AudioClip audioClip;
    public SoundType soundType;
}
public enum SoundType
{
    BonusPickup,
    Dash,
    Death,
    GoodBit,
    BadBit
}
