namespace GraficDisplay
{
    using GraphLib;

    partial class MainForm
    {
        PlotterDisplayEx display = null;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.display = new GraphLib.PlotterDisplayEx();
            this.numGraphsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BackgroundColorBot = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.display.BackgroundColorTop = System.Drawing.Color.Navy;
            this.display.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.display.DashedGridColor = System.Drawing.Color.Blue;
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.DoubleBuffering = true;
            this.display.Location = new System.Drawing.Point(0, 25);
            this.display.Name = "display";
            this.display.PlaySpeed = 0.5F;
            this.display.Size = new System.Drawing.Size(620, 433);
            this.display.SolidGridColor = System.Drawing.Color.Blue;
            this.display.TabIndex = 1;
            // 
            // numGraphsToolStripMenuItem
            // 
            this.numGraphsToolStripMenuItem.Name = "numGraphsToolStripMenuItem";
            this.numGraphsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.numGraphsToolStripMenuItem.Text = "COM_Port";
            // 
            // layoutToolStripMenuItem
            // 
            this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.numGraphsToolStripMenuItem});
            this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
            this.layoutToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            this.layoutToolStripMenuItem.Text = "Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 458);
            this.Controls.Add(this.display);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "GraphLib Demo";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numGraphsToolStripMenuItem;

    }
}

