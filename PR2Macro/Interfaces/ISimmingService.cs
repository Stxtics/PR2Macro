using PR2Macro.Enums;
using PR2Macro.Models;

namespace PR2Macro.Interfaces;

public interface ISimmingService
{
    Task<StartMacroResult> Start(MacroInfo macroInfo);
    Task<SwitchServerResult> SwitchServerRequest(MacroInfo macroInfo, Server server);
    PauseMacroResult PauseMacro(MacroInfo macroInfo);
    bool IsMacroPaused(MacroInfo macroInfo);
    Task<MacroInfo> StopMacro(MacroInfo macroInfo);
    Task<List<MacroInfo>> StopAllMacros();
}
