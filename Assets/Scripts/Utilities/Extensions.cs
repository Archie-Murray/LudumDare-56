using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public static class Extensions {
    public static IEnumerable<Enum> GetFlags(this Enum flags) {
        return Enum.GetValues(flags.GetType()).Cast<Enum>().Where(flags.HasFlag);
    }
    

    /// <summary>
    /// Gets, or adds if doesn't contain a component
    /// </summary>
    /// <typeparam name="T">Component Type</typeparam>
    /// <param name="gameObject">GameObject to get component from</param>
    /// <returns>Component</returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
        T component = gameObject.GetComponent<T>();
        if (!component) {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    
    /// <summary>
    /// Returns true if a GameObject has a component of type T
    /// </summary>
    /// <typeparam name="T">Component Type</typeparam>
    /// <param name="gameObject">GameObject to check for component on</param>
    /// <returns>If the component is present</returns>
    public static bool Has<T>(this GameObject gameObject) where T : Component { 
        return gameObject.GetComponent<T>() != null; 
    }

    public static T OrNull<T>(this T obj) where T : UnityEngine.Object {
        return obj ? obj : null;
    }

    public static void FlashColour(this SpriteRenderer spriteRenderer, Color colour, float time, MonoBehaviour monoBehaviour) {        
        monoBehaviour.StartCoroutine(Flash(spriteRenderer, colour, time));
    }

    private static IEnumerator Flash(SpriteRenderer spriteRenderer, Color colour, float time) { 
        Color original = spriteRenderer.material.color;
        spriteRenderer.material.color = colour;
        yield return new WaitForSeconds(time);
        spriteRenderer.material.color = original;
    }

    public static void FadeCanvas(this CanvasGroup canvasGroup, float duration, bool fadeToTransparent, MonoBehaviour monoBehaviour) {
        monoBehaviour.StartCoroutine(CanvasFade(canvasGroup, duration, fadeToTransparent));
    }

    private static IEnumerator CanvasFade(CanvasGroup canvasGroup, float duration, bool fadeToTransparent) {
        CountDownTimer timer = new CountDownTimer(duration);
        timer.Start();
        while (timer.IsRunning) {
            canvasGroup.alpha = fadeToTransparent ? timer.Progress() : 1f - timer.Progress();
            timer.Update(Time.fixedDeltaTime);
            yield return Yielders.WaitForFixedUpdate;
        }
        if (fadeToTransparent) {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0f;
        } else {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1f;
        }
    }
}