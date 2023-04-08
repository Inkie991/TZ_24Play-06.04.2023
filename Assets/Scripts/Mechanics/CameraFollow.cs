using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _stickman;
    [SerializeField] private float smoothSpeed;
    private Vector3 _offset;
    

    void Start()
    {
        _offset = transform.position - _stickman.position;
    }

    private void Update()
    {
        Vector3 targetPosition = _stickman.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
