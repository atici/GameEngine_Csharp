using System.Collections;
using System.Numerics;
using SDL3;
using Engine;

namespace FirstGame;
public class Grid<T> : IEnumerable<T>, IDrawable
	where T : IDrawable
{
	private T[,] _values;
	private int _height;
	private int _width;

	public float CellSize {get; private set;}
	public Vector2 OriginPosition = Vector2.Zero;
	public Color Color = new(0,0,0,255);

	public Grid(int height, int width, float cellSize, Func<Grid<T>,int,int,T> func){
		_height = height;
		_width = width;
		CellSize = cellSize;
		_values = new T[height, width];

		for (int x = 0; x < height; x++)
		{
			for (int y = 0; y < width; y++)
			{
				_values[x, y] = func(this, x, y);	
			}	
		}
	}

	public T GetValue(int x, int y) => _values == null ? default! : _values[x,y];

	public bool Draw(nint canvas)
	{
		int size = _height * _width;
		SDL.FRect[] fRectArray = new SDL.FRect[size];
		int i = 0;
		foreach(T item in this)
		{
			fRectArray[i] = item.GetFRect();
			i++;
		}
		SDL_e.SetRenderDrawColor(canvas, Color);
		return SDL.RenderRects(canvas, fRectArray, size);
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

	public GridItem(T value, ref Grid<GridItem<T>> grid, int x, int y)
	{
		Value = value;
		Grid = grid;
		X = x;
		Y = y;
	}

	public SDL.FRect GetFRect() => Value == null ? new() : Value.GetFRect();
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

	public GridItem(ref Grid<GridItem> grid, int x, int y)
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
		X = X * Grid.CellSize,
		Y = Y * Grid.CellSize,
		H = Grid.CellSize,
		W = Grid.CellSize 
	};
}
