using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using FANN.Net;
using System.Diagnostics;
using MnFrm;
using System.Xml;
using System.Text;
using System.Threading;
using System.Linq.Expressions;
using MySql.Data.MySqlClient; 

namespace MnFrm
{
    public partial class MainFrm : Form
    {
        public Trader trader;

        public Functions funky;

        public delegate void DlgtListBoxClear(ListBox lb);
        public DlgtListBoxClear DlgtTBLogClear;

        public delegate void DlgtAnyListBoxAdd(string pText, ListBox lb);
        public DlgtAnyListBoxAdd DlgtAnyLBLogAdd;

        public delegate void DlgtAnyLabelSetText(string pText, Label lb);
        public DlgtAnyLabelSetText DlgtAnyLblSetText;

        public NeuroNet nncmn;
        public NeuroNet nnc1;
        public NeuroNet nnc2;
        public NeuroNet nnc3;
        public NeuroNet nnc4;
        public NeuroNet nnc5;
        public NeuroNet nnc6;

        public DBLog logger;

        public void DlgtAnyLBLogAddProc(string pText, ListBox lb)
        {
            lb.Items.Add(pText);
        }

        public void DlgtAnyLblSetTextProc(string pText, Label lb)
        {
            lb.Text = pText;
        }

        public void DlgtTBLogClearProc(ListBox lb)
        {
            lb.Items.Clear();
        }

        public MainFrm()
        {
            InitializeComponent();

            funky = new Functions();

            nncmn = new NeuroNet(this);
            nncmn.TmrPrepare();
            
            nnc1 = new NeuroNet(this);
            nnc1.TmrPrepare();

            nnc2 = new NeuroNet(this);
            nnc2.TmrPrepare();

            nnc3 = new NeuroNet(this);
            nnc3.TmrPrepare();

            nnc4 = new NeuroNet(this);
            nnc4.TmrPrepare();

            nnc5 = new NeuroNet(this);
            nnc5.TmrPrepare();

            nnc6 = new NeuroNet(this);
            nnc6.TmrPrepare();

            nnc1.SetInitParams(ref tacb1, ref trstfcb1, ref afhcb1, ref afocb1, 0, 2, 11, 10, this);
            nnc2.SetInitParams(ref tacb2, ref trstfcb2, ref afhcb2, ref afocb2, 0, 2, 11, 10, this);
            nnc3.SetInitParams(ref tacb3, ref trstfcb3, ref afhcb3, ref afocb3, 0, 2, 11, 10, this);
            nnc4.SetInitParams(ref tacb4, ref trstfcb4, ref afhcb4, ref afocb4, 0, 2, 11, 10, this);
            nnc5.SetInitParams(ref tacb5, ref trstfcb5, ref afhcb5, ref afocb5, 0, 2, 11, 10, this);
            nnc6.SetInitParams(ref tacb6, ref trstfcb6, ref afhcb6, ref afocb6, 0, 2, 11, 10, this);
            nncmn.SetInitParams(ref overtralgcb, ref overtrstcb, ref overafhcb, ref overafocb, 0, 2, 11, 10, this);

            ImpSett1();
            ImpSett2();
            ImpSett3();
            ImpSett4();
            ImpSett5();
            ImpSett6();

            ImpTrade();

            if (nopathchb.Checked == true)
            {
                pathtb.Text = Application.StartupPath + "\\";
            }


            trader = new Trader(this);

            logger = new DBLog();

            Vars.TmrPrepare();

            pathlbl.Text = Application.StartupPath;

            this.Text = System.AppDomain.CurrentDomain.FriendlyName;

            string paramString = String.Join(" ", Vars.args_global);

            paramslbl.Text = paramString;

            foreach (string str in Vars.args_global)
            {
                if (Vars.args_global[0] == Vars.Autostart)
                {
                    trsdestartbtn_Click(null, null);
                }
            }
        }

        private void cb_Click(object sender, EventArgs e)
        {
            commstatlbl.Text = Vars.TrStarted;
            tabs.Enabled = false;

            nncmn.TimingStart(ref Vars.TotalTime, ref Vars.TmrTotal);

            tabs.SelectTab(0);
            this.Refresh();

            if (use1chb.Checked == true)
            {
                CreateNet1();
                RunNet1();
            }

            this.Refresh();

            if (use2chb.Checked == true)
            {
                CreateNet2();
                RunNet2();
            }

            tabs.SelectTab(1);
            this.Refresh();

            if (use3chb.Checked == true)
            {
                CreateNet3();
                RunNet3();
            }

            tabs.SelectTab(2);
            this.Refresh();

            if (use4chb.Checked == true)
            {
                CreateNet4();
                RunNet4();
            }

            tabs.SelectTab(3);
            this.Refresh();

            if (use5chb.Checked == true)
            {
                CreateNet5();
                RunNet5();
            }

            tabs.SelectTab(4);
            this.Refresh();

            if (use6chb.Checked == true)
            {
                CreateNet6();
                RunNet6();
            }

            tabs.SelectTab(5);
            tabs.Enabled = true;
            this.Refresh();

            nncmn.ExportLbToTxt(loglb, Vars.LogName);

            nncmn.TimingEnd(ref Vars.TotalTime, ref Vars.TmrTotal, ref commstatlbl);
        }

        public void CreateNet1()
        {
            nnc1.CreateAndTrainNN(ref statuslbl1, ref mselbl1, ref resrtb1, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb1, ref wfrtb1, ref wtotb1, ref lrtb1, ref shtb1, ref sotb1, ref bfltb1, ref trdttb1, ref eptb1, ref reptb1, ref ertb1,
                ref nettb1, ref createbtn1, ref runbtn1,  
                ref Vars.nn1, 
                ref over1chb, ref overtralgcb, ref overtrstcb, ref tacb1, ref trstfcb1,
                ref afhcb1, ref afocb1, ref Vars.TrData1, ref Vars.exist1, ref Vars.Layers, ref loglb, ref Vars.Net1, ref use1chb, ref drtb1, ref symboltb, ref isuseinfrcst1chb, pathtb.Text);
        }

        public void CreateNet2()
        {
            nnc2.CreateAndTrainNN(ref statuslbl2, ref mselbl2, ref resrtb2, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb2, ref wfrtb2, ref wtotb2, ref lrtb2, ref shtb2, ref sotb2, ref bfltb2, ref trdttb2, ref eptb2, ref reptb2, ref ertb2,
                ref nettb2, ref createbtn2, ref runbtn2, 
                ref Vars.nn2, 
                ref over2chb, ref overtralgcb, ref overtrstcb, ref tacb2, ref trstfcb2,
                ref afhcb2, ref afocb2, ref Vars.TrData2, ref Vars.exist2, ref Vars.Layers, ref loglb, ref Vars.Net2, ref use2chb, ref drtb2, ref symboltb, ref isuseinfrcst2chb, pathtb.Text);
        }

        public void CreateNet3()
        {
            nnc3.CreateAndTrainNN(ref statuslbl3, ref mselbl3, ref resrtb3, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb3, ref wfrtb3, ref wtotb3, ref lrtb3, ref shtb3, ref sotb3, ref bfltb3, ref trdttb3, ref eptb3, ref reptb3, ref ertb3,
                ref nettb3, ref createbtn3, ref runbtn3, ref Vars.nn3, ref over3chb, ref overtralgcb, ref overtrstcb, ref tacb3, ref trstfcb3,
                ref afhcb3, ref afocb3, ref Vars.TrData3, ref Vars.exist3, ref Vars.Layers, ref loglb, ref Vars.Net3, ref use3chb, ref drtb3, ref symboltb, ref isuseinfrcst3chb, pathtb.Text);
        }

        public void CreateNet4()
        {
            nnc4.CreateAndTrainNN(ref statuslbl4, ref mselbl4, ref resrtb4, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb4, ref wfrtb4, ref wtotb4, ref lrtb4, ref shtb4, ref sotb4, ref bfltb4, ref trdttb4, ref eptb4, ref reptb4, ref ertb4,
                ref nettb4, ref createbtn4, ref runbtn4,  ref Vars.nn4, ref over4chb, ref overtralgcb, ref overtrstcb, ref tacb4, ref trstfcb4,
                ref afhcb4, ref afocb4, ref Vars.TrData4, ref Vars.exist4, ref Vars.Layers, ref loglb, ref Vars.Net4, ref use4chb, ref drtb4, ref symboltb, ref isuseinfrcst4chb, pathtb.Text);
        }

        public void CreateNet5()
        {
            nnc5.CreateAndTrainNN(ref statuslbl5, ref mselbl5, ref resrtb5, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb5, ref wfrtb5, ref wtotb5, ref lrtb5, ref shtb5, ref sotb5, ref bfltb5, ref trdttb5, ref eptb5, ref reptb5, ref ertb5,
                ref nettb5, ref createbtn5, ref runbtn5, ref Vars.nn5, ref over5chb, ref overtralgcb, ref overtrstcb, ref tacb5, ref trstfcb5,
                ref afhcb5, ref afocb5, ref Vars.TrData5, ref Vars.exist5, ref Vars.Layers, ref loglb, ref Vars.Net5, ref use5chb, ref drtb5, ref symboltb, ref isuseinfrcst5chb, pathtb.Text);
        }

        public void CreateNet6()
        {
            nnc6.CreateAndTrainNN(ref statuslbl6, ref mselbl6, ref resrtb6, ref overlaytb, ref overw1tb, ref overw2tb, ref overlrtb, ref overshtb, ref oversotb,
                ref overbfltb, ref layerstb6, ref wfrtb6, ref wtotb6, ref lrtb6, ref shtb6, ref sotb6, ref bfltb6, ref trdttb6, ref eptb6, ref reptb6, ref ertb6,
                ref nettb6, ref createbtn6, ref runbtn6,  ref Vars.nn6, ref over6chb, ref overtralgcb, ref overtrstcb, ref tacb6, ref trstfcb6,
                ref afhcb6, ref afocb6, ref Vars.TrData6, ref Vars.exist6, ref Vars.Layers, ref loglb, ref Vars.Net6, ref use6chb, ref drtb6, ref symboltb, ref isuseinfrcst6chb, pathtb.Text);
        }

        private void createbtn1_Click(object sender, EventArgs e)
        {
            CreateNet1();
        }

        private void createbtn2_Click(object sender, EventArgs e)
        {
            CreateNet2();
        }

        private void createbtn3_Click(object sender, EventArgs e)
        {
            CreateNet3();
        }

        private void createbtn4_Click(object sender, EventArgs e)
        {
            CreateNet4();
        }

        private void createbtn5_Click(object sender, EventArgs e)
        {
            CreateNet5();
        }

        private void createbtn6_Click(object sender, EventArgs e)
        {
            CreateNet6();
        }

        public void RunNet1()
        {
            nnc1.RunNN(ref Vars.exist1, ref nettb1, ref drtb1, resrtb1, ref Vars.nn1, ref Vars.TrData1, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl1, ref loglb, ref Vars.Net1, ref Vars.Log1, ref Vars.LogName, ref Vars.Net1X, ref Vars.NN1LastRes, 
                pathtb.Text, symboltb.Text);
        }

        public void RunNet2()
        {
            nnc2.RunNN(ref Vars.exist2, ref nettb2, ref drtb2, resrtb2, ref Vars.nn2, ref Vars.TrData2, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl2, ref loglb, ref Vars.Net2, ref Vars.Log2, ref Vars.LogName, ref Vars.Net2X, ref Vars.NN2LastRes,
                pathtb.Text, symboltb.Text);
        }

        public void RunNet3()
        {
            nnc3.RunNN(ref Vars.exist3, ref nettb3, ref drtb3, resrtb3, ref Vars.nn3, ref Vars.TrData3, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl3, ref loglb, ref Vars.Net3, ref Vars.Log3, ref Vars.LogName, ref Vars.Net3X, ref Vars.NN3LastRes,
                pathtb.Text, symboltb.Text);
        }

        public void RunNet4()
        {
            nnc4.RunNN(ref Vars.exist4, ref nettb4, ref drtb4, resrtb4, ref Vars.nn4, ref Vars.TrData4, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl4, ref loglb, ref Vars.Net4, ref Vars.Log4, ref Vars.LogName, ref Vars.Net4X, ref Vars.NN4LastRes,
                pathtb.Text, symboltb.Text);
        }

        public void RunNet5()
        {
            nnc5.RunNN(ref Vars.exist5, ref nettb5, ref drtb5, resrtb5, ref Vars.nn5, ref Vars.TrData5, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl5, ref loglb, ref Vars.Net5, ref Vars.Log5, ref Vars.LogName, ref Vars.Net5X, ref Vars.NN5LastRes,
                pathtb.Text, symboltb.Text);
        }

        public void RunNet6()
        {
            nnc6.RunNN(ref Vars.exist6, ref nettb6, ref drtb6, resrtb6, ref Vars.nn6, ref Vars.TrData6, ref Vars.ResArHeader, ref Vars.RunCompl,
                ref Vars.CheckFiles, ref statuslbl6, ref loglb, ref Vars.Net6, ref Vars.Log6, ref Vars.LogName, ref Vars.Net6X, ref Vars.NN6LastRes,
                pathtb.Text, symboltb.Text);
        }

        private void runbtn1_Click(object sender, EventArgs e)
        {
            RunNet1();
        }

        public void runbtn2_Click(object sender, EventArgs e)
        {
            RunNet2();
        }

        public void runbtn3_Click(object sender, EventArgs e)
        {
            RunNet3();
        }

        public void runbtn4_Click(object sender, EventArgs e)
        {
            RunNet4();
        }

        public void runbtn5_Click(object sender, EventArgs e)
        {
            RunNet5();
        }

        public void runbtn6_Click(object sender, EventArgs e)
        {
            RunNet6();
        }

        public void ExpSett1()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use1chb, over1chb, layerstb1, trdttb1, nettb1, drtb1, tacb1, afhcb1, afocb1, lrtb1, wfrtb1, wtotb1, shtb1, sotb1, ertb1, bfltb1, trstfcb1, eptb1, reptb1, Vars.Net1, symboltb, isuseinfrcst1chb);
        }

        public void ExpSett2()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use2chb, over2chb, layerstb2, trdttb2, nettb2, drtb2, tacb2, afhcb2, afocb2, lrtb2, wfrtb2, wtotb2, shtb2, sotb2, ertb2, bfltb2, trstfcb2, eptb2, reptb2, Vars.Net2, symboltb, isuseinfrcst2chb);
        }

        public void ExpSett3()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use3chb, over3chb, layerstb3, trdttb3, nettb3, drtb3, tacb3, afhcb3, afocb3, lrtb3, wfrtb3, wtotb3, shtb3, sotb3, ertb3, bfltb3, trstfcb3, eptb3, reptb3, Vars.Net3, symboltb, isuseinfrcst3chb);
        }

        public void ExpSett4()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use4chb, over4chb, layerstb4, trdttb4, nettb4, drtb4, tacb4, afhcb4, afocb4, lrtb4, wfrtb4, wtotb4, shtb4, sotb4, ertb4, bfltb4, trstfcb4, eptb4, reptb4, Vars.Net4, symboltb, isuseinfrcst4chb);
        }

        public void ExpSett5()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use5chb, over5chb, layerstb5, trdttb5, nettb5, drtb5, tacb5, afhcb5, afocb5, lrtb5, wfrtb5, wtotb5, shtb5, sotb5, ertb5, bfltb5, trstfcb5, eptb5, reptb5, Vars.Net5, symboltb, isuseinfrcst5chb);
        }

        public void ExpSett6()
        {
            NetSett param = new NetSett();
            param.SetAllSettings(use6chb, over6chb, layerstb6, trdttb6, nettb6, drtb6, tacb6, afhcb6, afocb6, lrtb6, wfrtb6, wtotb6, shtb6, sotb6, ertb6, bfltb6, trstfcb6, eptb6, reptb6, Vars.Net6, symboltb, isuseinfrcst6chb);
        }

        public void ExpTrade()
        {
            NetSett param = new NetSett();
            param.ExportTradeSettings(vl1tb, vl2tb, vl3tb, vl4tb, vl5tb, vl6tb, pathtb, actTPtb, actSLtb, Vars.TradeSett, LotTb, cmndwaittb, cmndtaketb, cmndruntb, cmndgivetb, forecasttb, nopathchb, symboltb, isusedbcb);
        }

        public void ImpSett1()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use1chb, over1chb, layerstb1, trdttb1, nettb1, drtb1, tacb1, afhcb1, afocb1, lrtb1, wfrtb1, wtotb1, shtb1, sotb1, ertb1, bfltb1, trstfcb1, eptb1, reptb1, Vars.Net1, symboltb, isuseinfrcst1chb);
        }

        public void ImpSett2()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use2chb, over2chb, layerstb2, trdttb2, nettb2, drtb2, tacb2, afhcb2, afocb2, lrtb2, wfrtb2, wtotb2, shtb2, sotb2, ertb2, bfltb2, trstfcb2, eptb2, reptb2, Vars.Net2, symboltb, isuseinfrcst2chb);
        }

        public void ImpSett3()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use3chb, over3chb, layerstb3, trdttb3, nettb3, drtb3, tacb3, afhcb3, afocb3, lrtb3, wfrtb3, wtotb3, shtb3, sotb3, ertb3, bfltb3, trstfcb3, eptb3, reptb3, Vars.Net3, symboltb, isuseinfrcst3chb);
        }

        public void ImpSett4()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use4chb, over4chb, layerstb4, trdttb4, nettb4, drtb4, tacb4, afhcb4, afocb4, lrtb4, wfrtb4, wtotb4, shtb4, sotb4, ertb4, bfltb4, trstfcb4, eptb4, reptb4, Vars.Net4, symboltb, isuseinfrcst4chb);
        }

        public void ImpSett5()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use5chb, over5chb, layerstb5, trdttb5, nettb5, drtb5, tacb5, afhcb5, afocb5, lrtb5, wfrtb5, wtotb5, shtb5, sotb5, ertb5, bfltb5, trstfcb5, eptb5, reptb5, Vars.Net5, symboltb, isuseinfrcst5chb);
        }

        public void ImpSett6()
        {
            NetSett param = new NetSett();
            param.GetAllSettings(use6chb, over6chb, layerstb6, trdttb6, nettb6, drtb6, tacb6, afhcb6, afocb6, lrtb6, wfrtb6, wtotb6, shtb6, sotb6, ertb6, bfltb6, trstfcb6, eptb6, reptb6, Vars.Net6, symboltb, isuseinfrcst6chb);
        }

        public void ImpTrade()
        {
            NetSett param = new NetSett();
            param.ImportTradeSettings(vl1tb, vl2tb, vl3tb, vl4tb, vl5tb, vl6tb, pathtb, actTPtb, actSLtb, Vars.TradeSett, LotTb, cmndwaittb, cmndtaketb, cmndruntb, cmndgivetb, forecasttb, nopathchb, symboltb, isusedbcb);
        }

        private void expbtn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(loglb, cmnlogtb.Text);
        }

        private void logn1_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb1, Vars.Log1);
        }

        private void logn2btn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb2, Vars.Log2);
        }

        private void logn3btn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb3, Vars.Log3);
        }

        private void logn4btn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb4, Vars.Log4);
        }

        private void logn5btn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb5, Vars.Log5);
        }

        private void logn6btn_Click(object sender, EventArgs e)
        {
            nnc1.ExportLbToTxt(resrtb6, Vars.Log6);
        }

        private void expsettbtn1_Click(object sender, EventArgs e)
        {
            ExpSett1();
        }

        private void expsettbtn2_Click(object sender, EventArgs e)
        {
            ExpSett2();
        }

        private void expsettbtn3_Click(object sender, EventArgs e)
        {
            ExpSett3();
        }

        private void expsettbtn4_Click(object sender, EventArgs e)
        {
            ExpSett4();
        }

        private void expsettbtn5_Click(object sender, EventArgs e)
        {
            ExpSett5();
        }

        private void expsettbtn6_Click(object sender, EventArgs e)
        {
            ExpSett6();
        }

        private void impsettbtn1_Click(object sender, EventArgs e)
        {
            ImpSett1();
        }

        private void impsettbtn2_Click(object sender, EventArgs e)
        {
            ImpSett2();
        }

        private void impsettbtn3_Click(object sender, EventArgs e)
        {
            ImpSett3();
        }

        private void impsettbtn4_Click(object sender, EventArgs e)
        {
            ImpSett4();
        }

        private void impsettbtn5_Click(object sender, EventArgs e)
        {
            ImpSett5(); 
        }

        private void impsettbtn6_Click(object sender, EventArgs e)
        {
            ImpSett6();
        }

        private void trsdestartbtn_Click(object sender, EventArgs e)
        {
            if (trader.IsTrading == true)
            {
                trader.TmrTradeStop();
                trsdestartbtn.Text = Vars.TradeStopped;
            }
            else
                if (trader.IsTrading == false)
                {
                    trader.SetAllX();
                    trader.TmrPrepTrade();
                    trader.TmrTradeStart();
                    trsdestartbtn.Text = Vars.TradeStarted;
                }
        }

        private void expallbtn_Click(object sender, EventArgs e)
        {
            ExpSett1();
            ExpSett2();
            ExpSett3();
            ExpSett4();
            ExpSett5();
            ExpSett6();
            ExpTrade();
        }

        private void impallbtn_Click(object sender, EventArgs e)
        {
            ImpSett1();
            ImpSett2();
            ImpSett3();
            ImpSett4();
            ImpSett5();
            ImpSett6();
            ImpTrade();
        }

        public void ExptrdLogBtn_Click(object sender, EventArgs e)
        {
          //  nnc1.ExportLbToTxt(tradeloglb, TradeLogTb.Text);
        }

        private void RunAllbtn_Click(object sender, EventArgs e)
        {
            RunNet1();
            RunNet2();
            RunNet3();
            RunNet4();
            RunNet5();
            RunNet6();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExpTrade();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImpTrade();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            string act = "";

            if (killrb.Checked == true)
                act = Convert.ToString(killrb.Tag);

            if (copyrb.Checked == true)
                act = Convert.ToString(copyrb.Tag);

            funky.Action(Convert.ToString((sender as Button).Tag), act, flproclb, trgtdirlb, valuetb);
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            addbtn_Click(sender, e);
        }

        private void chngbtn_Click(object sender, EventArgs e)
        {
            addbtn_Click(sender, e);
        }

        private void executebtn_Click(object sender, EventArgs e)
        {
            addbtn_Click(sender, e);
        }

        private void flproclb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flproclb.SelectedIndex >= 0)
              valuetb.Text = Convert.ToString(flproclb.Items[flproclb.SelectedIndex]);
        }

        private void trgtdirlb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (trgtdirlb.SelectedIndex >= 0)
              valuetb.Text = Convert.ToString(trgtdirlb.Items[trgtdirlb.SelectedIndex]);
        }

        private string GetConnectionString()
        {
            return "SERVER=" + dbsrvrpath.Text + ";DATABASE=" + dbnametb.Text + ";" + "UID=root;" + "PASSWORD=cfitymrf;";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string MyConString = GetConnectionString();
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = "select * from nlc_forecast";
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string thisrow = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    thisrow += Reader.GetValue(i).ToString() + ",";
                listBox1.Items.Add(thisrow);
            }
            connection.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    MySqlCommand cmd;

                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO nlc_forecast(forecast, symbol, time_forecast) VALUES(@forecast, @symbol, @time_forecast)";
                    cmd.Parameters.AddWithValue("@forecast", "UP");
                    cmd.Parameters.AddWithValue("@symbol", this.symboltb.Text);
                    cmd.Parameters.AddWithValue("@time_forecast", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
