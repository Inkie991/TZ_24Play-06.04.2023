using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }
    
    public GameObject Player { get; private set; }
    public Transform Stickman { get; private set; }
    public GameStatus gameStatus;
    public int score;

    public void Startup()
    {
        Debug.Log("Gameplay manager starting...");

        gameStatus = GameStatus.Initilaze;
        SceneManager.sceneLoaded += OnSceneLoaded;

        Status = ManagerStatus.Started;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Preloader")
        {
            Player = GameObject.FindWithTag("Player");
            Stickman = Player.transform.GetChild(0).GetChild(0);
        }
    }

    void Update()
    { 
        switch (gameStatus)
        {
            case GameStatus.Initilaze:
                //Player.transform.position += Vector3.forward * (7 * Time.deltaTime);
                //Managers.Cubes.MainCube.transform.position += Vector3.forward * (7 * Time.deltaTime);
                break;
            case GameStatus.Start:      
                break;
            case GameStatus.Stay:
                break;
            case GameStatus.Finish:
                break;
            case GameStatus.Next:
                break;
            default:
                break;
        }
    }

    
}
