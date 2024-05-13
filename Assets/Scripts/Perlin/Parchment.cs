using UnityEngine;
using System.Collections;

public class Parchment : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap, FalloffMap};
	public DrawMode drawMode;
	public enum Aesthetic {Satellite, Parchment};
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

	void awake()
	{
		
		SquareFalloff = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);

	}

    void OnStart()
    {
        GenerateMap();
    }

	public void GenerateMap()
	{
		// Changes between the available visual styles
			regions = ParchmentRegions;

		if (RandomSeed)
		{
        	seed = Random.Range(1,999999);
		}


		// Moisture Map creates a separate instance of perlin noise with a different seed.

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

	
}



