namespace Engine;
public static class Constants
{
	public const int WINDOW_SIZE_HEIGHT = 900;
	public const int WINDOW_SIZE_WIDTH = 1600;

	public const int TARGET_FPS = 144;

	public const float UPDATE_CLOCK = 1/ TARGET_FPS; // Time in seconds for every update.
	public const int UPDATE_CLOCK_MS = TARGET_FPS * 1000; 
}