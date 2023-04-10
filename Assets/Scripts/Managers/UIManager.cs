using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    [SerializeField] private GameObject _canvasPrefab;
    private GameObject _startScreen;
    private GameObject _endScreen;
    
    public void Startup()
    {
        Debug.Log("UI manager starting...");

        EventManager.AddListener<GameStartedEvent>(OnGameStarted);
        EventManager.AddListener<PlayerLoseEvent>(OnPlayerLose);
        SceneManager.sceneLoaded += OnSceneLoaded;

        var canvas = Instantiate(_canvasPrefab);
        DontDestroyOnLoad(canvas);
        _startScreen = canvas.transform.GetChild(0).gameObject;
        _endScreen = canvas.transform.GetChild(1).gameObject;

        Status = ManagerStatus.Started;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Preloader")
        {
            _endScreen.SetActive(false);
            _startScreen.SetActive(true);
            _startScreen.GetComponent<GameStart>().SetColor();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    void OnPlayerLose(PlayerLoseEvent evt)
    {
        _endScreen.SetActive(true);
    }

    void OnGameStarted(GameStartedEvent evt)
    {
        _startScreen.SetActive(false);
    }
}
