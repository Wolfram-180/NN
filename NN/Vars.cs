using FANN.Net;
using MnFrm;
using System.Timers;

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Text;
using System.Threading;
//using System.Linq.Expressions;

using System.Reflection;


namespace MnFrm
{
    public static class Vars
    {
        public static string XmlExt = ".xml";
        public static string NetParamsFileName = "net_params";
        public static uint[] Layers;
        public static string TrStarted   = "train in process";
        public static string TrCompleted = "train done";
        public static string Noll        = "0";
        public static string NoWeb       = "Net doesn`t exists";
        public static string ResArHeader = "| Source | Result |  Delta | Src. move | Res. move | Diff. | Src. coeff. | Res. coeff. |";
        public static string RunCompl    = "Run complete";
        public static string CheckFiles  = "Check files";
        public static string BeginTrain  = "Begin train of ";
        public static string EndTrain    = "End train of ";
        public static string BeginRun    = "Begin run of ";
        public static string EndRun      = "End run of ";
        public static string LogName = "log_common.txt";

        public static string Lot = "Lot";
        public static string Symbol = "Symbol";
        public static string IsUseDB = "IsUseDB";
        public static string IsUseInFrcst = "IsUseInFrcst";

        public static string Log1 = "log1.txt";
        public static string Log2 = "log2.txt";
        public static string Log3 = "log3.txt";
        public static string Log4 = "log4.txt";
        public static string Log5 = "log5.txt";
        public static string Log6 = "log6.txt";

        public static string Net1 = "Net1";
        public static string Net2 = "Net2";
        public static string Net3 = "Net3";
        public static string Net4 = "Net4";
        public static string Net5 = "Net5";
        public static string Net6 = "Net6";

        public static string TradeSett = "TradeSett";
        
        public static string Cmpn = "component";
        public static string Id = "id";
        public static string Vl = "value";
        public static string NULL = "NULL";

        public static string ConvErr = "Possibly convert error, check data and delimiter";

        public static int Net1X = 0;
        public static int Net2X = 0;
        public static int Net3X = 0;
        public static int Net4X = 0;
        public static int Net5X = 0;
        public static int Net6X = 0;

        public static string NN1LastRes = "";
        public static string NN2LastRes = "";
        public static string NN3LastRes = "";
        public static string NN4LastRes = "";
        public static string NN5LastRes = "";
        public static string NN6LastRes = "";

        public static string Wait = "Wait";
        public static string Take = "Take";
        public static string Run = "Run";
        public static string Give = "Give";
        public static string ForecastPerc = "ForecastPerc";
        public static string NoPath = "NoPath";

        public static string FBtnDel  = "FDel";
        public static string FBtnChng = "FChng";
        public static string FBtnAdd  = "FAdd";
        public static string FBtnExec = "FExec";

        public static string FActKill = "kill";
        public static string FActCopy = "copy";

        public static double TotalTime = 0;
        public static double TimeCurr = 0;

        public static string Up = "UP";
        public static string Dn = "DN";
        public static string Na = "--";

        public static System.Timers.Timer TmrTotal = new System.Timers.Timer();
        public static System.Timers.Timer TmrCurr = new System.Timers.Timer();
        public static System.Timers.Timer TmrTrade = new System.Timers.Timer();

        public static NeuralNet nn1 = new NeuralNet();
        public static TrainingData TrData1 = new TrainingData();
        public static bool exist1 = false;

        public static NeuralNet nn2 = new NeuralNet();
        public static TrainingData TrData2 = new TrainingData();
        public static bool exist2 = false;

        public static NeuralNet nn3 = new NeuralNet();
        public static TrainingData TrData3 = new TrainingData();
        public static bool exist3 = false;

        public static NeuralNet nn4 = new NeuralNet();
        public static TrainingData TrData4 = new TrainingData();
        public static bool exist4 = false;

        public static NeuralNet nn5 = new NeuralNet();
        public static TrainingData TrData5 = new TrainingData();
        public static bool exist5 = false;

        public static NeuralNet nn6 = new NeuralNet();
        public static TrainingData TrData6 = new TrainingData();
        public static bool exist6 = false;

        public static string TradeStarted = "Trade started";
        public static string TradeStopped = "Trade stopped";
        public static string Deleted = "Deleted : ";
        public static string IsMaxXTrue = "IsMaxX = true";
        public static string IsMaxXFalse = "IsMaxX = false";
        public static string Forecast = "Forecast : ";
        public static string StartAllNets = "StartAllNets";

        public static string CmndWait = "Wait";
        public static string CmndTakeData = "TakeData";
        public static string CmndRunData = "RunData";
        public static string CmndNNCmnd = "NNCmnd";
        public static string CmndNNCmndReceivedInProc = "NNCmndReceivedInProc";

        public static string DoneIn = "done in ";

        public static string IsUse = "IsUse";
        public static string LayersParam = "Layers";
        public static string IsOverride = "IsOverride";
        public static string TrainDtFlName = "TrainDtFlName";
        public static string TrainedNetFlName = "TrainedNetFlName";
        public static string RunDtFlName = "RunDtFlName";
        public static string TrainAlg = "TrainAlg";
        public static string ActFuncHidd = "ActFuncHidd";
        public static string ActFuncOut = "ActFuncOut";
        public static string LearnRate = "LearnRate";
        public static string Weight1 = "Weight1";
        public static string Weight2 = "Weight2";
        public static string SteepHidd = "SteepHidd";
        public static string SteepOut = "SteepOut";
        public static string DesiredErr = "DesiredErr";
        public static string BitFailLmt = "BitFailLmt";
        public static string TrainStopFunc = "TrainStopFunc";
        public static string Epochs = "Epochs";
        public static string RepOnEp = "RepOnEp";
        public static string MaxErrN1 = "MaxErrN1";
        public static string MaxErrN2 = "MaxErrN2";
        public static string MaxErrN3 = "MaxErrN3";
        public static string MaxErrN4 = "MaxErrN4";
        public static string MaxErrN5 = "MaxErrN5";
        public static string MaxErrN6 = "MaxErrN6";
        public static string Path = "Path";
        public static string Points = "Points";
        public static string PointsSL = "PointsSL";

        public static string Autostart = "auto";

        public static string[] args_global;
        
        public static void TmrPrepare()
        {
            TmrTotal.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            TmrTotal.Interval = 1000;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TotalTime = TotalTime + 1;
        }
    }
}