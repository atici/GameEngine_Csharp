using System.Collections;
using System.Numerics;
using SDL3;
using Engine;

namespace Snake;
public class Grid<T> : IEnumerable<T>, IDrawable
{
	private T[,] _values;
	public int height { get; protected set; }
	public int width { get; protected set; }

	public float cellSize {get; private set;}
	public Vector2 originPosition = Vector2.Zero;
	public Color Color = new(0,0,0,255);

	public Grid(int width, int height, float cellSize, Vector2 originPosition, Func<Grid<T>, int, int, T> func) {
		this.height = height;
		this.width = width;
		this.cellSize = cellSize;
		this.originPosition = originPosition;
		_values = new T[height, width];

		for (int x = 0; x < height; x++)
		{
			for (int y = 0; y < width; y++)
			{
				_values[x, y] = func(this, x, y);	
			}	
		}
	}

	public T GetValue(Vector2 pos) => GetValue((int)pos.X, (int)pos.Y);
	public T GetValue(int x, int y) => _values[x,y] ?? default!; // == null ? default! : _values[x,y];

	public bool Draw(nint canvas)
	{
		int size = height * width;
		SDL.FRect[] fRectArray = new SDL.FRect[size];

		int i = 0;
		for (int x = 0; x < height; x++)
		{
			for (int y = 0; y < width; y++)
			{
				fRectArray[ i++ ] = new SDL.FRect{ X = (x*cellSize) + originPosition.X, Y = (y * cellSize) + originPosition.Y, H = cellSize, W = cellSize };
			}	
		}
		SDL_e.SetRenderDrawColor(canvas, Color);
		// return SDL.RenderLines(canvas, fPoints.ToArray(), fPoints.Count);
		return SDL.RenderRects(canvas, fRectArray, size);
	}

	public SDL.FRect GetFRect() => new SDL.FRect{
		X = originPosition.X , Y = originPosition.Y,
		H = height , W = width};

	// Enumerator
	public IEnumerator<T> GetEnumerator() {
		for (int x = 0; x < height; x++)
		{
			for (int y = 0; y < width; y++)
			{
				yield return _values[x, y];
			}	
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
public struct GridItem<T> : IDrawable
	where T : IDrawable
{
	public T ?Value;
	public Grid<GridItem<T>> Grid;
	public int X {get; private set;}
	public int Y {get; private set;}

	public GridItem(T value, Grid<GridItem<T>> grid, int x, int y)
	{
		Value = value;
		Grid = grid;
		X = x;
		Y = y;
	}

	public bool Draw(nint canvas)
	{
		if (Value is IDrawable)
			return (Value as IDrawable).Draw(canvas);
		return false;
	}
}

public struct GridItem : IDrawable
{
	public Grid<GridItem> Grid;
	public int X {get; private set;}
	public int Y {get; private set;}

	public GridItem(Grid<GridItem> grid, int x, int y)
	{
		Grid = grid;
		X = x;
		Y = y;
	}

	public bool Draw(nint canvas)
	{
		SDL.RenderRect(canvas, GetFRect());
		return true;
	}
	public SDL.FRect GetFRect() => new SDL.FRect {
		X = X * Grid.cellSize,
		Y = Y * Grid.cellSize,
		H = Grid.cellSize,
		W = Grid.cellSize 
	};
}
