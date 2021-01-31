using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public static AudioManager instance;
    [HideInInspector]
    public Sound currentMusic;

    // Start is called before the first frame update
    void Awake() {
        if (instance == null) {
            instance = this;
        }

        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * 0.3f /* SettingsManager.instance.MainVolume*/;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 0.0f;
        }

        SceneManager.activeSceneChanged += AssignMusicOnScene;
    }

    private void Start() {
        DropSystem.playerCollided += PlayerCollision;
        PlayTheme("level1");
    }

    private void Update() {
        transform.position = FindObjectOfType<SimplePlayerController>().transform.position;
    }

    private void AssignMusicOnScene(Scene scene1, Scene scene2) {
        //if (SceneManager.GetActiveScene().name == "Arena_1" || SceneManager.GetActiveScene().name == "Arena_2" || SceneManager.GetActiveScene().name == "Arena_3") {
        //    PlayTheme("Main_Loop");
        //}

        //if (SceneManager.GetActiveScene().name == "MainMenu") {
        //    if (currentMusic.source != null) {
        //        currentMusic.source.Stop();
        //    }
        //}
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayTheme(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (currentMusic.source != null) {
            currentMusic.source.Stop();
        }

        currentMusic = s;
        currentMusic.source.Play();
    }

    private void PlayerCollision() {
        int objectHit = UnityEngine.Random.Range(1, 3);
        PlaySound("Object_Hit" + objectHit);

        int ouch = UnityEngine.Random.Range(1, 5);
        PlaySound("Ouch" + ouch);
    }
}
