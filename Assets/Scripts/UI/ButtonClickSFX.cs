using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSFX : MonoBehaviour {
    [SerializeField] private SFXEmitter emitter;
    [SerializeField] private SoundEffectType sfx = SoundEffectType.UIClick;
    [SerializeField] private Button button;
    void Start() {
        emitter = GetComponent<SFXEmitter>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => emitter.Play(sfx));
    }
}