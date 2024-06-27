using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevToys.Api;
using System.ComponentModel.Composition;


[Export(typeof(IResourceAssemblyIdentifier))]
[Name(nameof(AssemblyIdentifier))]
internal class AssemblyIdentifier : IResourceAssemblyIdentifier
{
    public ValueTask<FontDefinition[]> GetFontDefinitionsAsync()
    {
        throw new NotImplementedException();
    }
}
