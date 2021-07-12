using System.Runtime.Remoting.Contexts;
using CitizenFX.Core;
using FiveMForge.Database.Contexts;

namespace FiveMForge.Controller.Base
{
    /// <summary>
    /// BaseClass
    /// Foundation for all C# Server-Side code.
    /// </summary>
    public class BaseClass : BaseScript
    {
        protected CoreContext Context { get; }

        protected BaseClass()
        {
            // Create a DB Context that's available for all scripts inheriting from here.
            Context = new CoreContext();
        }
    }
}