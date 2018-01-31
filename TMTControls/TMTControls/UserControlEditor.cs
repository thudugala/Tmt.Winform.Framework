using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TMTControls
{
    public class UserControlEditor : UITypeEditor
    {
        private IWindowsFormsEditorService editorService;

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null)
            {
                return base.GetEditStyle(context);
            }

            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || context.Instance == null || provider == null)
            {
                return value;
            }

            var host = provider.GetService(typeof(IDesignerHost)) as IDesignerHost;
            Type typ = host.GetType(host.RootComponentClassName);
            if (typ != null)
            {
                using (var lb = new ListBox())
                {
                    lb.Click += Lb_Click;

                    var panelDictionary = typ.Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(UserControl)) &&
                                                                        x.Namespace.Contains("Panels"))
                                                            .OrderBy(x => x.Name)
                                                            .ToDictionary(x => x.Name, x => x);

                    lb.Items.AddRange(panelDictionary.Keys.ToArray());

                    if (value != null)
                    {
                        lb.SelectedItem = (value as Type).Name;
                    }

                    editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                    editorService.DropDownControl(lb);

                    if (lb.SelectedItem != null)
                    {
                        value = panelDictionary[lb.SelectedItem.ToString()];
                    }
                }
            }
            return value;
        }

        private void Lb_Click(object sender, EventArgs e)
        {
            editorService.CloseDropDown();
        }
    }
}