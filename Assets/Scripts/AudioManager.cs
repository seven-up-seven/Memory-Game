using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;

    public AudioClip BGMusic;
    public AudioClip FlipCard;
    public AudioClip Correct;
    public AudioClip Finished;

    public static float masterVol;
    public static float musicVol;
    public static float sfxVol;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
 
    }

    private void Start()
    {
        musicSource.clip = BGMusic;
        musicSource.Play();
       //Dieu khien music volumn
        if (PlayerPrefs.HasKey("musicVolumn"))
        {
            Debug.Log("found music volumn!");
            LoadMusicVolumn();
        }
        else
        {
            Debug.Log("not found music volumn!");
            SetMusicVolumn();
        }

        //DK sfx volumn
        if (PlayerPrefs.HasKey("sfxVolumn"))
        {
            Debug.Log("found sfx volumn!");
            LoadSFXVolumn();
        }
        
        else
        {
            Debug.Log("not found sfx volumn!");
            SetSFXVolumn();
        }

        //DK master volumn
        if (PlayerPrefs.HasKey("masterVolumn"))
        {
            Debug.Log("found master volumn!");
            LoadMasterVolumn();
        }
            
        else
        {
            Debug.Log("not found master volumn!");
            SetMasterVolumn();
        }
            
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void SetMusicVolumn()
    {
        musicVol = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(musicVol) * 20);
        PlayerPrefs.SetFloat("musicVolumn", musicVol);
    }

    public void SetMasterVolumn()
    {
        masterVol = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(masterVol) * 20);
        PlayerPrefs.SetFloat("masterVolumn", masterVol);
    }

    public void SetSFXVolumn()
    {
        sfxVol = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(sfxVol) * 20);
        PlayerPrefs.SetFloat("sfxVolumn", sfxVol);
    }

    private void LoadMusicVolumn()
    {
        musicVol = PlayerPrefs.GetFloat("musicVolumn");
        musicSlider.value = musicVol;
        SetMusicVolumn();
    }
    private void LoadMasterVolumn()
    {
        masterVol = PlayerPrefs.GetFloat("masterVolumn");
        musicSlider.value = masterVol;
        SetMasterVolumn();
    }
    private void LoadSFXVolumn()
    {
        sfxVol = PlayerPrefs.GetFloat("sfxVolumn");
        sfxSlider.value = sfxVol;
        SetSFXVolumn();
    }
}
