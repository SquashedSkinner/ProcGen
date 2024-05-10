using UnityEngine;

public class CircularFalloffGenerator
{

public static float[,] GenerateFalloffMap(float Dim)
{
    int mapSize = (int)Dim;
    float[,] fallOffMap = new float[mapSize , mapSize];

    for (int x = 0; x < mapSize; x++)
    {
        for (int y = 0; y < mapSize; y++)
        {
           float innerRadius = 0f;
           float outerRadius = 1500.0f;
            int index = x * mapSize + y;
            float fallOff_A = x / (float)mapSize * 2 - 1;
            float fallOff_B = y / (float)mapSize * 2 - 1;

            float value = Mathf.Max(Mathf.Abs(fallOff_A), Mathf.Abs(fallOff_B));
            value = Evaluate(value);
            //fallOffMap[x,y] = RadialFalloff(value, 1024.0f, x, y, mapSize / 2f, mapSize / 2f);
            fallOffMap[x,y] = FeatheredRadialFallOff(value, innerRadius, outerRadius, x, y, mapSize / 2f, mapSize / 2f);
            //fallOffMap[x,y] = Evaluate(value);
        }
    }

    return fallOffMap;
}

public static float RadialFalloff(float value, float radius, int x, int y, float cx, float cy) 
{
  float dx = cx - x;
  float dy = cy - y;
  float distSqr = dx * dx + dy * dy;
  float radSqr = radius * radius;

  //if (distSqr > radSqr) return 0f;
  return value;
}

public static float FeatheredRadialFallOff(float value, float innerRadius, float outerRadius, int x, int y, float cx, float cy) 
{
  float dx = cx - x;
  float dy = cy - y;
  float distSqr = dx * dx + dy * dy;
  float iRadSqr = innerRadius * innerRadius;
  float oRadSqr = outerRadius * outerRadius;

  if (distSqr >= oRadSqr) return 0f;
  if (distSqr <= iRadSqr) return value;

  float dist = Mathf.Sqrt(distSqr);
  float t = Mathf.InverseLerp(innerRadius, outerRadius, dist);
  // Use t with whatever easing you want here, or leave it as is for linear easing
  return value * t;
}

static float Evaluate(float value)
{
    float a = 3;
    float b = 2.2f;

    return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
}

}
