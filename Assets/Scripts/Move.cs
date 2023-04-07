using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public float horizontalSpeed;
    

    void Start()
    {
        isMoving = true;
    }
    void LateUpdate()
    {
        if (Managers.Gameplay.gameStatus == GameStatus.Initilaze && isMoving)
        {
            //transform.position += new Vector3(1.5f * horizontalSpeed * Time.deltaTime,0,1 * speed * Time.deltaTime);
            transform.Translate(new Vector3(1.5f * horizontalSpeed * Time.deltaTime,0,1 * speed * Time.deltaTime));
        }
    }
}
