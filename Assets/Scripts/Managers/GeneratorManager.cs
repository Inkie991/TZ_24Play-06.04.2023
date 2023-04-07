using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    [SerializeField] private GameObject TrackGroundPrefab;
    public void Startup()
    {
        Debug.Log("Generator manager starting...");
        
        //EventManager.AddListener<PassColliderEvent>(//Spawn);

        Status = ManagerStatus.Started;
    }
}
