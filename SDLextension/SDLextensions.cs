namespace SDL3
{
    public static class SDL_e
	{
        public static bool SetRenderDrawColor(nint renderer, Engine.Color color)
		{
			return SDL.SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A );
		}
	}
}