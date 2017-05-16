using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Console.UI.Drawing
{
    public class Size
    {
        [TypeConverter(typeof(MeasurementConverter))]
        public Measurement Width { get; set; }
        [TypeConverter(typeof(MeasurementConverter))]
        public Measurement Height { get; set; }
    }
}
