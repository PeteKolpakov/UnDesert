using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractionHandler : MonoBehaviour
{

    private PlaceTree tree;

    private void Start()
    {
        tree = FindObjectOfType<PlaceTree>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                if (hitInfo.transform.gameObject.TryGetComponent<GroundTile>(out GroundTile target))
                {
                    if (!tree.allTrees.Contains(new Vector2(target.transform.position.x, target.transform.position.z)))
                    {
                        target.SetTree(tree.PlantTree(target.transform.position, target.transform, target._hydration));                      
                    }
                    else print("Tile Already Populated");
                }
            }
        }
    }
}
