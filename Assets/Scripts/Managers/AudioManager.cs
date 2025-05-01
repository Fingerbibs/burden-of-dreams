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

    private void Start(){
        PlayGermansTalking();
    }

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

    public void PlaySFX(AudioClip clip){
        sfxSource.clip = clip;
        sfxSource.loop = true;
        sfxSource.time = Random.Range(0f, clip.length);
        sfxSource.Play();
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
    public void PlayTerminalRun() => PlaySFX(library.terminalRun);
    public void PlayGermansTalking() => PlayVoice(library.germansTalking);
}