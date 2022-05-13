using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractionHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.TryGetComponent<GroundTile>(out GroundTile target))
                {
                    Debug.Log($"It's working! touched {target}");
                    Destroy(target.gameObject);
                }
                else
                {
                    Debug.Log("nope");
                }
            }
            else
            {
                Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
    }
}
