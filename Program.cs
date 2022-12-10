using System.Reflection;

typeof(Program).Assembly
    .GetTypes()
    .Where(_ => _.IsAssignableTo(typeof(IPuzzle)) && _.IsAbstract == false)
    .Select(_ => (Type: _, Attribute: _.GetCustomAttribute<PuzzleAttribute>()!))
    .OrderBy(_ => _.Attribute.Year)
    .ThenBy(_ => _.Attribute.Day)
    .Select(_ => Activator.CreateInstance(_.Type))
    .Cast<IPuzzle>()
    .ForEach(_ => _.Solve());