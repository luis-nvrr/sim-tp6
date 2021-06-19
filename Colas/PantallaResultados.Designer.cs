
namespace Numeros_aleatorios.Colas
{
    partial class PantallaResultados
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.btnSimular = new System.Windows.Forms.Button();
            this.grdRangoResultados = new System.Windows.Forms.DataGridView();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.txtCantSimulaciones = new System.Windows.Forms.TextBox();
            this.lblCantSimulaciones = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTiempoPromedioFinCobro = new System.Windows.Forms.TextBox();
            this.txtTiempoPromedioFinActualizacion = new System.Windows.Forms.TextBox();
            this.lblTiempoPromedioFinCobro = new System.Windows.Forms.Label();
            this.lblTiempoPromedioFinActualizacion = new System.Windows.Forms.Label();
            this.lblTiempoPromedioFinInforme = new System.Windows.Forms.Label();
            this.txtTiempoPromedioFinInforme = new System.Windows.Forms.TextBox();
            this.txtTiempoPromedioLlegadas = new System.Windows.Forms.TextBox();
            this.lblTiempoPromedioLlegadas = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMaximaEsperaCajas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOciosoActualizacion = new System.Windows.Forms.TextBox();
            this.txtOcupacionInformes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPromedioEsperaCajas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangoResultados)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(0)))), ((int)(((byte)(30)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1410, 71);
            this.panel1.TabIndex = 16;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitulo.Location = new System.Drawing.Point(12, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(355, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Ejercicio 15: Municipalidad";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblHasta);
            this.groupBox2.Controls.Add(this.lblDesde);
            this.groupBox2.Controls.Add(this.txtHasta);
            this.groupBox2.Controls.Add(this.txtDesde);
            this.groupBox2.Controls.Add(this.btnSimular);
            this.groupBox2.Controls.Add(this.grdRangoResultados);
            this.groupBox2.Controls.Add(this.btnAnterior);
            this.groupBox2.Controls.Add(this.btnSiguiente);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(12, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1398, 554);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grilla Desde Hasta";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(304, 33);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(56, 21);
            this.lblHasta.TabIndex = 42;
            this.lblHasta.Text = "Hasta: ";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(101, 33);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(60, 21);
            this.lblDesde.TabIndex = 39;
            this.lblDesde.Text = "Desde: ";
            // 
            // txtHasta
            // 
            this.txtHasta.Location = new System.Drawing.Point(366, 30);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(116, 29);
            this.txtHasta.TabIndex = 38;
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(167, 30);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(110, 29);
            this.txtDesde.TabIndex = 34;
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(503, 29);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(82, 30);
            this.btnSimular.TabIndex = 24;
            this.btnSimular.Text = "Simular";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // grdRangoResultados
            // 
            this.grdRangoResultados.AllowUserToAddRows = false;
            this.grdRangoResultados.AllowUserToDeleteRows = false;
            this.grdRangoResultados.AllowUserToResizeColumns = false;
            this.grdRangoResultados.AllowUserToResizeRows = false;
            this.grdRangoResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdRangoResultados.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdRangoResultados.BackgroundColor = System.Drawing.Color.White;
            this.grdRangoResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRangoResultados.Location = new System.Drawing.Point(20, 70);
            this.grdRangoResultados.Name = "grdRangoResultados";
            this.grdRangoResultados.RowHeadersVisible = false;
            this.grdRangoResultados.RowTemplate.Height = 25;
            this.grdRangoResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRangoResultados.Size = new System.Drawing.Size(1307, 438);
            this.grdRangoResultados.TabIndex = 7;
            this.grdRangoResultados.VirtualMode = true;
            this.grdRangoResultados.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grdRangoResultados_ColumnAdded);
            this.grdRangoResultados.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdRangoResultados_DataBindingComplete);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(1117, 22);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(75, 42);
            this.btnAnterior.TabIndex = 19;
            this.btnAnterior.Text = "Atras";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(1198, 22);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(121, 42);
            this.btnSiguiente.TabIndex = 18;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // txtCantSimulaciones
            // 
            this.txtCantSimulaciones.Location = new System.Drawing.Point(314, 38);
            this.txtCantSimulaciones.Name = "txtCantSimulaciones";
            this.txtCantSimulaciones.Size = new System.Drawing.Size(110, 29);
            this.txtCantSimulaciones.TabIndex = 21;
            // 
            // lblCantSimulaciones
            // 
            this.lblCantSimulaciones.AutoSize = true;
            this.lblCantSimulaciones.Location = new System.Drawing.Point(112, 38);
            this.lblCantSimulaciones.Name = "lblCantSimulaciones";
            this.lblCantSimulaciones.Size = new System.Drawing.Size(196, 21);
            this.lblCantSimulaciones.TabIndex = 22;
            this.lblCantSimulaciones.Text = "Cantidad de Simulaciones: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTiempoPromedioFinCobro);
            this.groupBox1.Controls.Add(this.txtTiempoPromedioFinActualizacion);
            this.groupBox1.Controls.Add(this.lblTiempoPromedioFinCobro);
            this.groupBox1.Controls.Add(this.lblTiempoPromedioFinActualizacion);
            this.groupBox1.Controls.Add(this.lblTiempoPromedioFinInforme);
            this.groupBox1.Controls.Add(this.txtTiempoPromedioFinInforme);
            this.groupBox1.Controls.Add(this.txtTiempoPromedioLlegadas);
            this.groupBox1.Controls.Add(this.lblTiempoPromedioLlegadas);
            this.groupBox1.Controls.Add(this.txtCantSimulaciones);
            this.groupBox1.Controls.Add(this.lblCantSimulaciones);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(541, 220);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parámetros de la Simulación";
            // 
            // txtTiempoPromedioFinCobro
            // 
            this.txtTiempoPromedioFinCobro.Location = new System.Drawing.Point(314, 178);
            this.txtTiempoPromedioFinCobro.Name = "txtTiempoPromedioFinCobro";
            this.txtTiempoPromedioFinCobro.Size = new System.Drawing.Size(110, 29);
            this.txtTiempoPromedioFinCobro.TabIndex = 33;
            // 
            // txtTiempoPromedioFinActualizacion
            // 
            this.txtTiempoPromedioFinActualizacion.Location = new System.Drawing.Point(314, 143);
            this.txtTiempoPromedioFinActualizacion.Name = "txtTiempoPromedioFinActualizacion";
            this.txtTiempoPromedioFinActualizacion.Size = new System.Drawing.Size(110, 29);
            this.txtTiempoPromedioFinActualizacion.TabIndex = 32;
            // 
            // lblTiempoPromedioFinCobro
            // 
            this.lblTiempoPromedioFinCobro.AutoSize = true;
            this.lblTiempoPromedioFinCobro.Location = new System.Drawing.Point(68, 181);
            this.lblTiempoPromedioFinCobro.Name = "lblTiempoPromedioFinCobro";
            this.lblTiempoPromedioFinCobro.Size = new System.Drawing.Size(230, 21);
            this.lblTiempoPromedioFinCobro.TabIndex = 31;
            this.lblTiempoPromedioFinCobro.Text = "Tiempo Promedio Fin de Cobro:";
            // 
            // lblTiempoPromedioFinActualizacion
            // 
            this.lblTiempoPromedioFinActualizacion.AutoSize = true;
            this.lblTiempoPromedioFinActualizacion.Location = new System.Drawing.Point(20, 146);
            this.lblTiempoPromedioFinActualizacion.Name = "lblTiempoPromedioFinActualizacion";
            this.lblTiempoPromedioFinActualizacion.Size = new System.Drawing.Size(278, 21);
            this.lblTiempoPromedioFinActualizacion.TabIndex = 30;
            this.lblTiempoPromedioFinActualizacion.Text = "Tiempo Promedio Fin de Actualización:";
            // 
            // lblTiempoPromedioFinInforme
            // 
            this.lblTiempoPromedioFinInforme.AutoSize = true;
            this.lblTiempoPromedioFinInforme.Location = new System.Drawing.Point(56, 111);
            this.lblTiempoPromedioFinInforme.Name = "lblTiempoPromedioFinInforme";
            this.lblTiempoPromedioFinInforme.Size = new System.Drawing.Size(242, 21);
            this.lblTiempoPromedioFinInforme.TabIndex = 28;
            this.lblTiempoPromedioFinInforme.Text = "Tiempo Promedio Fin de Informe:";
            // 
            // txtTiempoPromedioFinInforme
            // 
            this.txtTiempoPromedioFinInforme.Location = new System.Drawing.Point(314, 108);
            this.txtTiempoPromedioFinInforme.Name = "txtTiempoPromedioFinInforme";
            this.txtTiempoPromedioFinInforme.Size = new System.Drawing.Size(110, 29);
            this.txtTiempoPromedioFinInforme.TabIndex = 26;
            // 
            // txtTiempoPromedioLlegadas
            // 
            this.txtTiempoPromedioLlegadas.Location = new System.Drawing.Point(314, 73);
            this.txtTiempoPromedioLlegadas.Name = "txtTiempoPromedioLlegadas";
            this.txtTiempoPromedioLlegadas.Size = new System.Drawing.Size(110, 29);
            this.txtTiempoPromedioLlegadas.TabIndex = 25;
            // 
            // lblTiempoPromedioLlegadas
            // 
            this.lblTiempoPromedioLlegadas.AutoSize = true;
            this.lblTiempoPromedioLlegadas.Location = new System.Drawing.Point(76, 76);
            this.lblTiempoPromedioLlegadas.Name = "lblTiempoPromedioLlegadas";
            this.lblTiempoPromedioLlegadas.Size = new System.Drawing.Size(223, 21);
            this.lblTiempoPromedioLlegadas.TabIndex = 24;
            this.lblTiempoPromedioLlegadas.Text = "Tiempo Promedio de Llegadas:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtMaximaEsperaCajas);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtOciosoActualizacion);
            this.groupBox3.Controls.Add(this.txtOcupacionInformes);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtPromedioEsperaCajas);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(590, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(765, 220);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resultados";
            // 
            // txtMaximaEsperaCajas
            // 
            this.txtMaximaEsperaCajas.Location = new System.Drawing.Point(314, 143);
            this.txtMaximaEsperaCajas.Name = "txtMaximaEsperaCajas";
            this.txtMaximaEsperaCajas.Size = new System.Drawing.Size(110, 29);
            this.txtMaximaEsperaCajas.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 21);
            this.label2.TabIndex = 30;
            this.label2.Text = "Tiempo máximo de espera en cajas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 21);
            this.label3.TabIndex = 28;
            this.label3.Text = "Tiempo ocioso de Actualización:";
            // 
            // txtOciosoActualizacion
            // 
            this.txtOciosoActualizacion.Location = new System.Drawing.Point(314, 108);
            this.txtOciosoActualizacion.Name = "txtOciosoActualizacion";
            this.txtOciosoActualizacion.Size = new System.Drawing.Size(110, 29);
            this.txtOciosoActualizacion.TabIndex = 26;
            // 
            // txtOcupacionInformes
            // 
            this.txtOcupacionInformes.Location = new System.Drawing.Point(314, 73);
            this.txtOcupacionInformes.Name = "txtOcupacionInformes";
            this.txtOcupacionInformes.Size = new System.Drawing.Size(110, 29);
            this.txtOcupacionInformes.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 21);
            this.label4.TabIndex = 24;
            this.label4.Text = "Tiempo de ocupación de Informes:";
            // 
            // txtPromedioEsperaCajas
            // 
            this.txtPromedioEsperaCajas.Location = new System.Drawing.Point(314, 38);
            this.txtPromedioEsperaCajas.Name = "txtPromedioEsperaCajas";
            this.txtPromedioEsperaCajas.Size = new System.Drawing.Size(110, 29);
            this.txtPromedioEsperaCajas.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(267, 21);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tiempo promedio de espera en cajas:";
            // 
            // PantallaResultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1381, 766);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Name = "PantallaResultados";
            this.Text = "PantallaResultados";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PantallaResultados_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRangoResultados)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grdRangoResultados;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.TextBox txtCantSimulaciones;
        private System.Windows.Forms.Label lblCantSimulaciones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTiempoPromedioFinActualizacion;
        private System.Windows.Forms.Label lblTiempoPromedioFinInforme;
        private System.Windows.Forms.TextBox txtTiempoPromedioFinInforme;
        private System.Windows.Forms.TextBox txtTiempoPromedioLlegadas;
        private System.Windows.Forms.Label lblTiempoPromedioLlegadas;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.TextBox txtTiempoPromedioFinCobro;
        private System.Windows.Forms.TextBox txtTiempoPromedioFinActualizacion;
        private System.Windows.Forms.Label lblTiempoPromedioFinCobro;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMaximaEsperaCajas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOciosoActualizacion;
        private System.Windows.Forms.TextBox txtOcupacionInformes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPromedioEsperaCajas;
        private System.Windows.Forms.Label label5;
    }
}