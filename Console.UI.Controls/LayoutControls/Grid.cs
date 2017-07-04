using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Controls.LayoutControls
{
    public class Grid : Control
    {
        private ObservableCollection<Control> _children = new ObservableCollection<Control>();
        public ReadOnlyObservableCollection<Control> Children{ get { return new ReadOnlyObservableCollection<Control>(_children); } }
        public override void Render()
        {
            base.Render();

            var g = CreateGraphics();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void AddChild(Control control, int row, int column)
        {

        }
    } 
}
