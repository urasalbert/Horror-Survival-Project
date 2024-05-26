using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 100f;

    void Update()
    {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance, ~LayerMask.GetMask("Trigger")))
            {
                if (hit.collider != null)
                {
                    CoilHeadEnemy enemy = hit.collider.gameObject.GetComponent<CoilHeadEnemy>();
                    if (enemy != null)
                    {
                        enemy.OnPlayerSighted();
                    }
                }
            }
        }

    }

