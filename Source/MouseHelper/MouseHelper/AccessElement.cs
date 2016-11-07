using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace MouseHelper
{
    public class AccessElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public AutomationElement Element { get; set; }

        public AccessElement(int id, AutomationElement element)
        {
            Id = id;
            Name = element.Current.Name;
            Top = element.Current.BoundingRectangle.Top;
            Left = element.Current.BoundingRectangle.Left;
            Width = element.Current.BoundingRectangle.Width;
            Height = element.Current.BoundingRectangle.Height;
            Element = element;
        }
    }
}
