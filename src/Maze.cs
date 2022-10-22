using System.Drawing;
using System.Threading.Tasks;

namespace MazeMaker
{
	internal class Maze
	{
		#region vars
		private Tile[,] maze;

		private int width;
		private int height;

		private int init_x;
		private int init_y;

		private Stack<Point> TileCoordHistory;
		#endregion

		#region constructor
		public Maze(int width = 9, int height = 9)
		{
			this.width = (width / 2) * 2 + 1;
			this.height = (height / 2) * 2 + 1;

			MinimumWidthAndHeight();

			maze = new Tile[this.height, this.width];
			TileCoordHistory = new Stack<Point>();

			InitializePrimaryCoord();
		}
		#endregion

		#region indexer
		public Tile this[int i1, int i2]
		{
			get { return maze[i1, i2]; }
			set { maze[i1, i2] = value; }
		}
		#endregion

		#region public method
		public Maze Generate()
		{
			this[init_y, init_x] = Tile.path;
			TileCoordHistory.Push(new Point(init_x, init_y));

			while (TileCoordHistory.Count > 0)
			{
				var dir = ChooseDirection();

				if (dir != Direction.deadend)
				{
					Move(dir);
				}
				else
				{
					Return();
				}
			}

			var startend = ChooseStartEndPoint();

			this[startend.Item1.Y, startend.Item1.X] = Tile.path;
			this[startend.Item2.Y, startend.Item2.X] = Tile.path;

			return this;
		}

		public Tile[,] GetMazeArray()
		{
			return maze;
		}
		#endregion

		#region private method
		private void MinimumWidthAndHeight()
		{
			if (width < 5) width = 5;
			if (height < 5) height = 5;
		}

		private void InitializePrimaryCoord()
		{
			var mask = new Random().Next(4);

			init_x = (mask & 0b10) >> 1 == 0 ? 1 : width - 2;
			init_y = (mask & 0b01) == 0 ? 1 : height - 2;
		}

		private Direction ChooseDirection()
		{
			var neighbours = CheckNeighbourIfBlocking();
			var isDeadEnd = true;
			var avail = new List<Direction>(0);
			var rnd = new Random();

			for (int i = 0; i < 4; i++)
			{
				isDeadEnd &= neighbours[i];

				if (!neighbours[i]) avail.Add((Direction)i);
			}

			if (isDeadEnd) return Direction.deadend;

			int temp = rnd.Next(avail.Count);

			return avail[temp];
		}

		private List<bool> CheckNeighbourIfBlocking()
		{
			var p = TileCoordHistory.Peek();
			var neighbouring = new List<Point>()
			{
				new Point(p.X-2, p.Y),
				new Point(p.X, p.Y-2),
				new Point(p.X+2, p.Y),
				new Point(p.X, p.Y+2)
			};
			var ret = new bool[4];

			foreach (var np in neighbouring)
			{
				bool outofRange = (np.X < 0 || np.Y < 0) || (np.X >= width || np.Y >= height);

				ret[neighbouring.IndexOf(np)] = outofRange;

				if (!outofRange)
					ret[neighbouring.IndexOf(np)] = (maze[np.Y, np.X] == Tile.path);
			}

			return ret.ToList();
		}

		private Tuple<Point, Point> ChooseStartEndPoint()
		{
			Point start;
			Point end;

			// Implement

			start = new(0, 1);
			end = new(width - 1, height - 2);

			return new Tuple<Point, Point>(start, end);
		}

		private void Move(Direction direction)
		{
			var apply = new Size[]
			{
				new Size(-1, 0),
				new Size(0, -1),
				new Size(1, 0),
				new Size(0, 1)
			};
			var p = TileCoordHistory.Peek();

			for (int i = 0; i < 2; i++)
			{
				p += apply[(int)direction];

				this[p.Y, p.X] = Tile.path;

				TileCoordHistory.Push(p);
			}
		}

		private void Return()
		{
			for (int i = 0; i < 2; i++)
			{
				if (TileCoordHistory.Count > 0)
					TileCoordHistory.Pop();
			}
		}
		#endregion

		#region overrides
		public override string ToString()
		{
			string ret = "";

			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					ret += this[i, j] == Tile.wall ? "■" : "　";
				}

				ret += "\n";
			}

			return ret;
		}
		#endregion
	}
}