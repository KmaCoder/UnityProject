using Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiSettingsController : MonoBehaviour
    {
        public Sprite SoundOn;
        public Sprite SoundOff;
        public Sprite MusicOn;
        public Sprite MusicOff;

        private Transform _settingsPanel;

        private void Start()
        {
            _settingsPanel = transform.Find("SettingsPanel");
            UpdateMusicButton();
            UpdateSoundButton();
        }

        private void UpdateSoundButton()
        {
            _settingsPanel.Find("ButtonSound").GetComponent<Image>().sprite =
                SoundManager.Instance.SoundOn ? SoundOn : SoundOff;
        }

        private void UpdateMusicButton()
        {
            _settingsPanel.Find("ButtonMusic").GetComponent<Image>().sprite =
                SoundManager.Instance.MusicOn ? MusicOn : MusicOff;
        }

        public void ButtonSoundClicked()
        {
            SoundManager.Instance.SoundOn = !SoundManager.Instance.SoundOn;
            UpdateSoundButton();
        }
        
        public void ButtonMusicClicked()
        {
            SoundManager.Instance.MusicOn = !SoundManager.Instance.MusicOn;
            UpdateMusicButton();
        }
    }
}