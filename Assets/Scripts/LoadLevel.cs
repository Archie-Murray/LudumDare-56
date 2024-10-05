using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] string LevelToLoad;
    public void click()
    {
            SceneManager.LoadScene(LevelToLoad);
    }
}
