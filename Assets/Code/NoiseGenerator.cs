using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoiseGenerator  : MonoBehaviour
{ 

// Create a texture and fill it with Perlin noise.
// Try varying the xOrg, yOrg and scale values in the inspector
// while in Play mode to see the effect they have on the noise.
    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;

    void Start()
    {

    }

    public float GetSampleAt(Vector2 pos)
    {
            
        float sample = Mathf.PerlinNoise(pos.x, pos.y);
        Debug.Log(sample);
        return sample;                
            
    }    
    
    public float GetSample()
    {
            
         float sample = Mathf.PerlinNoise(xOrg, yOrg);
         return sample;                
            
    
    }

    void Update()
    {
    }
}

