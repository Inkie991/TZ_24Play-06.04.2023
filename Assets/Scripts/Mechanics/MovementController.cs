using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementController: MonoBehaviour
{
    [SerializeField] private float speed;

    private bool _isLocked;
    private bool _isGameStarted;
    private Sequence _sequence;

    private float _rightBorderX = 2f;
    private float _leftBorderX = -2f;

    struct InputPoint
    {
        public static float PosX = 0;
        public static float Sign = 0;
        public static float Speed = 0;
        public static float Distance = 0;

        public static void Reset()
        {
            PosX = 0;
            Sign = 0;
            Speed = 0;
            Distance = 0;
        }
    }
    
    private void Awake()
    {
        EventManager.AddListener<GameStartedEvent>(OnGameStarted);
        EventManager.AddListener<PlayerLoseEvent>(OnPlayerLose);
        _isLocked = true;
        _sequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartedEvent>(OnGameStarted);
        EventManager.RemoveListener<PlayerLoseEvent>(OnPlayerLose);
    }

    void Update()
    {
        if (_isLocked) return;

        HandleInput();

        if (
            EventSystem.current.IsPointerOverGameObject() ||
            (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        ) return;
            
        HandleMovement();
        
        transform.Translate(new Vector3(1.5f * InputPoint.Speed * Time.deltaTime,0,1 * speed * Time.deltaTime));
        
        CheckStop();

        if (_isGameStarted || InputPoint.Sign == 0) return;
             
         _isGameStarted = true;
    }
    
    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    InputPoint.PosX = touch.position.x;
                    break;
                case TouchPhase.Moved or TouchPhase.Stationary or TouchPhase.Ended:
                    InputPoint.Distance = touch.position.x - InputPoint.PosX;
                    InputPoint.PosX = touch.position.x;
                    InputPoint.Sign = Sign(InputPoint.Distance);
                    break;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            InputPoint.Sign = Sign(Input.GetAxis("Mouse X"));
            InputPoint.PosX = Input.GetAxis("Mouse X");
        }
    }
    
    void HandleMovement()
    {
        if (!Application.isMobilePlatform || Mathf.Abs(InputPoint.Distance) > 2f)
        {
            var delta = InputPoint.Distance / Screen.width * 100;
            var movementValue = delta;

            if (movementValue == 0) return;

            InputPoint.Speed = movementValue;

            if (_sequence.active)
                 _sequence.Kill();
            
            //transform.Translate(new Vector3(1.5f * InputPoint.Speed * Time.deltaTime,0,1 * speed * Time.deltaTime));
        }
    }
    
    void CheckStop()
    {
        if (InputPoint.Sign == 0) return;
        if (!Application.isMobilePlatform && !Input.GetMouseButtonUp(0)) return;
        if (Application.isMobilePlatform && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;
            if (touch.phase != TouchPhase.Ended) return;
        }

        if (transform.position.x > 1.9f || transform.position.x < -1.9f)
        {
            float posX = transform.position.x;
            float endValue = 0;

            if (transform.position.x > 1.9 && InputPoint.Speed > 0) endValue = _rightBorderX;
            if (transform.position.x < -1.9 && InputPoint.Speed < 0) endValue = _leftBorderX;
            
            
            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(() => posX, value =>
            {
                posX = value;
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
            }, endValue, 0.2f));
            _sequence.Play();
        }

        InputPoint.Reset();
    }
    
    private int Sign(float value)
    {
        if (value == 0) return 0;
        return (int) Mathf.Sign(value);
    }

    void OnGameStarted(GameStartedEvent evt) => UnlockMovement();
    void OnPlayerLose(PlayerLoseEvent evt) => LockMovement();

    private void LockMovement()
    {
        _isLocked = true;
        InputPoint.Reset();
    }
    
    void UnlockMovement()
    {
        _isLocked = false;
    }

}
