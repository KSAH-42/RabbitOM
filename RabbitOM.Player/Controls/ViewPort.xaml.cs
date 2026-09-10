using System;
using System.Windows.Controls;

namespace RabbitOM.Player.Controls
{
    // TODO: adding this class to project picture and add zoom caps and may be add videosource and launch something to call the update method and instanciated a visualhost for rendering in a seperate thread, we render actually into the main thread. and the service class could be injected here, it could be better to do that thing. but before, check if the control can be move as a custom control, but using event handler will add more complexity for people to maintain the class even it's feasible, usercontrol or customcontrol ? hmm constrol control could be the right way and the control is used by the mediaplayer control. and the viewport can include buffering. so implement as usercontrol and see what's happen next
    public partial class ViewPort : UserControl
    {
        public ViewPort()
        {
            InitializeComponent();
        }
    }
}
