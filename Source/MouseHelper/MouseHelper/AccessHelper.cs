using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedWinapi;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MouseHelper
{
    public class AccessHelper
    {
        public Dictionary<int, AccessElement> AccessElements { get; }
        public AutomationElement Target { get; }
        App myapp;

        public AccessHelper(IntPtr hwnd, App app)
        {
            myapp = app;
            Target = AutomationElement.FromHandle(hwnd);
            AccessElements = new Dictionary<int, AccessElement>();
            int i = 0;
            AutomationElementCollection elements = Target.FindAll(
                                      TreeScope.Descendants,
                                      new AndCondition(
                                            new PropertyCondition(AutomationElement.IsInvokePatternAvailableProperty, true),
                                            new PropertyCondition(AutomationElement.IsEnabledProperty, true)
                                      )
                              );
            foreach (AutomationElement element in elements)
            {
                AccessElements.Add(i, new AccessElement(i, element));
                i++;
            }

        }

        public string ConcatNames()
        {
            string outTxt = "";
            foreach (KeyValuePair<int,AccessElement> entry in AccessElements)
            {
                outTxt += entry.Value.Name + "\n";
            }
            return outTxt;
        }

        public Dictionary<int, string> GetNames()
        {
            Dictionary<int, string> outDict = new Dictionary<int, string>();
            foreach (KeyValuePair<int, AccessElement> entry in AccessElements)
            {
                outDict.Add(entry.Key, entry.Value.Name);
            }
            return outDict;
        }
        
        internal bool InvokeElement(int number)
        {
            if (number >= 0 && number <= AccessElements.Count) {
                (AccessElements[number].Element.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                return true;
            }

            return false;
        }
    }
}
