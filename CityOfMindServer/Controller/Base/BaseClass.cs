using CitizenFX.Core;
using FiveMForge.Context;
using FiveMForge.Controller.Config;

namespace FiveMForge.Controller.Base
{
    /// <summary>
    /// BaseClass
    /// Foundation for all C# Server-Side code.
    /// </summary>
    public class BaseClass : BaseScript
    {
        protected CoreContext Context;
        protected BaseClass() => Context = new CoreContext();
    }
}