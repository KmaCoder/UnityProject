using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioClip AttackClip;

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

        private bool _soundOn;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            _audioSource = gameObject.AddComponent<AudioSource> ();
        }

        public void PlayAttackSound()
        {
            _audioSource.clip = AttackClip;
            _audioSource.Play();
        }
    }
}