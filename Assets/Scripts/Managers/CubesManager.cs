using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubesManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    private Transform _cubesHolder;
    public GameObject MainCube { get; private set; }
    private List<Transform> _cubes;
    public void Startup()
    {
        Debug.Log("Cubes manager starting...");
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        Status = ManagerStatus.Started;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            _cubesHolder = GameObject.FindWithTag("CubeHolder").transform;
            MainCube = _cubesHolder.GetChild(0).GetChild(0).gameObject;
            MainCube.GetComponent<Collect>().isCollectingCubes = true;
            _cubes = new List<Transform>();
            AddCube(MainCube.transform);
        }
    }

    public void AddCube(Transform cube)
    {
        _cubes.Add(cube); 
        ReconstructPlayer(cube);
    }

    public void RemoveCube(Transform cube)
    {
        cube.parent.SetParent(null);
        if (cube == GetBottomCube())
        {
            cube.GetComponent<Collect>().isCollectingCubes = false;
        }
        _cubes.RemoveAt(_cubes.IndexOf(cube));
        SetMainCube();
    }

    private Transform GetBottomCube()
    {
        return _cubes[0];
    }

    private void ReconstructPlayer(Transform cube)
    {
        if (_cubes.Count > 1) cube.position = _cubes[^2].position + Vector3.up;
        cube.parent.SetParent(_cubesHolder);
        Managers.Gameplay.Stickman.position = cube.position + (Vector3.up * 1.2f);
    }

    private void SetMainCube()
    {
        MainCube = GetBottomCube().gameObject;
        MainCube.GetComponent<Collect>().isCollectingCubes = true;
    }

}
