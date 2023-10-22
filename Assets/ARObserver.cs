using UnityEngine;

public class ARObserver : DefaultObserverEventHandler
{
    protected override void OnTrackingFound()
    {
        GlobalVariables.hasGround = true;

        base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        GlobalVariables.hasGround = false;

        base.OnTrackingLost();
    }
}