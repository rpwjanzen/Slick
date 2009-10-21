using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slick
{
    class MouseInputBehaviour
    {
        NotificationBox notificationBox;

        public MouseInputBehaviour(NotificationBox notificationBox, MouseInputHandler mouseInputHandler) 
        {
            this.notificationBox = notificationBox;

            mouseInputHandler.leftMouseClick += handleMouseClick;
        }

        void handleMouseClick(int mouseX, int mouseY) 
        {
            if (notificationBox != null)
                notificationBox.handleMouseClick(mouseX, mouseY);
            else 
            {
                
            }
        }
    }
}
