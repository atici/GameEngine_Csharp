using System.Collections;
using System.Numerics;
using SDL3;

public struct GridItem<T> : IDrawable
	where T : IDrawable
{
	public T ?Value;
	public Grid<GridItem<T>> Grid;
	public int X {get; private set;}
	public int Y {get; private set;}

	public GridItem(T value, ref Grid<GridItem<T>> grid, int x, int y)
	{
		Value = value;
		Grid = grid;
		X = x;
		Y = y;
	}

	public SDL.FRect GetFRect() => Value == null ? new() : Value.GetFRect();
	public void Draw(nint renderer)
	{
		if (Value is IDrawable)
			(Value as IDrawable)?.Draw(renderer);
	}
}

public class Grid<T> : IEnumerable<T>, IDrawable
	where T : IDrawable
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

	public void Draw(nint renderer)
	{
		int size = _height * _width;
		SDL.FRect[] fRectArray = new SDL.FRect[size];
		int i = 0;
		foreach(T item in this)
		{
			fRectArray[i] = item.GetFRect();
			i++;
		}
		SDL.RenderRects(renderer, fRectArray, size);
	}
	public SDL.FRect GetFRect() => new SDL.FRect{
		X = OriginPosition.X , Y = OriginPosition.Y,
		H = _height , W = _width};

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