using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FANN.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Timers;

namespace MnFrm
{
    public class NeuroNet
    {
        System.Timers.Timer Tmr = new System.Timers.Timer();

        double currtm = 0;
        
        private MainFrm frm;

        public NeuroNet(MainFrm form)
        {
            frm = form;

            TmrPrepare();
        }

        public void TimingStart()
        {
            currtm = 0;
            Tmr.Enabled = true;
        }

        public void TimingStart(ref double TotalTime, ref System.Timers.Timer TotalTmr)
        {
            TotalTime = 0;
            TotalTmr.Enabled = true;
        }

        public void TimingEnd(ref Label messlbl)
        {
            Tmr.Enabled = false;
            messlbl.Text = messlbl.Text + " ; " + GetTimeString(currtm);
            currtm = 0;
        }

        public void TimingEnd(ref double TotalTime, ref System.Timers.Timer TotalTmr, ref Label messlbl)
        {
            TotalTmr.Enabled = false;
            messlbl.Text = Vars.DoneIn + GetTimeString(TotalTime);
            TotalTime = 0;
        }

        public void TmrPrepare()
        {
            Tmr.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            Tmr.Interval = 1000;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            currtm = currtm + 1;
        }

        public uint[] StringToInts(string[] myString)
        {
            List<uint> ints = new List<uint>();
            foreach (string s in myString)
            {
                uint i;
                if (uint.TryParse(s.Trim(), out i))
                    ints.Add(i);
            }
            return ints.ToArray();
        }

        public void PrepareTrain(ref Label statuslbl, ref Label mselbl, ref ListBox resrtb,
            ref Button createbtn, ref Button runbtn, ref MainFrm frm)
        {
            statuslbl.Text = Vars.TrStarted;
            mselbl.Text = Vars.Noll;
            resrtb.Items.Clear();
            createbtn.Enabled = false;
            runbtn.Enabled = false;
            frm.Refresh();
        }

        public void ResetMSE(bool exist, ref NeuralNet nn)
        {
            if (exist == true)
            {
                nn.ResetMSE();
                nn.ResetErrNo();
                nn.ResetErrStr();
            }
        }

        public void TrainStopFunc(ComboBox trstfcb, TextBox bfltb, ref NeuralNet nn)
        {
            switch (trstfcb.SelectedIndex)
            {
                case 0:
                    nn.SetTrainStopFunction(StopFunction.MSE);
                    break;
                case 1:
                    {
                        nn.SetTrainStopFunction(StopFunction.Bit);
                        nn.SetBitFailLimit(System.Convert.ToSingle(bfltb.Text));
                    }
                    break;
                default:
                    nn.SetTrainStopFunction(StopFunction.MSE);
                    break;
            }
        }

        public void EndingTrain(ref Button createbtn, ref Button runbtn, ref Label mselbl, ref Label statuslbl, ref NeuralNet nn)
        {
            createbtn.Enabled = true;
            runbtn.Enabled = true;
            mselbl.Text = System.Convert.ToString(nn.GetMSE());
            statuslbl.Text = Vars.TrCompleted;
        }

        public void SetInitParams(
            ref ComboBox TrainingAlg, 
            ref ComboBox TrainStopFunc,
            ref ComboBox ActivFuncHidd, 
            ref ComboBox ActivFuncOut, 
            int TrainStopFuncVal, int TrainingAlgVal, int ActivFuncHiddVal, int ActivFuncOutVal, MainFrm form)
        {
            TrainingAlg.DataSource = System.Enum.GetValues(typeof(FANN.Net.TrainingAlgorithm));
            ActivFuncHidd.DataSource = System.Enum.GetValues(typeof(FANN.Net.ActivationFunction));
            ActivFuncOut.DataSource = System.Enum.GetValues(typeof(FANN.Net.ActivationFunction));

            TrainStopFunc.SelectedIndex = TrainStopFuncVal;
            TrainingAlg.SelectedIndex   = TrainingAlgVal;
            ActivFuncHidd.SelectedIndex = ActivFuncHiddVal;
            ActivFuncOut.SelectedIndex  = ActivFuncOutVal;
        }

        public bool RunCheck(bool exist, string filename1, string filename2, ref NeuralNet nn)
        {
            if (exist == false)
            {
                if (File.Exists(filename1) == true)
                    nn.CreateFromFile(filename1);
                else
                {
                    return false;
                }
            }

            if (File.Exists(filename2) == false)
                return false;
            else
                return true;
        }

        public void ResultAreaPrepare(ref ListBox resrtb, string str, ref int NetXCounter)
        {
            frm.DlgtTBLogClear = new MainFrm.DlgtListBoxClear(frm.DlgtTBLogClearProc);
            resrtb.Invoke(frm.DlgtTBLogClear, resrtb);

            frm.DlgtAnyLBLogAdd = new MainFrm.DlgtAnyListBoxAdd(frm.DlgtAnyLBLogAddProc);
            resrtb.Invoke(frm.DlgtAnyLBLogAdd, str, resrtb);

            NetXCounter = 0;
        }

        public void ResultAreaAddStr(ref ListBox resrtb, double src, double res, double srcprev, double resprev, ref int cnt, ref string LastRes)
        {
            // | Source | Result |  Delta | Src. move | Res. move | Diff. | Src. coeff. | Res. coeff. |\n

            src = Math.Round(src, 5);
            res = Math.Round(res, 5);

            string mv1 = GetMoveDirection(Math.Round(src - srcprev, 5));
            string mv2 = GetMoveDirection(Math.Round(res - resprev, 5));
            string check = "";

            LastRes = mv2;

            if (mv1 != mv2)
            {
                check = "X";
                cnt = cnt + 1;
            }
            else
                check = " ";

            string mess =
                "|" + Convert.ToString(src).PadLeft(7, ' ') + " |" +
                Convert.ToString(res).PadLeft(7, ' ') + " |" +
                Convert.ToString(Math.Round(res - src, 5)).PadLeft(7, ' ') + " |" +
                mv1.PadLeft(10, ' ') + " |" +
                mv2.PadLeft(10, ' ') + " |" +
                check.PadLeft(6, ' ') + " |" +
                Convert.ToString(Math.Round(src - srcprev, 5)).PadLeft(12, ' ') + " |" +
                Convert.ToString(Math.Round(res - resprev, 5)).PadLeft(12, ' ') + " |";

            frm.DlgtAnyLBLogAdd = new MainFrm.DlgtAnyListBoxAdd(frm.DlgtAnyLBLogAddProc);
            resrtb.Invoke(frm.DlgtAnyLBLogAdd, mess, resrtb);

            frm.logger.Log("", "", mess);
        }

        public string GetMoveDirection(double val)
        {
            if (val == 0)
                return Vars.Na;
            else
            if (val > 0)
                return Vars.Up;
            else
                return Vars.Dn;
        }

        public void RunEnd(ref NeuralNet nn, ref Label statuslbl, string mess, string filename, ref ListBox runlog, int NetX)
        {
            frm.DlgtAnyLBLogAdd = new MainFrm.DlgtAnyListBoxAdd(frm.DlgtAnyLBLogAddProc);
            runlog.Invoke(frm.DlgtAnyLBLogAdd, Convert.ToString(NetX).PadLeft(54, ' '), runlog);
        }

        public string GetCreateBeginStr(string net)
        {
            return Vars.BeginTrain + net + " : " + System.Convert.ToString(DateTime.Now);
        }

        public string GetCreateEndStr(string net)
        {
            return Vars.EndTrain + net + " : " + System.Convert.ToString(DateTime.Now);
        }

        public string GetRunBeginStr(string net)
        {
            return Vars.BeginRun + net + " : " + System.Convert.ToString(DateTime.Now);
        }

        public string GetRunEndStr(string net)
        {
            return Vars.EndRun + net + " : " + System.Convert.ToString(DateTime.Now);
        }

        public void CreateAndTrainNN(
            ref Label statuslbl, ref Label mselbl,
            ref ListBox resrtb, ref RichTextBox overlaytb,
            ref TextBox overw1tb, ref TextBox overw2tb, ref TextBox overlrtb, ref TextBox overshtb, ref TextBox oversotb, ref TextBox overbfltb,
            ref RichTextBox layerstb, ref TextBox wfrtb, ref TextBox wtotb, ref TextBox lrtb, ref TextBox shtb, ref TextBox sotb, ref TextBox bfltb,
            ref TextBox trdttb, ref TextBox eptb,
            ref TextBox reptb, ref TextBox ertb, ref TextBox nettb,
            ref Button createbtn, ref Button runbtn,
            ref NeuralNet nn,
            ref CheckBox overchb,
            ref ComboBox overtralgcb, ref ComboBox overtrstcb, ref ComboBox tacb, ref ComboBox trstfcb, ref ComboBox afhcb, ref ComboBox afocb,
            ref TrainingData TrData,
            ref bool exist,
            ref uint[] Layers, ref ListBox loglb, ref string NetName, ref CheckBox usechb, ref TextBox drtb, ref TextBox Symbol, ref CheckBox IsUseInFrcst, 
            string FileBasePath
        )
        {
            TimingStart();

            string strlog = "";

            strlog = GetCreateBeginStr(NetName);

            loglb.Items.Add(strlog);

            frm.logger.Log(nettb.Text, Symbol.Text, strlog);

            PrepareTrain(ref statuslbl, ref mselbl, ref resrtb, ref createbtn, ref runbtn, ref frm);

            ResetMSE(exist, ref nn);

            NetSett param = new NetSett();
            param.SetAllSettings(usechb, overchb, layerstb, trdttb, nettb, drtb, tacb, afhcb, afocb, lrtb, wfrtb, wtotb, shtb, sotb, ertb, bfltb, trstfcb, eptb, reptb, NetName, Symbol, IsUseInFrcst);

            if (overchb.Checked == true)
            {
                Layers = StringToInts(overlaytb.Text.Split());
                nn.CreateStandardArray(Layers);
                nn.RandomizeWeights(System.Convert.ToDouble(overw1tb.Text), System.Convert.ToDouble(overw2tb.Text));
                nn.SetLearningRate(System.Convert.ToSingle(overlrtb.Text));
                nn.SetTrainingAlgorithm((TrainingAlgorithm)overtralgcb.SelectedValue);
                nn.SetActivationSteepnessHidden(System.Convert.ToDouble(overshtb.Text));
                nn.SetActivationSteepnessOutput(System.Convert.ToDouble(oversotb.Text));
                TrainStopFunc(overtrstcb, overbfltb, ref nn);
            }
            else
            {
                Layers = StringToInts(layerstb.Text.Split());
                nn.CreateStandardArray(Layers);

                nn.RandomizeWeights(System.Convert.ToDouble(wfrtb.Text), System.Convert.ToDouble(wtotb.Text));

                nn.SetLearningRate(System.Convert.ToSingle(lrtb.Text));

                nn.SetTrainingAlgorithm((TrainingAlgorithm)tacb.SelectedValue);
                nn.SetActivationSteepnessHidden(System.Convert.ToDouble(shtb.Text));
                nn.SetActivationSteepnessOutput(System.Convert.ToDouble(sotb.Text));
                TrainStopFunc(trstfcb, bfltb, ref nn);
            }

            nn.SetActivationFunctionHidden((ActivationFunction)afhcb.SelectedValue);
            nn.SetActivationFunctionOutput((ActivationFunction)afocb.SelectedValue);

            string TrainDataFile = FileBasePath + trdttb.Text + Symbol.Text;

            TrData.ReadTrainFromFile(TrainDataFile);

            nn.TrainOnData(TrData,
                System.Convert.ToUInt32(eptb.Text),
                System.Convert.ToUInt32(reptb.Text),
                System.Convert.ToSingle(ertb.Text));

            string NetFile = FileBasePath + nettb.Text + Symbol.Text;

            nn.Save(NetFile);

            exist = true;

            EndingTrain(ref createbtn, ref runbtn, ref mselbl, ref statuslbl, ref nn);

            strlog = GetCreateEndStr(NetName);

            loglb.Items.Add(strlog);

            frm.logger.Log(nettb.Text, Symbol.Text, strlog);

            TimingEnd(ref statuslbl);
        }

        public void RunNN(
            ref bool exist,
            ref TextBox nettb, ref TextBox drtb, ListBox resrtb,
            ref NeuralNet nn,
            ref TrainingData TrData,
            ref string ResArHeader, ref string RunCompl, ref string CheckFiles,
            ref Label statuslbl, ref ListBox loglb, ref string NetName, ref string RunLogName, 
            ref string CommonLogName, ref int NetXCounter, ref string LastRes, string PathToFiles, string Symbol
            )
        {
            NetXCounter = 0;
            
            frm.DlgtAnyLBLogAdd = new MainFrm.DlgtAnyListBoxAdd(frm.DlgtAnyLBLogAddProc);
            loglb.Invoke(frm.DlgtAnyLBLogAdd, GetRunBeginStr(NetName), loglb);

            string PathToNet = PathToFiles + nettb.Text + Symbol;
            string PathToRunData = PathToFiles + drtb.Text + Symbol;

            frm.logger.Log(nettb.Text, Symbol, String.Format("PathToNet : {0}, PathToRunData : {1}", PathToNet, PathToRunData));

            if (RunCheck(exist, PathToNet, PathToRunData, ref nn) == true)
            {
                TrData.ReadTrainFromFile(PathToRunData);

                ResultAreaPrepare(ref resrtb, ResArHeader, ref NetXCounter);

                double calcOutPrev1 = 0;
                double calcOutPrev2 = 0;

                double trgtDataPrev1 = 0;
                double trgtDataPrev2 = 0;

                for (uint i = 0; i < TrData.TrainingDataLength; ++i)
                {
                    double calcOut = Math.Round(nn.Run(TrData.Input[i])[0], 4);

                    if (i == 0)
                        calcOutPrev2 = calcOut;
                    else
                        calcOutPrev2 = calcOutPrev1;

                    calcOutPrev1 = calcOut;

                    double trgtData = Math.Round(TrData.Output[i][0], 4);

                    if (i == 0)
                        trgtDataPrev2 = trgtData;
                    else
                        trgtDataPrev2 = trgtDataPrev1;

                    trgtDataPrev1 = trgtData;

                    ResultAreaAddStr(ref resrtb, trgtData, calcOut, trgtDataPrev2, calcOutPrev2, ref NetXCounter, ref LastRes);
                }

                RunEnd(ref nn, ref statuslbl, RunCompl, PathToNet, ref resrtb, NetXCounter);
            }
            else
            {
                frm.DlgtTBLogClear = new MainFrm.DlgtListBoxClear(frm.DlgtTBLogClearProc);
                resrtb.Invoke(frm.DlgtTBLogClear, resrtb);
            }

            frm.DlgtAnyLBLogAdd = new MainFrm.DlgtAnyListBoxAdd(frm.DlgtAnyLBLogAddProc);
            loglb.Invoke(frm.DlgtAnyLBLogAdd, GetRunEndStr(NetName), loglb);

            ExportLbToTxt(loglb, CommonLogName);
            ExportLbToTxt(resrtb, RunLogName);

            resrtb.SelectedIndex = resrtb.Items.Count - 1; 
        }

        public string GetTimeString(double Tm)
        {
            return Tm + " s (" + Math.Round(Tm / 60, 2) + " m)";
        }

        public void ExportLbToTxt(ListBox lb, string flname)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(flname);

            foreach (object item in lb.Items)
                sw.WriteLine(item.ToString());

            sw.Close();
        }

        public void ExportLbToTxt(RichTextBox lb, string flname)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(flname);

            foreach (object item in lb.Lines)
                sw.WriteLine(item.ToString());

            sw.Close();
        }
    }
}
