using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevToys.Api;
using Microsoft.Extensions.Logging;
using TerraSoft.RomanizeGreek;

[Export(typeof(ICommandLineTool))]
[Name("RomanizeGreek")]
[CommandName(
    Name = "romanize",
    ResourceManagerBaseName = "TerraSoft.RomanizeGreek.Resources",
    DescriptionResourceName = nameof(Resources.Description))]
internal sealed class RomanizeGreekCli : ICommandLineTool
{
    [CommandLineOption(
        Name = "input",
        Alias = "i",
        IsRequired = true,
        DescriptionResourceName = nameof(Resources.InputOptionDescription))]
    private string? Input { get; set; }


    public ValueTask<int> InvokeAsync(ILogger logger, CancellationToken cancellationToken)
    {
        Console.WriteLine(Romanize.RomanizeText(Input ?? ""));

        return new ValueTask<int>(0);
    }

}


