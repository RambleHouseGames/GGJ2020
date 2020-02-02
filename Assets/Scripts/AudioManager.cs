using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource mainSource;

    public AudioClip song1Clip;
    public AudioClip song2Clip;
    public TextAsset song1Notes;
    public TextAsset song2Notes;

    public TextAsset PlaySong(bool song1, float delay) {
        AudioClip clip = song1 ? song1Clip : song2Clip;
        TextAsset notes = song1 ? song1Notes : song2Notes;
        mainSource.clip = clip;
        mainSource.PlayDelayed(delay);
        return notes;
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
