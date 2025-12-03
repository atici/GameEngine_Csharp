using SDL3;

namespace Engine;
public class Rect : DrawableComponent
{
	private float _heigth;
	public float height {
		get => _heigth;
		set {
			corners[0].Y = value * 0.5f * -1;
			corners[1].Y = value * 0.5f * -1;
			corners[2].Y = value * 0.5f;
			corners[3].Y = value * 0.5f;
			_heigth = value;
		}
	}
	private float _width;
	public float width {
		get => _width;
		set {
			corners[0].X = value * 0.5f * -1;
			corners[1].X = value * 0.5f;
			corners[2].X = value * 0.5f;
			corners[3].X = value * 0.5f * -1;
			_width = value;
		}
	}
	public bool fill = true;

	static readonly int[] indices = [0,1,2,2,3,0];
	public SDL.FPoint[] corners = new SDL.FPoint[4]; // Top right to clockwise.

	public Rect(GameObject go, float width, float height) : base(go) {
		this.width = width;
		this.height = height;
	}

	public override bool Draw(nint canvas) {
		if(!enabled) return true;
		// Create the calculated points.
		SDL.FPoint[] calculatedPoints = new SDL.FPoint[5]; 
		corners.CopyTo(calculatedPoints, 0);
		calculatedPoints[4] = SDL_e.MakeFPoint(calculatedPoints[0].X, calculatedPoints[0].Y);
		// Calculate world positions.
		for (int i = 0; i < 4; i++) {
			calculatedPoints[i] = SDL_e.RotatePoint(calculatedPoints[i], transform.rotation);
			calculatedPoints[i].X += transform.position.X;
			calculatedPoints[i].Y += transform.position.Y;
		}
		if (fill)
			return SDL.RenderGeometry(canvas, default, GetVertices(calculatedPoints[0..4]), 4, indices, 6);
		SDL_e.SetRenderDrawColor(canvas, color);
		return SDL.RenderLines(canvas, calculatedPoints, 4);
	}

	SDL.Vertex[] GetVertices(SDL.FPoint[] fPoints) {
		List<SDL.Vertex> result = new();
		foreach( var p in fPoints) {
			SDL.FPoint point = SDL_e.MakeFPoint(p.X, p.Y);
			result.Add( new SDL.Vertex{ Position= point, Color = color.SDL_FColor});
		}
		return result.ToArray();
	}
}