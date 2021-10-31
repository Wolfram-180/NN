using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MnFrm
{
    public class Functions
    {
        public void Action(string BtnType, string ActType, ListBox lbproc, ListBox lbtarget, TextBox tb)
        {
            if (BtnType == Vars.FBtnDel)
            {
                if (ActType == Vars.FActKill)
                {
                    DeleteStringInListBox(lbproc);
                };

                if (ActType == Vars.FActCopy)
                {
                    DeleteStringInListBox(lbtarget);
                };
            }
            if (BtnType == Vars.FBtnChng)
            {
                if (ActType == Vars.FActKill)
                {
                    ChangeStringInListBox(lbproc, tb);
                };

                if (ActType == Vars.FActCopy)
                {
                    ChangeStringInListBox(lbtarget, tb);
                };
            }
            if (BtnType == Vars.FBtnAdd)
            {
                if (ActType == Vars.FActKill)
                {
                    AddStringToListBox(lbproc, tb);
                };

                if (ActType == Vars.FActCopy)
                {
                    AddStringToListBox(lbtarget, tb);
                };
            };
            if (BtnType == Vars.FBtnExec)
            {
                if (ActType == Vars.FActKill)
                {
                    KillProc(lbproc);
                };

                if (ActType == Vars.FActCopy)
                {
                    CopyToTarget(lbproc, lbtarget);
                };
            };
        }

        public void KillProc(ListBox lb)
        {
            if (lb.Items.Count > 0)
            {
                foreach (string s in lb.Items)
                {
                    try
                    {
                        foreach (Process thisproc in Process.GetProcessesByName(s))
                        {
                            if (!thisproc.CloseMainWindow())
                            {
                                thisproc.Kill();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public void CopyToTarget(ListBox lbwhat, ListBox lbtarget)
        {
            if ((lbwhat.Items.Count > 0) && (lbtarget.Items.Count > 0))
            {
                foreach (string whats in lbwhat.Items)
                {
                    foreach (string tos in lbtarget.Items)
                    {
                        try
                        {
                            File.Copy(whats, tos + whats, true);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }
        
        public void AddStringToListBox(ListBox lb, TextBox tb)
        {
            if (tb.Text != "")
            {
                lb.Items.Add(tb.Text);
            }
        }

        public void DeleteStringInListBox(ListBox lb)
        {
            if (lb.SelectedIndex >= 0)
              lb.Items.Remove(lb.Items[lb.SelectedIndex]);
        }

        public void ChangeStringInListBox(ListBox lb, TextBox tb)
        {
            if (tb.Text != "")
            {
                lb.Items[lb.SelectedIndex] = tb.Text;
            }
        }
    }
}
