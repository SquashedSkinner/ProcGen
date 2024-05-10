using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap, FalloffMap, Heatmap};
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

	public TerrainType[] SatelliteRegions;
	public TerrainType[] ParchmentRegions;
	public TerrainType[] regions;
	public BiomeType[] biomes;

	float[,] SquareFalloff;
	public float[,] HeatMap;

	void awake()
	{
		
		SquareFalloff = FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);

	}

	public void GenerateMap() {
	// Changes between the available visual styles

	float EquatorLine = mapHeight / 2;

	if (aesthetic == Aesthetic.Satellite)
	{
		regions = SatelliteRegions;
		Debug.Log("Setting Selected");
	}
	else if (aesthetic == Aesthetic.Parchment)
	{
		regions = ParchmentRegions;
	}



		if (RandomSeed)
		{
        	seed = Random.Range(1,999999);
		}

		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
		WorldData[,] worldData = new WorldData[mapWidth,mapHeight];
		Color[] colourMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				if (useFalloff)
				{
					noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] - SquareFalloff[x,y]);
					worldData[x,y].Height = noiseMap[x,y];
					worldData[x,y].Temperature = DistanceFromEquator(EquatorLine, y);
				}
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapWidth + x] = regions [i].colour;
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

	void OnValidate() {
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

	float DistanceFromEquator(float EquatorLine, float y)
	{
		float x = y / EquatorLine;
		if (x < 0)
		{
			x = x * -1;
		}
		return x;
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}

[System.Serializable]
public struct BiomeType {
	public string BiomeName;
	public float Temperature;
	public float Moisture;
	public Color colour;
}

[System.Serializable]
public struct WorldData {
	public float Height;
	public float Temperature;
	public float Moisture;
	public Color Colour;
}
