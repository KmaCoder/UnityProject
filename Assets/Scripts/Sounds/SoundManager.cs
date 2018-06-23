using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioClip UIClicked;
        public AudioClip MainMusic;

        private AudioSource _soundSource;
        private AudioSource _uiSoundSource;
        private AudioSource _musicSource;

        public bool SoundOn
        {
            get { return _soundOn; }
            set
            {
                _soundOn = value;
                PlayerPrefs.SetInt("sound", value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public bool MusicOn
        {
            get { return _musicOn; }
            set
            {
                _musicOn = value;
                if(_musicOn)
                    _musicSource.Play();
                else
                    _musicSource.Pause();
                PlayerPrefs.SetInt("music", value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        private bool _soundOn;
        private bool _musicOn;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            _soundOn = PlayerPrefs.GetInt("sound", 1) == 1;
            _musicOn = PlayerPrefs.GetInt("music", 1) == 1;
            _soundSource = gameObject.AddComponent<AudioSource>();
            _uiSoundSource = gameObject.AddComponent<AudioSource>();
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.clip = MainMusic;
            _musicSource.loop = true;
            MusicOn = _musicOn;
        }

        public void PlaySound(AudioClip clip, AudioSource source)
        {
            if (SoundOn)
            {
//                if (source.clip == clip && source.isPlaying)
//                    return;
                source.clip = clip;
                source.Play();
            }
        }

        public void PlaySound(AudioClip clip)
        {
            if (SoundOn)
            {
                _soundSource.clip = clip;
                _soundSource.Play();
            }
        }

        public void PlayButtonClicked()
        {
            if (SoundOn)
            {
                _uiSoundSource.clip = UIClicked;
                _uiSoundSource.Play();
            }
        }
    }
}