using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CCT.SystemFramework.DOS
{
    public class DOSHelper
    {  
        // 
        // ִ��DOS�������DOS��������
        // 
        // dos����
        // �����������������쳣�����ؿ��ַ���
        public static string Execute(string dosCommand)
        {
            return Execute(dosCommand, 6 * 1000);
        }

        // 
        // ִ��DOS�������DOS��������
        // 
        // dos����
        // �ȴ�����ִ�е�ʱ�䣨��λ�����룩������趨Ϊ0�������޵ȴ�
        // �����������������쳣�����ؿ��ַ���
        public static string Execute(string dosCommand, int milliseconds)
        {
            string output = "";     //����ַ���
            if (dosCommand != null && dosCommand != "")
            {
                Process process = new Process();     //�������̶���
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";      //�趨��Ҫִ�е�����
                startInfo.Arguments = "/C " + dosCommand;   //�趨���������еġ�/C����ʾִ��������������˳�
                startInfo.UseShellExecute = false;     //��ʹ��ϵͳ��ǳ�������
                startInfo.RedirectStandardInput = false;   //���ض�������
                startInfo.RedirectStandardOutput = true;   //�ض������
                startInfo.CreateNoWindow = true;     //����������
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())       //��ʼ����
                    {
                        if (milliseconds == 0)
                            process.WaitForExit();     //�������޵ȴ����̽���
                        else
                            process.WaitForExit(milliseconds);  //����ȴ����̽������ȴ�ʱ��Ϊָ���ĺ���
                        output = process.StandardOutput.ReadToEnd();//��ȡ���̵����
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }
   
    }
}
