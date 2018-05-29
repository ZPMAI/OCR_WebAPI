using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

using DevExpress.XtraTabbedMdi;
using CCT.Common;
using CCT.Common.Reflection;

namespace CCT.MainFrame
{
    /// <summary>
    /// FrmMain 的摘要说明。


    /// </summary>
    public partial class FrmMain : Form
    {
        /// <summary>
        /// TreeViewIcon 的摘要说明。


        /// </summary>
        public struct TreeViewIcon
        {
            public const int CLOSE = 0;
            public const int OPEN = 1;
            public const int FILE = 2;
            public const int EXCEL = 3;
            public const int WORD = 4;
        }


        /// 构造函数


        public FrmMain()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;

            this.Load += delegate
            {
                tsslEnvironment.Text = Config.IsDebug? (Config.IsProduct?"生产数据测试环境":"测试环境") : "正式环境";
                tsslUsername.Text = Userinfo.Username;
                GeneralApp();
            };
        }

        /// 生成应用程序
        private void GeneralApp()
        {
            DataSet dataSet = CCTDContext.GetMenu(Userinfo.Username);

            if (dataSet.Tables[0].Rows.Count == 0) { return; }

            DataRow dataRow = dataSet.Tables[0].Rows[0];

            TreeNode root = CreateNode(dataRow);
            root.Expand();
            treeView.Nodes.Add(root);           
        }

        TreeNode CreateNode(DataRow dataRow)
        {
            TreeNode treeNode;

            if (dataRow["TIPSINFO"].ToString().Equals("Directory"))
            {
                treeNode = new TreeNode(dataRow["MENUNAME"].ToString(), TreeViewIcon.CLOSE, TreeViewIcon.OPEN);

                GeneralChildren(treeNode, dataRow);
            }
            else if (dataRow["TIPSINFO"].ToString().Equals("Excel"))
            {
                treeNode = new TreeNode(dataRow["MENUNAME"].ToString(), TreeViewIcon.EXCEL, TreeViewIcon.EXCEL);
                treeNode.Tag = new MenuInfo(dataRow["TIPSINFO"].ToString(), dataRow["TYPENAME"].ToString(), dataRow["ASSEMBLYNAME"].ToString());
            }
            else if (dataRow["TIPSINFO"].ToString().Equals("Word"))
            {
                treeNode = new TreeNode(dataRow["MENUNAME"].ToString(), TreeViewIcon.WORD, TreeViewIcon.WORD);
                treeNode.Tag = new MenuInfo(dataRow["TIPSINFO"].ToString(), dataRow["TYPENAME"].ToString(), dataRow["ASSEMBLYNAME"].ToString());
            }
            else //if (dataRow["MENUFLAG"].ToString().Equals(string.Empty))
            {
                treeNode = new TreeNode(dataRow["MENUNAME"].ToString(), TreeViewIcon.FILE, TreeViewIcon.FILE);
                treeNode.Tag = new MenuInfo(dataRow["TIPSINFO"].ToString(), dataRow["TYPENAME"].ToString(), dataRow["ASSEMBLYNAME"].ToString());
            }

            return treeNode;
        }

        void GeneralChildren(TreeNode parentNode, DataRow dataRow)
        {
            decimal menuId = Convert.ToDecimal(dataRow["MENUID"]);
            string filterExpression = string.Format("PARENTID={0}", menuId);
            DataRow[] childRows = dataRow.Table.Select(filterExpression);

            if (childRows.Length > 0)
            {
                foreach (DataRow childRow in childRows)
                {
                    TreeNode childNode = CreateNode(childRow);
                    parentNode.Nodes.Add(childNode);
                }
            }
        }


        #region 无效代码

        //Service service = new Service();
        //DataSet dataSet = service.SelectAppGroupModuleByStaffID(Userinfo.ID, Userinfo.Permission);
        //foreach (DataRow appRow in dataSet.Tables["App"].Rows)
        //{
        //    if (!appRow["Type"].ToString().Equals("Win"))
        //    {
        //        continue;
        //    }
        //    DataRow[] moduleRows = appRow.GetChildRows("App_Module");
        //    if (moduleRows.Length == 0)
        //    {
        //        continue;
        //    }
        //    else
        //    {                
        //        int appID = Convert.ToInt32(appRow["ID"]);
        //        string appName = Convert.ToString(appRow["AppName"]);
        //        TreeNode appNode = new TreeNode(appName, TreeViewIcon.CLOSE, TreeViewIcon.OPEN);
        //        treeView.Nodes.Add(appNode);
        //        string filterExpression = string.Format("AppID={0} AND ParentID=0", appID);
        //        DataRow[] groupRows = dataSet.Tables["Group"].Select(filterExpression);
        //        foreach (DataRow groupRow in groupRows)
        //        {
        //            GeneralGroup(appNode, groupRow, dataSet.Tables["Module"]);
        //        }
        //        GeneralModule(appNode, appID, 0, dataSet.Tables["Module"]);
        //        appNode.Expand();
        //    }
        //}

        // 生成分组
        //private void GeneralGroup(TreeNode parentNode, DataRow groupRow, DataTable moduleTable)
        //{
        //    DataRow[] moduleRows = groupRow.GetChildRows("Group_Module");
        //    DataRow[] childRows = groupRow.GetChildRows("Group_Group");

        //    if (childRows.Length == 0 
        //        && moduleRows.Length == 0)
        //    {
        //        return;
        //    }

        //    string groupName = Convert.ToString(groupRow["GroupName"]);
        //    TreeNode groupNode = new TreeNode(groupName, TreeViewIcon.CLOSE, TreeViewIcon.OPEN);

        //    foreach (DataRow childRow in childRows)
        //    {
        //        GeneralGroup(groupNode, childRow, moduleTable);
        //    }

        //    if (groupNode.Nodes.Count > 0 
        //        || moduleRows.Length > 0)
        //    {
        //        parentNode.Nodes.Add(groupNode);

        //        if (moduleRows.Length != 0)
        //        {
        //            int appID = Convert.ToInt32(groupRow["AppID"]);
        //            int groupID = Convert.ToInt32(groupRow["ID"]);

        //            GeneralModule(groupNode, appID, groupID, moduleTable);
        //        }
        //    }
        //}

        // 生成模块
        //private void GeneralModule(TreeNode parentNode, int appID, int groupID, DataTable moduleTable)
        //{
        //    string filterExpression = string.Format("AppID={0} AND GroupID={1}", appID, groupID);
        //    DataRow[] moduleRows = moduleTable.Select(filterExpression);

        //    foreach (DataRow moduleRow in moduleRows)
        //    {
        //        bool visible = Convert.ToBoolean(moduleRow["Visible"]);

        //        if (visible)
        //        {
        //            string moduleName = Convert.ToString(moduleRow["ModuleName"]);
        //            TreeNode moduleNode = new TreeNode(moduleName, TreeViewIcon.FILE, TreeViewIcon.FILE);
        //            moduleNode.Tag = moduleRow["Url"].ToString();
        //            parentNode.Nodes.Add(moduleNode);
        //        }
        //    }
        //}

        #endregion

        /// 树节点点击事件


        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null) { return; }

            MenuInfo menuInfo = e.Node.Tag as MenuInfo;

            if (menuInfo.MenuFlag.Equals(string.Empty))
            {
                string text = e.Node.Text;
                string className = string.Format("{0}, {1}", menuInfo.TypeName, menuInfo.AssemblyName);

                foreach (XtraMdiTabPage tabPage in xtraTabbedMdiManager.Pages)
                {
                    Form frmExist = tabPage.MdiChild;

                    if (frmExist.GetType().FullName.Equals(menuInfo.TypeName)
                        && frmExist.Text.Equals(text))
                    {
                        xtraTabbedMdiManager.SelectedPage = tabPage;
                        return;
                    }
                }

                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (menuInfo.TypeName == "OCR.WIN.fmMain")
                    {
                        dockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
                    }

                    Form form = ObjectHelper.CreateInstance(className) as Form;
                    form.Text = text;
                    form.MdiParent = this;
                    form.AutoScroll = true;
                    form.Show();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(string.Format("加载类 {0} 失败！\r\n{1}", className, exp.InnerException.Message));
                }
            }
            else if (menuInfo.MenuFlag.Equals("Excel") || menuInfo.MenuFlag.Equals("Word"))
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, menuInfo.TypeName);
                ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName);
                Process.Start(processStartInfo);
            }
        }


        /// 窗体关闭事件
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();

            // 判断关联进程是否已终止


            if (!process.HasExited)
            {
                // 立即停止关联的进程


                process.Kill();
            }
        }
    }
}
