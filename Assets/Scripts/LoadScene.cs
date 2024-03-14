using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class LoadScene : MonoBehaviour
{
    public string sceneName;
    
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
