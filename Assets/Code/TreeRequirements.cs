using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRequirements : MonoBehaviour
{

    public float minMoistRequired;
    
    [SerializeField]
    private TreeType type;

    private Dictionary<TreeType, Vector2Int> _interactionDirections;
    private GameObject _myGroundTile;

    public TreeType GetTreeType()
    {
        return type;
    }

}

public enum TreeType
{
    Beobab,
    Accacia,
    Cactus,
    Aloe,
    Palm
}
