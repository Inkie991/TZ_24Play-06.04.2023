using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratorManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    
    [SerializeField] private GameObject platformPrefab;
    private Transform lastPlatform;
    
    public void Startup()
    {
        Debug.Log("Generator manager starting...");
        
        EventManager.AddListener<PassColliderEvent>(SpawnPlatform);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Status = ManagerStatus.Started;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Preloader")
        {
            GameObject[] startPlatforms = GameObject.FindGameObjectsWithTag("Platform");
            lastPlatform = startPlatforms[0].transform;

            foreach (var platform in startPlatforms)
            {
                if (platform.transform.position.z > lastPlatform.position.z)
                {
                    lastPlatform = platform.transform;
                }
            }
        }
    }

    private void SpawnPlatform(PassColliderEvent evt)
    {
        Vector3 spawnPos = new Vector3(0, -100, lastPlatform.position.z + 30);
        var platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        platform.GetComponent<Platform>().Construct();
        lastPlatform = platform.transform;
        Invoke("PlatformAnimation", 0.5f);

    }

    private void PlatformAnimation()
    {
        lastPlatform.DOMoveY(0, 1f);
    }
}
