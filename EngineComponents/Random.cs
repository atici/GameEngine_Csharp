using System.Security.Cryptography;

namespace Engine;
public static class Random {

	public static int Range(int max) => Range(0, max);
	public static int Range(int min, int max) {
		return RandomNumberGenerator.GetInt32(min, max);
	}

	public static float Range(float max) => Range(0.0f, max);
	public static float Range(float min, float max, int depth = 3) {
		depth = 10^depth;
		min *= depth;
		max = max*depth - min;
		
		int result = RandomNumberGenerator.GetInt32((int)max);
		return (result + min) / depth;
	}
}