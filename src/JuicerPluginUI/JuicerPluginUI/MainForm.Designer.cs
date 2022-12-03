
namespace JuicerPluginUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LabelPlateDiametr = new System.Windows.Forms.Label();
            this.LabelStakeDiametr = new System.Windows.Forms.Label();
            this.LabelStakeHeight = new System.Windows.Forms.Label();
            this.LabelNumberOfTeeth = new System.Windows.Forms.Label();
            this.LabelNumberOfHoles = new System.Windows.Forms.Label();
            this.TextBoxPlateDiameter = new System.Windows.Forms.TextBox();
            this.TextBoxStakeDiameter = new System.Windows.Forms.TextBox();
            this.TextBoxStakeHeight = new System.Windows.Forms.TextBox();
            this.TextBoxNumberOfTeeth = new System.Windows.Forms.TextBox();
            this.TextBoxNumberOfHoles = new System.Windows.Forms.TextBox();
            this.ButtonBuild = new System.Windows.Forms.Button();
            this.LabelPlateDiametrRange = new System.Windows.Forms.Label();
            this.LabelStakeDiametrRange = new System.Windows.Forms.Label();
            this.LabelStakeHeightRange = new System.Windows.Forms.Label();
            this.LabelNumberOfTeethRange = new System.Windows.Forms.Label();
            this.LabelNumberOfHolesRange = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelLengthOfHoles = new System.Windows.Forms.Label();
            this.TextBoxLengthOfHoles = new System.Windows.Forms.TextBox();
            this.LabelLengthOfHolesRange = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelPlateDiametr
            // 
            this.LabelPlateDiametr.AutoSize = true;
            this.LabelPlateDiametr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelPlateDiametr.Location = new System.Drawing.Point(101, 10);
            this.LabelPlateDiametr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelPlateDiametr.Name = "LabelPlateDiametr";
            this.LabelPlateDiametr.Size = new System.Drawing.Size(183, 25);
            this.LabelPlateDiametr.TabIndex = 0;
            this.LabelPlateDiametr.Text = "Диаметр тарелки";
            // 
            // LabelStakeDiametr
            // 
            this.LabelStakeDiametr.AutoSize = true;
            this.LabelStakeDiametr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelStakeDiametr.Location = new System.Drawing.Point(137, 47);
            this.LabelStakeDiametr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelStakeDiametr.Name = "LabelStakeDiametr";
            this.LabelStakeDiametr.Size = new System.Drawing.Size(149, 25);
            this.LabelStakeDiametr.TabIndex = 1;
            this.LabelStakeDiametr.Text = "Диаметр кола";
            // 
            // LabelStakeHeight
            // 
            this.LabelStakeHeight.AutoSize = true;
            this.LabelStakeHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelStakeHeight.Location = new System.Drawing.Point(152, 84);
            this.LabelStakeHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelStakeHeight.Name = "LabelStakeHeight";
            this.LabelStakeHeight.Size = new System.Drawing.Size(129, 25);
            this.LabelStakeHeight.TabIndex = 2;
            this.LabelStakeHeight.Text = "Высота кола";
            // 
            // LabelNumberOfTeeth
            // 
            this.LabelNumberOfTeeth.AutoSize = true;
            this.LabelNumberOfTeeth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNumberOfTeeth.Location = new System.Drawing.Point(86, 121);
            this.LabelNumberOfTeeth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelNumberOfTeeth.Name = "LabelNumberOfTeeth";
            this.LabelNumberOfTeeth.Size = new System.Drawing.Size(192, 25);
            this.LabelNumberOfTeeth.TabIndex = 3;
            this.LabelNumberOfTeeth.Text = "Количество зубцов";
            // 
            // LabelNumberOfHoles
            // 
            this.LabelNumberOfHoles.AutoSize = true;
            this.LabelNumberOfHoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNumberOfHoles.Location = new System.Drawing.Point(48, 158);
            this.LabelNumberOfHoles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelNumberOfHoles.Name = "LabelNumberOfHoles";
            this.LabelNumberOfHoles.Size = new System.Drawing.Size(227, 25);
            this.LabelNumberOfHoles.TabIndex = 4;
            this.LabelNumberOfHoles.Text = "Количество отверстий";
            // 
            // TextBoxPlateDiameter
            // 
            this.TextBoxPlateDiameter.BackColor = System.Drawing.Color.White;
            this.TextBoxPlateDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxPlateDiameter.Location = new System.Drawing.Point(298, 7);
            this.TextBoxPlateDiameter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxPlateDiameter.Name = "TextBoxPlateDiameter";
            this.TextBoxPlateDiameter.Size = new System.Drawing.Size(100, 30);
            this.TextBoxPlateDiameter.TabIndex = 5;
            this.TextBoxPlateDiameter.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxPlateDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForCommasAndNumbers_KeyPress);
            // 
            // TextBoxStakeDiameter
            // 
            this.TextBoxStakeDiameter.BackColor = System.Drawing.Color.White;
            this.TextBoxStakeDiameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxStakeDiameter.Location = new System.Drawing.Point(298, 44);
            this.TextBoxStakeDiameter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxStakeDiameter.Name = "TextBoxStakeDiameter";
            this.TextBoxStakeDiameter.Size = new System.Drawing.Size(100, 30);
            this.TextBoxStakeDiameter.TabIndex = 6;
            this.TextBoxStakeDiameter.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxStakeDiameter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForCommasAndNumbers_KeyPress);
            // 
            // TextBoxStakeHeight
            // 
            this.TextBoxStakeHeight.BackColor = System.Drawing.Color.White;
            this.TextBoxStakeHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxStakeHeight.Location = new System.Drawing.Point(298, 80);
            this.TextBoxStakeHeight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxStakeHeight.Name = "TextBoxStakeHeight";
            this.TextBoxStakeHeight.Size = new System.Drawing.Size(100, 30);
            this.TextBoxStakeHeight.TabIndex = 7;
            this.TextBoxStakeHeight.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxStakeHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForCommasAndNumbers_KeyPress);
            // 
            // TextBoxNumberOfTeeth
            // 
            this.TextBoxNumberOfTeeth.BackColor = System.Drawing.Color.White;
            this.TextBoxNumberOfTeeth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxNumberOfTeeth.Location = new System.Drawing.Point(298, 117);
            this.TextBoxNumberOfTeeth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxNumberOfTeeth.Name = "TextBoxNumberOfTeeth";
            this.TextBoxNumberOfTeeth.Size = new System.Drawing.Size(100, 30);
            this.TextBoxNumberOfTeeth.TabIndex = 8;
            this.TextBoxNumberOfTeeth.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxNumberOfTeeth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerCheck_KeyPress);
            // 
            // TextBoxNumberOfHoles
            // 
            this.TextBoxNumberOfHoles.BackColor = System.Drawing.Color.White;
            this.TextBoxNumberOfHoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxNumberOfHoles.Location = new System.Drawing.Point(298, 154);
            this.TextBoxNumberOfHoles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxNumberOfHoles.Name = "TextBoxNumberOfHoles";
            this.TextBoxNumberOfHoles.Size = new System.Drawing.Size(100, 30);
            this.TextBoxNumberOfHoles.TabIndex = 9;
            this.TextBoxNumberOfHoles.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxNumberOfHoles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IntegerCheck_KeyPress);
            // 
            // ButtonBuild
            // 
            this.ButtonBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonBuild.Location = new System.Drawing.Point(187, 249);
            this.ButtonBuild.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonBuild.Name = "ButtonBuild";
            this.ButtonBuild.Size = new System.Drawing.Size(184, 46);
            this.ButtonBuild.TabIndex = 10;
            this.ButtonBuild.Text = "Построить";
            this.ButtonBuild.UseVisualStyleBackColor = true;
            this.ButtonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // LabelPlateDiametrRange
            // 
            this.LabelPlateDiametrRange.AutoSize = true;
            this.LabelPlateDiametrRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelPlateDiametrRange.Location = new System.Drawing.Point(406, 10);
            this.LabelPlateDiametrRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelPlateDiametrRange.Name = "LabelPlateDiametrRange";
            this.LabelPlateDiametrRange.Size = new System.Drawing.Size(124, 25);
            this.LabelPlateDiametrRange.TabIndex = 11;
            this.LabelPlateDiametrRange.Text = "166-226 мм";
            // 
            // LabelStakeDiametrRange
            // 
            this.LabelStakeDiametrRange.AutoSize = true;
            this.LabelStakeDiametrRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelStakeDiametrRange.Location = new System.Drawing.Point(406, 47);
            this.LabelStakeDiametrRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelStakeDiametrRange.Name = "LabelStakeDiametrRange";
            this.LabelStakeDiametrRange.Size = new System.Drawing.Size(113, 25);
            this.LabelStakeDiametrRange.TabIndex = 12;
            this.LabelStakeDiametrRange.Text = "70-130 мм";
            // 
            // LabelStakeHeightRange
            // 
            this.LabelStakeHeightRange.AutoSize = true;
            this.LabelStakeHeightRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelStakeHeightRange.Location = new System.Drawing.Point(406, 88);
            this.LabelStakeHeightRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelStakeHeightRange.Name = "LabelStakeHeightRange";
            this.LabelStakeHeightRange.Size = new System.Drawing.Size(113, 25);
            this.LabelStakeHeightRange.TabIndex = 13;
            this.LabelStakeHeightRange.Text = "60-120 мм";
            // 
            // LabelNumberOfTeethRange
            // 
            this.LabelNumberOfTeethRange.AutoSize = true;
            this.LabelNumberOfTeethRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNumberOfTeethRange.Location = new System.Drawing.Point(406, 121);
            this.LabelNumberOfTeethRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelNumberOfTeethRange.Name = "LabelNumberOfTeethRange";
            this.LabelNumberOfTeethRange.Size = new System.Drawing.Size(101, 25);
            this.LabelNumberOfTeethRange.TabIndex = 14;
            this.LabelNumberOfTeethRange.Text = "10-12 шт.";
            // 
            // LabelNumberOfHolesRange
            // 
            this.LabelNumberOfHolesRange.AutoSize = true;
            this.LabelNumberOfHolesRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelNumberOfHolesRange.Location = new System.Drawing.Point(406, 158);
            this.LabelNumberOfHolesRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelNumberOfHolesRange.Name = "LabelNumberOfHolesRange";
            this.LabelNumberOfHolesRange.Size = new System.Drawing.Size(112, 25);
            this.LabelNumberOfHolesRange.TabIndex = 15;
            this.LabelNumberOfHolesRange.Text = "90-100 шт.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(101, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 16;
            // 
            // LabelLengthOfHoles
            // 
            this.LabelLengthOfHoles.AutoSize = true;
            this.LabelLengthOfHoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelLengthOfHoles.Location = new System.Drawing.Point(17, 195);
            this.LabelLengthOfHoles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 10);
            this.LabelLengthOfHoles.Name = "LabelLengthOfHoles";
            this.LabelLengthOfHoles.Size = new System.Drawing.Size(264, 25);
            this.LabelLengthOfHoles.TabIndex = 17;
            this.LabelLengthOfHoles.Text = "Длина отвертий в тарелке";
            this.LabelLengthOfHoles.Click += new System.EventHandler(this.label2_Click);
            // 
            // TextBoxLengthOfHoles
            // 
            this.TextBoxLengthOfHoles.BackColor = System.Drawing.Color.White;
            this.TextBoxLengthOfHoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxLengthOfHoles.Location = new System.Drawing.Point(298, 190);
            this.TextBoxLengthOfHoles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxLengthOfHoles.Name = "TextBoxLengthOfHoles";
            this.TextBoxLengthOfHoles.Size = new System.Drawing.Size(100, 30);
            this.TextBoxLengthOfHoles.TabIndex = 18;
            this.TextBoxLengthOfHoles.TextChanged += new System.EventHandler(this.TextBoxValidator_TextChanged);
            this.TextBoxLengthOfHoles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CheckForCommasAndNumbers_KeyPress);
            // 
            // LabelLengthOfHolesRange
            // 
            this.LabelLengthOfHolesRange.AutoSize = true;
            this.LabelLengthOfHolesRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelLengthOfHolesRange.Location = new System.Drawing.Point(407, 195);
            this.LabelLengthOfHolesRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLengthOfHolesRange.Name = "LabelLengthOfHolesRange";
            this.LabelLengthOfHolesRange.Size = new System.Drawing.Size(117, 25);
            this.LabelLengthOfHolesRange.TabIndex = 19;
            this.LabelLengthOfHolesRange.Text = "16-35,5 шт.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 306);
            this.Controls.Add(this.LabelLengthOfHolesRange);
            this.Controls.Add(this.TextBoxLengthOfHoles);
            this.Controls.Add(this.LabelLengthOfHoles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelNumberOfHolesRange);
            this.Controls.Add(this.LabelNumberOfTeethRange);
            this.Controls.Add(this.LabelStakeHeightRange);
            this.Controls.Add(this.LabelStakeDiametrRange);
            this.Controls.Add(this.LabelPlateDiametrRange);
            this.Controls.Add(this.ButtonBuild);
            this.Controls.Add(this.TextBoxNumberOfHoles);
            this.Controls.Add(this.TextBoxNumberOfTeeth);
            this.Controls.Add(this.TextBoxStakeHeight);
            this.Controls.Add(this.TextBoxStakeDiameter);
            this.Controls.Add(this.TextBoxPlateDiameter);
            this.Controls.Add(this.LabelNumberOfHoles);
            this.Controls.Add(this.LabelNumberOfTeeth);
            this.Controls.Add(this.LabelStakeHeight);
            this.Controls.Add(this.LabelStakeDiametr);
            this.Controls.Add(this.LabelPlateDiametr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ручная соковыжималка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelPlateDiametr;
        private System.Windows.Forms.Label LabelStakeDiametr;
        private System.Windows.Forms.Label LabelStakeHeight;
        private System.Windows.Forms.Label LabelNumberOfTeeth;
        private System.Windows.Forms.Label LabelNumberOfHoles;
        private System.Windows.Forms.TextBox TextBoxPlateDiameter;
        private System.Windows.Forms.TextBox TextBoxStakeDiameter;
        private System.Windows.Forms.TextBox TextBoxStakeHeight;
        private System.Windows.Forms.TextBox TextBoxNumberOfTeeth;
        private System.Windows.Forms.TextBox TextBoxNumberOfHoles;
        private System.Windows.Forms.Button ButtonBuild;
        private System.Windows.Forms.Label LabelPlateDiametrRange;
        private System.Windows.Forms.Label LabelStakeDiametrRange;
        private System.Windows.Forms.Label LabelStakeHeightRange;
        private System.Windows.Forms.Label LabelNumberOfTeethRange;
        private System.Windows.Forms.Label LabelNumberOfHolesRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelLengthOfHoles;
        private System.Windows.Forms.TextBox TextBoxLengthOfHoles;
        private System.Windows.Forms.Label LabelLengthOfHolesRange;
    }
}

