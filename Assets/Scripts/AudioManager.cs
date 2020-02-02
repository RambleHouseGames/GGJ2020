using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource mainSource;

    public AudioClip song1Clip;
    public AudioClip song2Clip;
    public TextAsset song1Notes;
    public TextAsset song2Notes;

    public TextAsset SetupSong(bool song1) {
        AudioClip clip = song1 ? song1Clip : song2Clip;
        TextAsset notes = song1 ? song1Notes : song2Notes;
        mainSource.clip = clip;
        return notes;
    }

    public void PlaySong(float delay) {
        mainSource.PlayDelayed(delay);
    }

    public float Time {
        get {
            return mainSource.time;
        }
        set {
            mainSource.time = value;
        }
    }
}
