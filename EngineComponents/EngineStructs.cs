using System.Numerics;

namespace Engine;
public struct Transform
{
	public Vector2 Position;
	public float Rotation;

	public bool is_static = false;
	// Matrix3x2 translateToLocal => Matrix3x2.CreateTranslation(Position);
	
	public Transform(Vector2 position, float rotation = 0f)
	{
		Position = position;
		Rotation = rotation;
	}

	public Transform()
	{
		Position = Vector2.Zero;
		Rotation = 0f;
	}
}

public struct Color
{
	public byte R = 0;
	public byte G = 0;
	public byte B = 0;
	public byte A = 255;

	public Color(byte r, byte g, byte b, byte a = 255)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public static Color White 	=> new Color(255,255,255,255);
	public static Color Black 	=> new Color(0,0,0,255);
	public static Color Red 	=> new Color(255,0,0,255);
	public static Color Green	=> new Color(0,255,0,255);
	public static Color Blue	=> new Color(0,255,0,255);
}