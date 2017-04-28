using System;
using System.Drawing;
using System.Windows.Forms;

namespace TexasHoldem.UI
{
    public partial class FormTitleScreen : Form
    {
        #region Конструкторы
        public FormTitleScreen()
        {
            InitializeComponent();
            this.Icon = new Icon("Poker.ico");
            this.StartPosition = FormStartPosition.CenterScreen;
            txtYourName.Text = "Player";
        }
        #endregion

        #region Обработчики
        private void btnPlay_Click(object sender, EventArgs e)
        {
            string strPlayerName = txtYourName.Text;
            int iBuyInAmount = (int)nudBuyIn.Value;
            if (strPlayerName == null || _iPlayersQuantity < 2)
            {
                MessageBox.Show("Please choose game options before beginning.");
                return;
            }
            this.Hide();
            FormPoker FormPoker = new FormPoker(strPlayerName, iBuyInAmount, _iPlayersQuantity, this);
            FormPoker.ShowDialog();
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            btn2.FlatAppearance.BorderColor = Color.Red;
            btn3.FlatAppearance.BorderColor = Color.Black;
            btn4.FlatAppearance.BorderColor = Color.Black;
            _iPlayersQuantity = 2;
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            btn2.FlatAppearance.BorderColor = Color.Black;
            btn3.FlatAppearance.BorderColor = Color.Red;
            btn4.FlatAppearance.BorderColor = Color.Black;
            _iPlayersQuantity = 3;
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            btn2.FlatAppearance.BorderColor = Color.Black;
            btn3.FlatAppearance.BorderColor = Color.Black;
            btn4.FlatAppearance.BorderColor = Color.Red;
            _iPlayersQuantity = 4;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Поля
        private int _iPlayersQuantity = 1;
        #endregion        
    }
}