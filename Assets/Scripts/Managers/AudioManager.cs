using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    public AudioLibrary library;
    
    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else{
            Debug.Log("Destroying duplicate AudioManager/SceneLoader");
            Destroy(gameObject);
        }
    }

    private void Start(){}

    public void PlayMusic(AudioClip clip, bool loop = true){
        musicSource.clip = clip;
        musicSource.loop = loop;

        musicSource.Play();
    }

    public void StopMusic(){
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip newClip){
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip,bool loop = false, float volume = 0.3f){
        sfxSource.clip = clip;
        sfxSource.loop = loop;
        sfxSource.volume = volume;
        sfxSource.Play();
    }

    public void PlaySFXOnce(AudioClip clip){
        sfxSource.loop = false;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayVoice(AudioClip clip){
        voiceSource.clip = clip;
        voiceSource.loop = true;
        voiceSource.time = Random.Range(0f, clip.length);
        voiceSource.Play();
    }
    
    public void StopVoice(){
        voiceSource.Stop();
    }

    public void StopSFX(){
        sfxSource.Stop();
    }

    //HELPER FUNCTIONS
    public void PlayTerminalRun() => PlaySFX(library.terminalRun, true, 0.1f);
    public void PlayGermansTalking() => PlayVoice(library.germansTalking);

    // Menu
    public void PlayMenuTheme() => PlayMusic(library.menuTheme);
    public void PlayMenuHover() => PlaySFX(library.menuHover, false, 0.1f);
    public void PlayMenuSelect() => PlaySFX(library.menuSelect, false, 0.3f);

    // Stage
    public void PlayStage1Theme() => PlayMusic(library.stage1Theme);
}