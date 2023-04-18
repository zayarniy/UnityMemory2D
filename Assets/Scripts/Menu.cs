using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public UnityEngine.Audio.AudioMixer audioMixer;
    public UnityEngine.UI.Slider sliderVolume;

    public void Start()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(1) * 20);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //https://youtu.be/C1gCOoDU29M
    public void ValueChange()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderVolume.value)*20);
    }
}
