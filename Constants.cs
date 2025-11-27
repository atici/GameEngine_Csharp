public static class Constants
{
	public const int WINDOW_SIZE_HEIGHT = 900;
	public const int WINDOW_SIZE_WIDTH = 1600;
	public const int UPDATE_CLOCK_MS = 7; 
	public const float UPDATE_CLOCK = UPDATE_CLOCK_MS * Units.MILISECOND; // Time in seconds for every update.

	public static class Units
	{
		public const int METRE = 100;
		public const float MILISECOND = 0.001f;
		public const float GRAVITY = 9.81f * METRE / 1; // metre/second == 100/1
	}
}