using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public bool isMoving; //{ get; set; }

    void Start()
    {
        isMoving = true;
    }
    void LateUpdate()
    {
        if (Managers.Gameplay.gameStatus == GameStatus.Initilaze && isMoving)
        {
            transform.position += Vector3.forward * (7 * Time.deltaTime);
            //_rigidbody.velocity = Vector3.forward* (70 * Time.deltaTime);
        }
    }
}
