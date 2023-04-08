using UnityEngine;

public class Stickman : MonoBehaviour
{
    private CapsuleCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            EventManager.Broadcast(Events.PlayerLoseEvent);
        }
    }
}
