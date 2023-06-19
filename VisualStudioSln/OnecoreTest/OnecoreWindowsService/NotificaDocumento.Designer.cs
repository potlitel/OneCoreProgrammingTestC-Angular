
namespace OnecoreWindowsService
{
    partial class NotificaDocumento
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
            this.tmrChecker = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.tmrChecker)).BeginInit();
            // 
            // tmrChecker
            // 
            this.tmrChecker.Enabled = true;
            this.tmrChecker.Interval = 500D;
            this.tmrChecker.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrChecker_Elapsed);
            // 
            // NotificaDocumento
            // 
            this.ServiceName = "NotificaDocumento";
            ((System.ComponentModel.ISupportInitialize)(this.tmrChecker)).EndInit();

        }

        #endregion

        private System.Timers.Timer tmrChecker;
    }
}
