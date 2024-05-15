using UnityEngine;

public class LevelPersistance : MonoBehaviour
{
    // Level Generation Data
    [SerializeField]
    private GameObject PerlinIslandGenerator;
    [SerializeField]
    private GameObject WeatherGenerator;

    private float mapWidth;
    private float mapHeight;

    private float noiseScale;
    private int Octaves;
    private float Persistance;
    private float Lacunarity;

    private int Seed;

    private float Time;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SaveGenerationData()
    {
        GetWidth();
        GetHeight();
        GetNoiseScale();
        GetOctaves();
        GetPersistance();
        GetLacunarity();
        GetSeed();

        Debug.Log(Seed);

    }

    public void SaveTimeData()
    {
        GetTime();
        Debug.Log(Time);
    }

    public float GetWidth()
	{
		mapWidth = PerlinIslandGenerator.GetComponent<Parchment>().SetWidth();
        return mapWidth;
	}

	public float GetHeight()
	{
        mapHeight = PerlinIslandGenerator.GetComponent<Parchment>().SetHeight();
		return mapHeight;
	}

	public float GetNoiseScale()
	{
        noiseScale = PerlinIslandGenerator.GetComponent<Parchment>().SetNoiseScale();
		return noiseScale;
	}

	public int GetOctaves()
	{
        Octaves = PerlinIslandGenerator.GetComponent<Parchment>().SetOctaves();
		return Octaves;
	}

	public float GetPersistance()
	{
        Persistance = PerlinIslandGenerator.GetComponent<Parchment>().SetPersistance();
		return Persistance;
	}

	public float GetLacunarity()
	{
        Lacunarity = PerlinIslandGenerator.GetComponent<Parchment>().SetPersistance();
		return Lacunarity;
	}

	public int GetSeed()
	{
        Seed = PerlinIslandGenerator.GetComponent<Parchment>().SetSeed();
		return Seed;
	}

    public float GetTime()
    {
        Time = WeatherGenerator.GetComponent<WeatherGenerator>().SetTime();
		return Time;
    }

}
