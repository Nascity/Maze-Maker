# Maze Maker

Generates a maze with [recursive backtracker algorithm](https://en.m.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_depth-first_search).

## Todos
- Write a proper README.md


## Usage

```csharp
// using default value of width and height
var maze_default_width_height = new Maze();

// passing desired height
var maze_default_width = new Maze(height: 15);

// passing desired width and height
var maze_custom_value = new Maze(25, 25);
```

Maze can have desired dimension.

```csharp
var maze = new Maze(25, 15);
maze.Generate();
```

Generate maze by `Generate()` method. The actual array for the maze is accessible via indexer. The type of the array is enum `MazeMaker.Tile`. Read [Tile.cs](/src/Tile.cs) for additional information.

```csharp
Console.WriteLine(new Maze(15, 15).Generate().ToString());
```

output:

![](img/15by15.png)

## Demo (from /examples)

![showcase](img/showcase.gif)
