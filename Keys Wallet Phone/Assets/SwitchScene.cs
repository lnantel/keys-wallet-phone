using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{

    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadLevelAfterDelay(delay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        print("Prep Switching Scene");
        yield return new WaitForSeconds(delay);
        print("Switching Scene");
        LevelManager.instance.LoadScene("Main", Color.black);
    }

    public void StartGame()
    {
        LevelManager.instance.LoadScene("Main", Color.black);
    }
}
