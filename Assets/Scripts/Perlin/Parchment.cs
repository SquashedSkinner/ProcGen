using UnityEngine;
using System.Collections;

public class Parchment : MonoBehaviour {

	public GameObject ParchmentMap;
	public Animator BookAnimator;

	public GameObject ParentPage;
	public enum DrawMode {NoiseMap, ColourMap, FalloffMap};
	public DrawMode drawMode;
	public enum Aesthetic {Parchment};
	public Aesthetic aesthetic;

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;


	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public bool RandomSeed;
	public int seed;
	public Vector2 offset;

	public bool autoUpdate;
	public bool useFalloff;

	public TerrainType[] ParchmentRegions;
	public TerrainType[] regions;


	float[,] SquareFalloff;
	public float[,] HeatMap;

	void Awake()
	{	
		SquareFalloff = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
	}

    void Start()
    {
        GenerateMap();
		BookAnimator.ResetTrigger("RegenComplete");
    }

	public void GenerateMap()
	{
		
		// Changes between the available visual styles
			regions = ParchmentRegions;

        	seed = Random.Range(1,999999);

		// Noisemap creates island shapes
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
       
        Color[] colourMap = new Color[mapWidth * mapHeight];

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				if (useFalloff)
				{
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - SquareFalloff[x, y]);
                }

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }

				BookAnimator.SetTrigger("RegenComplete");
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
		} else if (drawMode == DrawMode.FalloffMap) {
			display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight)));
		} 

		ParchmentMap.transform.parent = ParentPage.transform;
		
		
		
	}

	void OnValidate()
	{
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}

		SquareFalloff = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
	}

	public float SetWidth()
	{
		return mapWidth;
	}

	public float SetHeight()
	{
		return mapHeight;
	}

	public float SetNoiseScale()
	{
		return noiseScale;
	}

	public int SetOctaves()
	{
		return octaves;
	}

	public float SetPersistance()
	{
		return persistance;
	}

	public float SetLacunarity()
	{
		return lacunarity;
	}

	public int SetSeed()
	{
		return seed;
	}

	
	
}



