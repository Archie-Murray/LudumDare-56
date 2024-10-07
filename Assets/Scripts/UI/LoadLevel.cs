using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {
    [SerializeField] string LevelToLoad;
    [SerializeField] private Texture2D cursor;
    private void Awake() {
        Cursor.SetCursor(cursor, Vector2.one * 2f, CursorMode.Auto);
    }

    public void Click() {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void Exit() {
        if (Application.isPlaying) {
            UnityEditor.EditorApplication.ExitPlaymode();
            return;
        }
        Application.Quit();
    }
}