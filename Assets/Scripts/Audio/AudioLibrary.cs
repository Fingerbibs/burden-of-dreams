using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Audio/Audio Library")]
public class AudioLibrary : ScriptableObject
{
    [Header ("SFX")]
    public AudioClip terminalRun;
    public AudioClip germansTalking;
    public AudioClip playerShoot;
    public AudioClip playerSwitch;
    public AudioClip playerAbsorb;
    public AudioClip playerDeath;
    public AudioClip disintegrate;
    public AudioClip enemyShoot;

    [Header ("Menu SFX")]
    public AudioClip menuHover;
    public AudioClip menuSelect;
    public AudioClip Death3;

    [Header ("Music")]
    public AudioClip menuTheme;
    public AudioClip stage1Theme;
}
