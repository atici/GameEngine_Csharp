using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Engine;

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
}