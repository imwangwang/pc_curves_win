using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

/* Copyright (c) 2008-2014 DI Zimmermann Stephan (stefan.zimmermann@tele2.at)
 *   
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

namespace GraficDisplay
{
    using GraphLib;

    public partial class MainForm : Form
    {

        private int NumGraphs = 1;
        private String CurExample = "STACKED";
        private String CurColorSchema = "GRAY";
        private PrecisionTimer.Timer mTimer = null;
        private DateTime lastTimerTick = DateTime.Now;
        
        public MainForm()
        {           
            InitializeComponent();

            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.None;
            
            CalcDataGraphs();
             
            display.Refresh();

            mTimer = new PrecisionTimer.Timer();
            mTimer.Period = 40;                         // 20 fps
            mTimer.Tick += new EventHandler(OnTimerTick);
            lastTimerTick = DateTime.Now;
            mTimer.Start();             
        }

        protected override void OnClosed(EventArgs e)
        {
            mTimer.Stop();
            mTimer.Dispose();
            base.OnClosed(e);
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (CurExample == "ANIMATED_AUTO" )
            {
                    try
                    {
                        TimeSpan dt = DateTime.Now - lastTimerTick;

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            
                            CalcSinusFunction_3(display.DataSources[j], j, (float)dt.TotalMilliseconds);
                            
                        }
                   
                        this.Invoke(new MethodInvoker(RefreshGraph));
                    }
                    catch (ObjectDisposedException ex)
                    {
                        // we get this on closing of form
                    }
                    catch (Exception ex)
                    {
                        Console.Write("exception invoking refreshgraph(): " + ex.Message);
                    }
                 
                
            }
        }
        private void RefreshGraph()
        {                             
            display.Refresh();             
        }

        protected void CalcSinusFunction_0(DataSource src, int idx)
        {
            for (int i = 0; i < src.Length; i++)
            {
                src.Samples[i].x = i;
                src.Samples[i].y = (float)(((float)200 * Math.Sin((idx + 1) *(i + 1.0) * 48 / src.Length)));
            }            
        }

        protected void CalcSinusFunction_1(DataSource src, int idx)
        {
            for (int i = 0; i < src.Length; i++)
            {
                src.Samples[i].x = i;
                
                src.Samples[i].y = (float)(((float)20 *
                                            Math.Sin(20 * (idx + 1) * (i + 1) * Math.PI / src.Length)) *
                                            Math.Sin(40 * (idx + 1) * (i + 1) * Math.PI / src.Length)) +
                                            (float)(((float)200 *
                                            Math.Sin(200 * (idx + 1) * (i + 1) * Math.PI / src.Length)));
            }
            src.OnRenderYAxisLabel = RenderYLabel;
        }

        protected void CalcSinusFunction_2(DataSource src, int idx)
        {
            for (int i = 0; i < src.Length; i++)
            {
                src.Samples[i].x = i;

                src.Samples[i].y = (float)(((float)20 *
                                            Math.Sin(40 * (idx + 1) * (i + 1) * Math.PI / src.Length)) *
                                            Math.Sin(160 * (idx + 1) * (i + 1) * Math.PI / src.Length)) +
                                            (float)(((float)200 *
                                            Math.Sin(4 * (idx + 1) * (i + 1) * Math.PI / src.Length)));
            }
            src.OnRenderYAxisLabel = RenderYLabel;
        }

        protected void CalcSinusFunction_3(DataSource ds, int idx,float time)
        {
            cPoint[] src = ds.Samples;
            for (int i = 0; i < src.Length; i++)
            {
                src[i].x = i;
                src[i].y = 200 + (float)((200 * Math.Sin((idx + 1) * (time + i * 100) / 8000.0)))+
                                +(float)((40  * Math.Sin((idx + 1) * (time + i * 200) / 2000.0)));
                /**
                            (float)( 4* Math.Sin( ((time + (i+8) * 100) / 900.0)))+
                            (float)(28 * Math.Sin(((time + (i + 8) * 100) / 290.0))); */
            }
            
        }


        private void ApplyColorSchema()
        {            
            switch (CurColorSchema)
            {
                case "DARK_GREEN":
                    {
                        Color[] cols = { Color.FromArgb(0,255,0), 
                                         Color.FromArgb(0,255,0),
                                         Color.FromArgb(0,255,0), 
                                         Color.FromArgb(0,255,0), 
                                         Color.FromArgb(0,255,0) ,
                                         Color.FromArgb(0,255,0),                              
                                         Color.FromArgb(0,255,0) };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j % 7];
                        }

                        display.BackgroundColorTop = Color.FromArgb(0, 64, 0);
                        display.BackgroundColorBot = Color.FromArgb(0, 64, 0);
                        display.SolidGridColor = Color.FromArgb(0, 128, 0);
                        display.DashedGridColor = Color.FromArgb(0, 128, 0);
                    }
                    break;
                case "WHITE":
                    {
                        Color[] cols = { Color.DarkRed, 
                                         Color.DarkSlateGray,
                                         Color.DarkCyan, 
                                         Color.DarkGreen, 
                                         Color.DarkBlue ,
                                         Color.DarkMagenta,                              
                                         Color.DeepPink };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j%7];
                        }

                        display.BackgroundColorTop = Color.White;
                        display.BackgroundColorBot = Color.White;
                        display.SolidGridColor = Color.LightGray;
                        display.DashedGridColor = Color.LightGray;
                    }
                    break;

                case "BLUE":
                    {
                        Color[] cols = { Color.Red, 
                                         Color.Orange,
                                         Color.Yellow, 
                                         Color.LightGreen, 
                                         Color.Blue ,
                                         Color.DarkSalmon,                              
                                         Color.LightPink };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j%7];
                        }

                        display.BackgroundColorTop = Color.Navy;
                        display.BackgroundColorBot = Color.FromArgb(0, 0, 64);
                        display.SolidGridColor = Color.Blue;
                        display.DashedGridColor = Color.Blue;
                    }
                    break;

                case "GRAY":
                    {
                        Color[] cols = { Color.DarkRed, 
                                         Color.DarkSlateGray,
                                         Color.DarkCyan, 
                                         Color.DarkGreen, 
                                         Color.DarkBlue ,
                                         Color.DarkMagenta,                              
                                         Color.DeepPink };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j % 7];
                        }

                        display.BackgroundColorTop = Color.White;
                        display.BackgroundColorBot = Color.LightGray;
                        display.SolidGridColor = Color.LightGray;
                        display.DashedGridColor = Color.LightGray;
                    }
                    break;

                case "RED":
                    {
                        Color[] cols = { Color.DarkCyan, 
                                         Color.Yellow,
                                         Color.DarkCyan, 
                                         Color.DarkGreen, 
                                         Color.DarkBlue ,
                                         Color.DarkMagenta,                              
                                         Color.DeepPink };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j % 7];
                        }

                        display.BackgroundColorTop = Color.DarkRed;
                        display.BackgroundColorBot = Color.Black;
                        display.SolidGridColor = Color.Red;
                        display.DashedGridColor = Color.Red;
                    }
                    break;

                case "LIGHT_BLUE":
                    {
                        Color[] cols = { Color.DarkRed, 
                                         Color.DarkSlateGray,
                                         Color.DarkCyan, 
                                         Color.DarkGreen, 
                                         Color.DarkBlue ,
                                         Color.DarkMagenta,                              
                                         Color.DeepPink };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j % 7];
                        }

                        display.BackgroundColorTop = Color.White;
                        display.BackgroundColorBot = Color.FromArgb(183,183,255);
                        display.SolidGridColor = Color.Blue;
                        display.DashedGridColor = Color.Blue;
                    }
                    break;

                case "BLACK":
                    {
                        Color[] cols = { Color.FromArgb(255,0,0), 
                                         Color.FromArgb(0,255,0),
                                         Color.FromArgb(255,255,0), 
                                         Color.FromArgb(64,64,255), 
                                         Color.FromArgb(0,255,255) ,
                                         Color.FromArgb(255,0,255),                              
                                         Color.FromArgb(255,128,0) };

                        for (int j = 0; j < NumGraphs; j++)
                        {
                            display.DataSources[j].GraphColor = cols[j % 7];
                        }

                        display.BackgroundColorTop = Color.Black;
                        display.BackgroundColorBot = Color.Black;
                        display.SolidGridColor = Color.DarkGray;
                        display.DashedGridColor = Color.DarkGray;
                    }
                    break;
            }

        }

        protected void CalcDataGraphs( )
        {

            this.SuspendLayout();
           
            display.DataSources.Clear();
            display.SetDisplayRangeX(0, 400);

            for (int j = 0; j < NumGraphs; j++)
            {
                display.DataSources.Add(new DataSource());
                display.DataSources[j].Name = "Graph " + (j + 1);                
                display.DataSources[j].OnRenderXAxisLabel += RenderXLabel;
              
                switch (CurExample)
                {
                    case "STACKED":
                        this.Text = "Stacked Graph";
                        display.PanelLayout = PlotterGraphPaneEx.LayoutMode.STACKED;
                        display.DataSources[j].Length = 5800;
                        display.DataSources[j].AutoScaleY = false;
                        display.DataSources[j].SetDisplayRangeY(-250, 250);
                        display.DataSources[j].SetGridDistanceY(100);
                        CalcSinusFunction_1(display.DataSources[j], j);
                        break;
                }             
            }
            
            ApplyColorSchema();

            this.ResumeLayout();
            display.Refresh();
           
        }

        private String RenderXLabel(DataSource s, int idx)
        {
            if (s.AutoScaleX)
            {
                //if (idx % 2 == 0)
                {
                    int Value = (int)(s.Samples[idx].x );
                    return "" + Value;
                }
                return "";
            }
            else
            {
                int Value = (int)(s.Samples[idx].x / 200);
                String Label = "" + Value + "\"";
                return Label;
            }
        }

        private String RenderYLabel(DataSource s, float  value)
        {             
            return String.Format("{0:0.0}", value);
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            display.Dispose();

            base.OnClosing(e);
        }

        private void stackedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.NORMAL;
        }

        private void verticalALignedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.VERTICAL_ARRANGED;
        }

        private void tiledVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_VER;
        }

        private void tiledHorizontalyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.PanelLayout = PlotterGraphPaneEx.LayoutMode.TILES_HOR;
        }
                
        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.None;
        }

        private void antiAliasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private void highSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }

        private void highQualityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
        }


         
         
    }
}