typeof(Program).Assembly
    .GetTypes()
    .Where(_ => _.IsAssignableTo(typeof(Puzzle)) && _.IsAbstract == false)
    .Select(Activator.CreateInstance)
    .Cast<Puzzle>()
    .OrderBy(_ => _.Year)
    .ThenBy(_ => _.Day)
    .ForEach(_ => _.Solve());