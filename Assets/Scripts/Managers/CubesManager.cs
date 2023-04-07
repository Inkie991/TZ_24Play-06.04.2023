using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    [SerializeField] private Transform _cubesHolder;
    public GameObject MainCube { get; private set; }
    private List<Transform> _cubes;
    public void Startup()
    {
        UnityEngine.Debug.Log("Gameplay manager starting...");

        MainCube = GameObject.Find("StartCube").transform.GetChild(0).gameObject;
        MainCube.GetComponent<Collect>().isCollectingCubes = true;
        _cubes = new List<Transform>();
        AddCube(MainCube.transform);
        
        Status = ManagerStatus.Started;
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
        Debug();
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

    private void Debug()
    {
        foreach (var cube in _cubes)
        {
            UnityEngine.Debug.Log(cube.name);
        }
    }

}
