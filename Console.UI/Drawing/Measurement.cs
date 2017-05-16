using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Drawing
{
    public class Measurement
    {
        public bool IsAuto { get; set; } = false;
        public bool IsStretch { get;set; } = false;
        public short? Amount { get; set; } = null;
    }


    public class MeasurementConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType.Equals(typeof(String));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var measurement = new Measurement();
            switch (value.ToString().ToLower())
            {
                case "stretch":
                    measurement.IsStretch = true;
                    break;
                case "auto":
                    measurement.IsAuto = true;
                    break;
                default:
                    measurement.Amount = short.Parse(value.ToString());
                    break;
            }
            return measurement;
        }
    }
    
}
