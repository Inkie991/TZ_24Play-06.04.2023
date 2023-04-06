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
        Debug.Log("Gameplay manager starting...");

        MainCube = GameObject.Find("StartCube").transform.GetChild(0).gameObject;
        MainCube.GetComponent<Collect>().isCollectingCubes = true;
        _cubes = new List<Transform>();
        AddCube(MainCube.transform);
        
        Status = ManagerStatus.Started;
    }

    public void AddCube(Transform cube)
    {
        if (_cubes.Count > 0) ReconstructPlayer(cube);
        _cubes.Add(cube);
    }

    public void RemoveCube(Transform cube)
    {
        cube.parent.SetParent(null);
        if (cube == GetBottomCube())
        {
            cube.GetComponent<Collect>().isCollectingCubes = false;
        }
        SetMainCube();
        _cubes.Remove(cube);
    }

    public Transform GetTopCube()
    {
        return _cubes[^1];
    }

    public Transform GetBottomCube()
    {
        return _cubes[0];
    }

    public void ReconstructPlayer(Transform cube)
    {
        cube.position = GetTopCube().position + Vector3.up;
        cube.parent.SetParent(_cubesHolder);
        Managers.Gameplay.Stickman.position = cube.position + (Vector3.up * 1.1f);
    }

    public void SetMainCube()
    {
        MainCube = GetBottomCube().gameObject;
        MainCube.GetComponent<Collect>().isCollectingCubes = true;
    }

}
