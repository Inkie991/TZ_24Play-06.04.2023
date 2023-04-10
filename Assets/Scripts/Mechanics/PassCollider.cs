using UnityEngine;

public class PassCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stickman"))
        {
            EventManager.Broadcast(Events.PassColliderEvent);
            Invoke("DestroyPlatform", 1f);
        }
    }

    private void DestroyPlatform()
    {
        Destroy(transform.parent.gameObject);
    }
}
