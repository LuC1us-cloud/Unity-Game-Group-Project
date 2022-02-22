using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame () {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame () {
        Debug.Log("Quit Successful");
        Application.Quit();
    }
}