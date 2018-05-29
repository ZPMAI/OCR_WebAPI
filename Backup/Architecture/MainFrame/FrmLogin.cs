using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using CCT.Common;
using CCT.Common.Xml;

namespace CCT.MainFrame
{
    public partial class FrmLogin : Form
    {
        private int i = 0;
        private static readonly string FilePath = @"D:\User.xml";

        /// 构造函数
        public FrmLogin()
        {
            InitializeComponent();

            this.Load += delegate
            {
                if (!File.Exists(FilePath)) { return; }

                XmlDocument xmlDocument = XmlHelper.LoadXmlFile(FilePath);
                XmlNode root = xmlDocument.SelectSingleNode("Username");
                txtUsername.Text = XmlHelper.GetValue(root);
            };

            btnCancel.Click += delegate
            {
                this.Close();
            };
        }


        /// 登陆按钮事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0 || txtPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("用户名称与密码输入不能为空！", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CCTDContext.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
            {
                Userinfo.Username = txtUsername.Text.Trim();
                this.DialogResult = DialogResult.OK;

                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    try
                    {
                        StreamWriter streamWriter = new StreamWriter(fileStream);
                        streamWriter.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
                        streamWriter.WriteLine("<Username>{0}</Username>", Userinfo.Username);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    catch
                    { }
                    finally
                    {
                        fileStream.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("用户名称与密码输入不正确！", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);                

                if (++i == 3) { this.DialogResult = DialogResult.Cancel; }
            }
        }
    }
}