using UnityEngine;

public class Stickman : MonoBehaviour
{
    private CapsuleCollider _collider;
    
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            EventManager.Broadcast(Events.PlayerLoseEvent);
        }
    }
}
