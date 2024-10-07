using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Utilities;

public class LoadLevel : MonoBehaviour {
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Image fade;
    private void Awake() {
        Cursor.SetCursor(cursor, Vector2.one * 2f, CursorMode.Auto);
    }

    public void Click() {
        StartCoroutine(LoadLevel1());
    }

    public void Exit() {
        StartCoroutine(ExitGame());
    }

    private IEnumerator LoadLevel1() {
        float timer = 0f;
        while (timer <= 1f) {
            timer += Time.fixedDeltaTime;
            fade.color = Color.Lerp(Color.clear, Color.black, timer);
            yield return Yielders.WaitForFixedUpdate;
        }
        fade.color = Color.black;
        SceneManager.LoadScene(1);
    }

    private IEnumerator ExitGame() {
        yield return Yielders.WaitForSeconds(0.1f);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}