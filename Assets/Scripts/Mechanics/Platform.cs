using UnityEngine;

public class Platform : MonoBehaviour
{
    private const int LeftBorder = -2;
    private const int RightBorder = 2;
    private const int CollectCubeMaxZPos = 6;
    private const int CollectCubeMinZPos = -8;
    private const float CollectCubeYOffset = 0.5f;
    
    
    [SerializeField] private GameObject wallCubePrefab;
    [SerializeField] private Transform wallHolder;
    [SerializeField] private GameObject collectCubePrefab;
    [SerializeField] private Transform collectHolder;
    public bool isStartingPlatform;

    private int _maxWallHeight;
    private int _maxCollectCubesCount;
    private int _collectCubesCount;

    void Start()
    {
        if (isStartingPlatform) Construct();
    }

    public void Construct()
    {
        SetParameters();
        Build();
    }

    private void SetParameters()
    {
        if (isStartingPlatform)
        {
            _maxCollectCubesCount = 3;
            _collectCubesCount = _maxCollectCubesCount;
            _maxWallHeight = 4;
        }
        else
        {
            _maxCollectCubesCount = 5;
            _collectCubesCount = Random.Range(1, _maxCollectCubesCount + 1);
            _maxWallHeight = Managers.Cubes.GetCubesCount() + _collectCubesCount;
        }
    }

    private void Build()
    {
        SpawnCollectable();
        BuildWall();
    }

    private void SpawnCollectable()
    {
        float zOffset = (float)(CollectCubeMaxZPos - CollectCubeMinZPos) / (_collectCubesCount - 1);
        
        for (float i = CollectCubeMinZPos; i <= CollectCubeMaxZPos; i += zOffset)
        {
            int chance = 20;
            bool spawned = false;
            for (int j = LeftBorder; j <= RightBorder; j++)
            {
                float fortune = Random.Range(0, 101);
                if (fortune < chance && !spawned)
                {
                    var cube = Instantiate(collectCubePrefab, collectHolder);
                    cube.transform.localPosition = new Vector3(j, CollectCubeYOffset, i);
                    spawned = true;
                }
                else
                {
                    chance += 20;
                }
            }
        }
    }

    private void BuildWall()
    {
        for (int i = LeftBorder; i <= RightBorder; i++)
        {
            int height = Random.Range(1, _maxWallHeight);
            for (int j = 0; j < height; j++)
            {
                var wallPiece = Instantiate(wallCubePrefab, wallHolder);
                wallPiece.transform.localPosition = new Vector3(i, j, 0);
            }
        } 
    }
}
