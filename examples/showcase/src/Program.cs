using MazeMaker;
using static System.Console;

namespace Showcase
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			try
			{
				var cursorpos = GetCursorPosition();

				ForegroundColor = ConsoleColor.Cyan;
				CancelKeyPress += (s, e) => ResetColor();

				for (int i = 0; i < int.Parse(args[2]); i++)
				{
					SetCursorPosition(cursorpos.Left, cursorpos.Top);

					WriteLine("");
					Write(new Maze(int.Parse(args[0]), int.Parse(args[1])).Generate());

					await Task.Delay(500);
				}
			}
			catch (Exception e)
			{
				WriteLine(e.Message);
			}
			finally
			{
				ResetColor();
			}
		}
	}
}