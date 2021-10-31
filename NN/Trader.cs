using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace MnFrm
{
    public class Trader
    {
        int N1MaxX = 0;
        int N2MaxX = 0;
        int N3MaxX = 0;
        int N4MaxX = 0;
        int N5MaxX = 0;
        int N6MaxX = 0;

        public System.Timers.Timer TradeTmr = new System.Timers.Timer();

        public bool IsTrading = false;

        int TradeInterval = 5000;

        double TradeTime = 0;

        private MainFrm frm;

        public System.Timers.Timer Tmr = new System.Timers.Timer();

        double up = 0;
        double down = 0;

        int LogLength = 500;

        private ListBox LogLb;

        string CmndWait = Vars.CmndWait;
        string CmndTakeData = Vars.CmndTakeData;
        string CmndRunData = Vars.CmndRunData;
        string CmndNNCmnd = Vars.CmndNNCmnd;
        string filespath = "";
/*
        private void Log(string mess)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                {
                    MySqlCommand cmd;

                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO nlc_log (mess) VALUES (@mess)";
                    cmd.Parameters.AddWithValue("@mess", frm.symboltb.Text + " : " + mess);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
*/
        public Trader(MainFrm form)
        {
            frm = form;
            // LogLb = form.tradeloglb;
        }

        public void TmrPrepTrade()
        {
            Tmr.Elapsed += new ElapsedEventHandler(OnTmrTradeEvent);
            Tmr.Interval = TradeInterval;
            TradeTime = 0;
        }

        public void TmrTradeStart()
        {
            Tmr.Enabled = true;
            IsTrading = true;
            //Log(Vars.TradeStarted);
            frm.logger.Log("", "", Vars.TradeStarted);
        }

        public void TmrTradeStop()
        {
            Tmr.Enabled = false;
            IsTrading = false;
            //Log(Vars.TradeStopped);
            frm.logger.Log("", "", Vars.TradeStopped);
        }
/*
        private string GetConnectionString()
        {
            return "SERVER=localhost;DATABASE=nalice;UID=root;PASSWORD=cfitymrf;";
        }
*/
        private void OnTmrTradeEvent(object source, ElapsedEventArgs e)
        {
            string UpOrDown;
            
            TradeTime = TradeTime + 1;

            Tmr.Enabled = false;

            if (GetCommand() == CmndTakeData)
            {
                StartAllNets();

                UpOrDown = IsUpDown();

                frm.logger.Log("", "", "Trader IsUpDown() returns : " + UpOrDown);

          //      if ((IsMaxX() == true) &&
          //         (GetForecastPerc() >= Convert.ToDouble(frm.forecasttb.Text)) &&
          //          ((IsUpDown() == Vars.Up) || (IsUpDown() == Vars.Dn))
          //          )
                if (IsMaxX() == true)
                {
                    if (GetForecastPerc() >= Convert.ToDouble(frm.forecasttb.Text))
                    {
                        if ((UpOrDown == Vars.Up) || (UpOrDown == Vars.Dn))
                        {
                            WriteCommand(CmndNNCmnd, IsUpDown(), Convert.ToInt16(frm.actTPtb.Text), Convert.ToInt16(frm.actSLtb.Text), frm.LotTb.Text);

                            //if (frm.isusedbcb.Checked == true)
                            //{
                                frm.logger.Log("", frm.symboltb.Text, UpOrDown);
                            //}
/*
                                try
                                {
                                    using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
                                    {
                                        MySqlCommand cmd;

                                        connection.Open();
                                        cmd = connection.CreateCommand();
                                        cmd.CommandText = "INSERT INTO nlc_forecast(forecast, symbol, time_forecast) VALUES(@forecast, @symbol, @time_forecast)";
                                        cmd.Parameters.AddWithValue("@forecast", UpOrDown);
                                        cmd.Parameters.AddWithValue("@symbol", frm.symboltb.Text);
                                        cmd.Parameters.AddWithValue("@time_forecast", DateTime.Now);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
*/
                        }
                    }
                }
                else
                {
                    DeleteCommand(filespath + CmndTakeData);
                    DeleteCommand(filespath + CmndRunData);
                    DeleteCommand(filespath + CmndNNCmnd);
                }
            }

            Tmr.Enabled = true;
        }

        public void SetAllX()
        {
            CmndWait     = frm.cmndwaittb.Text + frm.symboltb.Text;
            CmndTakeData = frm.cmndtaketb.Text + frm.symboltb.Text;
            CmndRunData  = frm.cmndruntb.Text + frm.symboltb.Text;
            CmndNNCmnd   = frm.cmndgivetb.Text + frm.symboltb.Text;
            filespath    = frm.pathtb.Text;

            N1MaxX = Convert.ToInt16(frm.vl1tb.Text);
            N2MaxX = Convert.ToInt16(frm.vl2tb.Text);
            N3MaxX = Convert.ToInt16(frm.vl3tb.Text);
            N4MaxX = Convert.ToInt16(frm.vl4tb.Text);
            N5MaxX = Convert.ToInt16(frm.vl5tb.Text);
            N6MaxX = Convert.ToInt16(frm.vl6tb.Text);

            /*
            N1MaxX = frm.use1chb.Checked == true ? Convert.ToInt16(frm.vl1tb.Text) : N1MaxX = -1;
            N2MaxX = frm.use2chb.Checked == true ? Convert.ToInt16(frm.vl2tb.Text) : N2MaxX = -1;
            N3MaxX = frm.use3chb.Checked == true ? Convert.ToInt16(frm.vl3tb.Text) : N3MaxX = -1;
            N4MaxX = frm.use4chb.Checked == true ? Convert.ToInt16(frm.vl4tb.Text) : N4MaxX = -1;
            N5MaxX = frm.use5chb.Checked == true ? Convert.ToInt16(frm.vl5tb.Text) : N5MaxX = -1;
            N6MaxX = frm.use6chb.Checked == true ? Convert.ToInt16(frm.vl6tb.Text) : N6MaxX = -1;
             */
        }

        private string GetCommand()
        {
            string text = "";

            if (File.Exists(filespath + CmndWait) == true)
            {
                text = Vars.Na;
                return text;
            }
/*
            if (File.Exists(filespath + CmndNNCmndReceivedInProc) == true)
            {
                text = Vars.Na;
                return text;
            }
*/
            if (File.Exists(filespath + CmndTakeData) == true)
            {
                //Log(CmndTakeData); // add comm 25022013
                frm.logger.Log("", "", CmndTakeData);
                return CmndTakeData;
            }

            return Vars.Na;
        }

        private void DeleteCommand(string cmnd)
        {
            File.Delete(cmnd);
            //Log(Vars.Deleted + cmnd);
            frm.logger.Log("", "", Vars.Deleted + cmnd);
        }

        private void WriteCommand(string cmndfl, string cmnd, int TP, int SL, string Lot)
        {
            string flpath = filespath + cmndfl;
            string txt = cmnd + TP + ' ' + SL + ' ' + Lot;

            if (File.Exists(flpath) == true)
                File.Delete(flpath);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(flpath);

            sw.WriteLine(txt);

            //Log(txt);
            frm.logger.Log("", "", "WriteCommand : " + txt);

            sw.Close();

            DeleteCommand(CmndTakeData);
        }

        public string IsUpDown()
        {
            if (up > down)
                return Vars.Up;

            if (down > up)
                return Vars.Dn;

            return Vars.Na;
        }

        public bool IsMaxX()
        {
            if (
                (frm.use1chb.Checked == false) || (N1MaxX > Vars.Net1X) &&
                (frm.use2chb.Checked == false) || (N2MaxX > Vars.Net2X) &&
                (frm.use3chb.Checked == false) || (N3MaxX > Vars.Net3X) &&
                (frm.use4chb.Checked == false) || (N4MaxX > Vars.Net4X) &&
                (frm.use5chb.Checked == false) || (N5MaxX > Vars.Net5X) &&
                (frm.use6chb.Checked == false) || (N6MaxX > Vars.Net6X)
               )
            {
                //Log(Vars.IsMaxXTrue);
                frm.logger.Log("", "", Vars.IsMaxXTrue);
                return true;
            }
            else
            {
                //Log(Vars.IsMaxXFalse);
                frm.logger.Log("", "", Vars.IsMaxXFalse);
                return false;
            }
        }

        public double GetForecastPerc()
        {
            up = 0;
            down = 0;

            double total = 0;

            up = Vars.NN1LastRes == Vars.Up ? up + 1 : up;
            up = Vars.NN2LastRes == Vars.Up ? up + 1 : up;
            up = Vars.NN3LastRes == Vars.Up ? up + 1 : up;
            up = Vars.NN4LastRes == Vars.Up ? up + 1 : up;
            up = Vars.NN5LastRes == Vars.Up ? up + 1 : up;
            up = Vars.NN6LastRes == Vars.Up ? up + 1 : up;

            down = Vars.NN1LastRes == Vars.Dn ? down + 1 : down;
            down = Vars.NN2LastRes == Vars.Dn ? down + 1 : down;
            down = Vars.NN3LastRes == Vars.Dn ? down + 1 : down;
            down = Vars.NN4LastRes == Vars.Dn ? down + 1 : down;
            down = Vars.NN5LastRes == Vars.Dn ? down + 1 : down;
            down = Vars.NN6LastRes == Vars.Dn ? down + 1 : down;

            total = up + down;

            double forecast = 0;

            if (up > down)
                forecast = Math.Round(up / total * 100);

            if (down > up)
                forecast = Math.Round(down / total * 100);

            //Log(Vars.Forecast + forecast);
            frm.logger.Log("", "", Vars.Forecast + forecast);

            return forecast;
        }

        private void StartAllNets()
        {
            //Log(Vars.StartAllNets); // add comm 25022013
            frm.logger.Log("", "", Vars.StartAllNets);

            if (frm.use1chb.Checked == true)
                frm.RunNet1();

            if (frm.use2chb.Checked == true)
                frm.RunNet2();

            if (frm.use3chb.Checked == true)
                frm.RunNet3();

            if (frm.use4chb.Checked == true)
                frm.RunNet4();

            if (frm.use5chb.Checked == true)
                frm.RunNet5();

            if (frm.use6chb.Checked == true)
                frm.RunNet6();
        }
    }
}
