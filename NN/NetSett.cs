using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FANN.Net;
using System.Diagnostics;
using MnFrm;
using System.Threading;
using System.Linq.Expressions;

namespace MnFrm
{
    public class NetSett
    {
        public void SetAllSettings(CheckBox IsUseV,
            CheckBox IsOverrideV,
            RichTextBox LayersV,
            TextBox TrainDtFlNameV,
            TextBox TrainedNetFlNameV,
            TextBox RunDtFlNameV,
            ComboBox TrainAlgV,
            ComboBox ActFuncHiddV,
            ComboBox ActFuncOutV,
            TextBox LearnRateV,
            TextBox Weight1V,
            TextBox Weight2V,
            TextBox SteepHiddV,
            TextBox SteepOutV,
            TextBox DesiredErrV,
            TextBox BitFailLmtV,
            ComboBox TrainStopFuncV,
            TextBox EpochsV,
            TextBox RepOnEpV,
            string NetID,
            TextBox Symbol,
            CheckBox IsUseInFrcst
            )
        {
            try
            {
                CreateNetSettings(NetID);

                WriteSetting(Vars.IsUse, Convert.ToString(IsUseV.Checked), NetID);
                WriteSetting(Vars.LayersParam, string.Join(":", LayersV.Lines), NetID);
                WriteSetting(Vars.IsOverride, Convert.ToString(IsOverrideV.Checked), NetID);
                WriteSetting(Vars.TrainDtFlName, TrainDtFlNameV.Text, NetID);
                WriteSetting(Vars.TrainedNetFlName, TrainedNetFlNameV.Text, NetID);
                WriteSetting(Vars.RunDtFlName, RunDtFlNameV.Text, NetID);
                WriteSetting(Vars.TrainAlg, Convert.ToString(TrainAlgV.SelectedIndex), NetID);
                WriteSetting(Vars.ActFuncHidd, Convert.ToString(ActFuncHiddV.SelectedIndex), NetID);
                WriteSetting(Vars.ActFuncOut, Convert.ToString(ActFuncOutV.SelectedIndex), NetID);
                WriteSetting(Vars.LearnRate, LearnRateV.Text, NetID);
                WriteSetting(Vars.Weight1, Weight1V.Text, NetID);
                WriteSetting(Vars.Weight2, Weight2V.Text, NetID);
                WriteSetting(Vars.SteepHidd, SteepHiddV.Text, NetID);
                WriteSetting(Vars.SteepOut, SteepOutV.Text, NetID);
                WriteSetting(Vars.DesiredErr, DesiredErrV.Text, NetID);
                WriteSetting(Vars.BitFailLmt, BitFailLmtV.Text, NetID);
                WriteSetting(Vars.TrainStopFunc, Convert.ToString(TrainStopFuncV.SelectedIndex), NetID);
                WriteSetting(Vars.Epochs, EpochsV.Text, NetID);
                WriteSetting(Vars.RepOnEp, RepOnEpV.Text, NetID);
                WriteSetting(Vars.Symbol, Symbol.Text, NetID);
                WriteSetting(Vars.IsUseInFrcst, Convert.ToString(IsUseInFrcst.Checked), NetID);
            }
            catch (ArithmeticException)
            {
                MessageBox.Show(Vars.ConvErr);
            }
        }

        public void GetAllSettings(CheckBox IsUseV,
            CheckBox IsOverrideV,
            RichTextBox LayersV,
            TextBox TrainDtFlNameV,
            TextBox TrainedNetFlNameV,
            TextBox RunDtFlNameV,
            ComboBox TrainAlgV,
            ComboBox ActFuncHiddV,
            ComboBox ActFuncOutV,
            TextBox LearnRateV,
            TextBox Weight1V,
            TextBox Weight2V,
            TextBox SteepHiddV,
            TextBox SteepOutV,
            TextBox DesiredErrV,
            TextBox BitFailLmtV,
            ComboBox TrainStopFuncV,
            TextBox EpochsV,
            TextBox RepOnEpV,
            string NetID,
            TextBox Symbol,
            CheckBox IsUseInFrcst
            )
        {
            string filepath = NetID + Vars.XmlExt;

            if (File.Exists(filepath) == true)
                try
                {
                    IsUseV.Checked = Convert.ToBoolean(ReadSetting(NetID, Vars.IsUse));
                    IsOverrideV.Checked = Convert.ToBoolean(ReadSetting(NetID, Vars.IsOverride));
                    LayersV.Lines = ReadSetting(NetID, Vars.LayersParam).Split(new char[] { ':' });
                    TrainDtFlNameV.Text = ReadSetting(NetID, Vars.TrainDtFlName);
                    TrainedNetFlNameV.Text = ReadSetting(NetID, Vars.TrainedNetFlName);
                    RunDtFlNameV.Text = ReadSetting(NetID, Vars.RunDtFlName);

                    TrainAlgV.SelectedIndex = Convert.ToInt16(ReadSetting(NetID, Vars.TrainAlg));
                    ActFuncHiddV.SelectedIndex = Convert.ToInt16(ReadSetting(NetID, Vars.ActFuncHidd));
                    ActFuncOutV.SelectedIndex = Convert.ToInt16(ReadSetting(NetID, Vars.ActFuncOut));

                    LearnRateV.Text = ReadSetting(NetID, Vars.LearnRate);
                    Weight1V.Text = ReadSetting(NetID, Vars.Weight1);
                    Weight2V.Text = ReadSetting(NetID, Vars.Weight2);
                    SteepHiddV.Text = ReadSetting(NetID, Vars.SteepHidd);
                    SteepOutV.Text = ReadSetting(NetID, Vars.SteepOut);
                    DesiredErrV.Text = ReadSetting(NetID, Vars.DesiredErr);
                    BitFailLmtV.Text = ReadSetting(NetID, Vars.BitFailLmt);

                    TrainStopFuncV.SelectedIndex = Convert.ToInt16(ReadSetting(NetID, Vars.TrainStopFunc));

                    EpochsV.Text = ReadSetting(NetID, Vars.Epochs);
                    RepOnEpV.Text = ReadSetting(NetID, Vars.RepOnEp);

                    IsUseInFrcst.Checked = Convert.ToBoolean(ReadSetting(NetID, Vars.IsUseInFrcst));
                   // Symbol.Text = ReadSetting(NetID, Vars.Symbol);
                }
                catch (ArithmeticException)
                {
                    MessageBox.Show(Vars.ConvErr);
                }
        }

        private void CreateNetSettings(string netid)
        {
            string filepath = netid + Vars.XmlExt;

            XmlTextWriter xtw = new XmlTextWriter(filepath, Encoding.UTF32);
            xtw.WriteStartDocument();
            xtw.WriteStartElement(netid);
            xtw.WriteEndDocument();
            xtw.Close();
        }

        private void WriteSetting(string component, string value, string netid)
        {
            string filepath = netid + Vars.XmlExt;

            XmlDocument xd = new XmlDocument();
            FileStream fs = new FileStream(filepath, FileMode.Open);
            xd.Load(fs);

            XmlElement cmpn = xd.CreateElement(Vars.Cmpn);
            cmpn.SetAttribute(Vars.Id, component);

            XmlElement val = xd.CreateElement(Vars.Vl);
            XmlText val_ = xd.CreateTextNode(value);

            val.AppendChild(val_);
            cmpn.AppendChild(val);

            xd.DocumentElement.AppendChild(cmpn);

            fs.Close();
            xd.Save(filepath);
        }

        public string ReadSetting(string netid, string component)
        {
            string filepath = netid + Vars.XmlExt;

            if (File.Exists(filepath) == true)
            {

                string result = Vars.NULL;

                XmlDocument xd = new XmlDocument();
                FileStream fs = new FileStream(filepath, FileMode.Open);
                xd.Load(fs);

                XmlNodeList list = xd.GetElementsByTagName(Vars.Cmpn);

                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement id = (XmlElement)xd.GetElementsByTagName(Vars.Cmpn)[i];
                    XmlElement cmpn = (XmlElement)xd.GetElementsByTagName(Vars.Vl)[i];

                    if (id.GetAttribute(Vars.Id) == component)
                    {
                        result = cmpn.InnerText;
                    }
                }
                fs.Close();

                return result;
            }

            return "";
        }

        public void ExportTradeSettings(
            TextBox MaxErrN1,
            TextBox MaxErrN2,
            TextBox MaxErrN3,
            TextBox MaxErrN4,
            TextBox MaxErrN5,
            TextBox MaxErrN6,
            TextBox Path,
            TextBox Points,
            TextBox PointsSL,
            string NetID,
            TextBox Lot,
            TextBox cmndwaittb, TextBox cmndtaketb, TextBox cmndruntb, TextBox cmndgivetb, TextBox forecasttb, CheckBox IsNoPath, TextBox symboltb,
            CheckBox isusedbcb
            )
        {
            try
            {
                NetID = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_" + NetID;// +"_" + symboltb.Text;
                
                CreateNetSettings(NetID);

                WriteSetting(Vars.MaxErrN1, MaxErrN1.Text, NetID);
                WriteSetting(Vars.MaxErrN2, MaxErrN2.Text, NetID);
                WriteSetting(Vars.MaxErrN3, MaxErrN3.Text, NetID);
                WriteSetting(Vars.MaxErrN4, MaxErrN4.Text, NetID);
                WriteSetting(Vars.MaxErrN5, MaxErrN5.Text, NetID);
                WriteSetting(Vars.MaxErrN6, MaxErrN6.Text, NetID);
                WriteSetting(Vars.Path, Path.Text, NetID);
                WriteSetting(Vars.Points, Points.Text, NetID);
                WriteSetting(Vars.PointsSL, PointsSL.Text, NetID);
                WriteSetting(Vars.Lot, Lot.Text, NetID);

                WriteSetting(Vars.Wait, cmndwaittb.Text, NetID);
                WriteSetting(Vars.Take, cmndtaketb.Text, NetID);
                WriteSetting(Vars.Run, cmndruntb.Text, NetID);
                WriteSetting(Vars.Give, cmndgivetb.Text, NetID);

                WriteSetting(Vars.ForecastPerc, forecasttb.Text, NetID);
                WriteSetting(Vars.NoPath, Convert.ToString(IsNoPath.Checked), NetID);

                WriteSetting(Vars.Symbol, symboltb.Text, NetID);

                WriteSetting(Vars.IsUseDB, (isusedbcb.Checked).ToString(), NetID); 
            }
            catch (ArithmeticException)
            {
                MessageBox.Show(Vars.ConvErr);
            }
        }

        public void ImportTradeSettings(
                    TextBox MaxErrN1,
                    TextBox MaxErrN2,
                    TextBox MaxErrN3,
                    TextBox MaxErrN4,
                    TextBox MaxErrN5,
                    TextBox MaxErrN6,
                    TextBox Path,
                    TextBox Points,
                    TextBox PointsSL,
                    string NetID,
                    TextBox LotTb,
            TextBox cmndwaittb, TextBox cmndtaketb, TextBox cmndruntb, TextBox cmndgivetb, TextBox forecasttb, CheckBox nopathchb, TextBox symboltb,
            CheckBox isusedbcb
            )
        {
            NetID = System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_" + NetID;// +"_" + symboltb.Text;
            
            string filepath = NetID + Vars.XmlExt;

            if (File.Exists(filepath) == true)
                try
                {
                    MaxErrN1.Text = ReadSetting(NetID, Vars.MaxErrN1);
                    MaxErrN2.Text = ReadSetting(NetID, Vars.MaxErrN2);
                    MaxErrN3.Text = ReadSetting(NetID, Vars.MaxErrN3);
                    MaxErrN4.Text = ReadSetting(NetID, Vars.MaxErrN4);
                    MaxErrN5.Text = ReadSetting(NetID, Vars.MaxErrN5);
                    MaxErrN6.Text = ReadSetting(NetID, Vars.MaxErrN6);
                    Path.Text = ReadSetting(NetID, Vars.Path);
                    Points.Text = ReadSetting(NetID, Vars.Points);
                    PointsSL.Text = ReadSetting(NetID, Vars.PointsSL);
                    LotTb.Text = ReadSetting(NetID, Vars.Lot);
                    cmndwaittb.Text = ReadSetting(NetID, Vars.Wait); 
                    cmndtaketb.Text = ReadSetting(NetID, Vars.Take); 
                    cmndruntb.Text = ReadSetting(NetID, Vars.Run);
                    cmndgivetb.Text = ReadSetting(NetID, Vars.Give);
                    forecasttb.Text = ReadSetting(NetID, Vars.ForecastPerc);

                    string ischeck = ReadSetting(NetID, Vars.NoPath);
                    if ((ischeck == "") || (ischeck == "NULL"))
                        ischeck = "true";
                    nopathchb.Checked = Convert.ToBoolean(ischeck);

                    symboltb.Text = ReadSetting(NetID, Vars.Symbol);

                    string isusedb = ReadSetting(NetID, Vars.IsUseDB);
                    if ((isusedb == "") || (isusedb == "NULL"))
                        isusedb = "false";
                    isusedbcb.Checked = Convert.ToBoolean(isusedb);
                }
                catch (ArithmeticException)
                {
                    MessageBox.Show(Vars.ConvErr);
                }
        }
    }
}
