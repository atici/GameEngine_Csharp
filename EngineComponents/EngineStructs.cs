using SDL3;

namespace Engine;
public struct Color
{
	public byte R = 0;
	public byte G = 0;
	public byte B = 0;
	public byte A = 255;

	public Color(float r, float g, float b, float a = 255) {
		R = (byte)r;
		G = (byte)g;
		B = (byte)b;
		A = (byte)a;
	}
	public Color(int r, int g, int b, int a = 255) {
		R = (byte)r;
		G = (byte)g;
		B = (byte)b;
		A = (byte)a;
	}
	public Color(byte r, byte g, byte b, byte a = 255)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public static Color White 	=> new Color(255,255,255,255);
	public static Color Black 	=> new Color(0,0,0,255);
	public static Color Red 	=> new Color(255,0,0,255);
	public static Color Green	=> new Color(0,255,0,255);
	public static Color Blue	=> new Color(0,0,255,255);

	public SDL.FColor SDL_FColor => new SDL.FColor(R / 256f, G / 256f, B / 256f, A / 256f); }