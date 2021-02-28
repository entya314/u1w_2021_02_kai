using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
    public enum SliderType
    {
        MUSIC,
        SE,
    }
    [SerializeField]
    SliderType sliderType;
    private Slider slider;
    private MusicManager musicManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
        musicManager = MusicManager.Instance;
        if (sliderType == SliderType.MUSIC)
        {
            slider.SetValueWithoutNotify (musicManager.GettingMusicVolume()*slider.maxValue);
        }
        else if (sliderType == SliderType.SE)
        {
            slider.SetValueWithoutNotify (musicManager.GettingSEVolume() * slider.maxValue);

        }
    }
    // Update is called once per frame
    public void SetMusicSlider()
    {
        musicManager.SettingPlayMusic(slider.value / 10.0f);
    }

    public void SetSESlider()
    {
        musicManager.SettingPlaySE(slider.value / 10.0f);
        musicManager.PlaySE();
    }

}
