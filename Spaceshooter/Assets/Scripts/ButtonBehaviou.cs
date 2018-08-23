using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviou : MonoBehaviour {

    public void LoadLevelByIndex(int levelIndex)
    {
        Application.LoadLevel(levelIndex);
    }
    public void LoadLevelByName(string levelName)
    {

        Application.LoadLevel(levelName);

    }

    public void ResetStats()
    {
        PlayerC.score = 0;
        PlayerC.lives = 3;
        PlayerC.missed = 0;
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            ResetStats();
            Application.LoadLevel(1);
        }
    }

}
