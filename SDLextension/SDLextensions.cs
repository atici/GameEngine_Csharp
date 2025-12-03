using System.Drawing;
using System.Numerics;

namespace SDL3;

public static class SDL_e
{
	public static bool SetRenderDrawColor(nint renderer, Engine.Color color)
	{
		return SDL.SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
	}
	
	public static bool RenderFillCircle(nint renderer, float cx, float cy, float r)
	{
		SDL.FPoint[] points = GetCircleFPoints(cx,cy,r);
		return SDL.RenderLines(renderer, points, points.Length);
	}

	public static bool RenderCircle(nint renderer, float cx, float cy, float r)
	{
		SDL.FPoint[] points = GetCircleFPoints(cx,cy,r);
		return SDL.RenderPoints(renderer, points, points.Length);
	}

	public static SDL.FPoint[] GetCircleFPoints(float cx, float cy, float r)
	{
		List<SDL.FPoint> result = new();
		float x = 0;
		float y = -r;
		float p = -r + 0.25f;

		while (x < -y)
		{
			if (p > 0)
			{
				y++;
				p += 2 * (x + y) + 1;
			}
			else p += 2 * x + 1;

			result.Add(MakeFPoint(cx + x, cy + y));
			result.Add(MakeFPoint(cx - x, cy + y));
			result.Add(MakeFPoint(cx - x, cy - y));
			result.Add(MakeFPoint(cx + x, cy - y));
			result.Add(MakeFPoint(cx + y, cy + x));
			result.Add(MakeFPoint(cx - y, cy + x));
			result.Add(MakeFPoint(cx - y, cy - x));
			result.Add(MakeFPoint(cx + y, cy - x));

			x++;
		}
		return result.ToArray();
	}
	public static SDL.FPoint MakeFPoint(float x, float y) => new SDL.FPoint { X = x, Y = y };

	public static SDL.FPoint[] FillRect(SDL.FPoint[] corners) {
		List<SDL.FPoint> result = new();
		Vector2 topStart = FPoint2Vector(corners[0]);
		Vector2 topEnd = FPoint2Vector(corners[1]);
		Vector2 topDir = Vector2.Normalize(topEnd - topStart);
		Vector2 bottomStart = FPoint2Vector(corners[3]);
		Vector2 bottomEnd = FPoint2Vector(corners[2]);
		Vector2 bottomDir = Vector2.Normalize(bottomEnd - bottomStart);	

		float topEndMag = topEnd.Length();
		float bottomEndMag = bottomEnd.Length();
		
		Vector2 currentTop = topStart;
		Vector2 currentBottom = bottomStart;
		while(true){
			currentTop += topDir;
			currentBottom += bottomDir;
			bool topCond = currentTop.Length() < topEndMag;
			bool bottomCond = currentBottom.Length() < bottomEndMag;
			if (topCond) {
				result.Add(Vector2FPoint(currentTop));
			} else
				result.Add(Vector2FPoint(topEnd));

			if (bottomCond) {
				result.Add(Vector2FPoint(currentBottom));
			} else 
				result.Add(Vector2FPoint(bottomEnd));
			
			if(!topCond && !bottomCond) break;
		}
		return result.ToArray();
	}

	public static SDL.FPoint RotatePoint(SDL.FPoint point, float delta) {
		delta *= Engine.Conversion.Deg2Rad;
		return new SDL.FPoint {
			X = point.X * MathF.Cos(delta) - point.Y * MathF.Sin(delta),
			Y = point.X * MathF.Sin(delta) + point.Y * MathF.Cos(delta)
		};
	}

	public static SDL.FPoint Vector2FPoint(Vector2 v) => MakeFPoint(v.X, v.Y);
	public static Vector2 FPoint2Vector(SDL.FPoint p) => new Vector2(p.X, p.Y);
}