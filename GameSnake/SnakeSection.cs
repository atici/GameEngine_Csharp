using System.Numerics;
using Engine;

namespace Snake;
public class Section : GameObject{

	public enum State{Empty = 0, Body = 1, Head = 3, Food = 2, Dead = 4}
	private State _state = 0;
	public State state {
		get => _state;
		set {
			switch (value) {
				case State.Empty:
					rect.color = Color.Black;
				break;
				case State.Body:
					rect.color = Color.White;
				break;
				case State.Head:
					rect.color = Color.Green;
				break;
				case State.Food:
					rect.color = Color.Red;
				break;
				case State.Dead:
					// rect.color = new Color(128, 0, 128);
				break;
			}
			_state = value;
		}
	}

	private Rect rect;
	public Vector2 gridPos;

	public Section(int x, int y, Grid<Section> g) {
		float size = g.cellSize;
		transform.position = new Vector2(x * size + g.originPosition.X, y * size + g.originPosition.Y);
		rect = new Rect(size - 2f, size - 2f, this);
		state = State.Empty;
		gridPos = new Vector2(x,y);
	}

	protected override void Init() {
	}
}