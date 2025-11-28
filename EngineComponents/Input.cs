using SDL3;

namespace Engine;
public static class Input
{
	public static SDL.Event Event {get; private set;} 
	public static void _UpdateEvent(SDL.Event e) => Event = e;
	public static SDL.KeyboardEvent KeyboardEvent => Event.Key;
	
	public static bool GetKeyDown(SDL.Keycode key) {
		return Event.Key.Down && Event.Key.Key == key; 
	}
}