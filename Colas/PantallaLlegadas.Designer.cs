
namespace Numeros_aleatorios.Colas
{
    partial class PantallaLlegadas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grdRangoResultados = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangoResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(30)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 71);
            this.panel1.TabIndex = 17;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitulo.Location = new System.Drawing.Point(12, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(331, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Llegadas de 60 personas";
            // 
            // grdRangoResultados
            // 
            this.grdRangoResultados.AllowUserToAddRows = false;
            this.grdRangoResultados.AllowUserToDeleteRows = false;
            this.grdRangoResultados.AllowUserToResizeColumns = false;
            this.grdRangoResultados.AllowUserToResizeRows = false;
            this.grdRangoResultados.BackgroundColor = System.Drawing.Color.White;
            this.grdRangoResultados.Location = new System.Drawing.Point(29, 155);
            this.grdRangoResultados.Name = "grdRangoResultados";
            this.grdRangoResultados.RowHeadersVisible = false;
            this.grdRangoResultados.RowTemplate.Height = 25;
            this.grdRangoResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRangoResultados.Size = new System.Drawing.Size(1307, 438);
            this.grdRangoResultados.TabIndex = 18;
            // 
            // PantallaLlegadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1364, 749);
            this.Controls.Add(this.grdRangoResultados);
            this.Controls.Add(this.panel1);
            this.Name = "PantallaLlegadas";
            this.Text = "PantallaLlegadas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangoResultados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView grdRangoResultados;
    }
}