using PR2Macro.Enums;
using PR2Macro.Models;

namespace PR2Macro.Interfaces
{
    public interface IResourcesService
    {
        Bitmap GetResource(Resource resource, MacroInfo macroInfo);
    }
}
