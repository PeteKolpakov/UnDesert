using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTree : MonoBehaviour
{
    public GameObject _selected;
    private GameObject _highlight;

    private float[] rotation = { 0f, 90f, -90f, 180f };

    public float OffsetHeight = 1f;

    private float minMoistureRequired;

    public List<Vector2> allTrees;

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (_selected == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (hit.transform.tag != ("Tree")) return;
                    else _selected = hit.collider.gameObject;
                }
            }
        }
    }

    public TreeRequirements PlantTree(Vector3 tilePos, Transform tile, float moistureLevel)
    {
        if (_selected != null)
        {
            var TreeInfo = _selected.GetComponent<TreeRequirements>();
            minMoistureRequired = TreeInfo.minMoistRequired;


            if (moistureLevel >= minMoistureRequired)
            {
                var tree = Instantiate(_selected, new Vector3(tilePos.x, tilePos.y + OffsetHeight, tilePos.z), Quaternion.Euler(0f, rotation[Random.Range(0, rotation.Length)], 0f), tile.transform);
                allTrees.Add(new Vector2(tree.transform.position.x, tree.transform.position.z));
                _selected = null;
                return TreeInfo;

            }
            else
            {
                print("Invalid Tile");
                return null;
            }
        }
        return null;
    }
    public RaycastHit CastRay()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

        return hit;
    }
}
