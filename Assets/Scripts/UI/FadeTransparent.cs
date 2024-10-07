using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Utilities;

public class FadeTransparent : MonoBehaviour {
    public void Awake() {
        StartCoroutine(Fade(GetComponent<Image>()));
    }

    private IEnumerator Fade(Image image) {
        float timer = 0f;
        while (timer <= 1f) {
            timer += Time.fixedDeltaTime;
            image.color = Color.Lerp(Color.black, Color.clear, timer);
            yield return Yielders.WaitForFixedUpdate;
        }
        image.color = Color.clear;
    }
}