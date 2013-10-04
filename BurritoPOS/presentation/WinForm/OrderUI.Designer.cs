namespace BurritoPOS.presentation
{
    partial class OrderUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.submitBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.addBurritoBtn = new System.Windows.Forms.Button();
            this.editBurritoBtn = new System.Windows.Forms.Button();
            this.removeBurritoBtn = new System.Windows.Forms.Button();
            this.priceLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Neato Burrito Order System";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.priceLbl);
            this.groupBox1.Controls.Add(this.removeBurritoBtn);
            this.groupBox1.Controls.Add(this.editBurritoBtn);
            this.groupBox1.Controls.Add(this.addBurritoBtn);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(13, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 288);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order Information";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.MaximumSize = new System.Drawing.Size(427, 208);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(427, 208);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(427, 208);
            this.dataGridView1.TabIndex = 0;
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(126, 345);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(97, 23);
            this.submitBtn.TabIndex = 3;
            this.submitBtn.Text = "Submit Order";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(229, 345);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(94, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel Order";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // addBurritoBtn
            // 
            this.addBurritoBtn.Location = new System.Drawing.Point(74, 233);
            this.addBurritoBtn.Name = "addBurritoBtn";
            this.addBurritoBtn.Size = new System.Drawing.Size(97, 23);
            this.addBurritoBtn.TabIndex = 5;
            this.addBurritoBtn.Text = "Add Burrito";
            this.addBurritoBtn.UseVisualStyleBackColor = true;
            this.addBurritoBtn.Click += new System.EventHandler(this.addBurritoBtn_Click);
            // 
            // editBurritoBtn
            // 
            this.editBurritoBtn.Location = new System.Drawing.Point(177, 233);
            this.editBurritoBtn.Name = "editBurritoBtn";
            this.editBurritoBtn.Size = new System.Drawing.Size(97, 23);
            this.editBurritoBtn.TabIndex = 6;
            this.editBurritoBtn.Text = "Edit Burrito";
            this.editBurritoBtn.UseVisualStyleBackColor = true;
            this.editBurritoBtn.Click += new System.EventHandler(this.editBurritoBtn_Click);
            // 
            // removeBurritoBtn
            // 
            this.removeBurritoBtn.Location = new System.Drawing.Point(280, 233);
            this.removeBurritoBtn.Name = "removeBurritoBtn";
            this.removeBurritoBtn.Size = new System.Drawing.Size(97, 23);
            this.removeBurritoBtn.TabIndex = 7;
            this.removeBurritoBtn.Text = "Remove Burrito";
            this.removeBurritoBtn.UseVisualStyleBackColor = true;
            this.removeBurritoBtn.Click += new System.EventHandler(this.removeBurritoBtn_Click);
            // 
            // priceLbl
            // 
            this.priceLbl.AutoSize = true;
            this.priceLbl.Location = new System.Drawing.Point(174, 272);
            this.priceLbl.Name = "priceLbl";
            this.priceLbl.Size = new System.Drawing.Size(91, 13);
            this.priceLbl.TabIndex = 8;
            this.priceLbl.Text = "Total Price: $0.00";
            // 
            // OrderUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 378);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "OrderUI";
            this.Text = "New Order";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label priceLbl;
        private System.Windows.Forms.Button removeBurritoBtn;
        private System.Windows.Forms.Button editBurritoBtn;
        private System.Windows.Forms.Button addBurritoBtn;
    }
}