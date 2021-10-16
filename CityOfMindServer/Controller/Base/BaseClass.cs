using System.Runtime.Remoting.Contexts;
using CitizenFX.Core;
using CityOfMindDatabase.Contexts;
using FiveMForge.Database;

namespace FiveMForge.Controller.Base
{
    /// <summary>
    /// BaseClass
    /// Foundation for all C# Server-Side code.
    /// </summary>
    public class BaseClass : BaseScript
    {
        protected CoreContext Context;
        protected BaseClass() => Context = new CoreContext(CityOfMindDatabase.Config.ConfigController.GetInstance().ConnectionString);
    }
}