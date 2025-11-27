using System.Diagnostics.Contracts;
using System.Numerics;
using SDL3;

public class GameObject
{
	public string name = "Gameobject";
	public Transform transform = new();

	internal virtual void Update(float delta){}
	internal virtual void Init(){}

	public GameObject()
	{
		MainGameLoop.UpdateEvent += (s, d) => Update(d); 
		Init();
	}

	~GameObject()
	{
		MainGameLoop.UpdateEvent -= (s, d) => Update(d); 
	}
}

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