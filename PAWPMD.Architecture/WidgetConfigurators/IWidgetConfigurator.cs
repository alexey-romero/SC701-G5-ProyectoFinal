using PAWPMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAWPMD.Architecture.WidgetConfigurators
{
 public interface IWidgetConfigurator
    {
        void ConfigureWidget(Widget widget);
    }
}
