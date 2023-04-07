using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(GameplayManager))]
[RequireComponent(typeof(CubesManager))]

public class Managers : MonoBehaviour
{
    public static GameplayManager Gameplay { get; private set; }
    public static CubesManager Cubes { get; private set; }

    private List<IGameManager> _startSequence;

    void Awake()
    {
        DOTween.Init();
        Application.targetFrameRate = 60;
        Gameplay = GetComponent<GameplayManager>();
        Cubes = GetComponent<CubesManager>();

        _startSequence = new List<IGameManager>
        {
            Gameplay,
            Cubes
        };

        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartupManagers());
    }

    private IEnumerator<Object> StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.Status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null;
        }

        Debug.Log("All managers started up"); 
    }
}
