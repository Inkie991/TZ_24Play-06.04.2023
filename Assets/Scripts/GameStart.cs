using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Update()
    {
        
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            EventManager.Broadcast(Events.GameStartedEvent);
            this.enabled = false;
        }
    }
}
