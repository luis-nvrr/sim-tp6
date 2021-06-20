
namespace Numeros_aleatorios.Colas
{
    partial class RungeKuttaResultados
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
            this.grdResultados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // grdResultados
            // 
            this.grdResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResultados.Location = new System.Drawing.Point(38, 38);
            this.grdResultados.Name = "grdResultados";
            this.grdResultados.RowTemplate.Height = 25;
            this.grdResultados.Size = new System.Drawing.Size(1124, 359);
            this.grdResultados.TabIndex = 0;
            // 
            // RungeKuttaResultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 447);
            this.Controls.Add(this.grdResultados);
            this.Name = "RungeKuttaResultados";
            this.Text = "RungeKuttaResultados";
            this.Load += new System.EventHandler(this.RungeKuttaResultados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdResultados;
    }
}