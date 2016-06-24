using System.Windows.Controls;

namespace PlaneLanding
{
    public class Container : StackPanel
    {
        public Container()
        {
            Grid.SetIsSharedSizeScope(this, true);
        }
    }
}
