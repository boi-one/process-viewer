namespace ProcessViewer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            getProcess = new Button();
            processesView = new ListView();
            SuspendLayout();
            // 
            // getProcess
            // 
            getProcess.Location = new Point(12, 415);
            getProcess.Name = "getProcess";
            getProcess.Size = new Size(116, 23);
            getProcess.TabIndex = 0;
            getProcess.Text = "Add Program";
            getProcess.UseVisualStyleBackColor = true;
            getProcess.Click += button1_Click;
            // 
            // processesView
            // 
            processesView.Location = new Point(12, 12);
            processesView.Name = "processesView";
            processesView.Size = new Size(776, 397);
            processesView.TabIndex = 1;
            processesView.UseCompatibleStateImageBehavior = false;
            processesView.View = View.Details;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(processesView);
            Controls.Add(getProcess);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button getProcess;
        private ListView processesView;
    }
}
