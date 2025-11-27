using System.Numerics;
using SDL3;

public struct Transform
{
	public Vector2 Position;
	public float Rotation;
	public Vector2 Size;

	public bool is_static = false;
	// Matrix3x2 translateToLocal => Matrix3x2.CreateTranslation(Position);

	public SDL.FRect FRect => 
		new SDL.FRect {
			X = Position.X,
			Y = Position.Y,
			H = Size.X,
			W = Size.Y
		};

	public Transform(Vector2 position, Vector2 size, float rotation = 0f)
	{
		Position = position;
		Rotation = rotation;
		Size = size;
	}

	public Transform(Vector2 position, float rotation = 0f)
	{
		Position = position;
		Rotation = rotation;
		Size = Vector2.Zero;
	}

	public Transform()
	{
		Position = Vector2.Zero;
		Size = Vector2.Zero;
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
}