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

            s.source.volume = s.volume * 0.3f /*SettingsManager.instance.MainVolume*/;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = 0.0f;
        }

        SceneManager.activeSceneChanged += AssignMusicOnScene;
    }

    private void Start() {
        DropSystem.playerCollided += PlayerCollision;
        PickableObject.objectDropped += ObjectDropped;
        PickableObject.objecPickedUp += ObjectPickedUp;
        TimerManager.reachedStep += IncreaseStressLevel;
        Doormat.keysCheck += KeysCheck;
        Doormat.walletCheck += WalletCheck;
        Doormat.phoneCheck += PhoneCheck;
        Doormat.success += DoorSuccess;
        Doormat.missingObject += ObjectMissing;
    }

    private void AssignMusicOnScene(Scene scene1, Scene scene2) {
        if (SceneManager.GetActiveScene().name == "Main") {
            PlayTheme("level1");
        }

        if (SceneManager.GetActiveScene().name == "MainMenu") {
            PlayTheme("menu");
        }

        if(SceneManager.GetActiveScene().name == "LoseScreen") {
            PlaySound("voice8");
            PlayTheme("mort");
        }

        if(SceneManager.GetActiveScene().name == "WinScreen") {
            PlayTheme("succès");
        }
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

    private void ObjectDropped(PickableObject obj) {
        PlaySound("Lost_" + obj.name);
    }

    private void ObjectPickedUp(PickableObject obj) {
        PlaySound("PickUp_" + obj.name);
    }

    private void IncreaseStressLevel(int step) {
        PlayTheme("level" + (step + 1));
        if(step > 0)
            PlaySound("voice" + (step));
    }

    private void KeysCheck() {
        PlaySound("FrontDoor_Key");
    }

    private void WalletCheck() {
        PlaySound("FrontDoor_Wallet");
    }

    private void PhoneCheck() {
        PlaySound("FrontDoor_Phone");
    }

    private void ObjectMissing() {
        PlaySound("FrontDoor_Missing");
    }

    private void DoorSuccess() {
        PlaySound("FrontDoor_Success");
    }
}
