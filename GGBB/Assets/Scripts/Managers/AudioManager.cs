using UnityEngine;
using FMODUnity;

using FMOD.Studio;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public string RandomnessIntensity = "RandomnessIntensity";

    [SerializeField] EventReference testAudio;

    private void Awake() {
        // Singleton Attribute
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            PlaySFX(testAudio, transform.position);
        }
    }

    public void PlaySFX( // Plays a oneshot SFX with position-dependent audio, where parameters cannot be changed after initiation, ideal for short-duration SFX
        EventReference sfx,
        Vector3 playPosition,
        int randomnessIntensity = 50 // Defaults to 50% randomness
    ) {
        if (!sfx.IsNull) {
            EventInstance sfxInstance = CreateInstance(sfx, playPosition);
            sfxInstance.setParameterByName(RandomnessIntensity, randomnessIntensity);

            sfxInstance.start();
            sfxInstance.release();
        }
        else Debug.Log("Sound not found: " + sfx);

    }

    public void PlayLoop( // Plays a looping sound with position-dependent audio, where parameters can be changed after initiation, ideal for long-duration sounds
        EventReference sound,
        Vector3 playPosition,
        int randomnessIntensity = 50 // Defaults to 50% randomness
    ) {
        if (sound.IsNull) {
            EventInstance loopInstance = CreateInstance(sound, playPosition);
            loopInstance.setParameterByName(RandomnessIntensity, randomnessIntensity);
            loopInstance.start();
        }
        else Debug.Log("Sound not found: " + sound);
    }

    public EventInstance CreateInstance(EventReference audio, Vector3 eventPosition) {
        EventInstance audioInstance = RuntimeManager.CreateInstance(audio);
        audioInstance.set3DAttributes(RuntimeUtils.To3DAttributes(eventPosition));
        return audioInstance;

    }

    public void ClearInstance(params EventInstance[] audioInstance) {
        foreach (EventInstance instance in audioInstance) {
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }

}