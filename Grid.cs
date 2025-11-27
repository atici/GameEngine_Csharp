using System.Collections;
using System.Numerics;

public class Grid<T> : IEnumerable<T>
{
	private T[,] values;
	int _height;
	int _width;
	public float CellSize {get; private set;}

	public Vector2 OriginPosition = Vector2.Zero;

	public Grid(int height, int width, float cellSize, Func<Grid<T>,int,int,T> func){
		_height = height;
		_width = width;
		CellSize = cellSize;
		values = new T[height, width];

		for (int x = 0; x < height; x++)
		{
			for (int y = 0; y < width; y++)
			{
				values[x, y] = func(this, x, y);	
			}	
		}
	}

	public T GetValue(int x, int y) => values == null ? default! : values[x,y];

	// Enumerator
	public IEnumerator<T> GetEnumerator() {
		for (int x = 0; x < _height; x++)
		{
			for (int y = 0; y < _width; y++)
			{
				yield return values[x, y];
			}	
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}