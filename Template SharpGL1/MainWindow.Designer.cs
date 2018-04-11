namespace Template_SharpGL1
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.GL = new SharpGL.OpenGLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Rotation = new System.Windows.Forms.ToolStripButton();
            this.Grid = new System.Windows.Forms.ToolStripButton();
            this.normButton = new System.Windows.Forms.ToolStripButton();
            this.bufButton = new System.Windows.Forms.ToolStripButton();
            this.doubleBufButton = new System.Windows.Forms.ToolStripButton();
            this.lookModeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.buildButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.angleTextBox = new System.Windows.Forms.TextBox();
            this.trackBarLight = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GL)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).BeginInit();
            this.SuspendLayout();
            // 
            // GL
            // 
            this.GL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GL.DrawFPS = false;
            this.GL.Location = new System.Drawing.Point(12, 11);
            this.GL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GL.Name = "GL";
            this.GL.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GL.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.GL.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.GL.Size = new System.Drawing.Size(1253, 453);
            this.GL.TabIndex = 0;
            this.GL.OpenGLDraw += new SharpGL.RenderEventHandler(this.GL_OpenGLDraw);
            this.GL.Load += new System.EventHandler(this.openGLControl1_Load);
            this.GL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GL_KeyDown);
            this.GL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GL_KeyUp);
            this.GL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GL_MouseDown);
            this.GL.MouseEnter += new System.EventHandler(this.GL_MouseEnter);
            this.GL.MouseLeave += new System.EventHandler(this.GL_MouseLeave);
            this.GL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GL_MouseMove);
            this.GL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GL_MouseUp);
            this.GL.Resize += new System.EventHandler(this.openGLControl1_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Rotation,
            this.Grid,
            this.normButton,
            this.bufButton,
            this.doubleBufButton,
            this.lookModeButton,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Rotation
            // 
            this.Rotation.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Rotation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Rotation.ImageTransparentColor = System.Drawing.Color.AliceBlue;
            this.Rotation.Name = "Rotation";
            this.Rotation.Size = new System.Drawing.Size(62, 22);
            this.Rotation.Text = "Вращение";
            this.Rotation.Click += new System.EventHandler(this.isRotationButton);
            // 
            // Grid
            // 
            this.Grid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Grid.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Grid.Image = ((System.Drawing.Image)(resources.GetObject("Grid.Image")));
            this.Grid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(40, 22);
            this.Grid.Text = "Сетка";
            this.Grid.Click += new System.EventHandler(this.isGridButton);
            // 
            // normButton
            // 
            this.normButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.normButton.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.normButton.Image = ((System.Drawing.Image)(resources.GetObject("normButton.Image")));
            this.normButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.normButton.Name = "normButton";
            this.normButton.Size = new System.Drawing.Size(57, 22);
            this.normButton.Text = "Нормали";
            this.normButton.Click += new System.EventHandler(this.normButton_Click);
            // 
            // bufButton
            // 
            this.bufButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bufButton.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bufButton.Image = ((System.Drawing.Image)(resources.GetObject("bufButton.Image")));
            this.bufButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bufButton.Name = "bufButton";
            this.bufButton.Size = new System.Drawing.Size(79, 22);
            this.bufButton.Text = "Буферизация";
            this.bufButton.Click += new System.EventHandler(this.isBufButton);
            // 
            // doubleBufButton
            // 
            this.doubleBufButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.doubleBufButton.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doubleBufButton.Image = ((System.Drawing.Image)(resources.GetObject("doubleBufButton.Image")));
            this.doubleBufButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.doubleBufButton.Name = "doubleBufButton";
            this.doubleBufButton.Size = new System.Drawing.Size(124, 22);
            this.doubleBufButton.Text = "Двойная буферизация";
            this.doubleBufButton.Click += new System.EventHandler(this.isBufButton);
            // 
            // lookModeButton
            // 
            this.lookModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lookModeButton.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lookModeButton.Image = ((System.Drawing.Image)(resources.GetObject("lookModeButton.Image")));
            this.lookModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lookModeButton.Name = "lookModeButton";
            this.lookModeButton.Size = new System.Drawing.Size(95, 22);
            this.lookModeButton.Text = "Режим проекции";
            this.lookModeButton.Click += new System.EventHandler(this.isBufButton);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(118, 22);
            this.toolStripButton1.Text = "Сглаженные нормали";
            this.toolStripButton1.Click += new System.EventHandler(this.SmoothButton);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(101, 22);
            this.toolStripButton2.Text = "Текстурирование";
            this.toolStripButton2.Click += new System.EventHandler(this.TextureButton);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Location = new System.Drawing.Point(0, 448);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1276, 46);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(109, 467);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "x:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(220, 467);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "y:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(335, 467);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "z:";
            // 
            // xTextBox
            // 
            this.xTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xTextBox.Location = new System.Drawing.Point(131, 467);
            this.xTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(76, 20);
            this.xTextBox.TabIndex = 6;
            this.xTextBox.TextChanged += new System.EventHandler(this.anyTextBox_TextChanged);
            // 
            // yTextBox
            // 
            this.yTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yTextBox.Location = new System.Drawing.Point(242, 467);
            this.yTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(76, 20);
            this.yTextBox.TabIndex = 7;
            this.yTextBox.TextChanged += new System.EventHandler(this.anyTextBox_TextChanged);
            // 
            // zTextBox
            // 
            this.zTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.zTextBox.Location = new System.Drawing.Point(358, 466);
            this.zTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(76, 20);
            this.zTextBox.TabIndex = 8;
            this.zTextBox.TextChanged += new System.EventHandler(this.anyTextBox_TextChanged);
            // 
            // buildButton
            // 
            this.buildButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buildButton.BackColor = System.Drawing.SystemColors.Window;
            this.buildButton.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buildButton.Location = new System.Drawing.Point(485, 459);
            this.buildButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(86, 26);
            this.buildButton.TabIndex = 9;
            this.buildButton.Text = "Построить";
            this.buildButton.UseVisualStyleBackColor = false;
            this.buildButton.Click += new System.EventHandler(this.isBuildButton);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(670, 467);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Угол поворота:";
            // 
            // angleTextBox
            // 
            this.angleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.angleTextBox.Location = new System.Drawing.Point(780, 465);
            this.angleTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.angleTextBox.Name = "angleTextBox";
            this.angleTextBox.Size = new System.Drawing.Size(76, 20);
            this.angleTextBox.TabIndex = 11;
            this.angleTextBox.TextChanged += new System.EventHandler(this.anyTextBox_TextChanged);
            // 
            // trackBarLight
            // 
            this.trackBarLight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarLight.LargeChange = 2;
            this.trackBarLight.Location = new System.Drawing.Point(976, 448);
            this.trackBarLight.Maximum = 2;
            this.trackBarLight.Name = "trackBarLight";
            this.trackBarLight.Size = new System.Drawing.Size(104, 45);
            this.trackBarLight.TabIndex = 12;
            this.trackBarLight.Scroll += new System.EventHandler(this.trackBarLight_Scroll);
            this.trackBarLight.ValueChanged += new System.EventHandler(this.trackBarLight_Scroll);
            this.trackBarLight.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.trackBarLight_GiveFeedback);
            this.trackBarLight.MouseCaptureChanged += new System.EventHandler(this.trackBarLight_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(933, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Свет";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 494);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBarLight);
            this.Controls.Add(this.angleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.zTextBox);
            this.Controls.Add(this.yTextBox);
            this.Controls.Add(this.xTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.GL);
            this.Name = "MainWindow";
            this.Text = "Лабораторная работа №3";
            ((System.ComponentModel.ISupportInitialize)(this.GL)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl GL;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Rotation;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox zTextBox;
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox angleTextBox;
        private System.Windows.Forms.ToolStripButton Grid;
        private System.Windows.Forms.TrackBar trackBarLight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton normButton;
        private System.Windows.Forms.ToolStripButton bufButton;
        private System.Windows.Forms.ToolStripButton doubleBufButton;
        private System.Windows.Forms.ToolStripButton lookModeButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

