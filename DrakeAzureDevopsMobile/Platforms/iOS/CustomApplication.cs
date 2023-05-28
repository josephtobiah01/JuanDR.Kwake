using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace DrakeAzureDevopsMobile.Platforms.iOS
{
    public class CustomApplication : UIApplication
    {
        public CustomApplication() : base()
        {
        }

        public CustomApplication(IntPtr handle) : base(handle)
        {

        }

        public CustomApplication(Foundation.NSObjectFlag t) : base(t)
        {

        }

        public override void SendEvent(UIEvent uievent)
        {
            if(uievent.Type == UIEventType.Touches)
            {
                if(uievent.AllTouches.Cast<UITouch>().Any(t => t.Phase == UITouchPhase.Began))
                {
                    (App.Current as App).ResetIdleTimer();
                }
            }
            base.SendEvent(uievent);
        }
    }

}
