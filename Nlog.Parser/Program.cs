using Nlog.Parser.Libs;
using System.Diagnostics;

var sw  = Stopwatch.StartNew();
var parser = new NlogParser();
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds);
Console.ReadLine();


