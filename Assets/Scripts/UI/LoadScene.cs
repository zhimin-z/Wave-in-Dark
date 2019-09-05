using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string loadLevel;
    
    public void LoadLevel()
    {
        SceneManager.LoadScene(loadLevel);
        Cursor.lockState = CursorLockMode.None;
    } 
}