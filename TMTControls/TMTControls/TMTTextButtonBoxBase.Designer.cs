﻿namespace TMTControls
{
    partial class TMTTextButtonBoxBase
    {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTTextButtonBoxBase));
            this.InnerTextBox = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InnerTextBox
            // 
            this.InnerTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("InnerTextBox.AutoCompleteCustomSource"),
            resources.GetString("InnerTextBox.AutoCompleteCustomSource1"),
            resources.GetString("InnerTextBox.AutoCompleteCustomSource2"),
            resources.GetString("InnerTextBox.AutoCompleteCustomSource3"),
            resources.GetString("InnerTextBox.AutoCompleteCustomSource4"),
            resources.GetString("InnerTextBox.AutoCompleteCustomSource5")});
            this.InnerTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.InnerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.InnerTextBox, "InnerTextBox");
            this.InnerTextBox.Name = "InnerTextBox";
            this.InnerTextBox.TextChanged += new System.EventHandler(this.InnerTextBox_TextChanged);
            this.InnerTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InnerTextBox_KeyPress);
            this.InnerTextBox.Validated += new System.EventHandler(this.InnerTextBox_Validated);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.buttonOK.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabStop = false;
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.Button_Click);
            // 
            // TMTTextButtonBoxBase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InnerTextBox);
            this.Controls.Add(this.buttonOK);
            this.Name = "TMTTextButtonBoxBase";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.TextBox InnerTextBox;
                
    }
}
