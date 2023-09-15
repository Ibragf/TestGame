namespace Client
{
    partial class GameForm
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
            this.GuessTextBox = new System.Windows.Forms.TextBox();
            this.GuessButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.GamesCountLabel = new System.Windows.Forms.Label();
            this.GuessResultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GuessTextBox
            // 
            this.GuessTextBox.Location = new System.Drawing.Point(109, 53);
            this.GuessTextBox.Name = "GuessTextBox";
            this.GuessTextBox.Size = new System.Drawing.Size(100, 20);
            this.GuessTextBox.TabIndex = 0;
            // 
            // GuessButton
            // 
            this.GuessButton.Location = new System.Drawing.Point(121, 79);
            this.GuessButton.Name = "GuessButton";
            this.GuessButton.Size = new System.Drawing.Size(75, 23);
            this.GuessButton.TabIndex = 1;
            this.GuessButton.Text = "Угадать";
            this.GuessButton.UseVisualStyleBackColor = true;
            this.GuessButton.Click += new System.EventHandler(this.GuessButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(121, 108);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(75, 23);
            this.LogoutButton.TabIndex = 2;
            this.LogoutButton.Text = "Выход";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(139, 134);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(35, 13);
            this.UserNameLabel.TabIndex = 3;
            this.UserNameLabel.Text = "label1";
            this.UserNameLabel.Visible = false;
            // 
            // GamesCountLabel
            // 
            this.GamesCountLabel.AutoSize = true;
            this.GamesCountLabel.Location = new System.Drawing.Point(139, 161);
            this.GamesCountLabel.Name = "GamesCountLabel";
            this.GamesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.GamesCountLabel.TabIndex = 4;
            this.GamesCountLabel.Text = "label2";
            this.GamesCountLabel.Visible = false;
            // 
            // GuessResultLabel
            // 
            this.GuessResultLabel.AutoSize = true;
            this.GuessResultLabel.Location = new System.Drawing.Point(139, 24);
            this.GuessResultLabel.Name = "GuessResultLabel";
            this.GuessResultLabel.Size = new System.Drawing.Size(35, 13);
            this.GuessResultLabel.TabIndex = 5;
            this.GuessResultLabel.Text = "label1";
            this.GuessResultLabel.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 183);
            this.Controls.Add(this.GuessResultLabel);
            this.Controls.Add(this.GamesCountLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.GuessButton);
            this.Controls.Add(this.GuessTextBox);
            this.Name = "GameForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox GuessTextBox;
        private System.Windows.Forms.Button GuessButton;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label GamesCountLabel;
        private System.Windows.Forms.Label GuessResultLabel;
    }
}