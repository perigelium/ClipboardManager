namespace ClipboardManager
{
    partial class ClipboardManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipboardManager));
            this.list_clipboard = new System.Windows.Forms.ListBox();
            this.autoload = new System.Windows.Forms.CheckBox();
            this.size_tray = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBoxTags = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSaveSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open_fd = new System.Windows.Forms.OpenFileDialog();
            this.save_fd = new System.Windows.Forms.SaveFileDialog();
            this.textBoxItemContent = new System.Windows.Forms.TextBox();
            this.StripMenuGlueSelected = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.size_tray)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_clipboard
            // 
            resources.ApplyResources(this.list_clipboard, "list_clipboard");
            this.list_clipboard.FormattingEnabled = true;
            this.list_clipboard.Name = "list_clipboard";
            this.list_clipboard.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.list_clipboard.SelectedIndexChanged += new System.EventHandler(this.list_clipboard_SelectedIndexChanged);
            this.list_clipboard.KeyUp += new System.Windows.Forms.KeyEventHandler(this.list_clipboard_KeyUp);
            // 
            // autoload
            // 
            resources.ApplyResources(this.autoload, "autoload");
            this.autoload.Name = "autoload";
            this.autoload.UseVisualStyleBackColor = true;
            this.autoload.CheckedChanged += new System.EventHandler(this.autoload_CheckedChanged);
            // 
            // size_tray
            // 
            resources.ApplyResources(this.size_tray, "size_tray");
            this.size_tray.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.size_tray.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.size_tray.Name = "size_tray";
            this.size_tray.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.size_tray.ValueChanged += new System.EventHandler(this.size_tray_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // _notifyIcon
            // 
            resources.ApplyResources(this._notifyIcon, "_notifyIcon");
            // 
            // textBoxTags
            // 
            resources.ApplyResources(this.textBoxTags, "textBoxTags");
            this.textBoxTags.Name = "textBoxTags";
            this.textBoxTags.Leave += new System.EventHandler(this.textBoxTags_Leave);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuSaveSelection,
            this.StripMenuGlueSelected,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuSaveSelection
            // 
            this.toolStripMenuSaveSelection.Name = "toolStripMenuSaveSelection";
            resources.ApplyResources(this.toolStripMenuSaveSelection, "toolStripMenuSaveSelection");
            this.toolStripMenuSaveSelection.Click += new System.EventHandler(this.toolStripMenuSaveSelection_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // textBoxItemContent
            // 
            resources.ApplyResources(this.textBoxItemContent, "textBoxItemContent");
            this.textBoxItemContent.Name = "textBoxItemContent";
            this.textBoxItemContent.ReadOnly = true;
            // 
            // StripMenuGlueSelected
            // 
            this.StripMenuGlueSelected.Name = "StripMenuGlueSelected";
            resources.ApplyResources(this.StripMenuGlueSelected, "StripMenuGlueSelected");
            this.StripMenuGlueSelected.Click += new System.EventHandler(this.StripMenuGlueSelected_Click);
            // 
            // ClipboardManager
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxItemContent);
            this.Controls.Add(this.textBoxTags);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.size_tray);
            this.Controls.Add(this.autoload);
            this.Controls.Add(this.list_clipboard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClipboardManager";
            ((System.ComponentModel.ISupportInitialize)(this.size_tray)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_clipboard;
        private System.Windows.Forms.CheckBox autoload;
        private System.Windows.Forms.NumericUpDown size_tray;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog open_fd;
        private System.Windows.Forms.SaveFileDialog save_fd;
        private System.Windows.Forms.TextBox textBoxItemContent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSaveSelection;
        private System.Windows.Forms.ToolStripMenuItem StripMenuGlueSelected;
    }
}

