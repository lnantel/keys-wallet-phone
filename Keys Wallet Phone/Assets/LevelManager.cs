using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private Animator _Anim;
    private Image _Solid;
    private IEnumerator _CurrentChangeSceneRoutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        _Anim = GetComponent<Animator>();
        _Solid = GetComponentInChildren<Image>();
    }

    public void LoadScene(string sceneName, Color fadeColor)
    {
        if (_CurrentChangeSceneRoutine != null)
            return;

        _CurrentChangeSceneRoutine = ChangeScene(sceneName, fadeColor);
        StartCoroutine(_CurrentChangeSceneRoutine);
    }

    private IEnumerator ChangeScene(string sceneName, Color fadeColor)
    {
        _Solid.color = fadeColor;
        _Anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.25f);

        SceneManager.LoadScene(sceneName);

        _Anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.25f);

        _CurrentChangeSceneRoutine = null;
    }
}
