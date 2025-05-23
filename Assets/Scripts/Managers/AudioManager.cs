using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource stageSource;
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

    public void PlaySFX(AudioClip clip,bool loop = false, float volume = 0.3f, float pan = 0){
        sfxSource.clip = clip;
        sfxSource.loop = loop;
        sfxSource.volume = volume;
        sfxSource.panStereo = pan;
        sfxSource.Play();
    }

    public void PlayStageFX(AudioClip clip,bool loop = false, float volume = 0.7f, float pan = 0){
        stageSource.clip = clip;
        stageSource.loop = loop;
        stageSource.volume = volume;
        stageSource.panStereo = pan;
        stageSource.Play();
    }
    public void PlayStageFXOnce(AudioClip clip)
    {
        stageSource.PlayOneShot(clip, .5f);

    }

    public void PlaySFXOnce(AudioClip clip, float volume = 1.0f){
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayVoice(AudioClip clip){
        voiceSource.clip = clip;
        voiceSource.loop = true;
        voiceSource.time = Random.Range(0f, clip.length);
        voiceSource.Play();
    }
    
    public void PlayVoiceOnce(AudioClip clip, float volume = 1.0f){
        voiceSource.loop = false;
        voiceSource.PlayOneShot(clip, volume);
    }
    
    public void StopVoice(){
        voiceSource.Stop();
    }

    public void StopSFX(){
        sfxSource.Stop();
    }

    public void StopStage(){
        stageSource.Stop();
    }

    //HELPER FUNCTIONS
    public void PlayTerminalRun() => PlayVoice(library.terminalRun);
    public void PlayGermansTalking() => PlayVoice(library.germansTalking);

    // Menu
    public void PlayMenuTheme() => PlayMusic(library.menuTheme);
    public void PlayMenuHover() => PlaySFX(library.menuHover, false, 0.05f);
    public void PlayMenuSelect() => PlaySFX(library.menuSelect, false, 0.1f);
    public void PlayDeath3() => PlayMusic(library.Death3);
    public void PlayQuoteTheme() => PlayMusic(library.quoteTheme);

    // Stage
    public void PlayStage1Theme() => PlayMusic(library.stage1Theme);
    // public void PlayDisintegrate() => PlaySFX(library.disintegrate,false, 0.1f, -0.7f);
    public void PlayDisintegrate() => PlaySFXOnce(library.disintegrate);

    // Player
    public void PlayPlayerShoot() => PlaySFX(library.playerShoot, false, 0.09f, 0.7f);
    public void PlayPlayerShift() => PlaySFXOnce(library.playerSwitch, 0.15f);
    public void PlayAbsorb() => PlaySFXOnce(library.playerAbsorb, 0.7f);
    public void PlayPlayerDeath() => PlaySFXOnce(library.playerDeath, 3f);
    public void PlayBossPhase() => PlayStageFX(library.bossPhaseEnd, false, 0.5f, -0.3f);
    public void PlayBossSpawn() => PlayStageFX(library.bossSpawn);
    public void PlayBossHit() => PlayStageFX(library.bossHit,false, 0.1f, -0.45f);
    public void PlayBossDeath() => PlayStageFX(library.bossDeath);
    public void PlayBossPreDeath() => PlayStageFXOnce(library.bossPreDeath);

    

}