using AdventOfCode.Puzzles.Of2022;

new Day03().Solve();

// typeof(Program).Assembly
//     .GetTypes()
//     .Where(_ => _.IsAssignableTo(typeof(Puzzle)) && _.IsAbstract == false)
//     .Select(Activator.CreateInstance)
//     .Cast<Puzzle>()
//     .OrderBy(_ => _.Year)
//     .ThenBy(_ => _.Day)
//     .ForEach(_ => _.Solve());