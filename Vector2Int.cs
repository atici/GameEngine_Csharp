// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Representation of 2D vectors and points.
[StructLayout(LayoutKind.Sequential)]
public struct Vector2Int : IEquatable<Vector2Int>
{
	public int X
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get { return m_X; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set { m_X = value; }
	}


	public int Y
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get { return m_Y; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set { m_Y = value; }
	}

	private int m_X;
	private int m_Y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2Int(int x, int y)
	{
		m_X = x;
		m_Y = y;
	}

	// Set x and y components of an existing Vector.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(int x, int y)
	{
		m_X = x;
		m_Y = y;
	}

	// Access the /x/ or /y/ component using [0] or [1] respectively.
	public int this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			switch (index)
			{
				case 0: return X;
				case 1: return Y;
				default:
					throw new IndexOutOfRangeException(String.Format("Invalid Vector2Int index addressed: {0}!", index));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set
		{
			switch (index)
			{
				case 0: X = value; break;
				case 1: Y = value; break;
				default:
					throw new IndexOutOfRangeException(String.Format("Invalid Vector2Int index addressed: {0}!", index));
			}
		}
	}

	// Returns the length of this vector (RO).
	public float magnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return (int)Math.Sqrt((float)(X * X + Y * Y)); } }

	// Returns the squared length of this vector (RO).
	public int sqrMagnitude { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return X * X + Y * Y; } }

	// Returns the distance between /a/ and /b/.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Distance(Vector2Int a, Vector2Int b)
	{
		float diff_x = a.X - b.X;
		float diff_y = a.Y - b.Y;

		return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
	}

	// Returns a vector that is made from the smallest components of two vectors.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y)); }

	// Returns a vector that is made from the largest components of two vectors.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y)); }

	// Multiplies two vectors component-wise.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int Scale(Vector2Int a, Vector2Int b) { return new Vector2Int(a.X * b.X, a.Y * b.Y); }

	// Multiplies every component of this vector by the same component of /scale/.
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Scale(Vector2Int scale) { X *= scale.X; Y *= scale.Y; }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clamp(Vector2Int min, Vector2Int max)
	{
		X = Math.Max(min.X, X);
		X = Math.Min(max.X, X);
		Y = Math.Max(min.Y, Y);
		Y = Math.Min(max.Y, Y);
	}

	// Converts a Vector2Int to a [[Vector2]].
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator Vector2(Vector2Int v)
	{
		return new Vector2(v.X, v.Y);
	}
	public static Vector2Int operator -(Vector2Int v)
	{
		return new Vector2Int(-v.X, -v.Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator +(Vector2Int a, Vector2Int b)
	{
		return new Vector2Int(a.X + b.X, a.Y + b.Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator -(Vector2Int a, Vector2Int b)
	{
		return new Vector2Int(a.X - b.X, a.Y - b.Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator *(Vector2Int a, Vector2Int b)
	{
		return new Vector2Int(a.X * b.X, a.Y * b.Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator *(int a, Vector2Int b)
	{
		return new Vector2Int(a * b.X, a * b.Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator *(Vector2Int a, int b)
	{
		return new Vector2Int(a.X * b, a.Y * b);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2Int operator /(Vector2Int a, int b)
	{
		return new Vector2Int(a.X / b, a.Y / b);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
	{
		return lhs.X == rhs.X && lhs.Y == rhs.Y;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
	{
		return !(lhs == rhs);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object ?other)
	{
		if (other is Vector2Int v)
			return Equals(v);
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector2Int other)
	{
		return X == other.X && Y == other.Y;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int GetHashCode()
	{
		const int p1 = 73856093;
		const int p2 = 83492791;
		return (X * p1) ^ (Y * p2);
	}

	public static Vector2Int zero { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_Zero; } }
	public static Vector2Int one { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_One; } }
	public static Vector2Int up { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_Up; } }
	public static Vector2Int down { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_Down; } }
	public static Vector2Int left { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_Left; } }
	public static Vector2Int right { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return s_Right; } }

	private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);
	private static readonly Vector2Int s_One = new Vector2Int(1, 1);
	private static readonly Vector2Int s_Up = new Vector2Int(0, 1);
	private static readonly Vector2Int s_Down = new Vector2Int(0, -1);
	private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);
	private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
}