using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void OnMode1vs1()
    {
        PlayerPrefs.SetString("GameMode", "1v1");
        SceneManager.LoadScene("GameScene");
    }

    public void OnMode1vs2()
    {
        PlayerPrefs.SetString("GameMode", "1v2");
        SceneManager.LoadScene("GameScene");
    }

    public void OnMode2vs2()
    {
        PlayerPrefs.SetString("GameMode", "2v2");
        SceneManager.LoadScene("GameScene");
    }
}
