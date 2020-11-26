using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSampling : MonoBehaviour
{
    public int pixelPerGrid;
    public int resolution;

    private Perlin map;
    private Texture2D tex;
    private float t = 0.0f;

    private void OnEnable()
    {

        //initialize noise map
        map = new Perlin(
            (int)(Mathf.Ceil(resolution / (float)pixelPerGrid)), //width
            3 //dimensions
        );

        tex = new Texture2D(resolution, resolution, TextureFormat.RGBA32, true);
        tex.name = "Sample Perlin Noise";

        GetComponent<MeshRenderer>().material.mainTexture = tex;
        
        FillTexture();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        FillTexture();
    }

    // update tex's content
    private void FillTexture()
    {
        for(int i = 0; i < resolution; i++)
        {
            for(int j = 0; j < resolution; j++)
            {
                //calculate noise
                VecN marker = new VecN(
                    new float[]
                    {
                        i / (float) pixelPerGrid,
                        j / (float) pixelPerGrid,
                        t
                    }
                    );
                float n = map.Noise(marker);

                //convert to color format
                byte col = (byte)(n * 255);
                tex.SetPixel(i, j, new Color32(col, col, col, 255));

            }
        }
        tex.Apply();
    }

}
