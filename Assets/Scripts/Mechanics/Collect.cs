using UnityEngine;

public class Collect : MonoBehaviour
{
    public bool isCollectingCubes = false;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollectCube"))
        {
            if (isCollectingCubes)
            {
                Enter enter = collision.gameObject.GetComponent<Enter>();
                if (enter.entered == false)
                {
                    Managers.Gameplay.score++;
                    Managers.Cubes.AddCube(collision.transform);
                }

                enter.entered = true;
            }
        }
    
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (transform.position.y - collision.transform.position.y < 0.2f) Managers.Cubes.RemoveCube(transform);
        }
    }
}
