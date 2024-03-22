using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Gen settings
    [Header("Generation Settings")]
    public int pixWidth = 0;
    public int pixHeight = 0;
    private Texture2D noiseTex;
    private Color[] pix;
    public float scale;
    #endregion

    [Header("Tree settings")]
    public float treeMin;
    public float treeMax;
    public GameObject treeObj;

    public void generateTrees()
    {
        Debug.Log("Generating!");
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        float randomorg = Random.Range(0, 100);

        float y = 0.0f;
        while (y < noiseTex.height)
        {
            float x = 0.0f;
            while (x < noiseTex.height)
            {
                float xCoords = randomorg + x / noiseTex.width * scale;
                float yCoord = randomorg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoords, yCoord);
                if (sample == Mathf.Clamp(sample, treeMin, treeMax))
                {
                    Instantiate(treeObj, new Vector3(x + .5f, y + .5f, 0), Quaternion.identity);
                }
                x++;

            }
            y++;
        }
    }
}
