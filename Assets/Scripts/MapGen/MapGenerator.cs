using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Gen settings
    [Header("Generation Settings")]
    public int pixWidth = 0;
    public int pixHeight = 0;
    public float scale;
    #endregion

    [Header("Tree settings")]
    public float treeMin;
    public float treeMax;
    public GameObject treeObj;

    [Header("Rock Settings")]
    public float rockMin;
    public float rockMax;
    public GameObject rockObj;

    public void generateTrees()
    {
        //Debug.Log("Generating!");
        float randomorg = Random.Range(0, 100);

        float y = 0.0f;
        while (y < pixHeight)
        {
            float x = 0.0f;
            while (x < pixWidth)
            {
                float xCoords = randomorg + x / pixWidth * scale;
                float yCoord = randomorg + y / pixHeight * scale;
                float sample = Mathf.PerlinNoise(xCoords, yCoord);
                if (sample == Mathf.Clamp(sample, treeMin, treeMax))
                {
                    Instantiate(treeObj, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
                }
                else if(sample == Mathf.Clamp(sample, rockMin, rockMax))
                {
                    Instantiate(rockObj, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
                }
                x++;

            }
            y++;
        }
    }
}
