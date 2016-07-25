using System;
using System.Collections.Generic;
using System.Text;
using KeePass.Plugins;
using KeePassLib;
using System.Windows.Forms;
using System.IO;

namespace KPUsage
{
    public sealed class KPUsageExt : Plugin
    {
        private IPluginHost host = null;

        private ToolStripSeparator sep = null;
        private ToolStripMenuItem exportUsageMenuItem = null;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;

            var menu = this.host.MainWindow.ToolsMenu.DropDownItems;
            sep = new ToolStripSeparator();
            menu.Add(sep);

            // Add menu item
            exportUsageMenuItem = new ToolStripMenuItem("Export Usage Statistics");
            exportUsageMenuItem.Click += ExportUsageMenuItem_Click;

            menu.Add(exportUsageMenuItem);

            return true;
        }

        private void ExportUsageMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.host.Database.IsOpen)
            {
                MessageBox.Show("You must open a database to export usage statistics!", "KPUsagePlugin");
                return;
            }

            var sfDialog = new SaveFileDialog();

            if (sfDialog.ShowDialog() == DialogResult.OK)
            {
                var dic = new Dictionary<ulong, List<PwEntry>>();

                this.host.Database.RootGroup.TraverseTree(TraversalMethod.PreOrder, null,
                    (PwEntry en) => 
                    {
                        
                        if (!dic.ContainsKey(en.UsageCount))
                        {
                            var tempList = new List<PwEntry>();
                            dic.Add(en.UsageCount, tempList);
                        }

                        var numList = dic[en.UsageCount];
                        numList.Add(en);
                        return true;
                    });

                using (var file = new StreamWriter(sfDialog.FileName, false))
                {
                    file.WriteLine("Name, Usage Count");
                    foreach (var kvp in dic)
                    {
                        var count = kvp.Key;
                        foreach (var entry in kvp.Value)
                        {
                            file.WriteLine($"\"{entry.Strings.ReadSafe(PwDefs.TitleField)}\", {count}");
                        }
                    }
                }

            }
        }
    }
}
