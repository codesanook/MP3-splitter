﻿using System;

namespace ColdCutsNS
{
    partial class ImageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.Panel();
            this.increaseResolutionButton = new System.Windows.Forms.Button();
            this.decreaseResolutionButton = new System.Windows.Forms.Button();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoScroll = true;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(942, 154);
            this.panel.TabIndex = 1;
            this.panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WaveFormPictureBoxClicked);
            // 
            // increaseResolutionButton
            // 
            this.increaseResolutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.increaseResolutionButton.Enabled = false;
            this.increaseResolutionButton.Location = new System.Drawing.Point(0, 157);
            this.increaseResolutionButton.Name = "increaseResolutionButton";
            this.increaseResolutionButton.Size = new System.Drawing.Size(21, 23);
            this.increaseResolutionButton.TabIndex = 0;
            this.increaseResolutionButton.Text = "+";
            this.increaseResolutionButton.UseVisualStyleBackColor = true;
            this.increaseResolutionButton.Click += new System.EventHandler(this.DecreaseSoundwaveResolution);
            // 
            // decreaseResolutionButton
            // 
            this.decreaseResolutionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.decreaseResolutionButton.Enabled = false;
            this.decreaseResolutionButton.Location = new System.Drawing.Point(27, 157);
            this.decreaseResolutionButton.Name = "decreaseResolutionButton";
            this.decreaseResolutionButton.Size = new System.Drawing.Size(21, 23);
            this.decreaseResolutionButton.TabIndex = 1;
            this.decreaseResolutionButton.Text = "-";
            this.decreaseResolutionButton.UseVisualStyleBackColor = true;
            this.decreaseResolutionButton.Click += new System.EventHandler(this.IncreaseSoundwaveResolution);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlayMenuItem,
            this.AddMenuItem,
            this.DeleteMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(153, 92);
            // 
            // AddMenuItem
            // 
            this.AddMenuItem.Name = "AddMenuItem";
            this.AddMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddMenuItem.Text = "Add";
            this.AddMenuItem.Click += new System.EventHandler(this.AddMenuItem_Click);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DeleteMenuItem.Text = "Delete";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // PlayMenuItem
            // 
            this.PlayMenuItem.Name = "PlayMenuItem";
            this.PlayMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PlayMenuItem.Text = "Play";
            this.PlayMenuItem.Click += new System.EventHandler(this.PlayMenuItem_Click);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(942, 180);
            this.ControlBox = false;
            this.Controls.Add(this.decreaseResolutionButton);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.increaseResolutionButton);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageForm";
            this.ShowInTaskbar = false;
            this.LocationChanged += new System.EventHandler(this.ImageForm_LocationChanged);
            this.DoubleClick += new System.EventHandler(this.ImageForm_DoubleClick);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel;
        public System.Windows.Forms.Button decreaseResolutionButton;
        public System.Windows.Forms.Button increaseResolutionButton;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayMenuItem;
    }
}