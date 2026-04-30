using UnityEngine;
using TMPEffects.Components;
using UnityEngine.InputSystem;

public class TutorialSkip : MonoBehaviour
{
    [SerializeField] private TMPWriter text;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame /*|| Touchscreen.current.primaryTouch.press.isPressed*/)
        {
            text.SkipWriter(true);
            this.enabled = false;
        }
    }
}
