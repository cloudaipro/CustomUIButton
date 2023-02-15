using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomUIButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    public class CustomUIEvent : UnityEvent { }
    public CustomUIEvent OnEvent;
    public Image backgroundGraphic;

    public bool buttonEnabled = true;

    public Color defaultColor = Color.white;
    public Color hoverColor = Color.white;
    public Color pressedColor = Color.white;
    public Color disabledColor = Color.gray;

    public Vector3 defaultScale = Vector3.one;
    public Vector3 hoverScale = Vector3.one;
    public Vector3 pressedScale = Vector3.one;

    private void Awake()
    {
        backgroundGraphic.color = (buttonEnabled) ? defaultColor : disabledColor;
        transform.localScale = defaultScale;           
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!buttonEnabled) return;
        StartCoroutine(Transition(hoverScale, hoverColor, 0.25f));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Transition(defaultScale, defaultColor, 0.25f));
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(Transition(pressedScale, pressedColor, 0.25f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Transition(hoverScale, hoverColor, 0.25f));
    }


    public IEnumerator Transition(Vector3 newSize, Color newColor, float transitionTime)
    {
        float timer = 0;
        Vector3 startSize = transform.localScale;
        Color startColor = backgroundGraphic.color;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;

            yield return null;

            transform.localScale = Vector3.Lerp(startSize, newSize, timer / transitionTime);
            backgroundGraphic.color = Color.Lerp(startColor, newColor, timer / transitionTime);
        }
    }

}
