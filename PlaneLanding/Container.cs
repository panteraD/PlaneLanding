using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
