using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TexasHoldem.Logic;

namespace TexasHoldem.UI
{
    public partial class FormPoker : Form
    {
        #region Конструкторы
        public FormPoker(string strPlayerName, int iBuyInAmount, int iPlayerQuantity, FormTitleScreen frmTitleScreen)
        {
            InitializeComponent();
            this._frmTitleScreen = frmTitleScreen;
            this.Icon = new Icon("Poker.ico");
            this._strPlayerName = strPlayerName;
            this._iBuyInAmount = iBuyInAmount;
            this._iPlayersQuantity = iPlayerQuantity;
            timerCount = 0;
            //code to resize screen
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            width_ratio = (screenWidth / 1366f);
            height_ratio = (screenHeight / 768f);
            SizeF scale = new SizeF(width_ratio, height_ratio);
            this.Scale(scale);
            //foreach (Control control in this.Controls)
            //{
            //    control.Font = new Font(control.Font.Name, control.Font.SizeInPoints * height_ratio * width_ratio, control.Font.Style);
            //}
            cardWidth = Convert.ToInt32(71f * width_ratio);
            cardHeight = Convert.ToInt32(96f * height_ratio);
            Player humanPlayer = new HumanPlayer(strPlayerName, iBuyInAmount);            
            _playerList.Add(humanPlayer);
            lblName.Text = humanPlayer.Name;            
            for (int i = 1; i < iPlayerQuantity; i++)
            {
                _playerList.Add(new AIPlayer(iBuyInAmount, _rnd));
            }
            _pokerTable = new Table(_playerList, _rnd);
        }
        #endregion

        #region Методы
        private void Initalize()
        {
            //make pbMain the parent of many controls to support transparency
            panelPlayer.Parent = pbMain;
            panel2.Parent = pbMain;
            panel3.Parent = pbMain;
            panel4.Parent = pbMain;
            pbDealer.Parent = pbMain;
            btnCheck.Parent = pbMain;
            btnAllIn.Parent = pbMain;
            btnCancel.Parent = pbMain;
            btnOK.Parent = pbMain;
            btnFold.Parent = pbMain;
            btnRaise.Parent = pbMain;
            lblBanner.Parent = pbMain;
            lblRaiseAmount.Parent = pbMain;
            tbRaise.Parent = pbMain;
            nudBetRaise.Parent = pbMain;
            _bitmap = new Bitmap(pbMain.Width, pbMain.Height);
            panelBubble.Parent = pbMain;
            lblPot.Parent = pbMain;
            panelBubble.BringToFront();
            _g = Graphics.FromImage(_bitmap);
            if (_iPlayersQuantity == 2)
                panel2.Location = panel3.Location;
            if (_iPlayersQuantity == 3)
                panel3.Show();
            else if (_iPlayersQuantity == 4)
            {
                panel3.Show();
                panel4.Show();
            }
            _labelListMoney = new List<Label>();
            _labelListName = new List<Label>();
            _labelListAction = new List<Label>();
            _panelList = new List<Panel>();
            _labelListMoney.Add(lblMoney); _labelListMoney.Add(lblM2); _labelListMoney.Add(lblM3); _labelListMoney.Add(lblM4);
            _labelListName.Add(lblName); _labelListName.Add(lblP2); _labelListName.Add(lblP3); _labelListName.Add(lblP4);
            _labelListAction.Add(lblAction); _labelListAction.Add(lblA2); _labelListAction.Add(lblA3); _labelListAction.Add(lblA4);
            _panelList.Add(panelPlayer); _panelList.Add(panel2); _panelList.Add(panel3); _panelList.Add(panel4);
            //resize font
            //foreach (Panel panel in _panelList)
            //{
            //    foreach (Control control in panel.Controls)
            //    {
            //        control.Font = new Font(control.Font.Name, control.Font.SizeInPoints * height_ratio * width_ratio, control.Font.Style);
            //    }
            //}
            //foreach (Control control in panelBubble.Controls)
            //{
            //    control.Font = new Font(control.Font.Name, control.Font.SizeInPoints * height_ratio * width_ratio, control.Font.Style);
            //}
            for (int i = 1; i < _iPlayersQuantity; i++)
            {
                _labelListName[i].Text = _playerList[i].Name;
            }
            //bind ChipStack to labelListMoney
            Binding b;
            for (int i = 0; i < _iPlayersQuantity; i++)
            {
                b = new Binding("Text", _pokerTable[i], "ChipStack", true);
                b.FormatString = "";
                _labelListMoney[i].DataBindings.Add(b);
                b = new Binding("Text", _pokerTable[i], "SimplifiedMessage");
                b.FormatString = "";
                _labelListAction[i].DataBindings.Add(b);
            }
            HideButtons();
        }
        private void SetDrawingPositions()
        {
            int holeHeight = Convert.ToInt32((60f / 768f) * screenHeight);
            for (int i = 0; i < _pokerTable.Players.Count; i++)
            {
                _holeCardPosition.Add(new Rectangle(_panelList[i].Location.X + Convert.ToInt32((18f / 1366f) * screenWidth), _panelList[i].Location.Y - holeHeight, cardWidth, cardHeight));
                _holeCardPosition.Add(new Rectangle(_panelList[i].Location.X + Convert.ToInt32((87f / 1366f) * screenWidth), _panelList[i].Location.Y - holeHeight, cardWidth, cardHeight));
            }
            int communityHeight = Convert.ToInt32((322f * height_ratio));
            _flopPositions.Add(new Rectangle(Convert.ToInt32(480f * width_ratio), communityHeight, cardWidth, cardHeight));
            _flopPositions.Add(new Rectangle(Convert.ToInt32(569f * width_ratio), communityHeight, cardWidth, cardHeight));
            _flopPositions.Add(new Rectangle(Convert.ToInt32(658f * width_ratio), communityHeight, cardWidth, cardHeight));
            _turnPosition = new Rectangle(Convert.ToInt32(749f * width_ratio), communityHeight, cardWidth, cardHeight);
            _riverPosition = new Rectangle(Convert.ToInt32(837f * width_ratio), communityHeight, cardWidth, cardHeight);
        }
        private void DrawToScreen()
        {
            _g.Clear(Color.Transparent);
            for (int i = 0; i < _pokerTable.Players.Count * 2; i++)
            {
                if (_pokerTable[i / 2]._bBusted)
                    continue;
                _g.DrawImage(_pokerTable[i / 2].Hand[i % 2].GetImage(), _holeCardPosition[i]);
            }
            pbDealer.Location = new Point(_panelList[_pokerTable.DealerPosition].Location.X - Convert.ToInt32(60 * width_ratio), _panelList[_pokerTable.DealerPosition].Location.Y - Convert.ToInt32(15 * height_ratio));

            if (_pokerTable[0].Hand.Count > 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    _g.DrawImage(_pokerTable.CommunityCards[i].GetImage(), _flopPositions[i]);
                }

                if (_pokerTable[0].Hand.Count > 5)
                {
                    _g.DrawImage(_pokerTable.CommunityCards[3].GetImage(), _turnPosition);
                    if (_pokerTable[0].Hand.Count > 6)
                        _g.DrawImage(_pokerTable.CommunityCards[4].GetImage(), _riverPosition);
                }
            }
            pbMain.Image = _bitmap;
        }
        private void RoundStart()
        {
            if (PlayerWon() != 0)
            {
                timerWait3Seconds.Stop();
                lblBanner.Hide();
                //Checks if the player has won at start of new round
                if (PlayerWon() == 1)
                {
                    panelBubble.Hide();
                    MessageBox.Show("You won!");
                    this.Close();
                }
                else if (PlayerWon() == -1)
                {
                    panelBubble.Hide();
                    MessageBox.Show("You lose!");
                    this.Close();
                }
                return;
            }
            HideControls();
            for (int i = 0; i < _pokerTable.Players.Count; i++)
            {
                if (_pokerTable[i].Folded)
                {
                    _panelList[i].BackgroundImage = Image.FromFile("panelNormal.png");
                }
            }
            //resetting variables to start new match
            timerCount = 0;
            showdownCount = 0;
            lblBanner.Show();
            if (_pokerTable.RoundCount == 10)
                lblBanner.Text = "The minimum blinds have" + Environment.NewLine + "been raised";
            else
                lblBanner.Text = "New Round";
            _pokerTable.StartNextRound();
            _pokerTable.DealHoleCards();
            DrawToScreen();
            UpdateMove();
            lblBubble.Text = _pokerTable[_pokerTable.CurrentIndex].Name + " is the dealer";
            timerNextMove.Start();
        }
        private bool PlayerBustedOut()
        {
            foreach (Player player in _pokerTable)
            {
                if (player._bBusted)
                    continue;
                if (player.ChipStack == 0)
                {
                    return true;
                }
            }
            return false;
        }
        private void HideControls()
        {
            for (int i = 0; i < _pokerTable.Players.Count; i++)
            {
                if (_pokerTable[i]._bBusted)
                {
                    if (i == 0)
                        continue;
                    else
                        _panelList[i].Hide();
                }
            }
        }
        private int PlayerWon()
        {
            if (!_pokerTable[0]._bBusted)
            {
                for (int i = 1; i < _pokerTable.Players.Count; i++)
                {
                    if (!_pokerTable[i]._bBusted)
                        return 0;
                }
                return 1;
            }
            return -1;
        }
        private void UpdateMove()
        {
            panelBubble.Show();
            panelBubble.Location = new Point(_panelList[_pokerTable.CurrentIndex].Location.X + Convert.ToInt32(176 * width_ratio), _panelList[_pokerTable.CurrentIndex].Location.Y - Convert.ToInt32(80 * width_ratio));
            lblBubble.Text = _pokerTable[_pokerTable.CurrentIndex].Message;
            lblPot.Text = "Blinds: $" + _pokerTable.SmallBlind.ToString() + "/" + _pokerTable.BigBlind.ToString()
                + Environment.NewLine + "Amount in pot: " + _pokerTable.Pot.Amount.ToString();
        }
        private void SetBetAmounts(int minimum, int maximum)
        {
            nudBetRaise.Minimum = minimum;
            nudBetRaise.Maximum = maximum;
            nudBetRaise.Value = minimum;
            tbRaise.Minimum = minimum;
            tbRaise.Maximum = maximum;
            tbRaise.Value = minimum;
        }
        private void HideButtons()
        {
            btnRaise.Hide();
            btnCheck.Hide();
            btnFold.Hide();
            btnAllIn.Hide();
        }
        private void HideBetBlock()
        {
            btnOK.Hide();
            btnCancel.Hide();
            lblRaiseAmount.Hide();
            nudBetRaise.Hide();
            tbRaise.Hide();
        }
        private void InitalizeButtons()
        {
            btnAllIn.Show();
            btnFold.Show();
            if (_pokerTable.Players[0].GetAmountToCall(_pokerTable.Pot) <= _pokerTable.Players[0].ChipStack)
            {
                btnCheck.Show();

            }
            if (_pokerTable.Pot.MinimumRaise <= _pokerTable.Players[0].ChipStack)
            {
                btnRaise.Show();
            }
            panelBubble.Hide();
            int amountToCall = _pokerTable[_pokerTable.CurrentIndex].GetAmountToCall(_pokerTable.Pot);
            if (amountToCall != 0)
            {
                btnCheck.Text = "Call " + amountToCall.ToString();
                btnRaise.Text = "Raise";
            }
            else
            {
                btnCheck.Text = "Check";
                btnRaise.Text = "Bet";
            }
        }
        #endregion

        #region Обработчики
        private void FormPoker_Load(object sender, EventArgs e)
        {
            Initalize();
            SetDrawingPositions();
            RoundStart();
        }        
        private void timerBusted_Tick(object sender, EventArgs e)
        {
            lblBanner.Hide();
            if (bustedIndex >= _pokerTable.Players.Count)
            {
                timerBusted.Stop();
                _pokerTable.CurrentIndex = _pokerTable.DealerPosition;
                bustedIndex = 0;
                RoundStart();
                return;
            }
            while (_pokerTable[bustedIndex].ChipStack != 0 || _pokerTable[bustedIndex]._bBusted)
            {
                bustedIndex++;
                if (bustedIndex >= _pokerTable.Players.Count)
                {
                    timerBusted.Stop();
                    _pokerTable.CurrentIndex = _pokerTable.DealerPosition;
                    bustedIndex = 0;
                    RoundStart();
                    return;
                }
            }
            _pokerTable.CurrentIndex = bustedIndex;
            _pokerTable[bustedIndex].Leave();
            UpdateMove();
            bustedIndex++;
        }
        private void btnAllIn_MouseEnter(object sender, EventArgs e)
        {
            btnAllIn.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnAllIn_MouseLeave(object sender, EventArgs e)
        {
            btnAllIn.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnFold_MouseEnter(object sender, EventArgs e)
        {
            btnFold.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnFold_MouseLeave(object sender, EventArgs e)
        {
            btnFold.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnCheck_MouseEnter(object sender, EventArgs e)
        {
            btnCheck.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnCheck_MouseLeave(object sender, EventArgs e)
        {
            btnCheck.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnRaise_MouseEnter(object sender, EventArgs e)
        {
            btnRaise.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnRaise_MouseLeave(object sender, EventArgs e)
        {
            btnRaise.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnOK_MouseEnter(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnOK_MouseLeave(object sender, EventArgs e)
        {
            btnOK.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = Image.FromFile("hover.png");
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackgroundImage = Image.FromFile("normal.png");
        }
        private void btnFold_Click(object sender, EventArgs e)
        {
            _pokerTable[0].Fold(_pokerTable.Pot);
            UpdateMove();
            HideButtons();
            panelPlayer.BackgroundImage = Image.FromFile("inactivebutton.png");
            timerNextMove.Start();
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (_pokerTable[_pokerTable.CurrentIndex].GetAmountToCall(_pokerTable.Pot) != 0)
            {
                _pokerTable[0].Call(_pokerTable.Pot);
            }
            else
            { 
                _pokerTable[0].Check(_pokerTable.Pot);
            }                
            UpdateMove();
            HideButtons();
            timerNextMove.Start();
        }
        private void btnRaise_Click(object sender, EventArgs e)
        {
            btnOK.Show();
            btnCancel.Show();
            lblRaiseAmount.Show();
            nudBetRaise.Show();
            tbRaise.Show();
            if (_pokerTable[_pokerTable.CurrentIndex].GetAmountToCall(_pokerTable.Pot) == 0)
            {
                lblRaiseAmount.Text = "Bet Amount: ";
            }
            else
            {
                lblRaiseAmount.Text = "Raise Amount: ";
            }
            SetBetAmounts(_pokerTable.Pot.MinimumRaise, (_pokerTable[0].ChipStack - _pokerTable[0].GetAmountToCall(_pokerTable.Pot)));            
        }
        private void btnAllIn_Click(object sender, EventArgs e)
        {
            _pokerTable[0].AllIn(_pokerTable.Pot, _pokerTable.DecrementIndex(_pokerTable.CurrentIndex));
            UpdateMove();
            HideButtons();
            timerNextMove.Start();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_pokerTable[_pokerTable.CurrentIndex].GetAmountToCall(_pokerTable.Pot) != 0)
            {
                _pokerTable[0].Raise(Convert.ToInt32(nudBetRaise.Value), _pokerTable.Pot, _pokerTable.DecrementIndex(_pokerTable.CurrentIndex));
            }
            else
            {
                _pokerTable[0].Bet(Convert.ToInt32(nudBetRaise.Value), _pokerTable.Pot, _pokerTable.DecrementIndex(_pokerTable.CurrentIndex));
            }    
            UpdateMove();
            HideButtons();
            HideBetBlock();
            timerNextMove.Start();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            HideBetBlock();
        }
        private void tbRaise_Scroll(object sender, EventArgs e)
        {
            for (int i = tbRaise.Minimum; i < tbRaise.Maximum; i += tbRaise.TickFrequency)
            {
                if (tbRaise.Value - i > 0 && tbRaise.Value - i < tbRaise.TickFrequency)
                { 
                    tbRaise.Value = i;
                }                    
            }
            nudBetRaise.Value = tbRaise.Value;
        }
        private void nudBetRaise_ValueChanged(object sender, EventArgs e)
        {
            if (nudBetRaise.Value < tbRaise.Minimum || nudBetRaise.Value > tbRaise.Maximum)
            { 
                return;
            }                
            tbRaise.Value = (int)nudBetRaise.Value;
        }
        private void timerNextMove_Tick(object sender, EventArgs e)
        {
            timerCount++;
            if (_pokerTable.PlayerWon())
            {
                panelBubble.Hide();
                _pokerTable.CurrentIndex = _pokerTable.IncrementIndexShowdown(_pokerTable.CurrentIndex);
                _pokerTable[_pokerTable.CurrentIndex].CollectMoney(_pokerTable.Pot);
                lblBanner.Text = _pokerTable[_pokerTable.CurrentIndex].Message;
                lblBanner.Show();
                timerNextMove.Stop();
                timerWait3Seconds.Start();
                return;
            }
            if (_pokerTable.BeginNextTurn())
            {
                _pokerTable.CurrentIndex = _pokerTable.IncrementIndex(_pokerTable.CurrentIndex);
                lblBanner.Hide();
                if (timerCount == 1)
                {
                    _pokerTable.PaySmallBlind();
                }
                else if (timerCount == 2)
                {
                    _pokerTable.PayBigBlind();
                }
                else if (_pokerTable.CurrentIndex == 0)
                {
                    InitalizeButtons();
                    timerNextMove.Stop();
                    return;
                }
                else
                {
                    AIPlayer currentPlayer = (AIPlayer)_pokerTable[_pokerTable.CurrentIndex];
                    currentPlayer.MakeADecision(_pokerTable.Pot, _pokerTable.DecrementIndex(_pokerTable.CurrentIndex));
                    _pokerTable[_pokerTable.CurrentIndex] = currentPlayer;
                    if (currentPlayer.Folded)
                    { 
                        _panelList[_pokerTable.CurrentIndex].BackgroundImage = Image.FromFile("inactivebutton.png");
                    }
                }
                UpdateMove();
            }
            else
            {
                _pokerTable.TurnCount = 0;
                lblBanner.Show();
                panelBubble.Hide();
                if (_pokerTable[0].Hand.Count == 2)
                {
                    _pokerTable.DealFlop();
                    lblBanner.Text = "Dealing the Flop";
                    toolTipHint.SetToolTip(panelPlayer, HandCombination.EvaluateHand(new Hand(_pokerTable[0].Hand)).ToString());
                }
                else if (_pokerTable[0].Hand.Count == 5)
                {
                    _pokerTable.DealTurn();
                    lblBanner.Text = "Dealing the Turn";
                    toolTipHint.SetToolTip(panelPlayer, HandCombination.EvaluateHand(new Hand(_pokerTable[0].Hand)).ToString());
                }
                else if (_pokerTable[0].Hand.Count == 6)
                {
                    _pokerTable.DealRiver();
                    lblBanner.Text = "Dealing the River";
                    toolTipHint.SetToolTip(panelPlayer, HandCombination.EvaluateHand(new Hand(_pokerTable[0].Hand)).ToString());
                }
                else if (_pokerTable[0].Hand.Count == 7)
                {
                    lblBanner.Text = "Showdown";
                    timerNextMove.Stop();
                    timerShowdown.Start();
                    return;
                }
                int dealerPosition = _pokerTable.DealerPosition;
                _pokerTable.CurrentIndex = _pokerTable.DealerPosition;
                _pokerTable.Pot.AgressorIndex = _pokerTable.DealerPosition;
                DrawToScreen();
            }
        }        
        private void timerWait3Seconds_Tick(object sender, EventArgs e)
        {

            if (PlayerBustedOut())
            {
                timerWait3Seconds.Stop();
                timerBusted.Start();
                return;
            }
            RoundStart();
            timerWait3Seconds.Stop();
        }
        private void timerShowdown_Tick(object sender, EventArgs e)
        {
            showdownCount++;
            if (showdownCount > _pokerTable.Pot.PlayersInPot.Count)
            {
                for (int i = 0; i < _pokerTable[_pokerTable.CurrentIndex].Hand.Count; i++)
                { 
                    _pokerTable[_pokerTable.CurrentIndex].Hand[i].UnHighlight();
                }
                DrawToScreen();
                _pokerTable.ShowDown();
                lblBanner.Text = _pokerTable.WinnerMessage;
                timerShowdown.Stop();
                timerWait3Seconds.Start();
            }
            else
            {
                for (int i = 0; i < _pokerTable[_pokerTable.CurrentIndex].Hand.Count; i++)
                { 
                     _pokerTable[_pokerTable.CurrentIndex].Hand[i].UnHighlight();
                }
                int currentIndex = _pokerTable.IncrementIndexShowdown(_pokerTable.CurrentIndex);
                _pokerTable.CurrentIndex = currentIndex;
                _pokerTable[currentIndex].Hand[0].FaceUp = true;
                _pokerTable[currentIndex].Hand[1].FaceUp = true;
                Hand bestHand = HandCombination.EvaluateHand(new Hand(_pokerTable[currentIndex].Hand));
                for (int i = 0; i < _pokerTable[currentIndex].Hand.Count; i++)
                {
                    for (int j = 0; j < bestHand.Count; j++)
                    {
                        if (bestHand[j] == _pokerTable[currentIndex].Hand[i] && bestHand[j].Suit == _pokerTable[currentIndex].Hand[i].Suit)
                        {
                            _pokerTable[currentIndex].Hand[i].Highlight();
                        }
                    }
                }
                lblBanner.Text = _pokerTable[currentIndex].Name + " has: " + Environment.NewLine + bestHand.ToString();
                DrawToScreen();
            }
        }
        private void toolTipHint_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }        
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormGameOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frmTitleScreen.Show();
        }
        #endregion

        #region Свойства
        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region Поля
        private string _strPlayerName;
        private int _iBuyInAmount;
        private int _iPlayersQuantity;
        private Table _pokerTable;
        private PlayerList _playerList = new PlayerList();
        private Bitmap _bitmap;
        private Graphics _g;
        private List<Label> _labelListMoney;
        private List<Label> _labelListName;
        private List<Label> _labelListAction;
        private List<Panel> _panelList;
        private List<Rectangle> _holeCardPosition = new List<Rectangle>();
        private List<Rectangle> _flopPositions = new List<Rectangle>(3);
        private Rectangle _turnPosition;
        private Rectangle _riverPosition;
        private FormTitleScreen _frmTitleScreen;
        private static Random _rnd = new Random();
        //counters
        int timerCount = 0;
        int showdownCount = 0;
        int bustedIndex = 0;
        //scaling factors
        float width_ratio, height_ratio;
        int cardWidth, cardHeight;
        int screenWidth, screenHeight;
        #endregion
    }
}