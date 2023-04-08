using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Image _background;
    private Color startColor = new Color(0.7686275f, 0.7686275f, 0.7686275f, 0.4313726f);
    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeAndStart());
        }
    }

    public void SetColor()
    {
        _background.color = startColor;
    }

    private IEnumerator FadeAndStart()
    {
        _background.DOColor(Color.clear, 0.5f);
        yield return new WaitForSeconds(0.5f);
        EventManager.Broadcast(Events.GameStartedEvent);
    }
}
