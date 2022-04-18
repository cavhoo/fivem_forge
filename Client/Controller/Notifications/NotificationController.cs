using System;
using CitizenFX.Core;
using Client.Models;

using static CitizenFX.Core.Native.API;

namespace Client.Controller.Notifications
{
  public class NotificationController : BaseScript
  {
    public NotificationController()
    {
      EventHandlers[ServerEvents.ShowNotification] += new Action<string>(OnShowServerNotification);
    }

    private void OnShowServerNotification(string notification)
    {
      Debug.WriteLine(notification);
    }
  }
}