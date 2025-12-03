using SDL3;

namespace Engine;
public class Circle : DrawableComponent
{
	private float _radius;
	public float radius {
		get => _radius;
		set {
			points = SDL_e.GetCircleFPoints(0,0, value);
			_radius = value;
		}
	}
	public bool fill = true;
	
	private SDL.FPoint[] points;

	public Circle(GameObject go, float radius) : base(go) {
		this.points = SDL_e.GetCircleFPoints(0,0, radius);
		this._radius = radius;
	}

	public override bool Draw(nint canvas) {
		if(!enabled) return false;

		int pointsCount = points.Length;
		SDL.FPoint[] calculatedPoints = new SDL.FPoint[pointsCount]; 
		for (int i = 0; i < pointsCount; i++) {
			calculatedPoints[i].X += transform.position.X;
			calculatedPoints[i].Y += transform.position.Y;
		}
		SDL_e.SetRenderDrawColor(canvas, color);
		if (fill)
			return SDL.RenderLines(canvas, calculatedPoints, pointsCount);
		else
			return SDL.RenderPoints(canvas, calculatedPoints, pointsCount);
	}
}