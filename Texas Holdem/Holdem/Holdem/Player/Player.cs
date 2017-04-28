using System.ComponentModel;

namespace TexasHoldem.Logic
{
    public abstract class Player : INotifyPropertyChanged
    {
        #region Конструкторы
        public Player(int buyInAmount)
        {
            ChipStack = buyInAmount;
            _iInitialStack = ChipStack;
            _iAmountInPot = 0;
            _bFolded = false;
            _strMessage = "No decision has been made";
            _strSimplifiedMessage = "";
            _bBusted = false;
        }
        public Player(string name, int buyInAmount)
        {
            if (name == "")
                name = "You";
            this._strName = name;
            ChipStack = buyInAmount;
            _iInitialStack = ChipStack;
            _iAmountInPot = 0;
            _bFolded = false;
            _strMessage = "No decision has been made";
            _strSimplifiedMessage = "";
            _bBusted = false;
        }
        #endregion

        #region Методы
        public int GetAmountToCall(Pot mainPot)
        {
            return mainPot.MaximumAmount - _iAmountInPot;
        }
        public void AddToHand(Hand hand)
        {
            _hand += hand;
        }
        public void AddToHand(Card card)
        {
            _hand.Add(card);
        }
        public void PaySmallBlind(int amount, Pot mainPot, int index)
        {
            if (ChipStack <= amount)
            {
                AllIn(mainPot, index);
                return;
            }
            ChipStack -= amount;
            _iAmountInPot += amount;
            mainPot.Add(amount);
            mainPot.AddPlayer(this);
            mainPot.MaximumAmount = _iAmountInPot;
            mainPot.MinimumRaise = amount;
            Message = this.Name + " pays the small blind";
            SimplifiedMessage = "SMALL BLIND " + amount;
        }
        public void PayBigBlind(int amount, Pot mainPot, int index)
        {
            if (ChipStack <= amount)
            {
                AllIn(mainPot, index);
                return;
            }
            Message = "Pay blind of " + amount.ToString();
            ChipStack -= amount;
            _iAmountInPot += amount;
            mainPot.Add(amount);
            mainPot.AddPlayer(this);
            mainPot.MaximumAmount = _iAmountInPot;
            mainPot.MinimumRaise = amount;
            Message = this.Name + " pays the big blind";
            SimplifiedMessage = "BIG BLIND " + amount;
            mainPot.AgressorIndex = index;
        }
        public void Fold(Pot mainPot)
        {

            _bFolded = true;
            mainPot.PlayersInPot.Remove(this);
            Message = "Fold";
            SimplifiedMessage = "FOLDED";

        }
        public void Check(Pot mainPot)
        {
            Message = "Check";
            SimplifiedMessage = "CHECK";
        }
        public void Call(Pot mainPot)
        {

            int amount = mainPot.MaximumAmount - _iAmountInPot;
            if (ChipStack <= amount)
            {
                AllIn(mainPot);
                return;
            }
            ChipStack -= amount;
            _iAmountInPot += amount;
            mainPot.Add(amount);
            mainPot.AddPlayer(this);
            Message = "Call " + amount.ToString();
            SimplifiedMessage = "CALL " + amount;
        }
        public void Raise(int raise, Pot mainPot, int index)
        {
            int amount = mainPot.MaximumAmount + raise - _iAmountInPot;
            if (ChipStack <= amount)
            {
                AllIn(mainPot, index);
                return;
            }
            ChipStack -= amount;
            _iAmountInPot += amount;
            mainPot.Add(amount);
            mainPot.MaximumAmount = _iAmountInPot;
            mainPot.AddPlayer(this);
            mainPot.MinimumRaise = raise;
            Message = "Call " + (amount - raise).ToString() + " and raise " + raise.ToString();
            SimplifiedMessage = "RAISE " + (amount - raise);
            mainPot.AgressorIndex = index;
        }
        public void Bet(int bet, Pot mainPot, int index)
        {
            if (ChipStack <= bet)
            {
                AllIn(mainPot, index);
                return;
            }
            ChipStack -= bet;
            _iAmountInPot += bet;
            mainPot.Add(bet);
            mainPot.MaximumAmount = _iAmountInPot;
            mainPot.MinimumRaise = bet;
            Message = "Bet " + bet.ToString();
            SimplifiedMessage = "BET " + bet;
            mainPot.AgressorIndex = index;
        }
        public void AllIn(Pot mainPot)
        {
            AmountContributed = ChipStack;
            if (mainPot.MinimumAllInAmount == 0)
            {
                mainPot.AmountInPotBeforeAllIn = mainPot.Amount;
                mainPot.MinimumAllInAmount = ChipStack;
            }
            else if (_iChipStack < mainPot.MinimumAllInAmount)
            {
                mainPot.MinimumAllInAmount = ChipStack;
            }
            if (ChipStack > mainPot.MinimumRaise)
            { 
                mainPot.MinimumRaise = ChipStack;
            }
            mainPot.AddPlayer(this);
            mainPot.Add(ChipStack);
            _iAmountInPot += ChipStack;
            ChipStack = 0;
            if (_iAmountInPot > mainPot.MaximumAmount)
            { 
                mainPot.MaximumAmount = _iAmountInPot;
            }
            Message = "I'm All-In";
            SimplifiedMessage = "ALL IN";
        }
        public void AllIn(Pot mainPot, int index)
        {
            AmountContributed = ChipStack;
            if (mainPot.MinimumAllInAmount == 0)
            {
                mainPot.AmountInPotBeforeAllIn = mainPot.Amount;
                mainPot.MinimumAllInAmount = ChipStack;
            }
            else if (_iChipStack < mainPot.MinimumAllInAmount)
            {
                mainPot.MinimumAllInAmount = ChipStack;
            }
            if (ChipStack > mainPot.MinimumRaise)
            { 
                mainPot.MinimumRaise = ChipStack;
            }
            mainPot.AddPlayer(this);
            mainPot.Add(ChipStack);
            _iAmountInPot += ChipStack;
            ChipStack = 0;
            if (_iAmountInPot > mainPot.MaximumAmount)
            { 
                mainPot.MaximumAmount = _iAmountInPot;
            }
            Message = "I'm All-In";
            SimplifiedMessage = "ALL IN";
            mainPot.AgressorIndex = index;
        }
        public void Reset()
        {
            this._iAmountInPot = 0;
            this._bFolded = false;
            InitialStack = ChipStack;
            this._hand.Clear();
            this.Message = "";
            this.SimplifiedMessage = "";
        }
        public void CollectMoney(Pot mainPot)
        {
            this.ChipStack += mainPot.Amount;
            this.Message = this.Name + " wins the pot!";
        }
        public void Leave()
        {
            this.Message = this.Name + " busted out!";
            _bBusted = true;
        }
        #endregion

        #region События и обработчики
        public event PropertyChangedEventHandler PropertyChanged;
        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            { 
                handler(this, e);
            } 
        }
        #endregion

        #region Свойства
        public string Name
        {
            get 
            { 
                return _strName; 
            }
            set 
            { 
                _strName = value;
            }
        }
        public int ChipStack
        {
            get 
            { 
                return _iChipStack;
            }
            set
            {
                if (value < 0)
                    value = 0;
                _iChipStack = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("ChipStack"));
            }
        }
        public int AmountContributed
        {
            get 
            { 
                return _iAmountContributed;
            }
            set 
            { 
                _iAmountContributed = value;
            }
        }
        public string Message
        {
            get
            {
                return _strMessage;
            }
            set
            {
                _strMessage = value;
            }
        }
        public string SimplifiedMessage
        {
            get
            {
                return _strSimplifiedMessage;
            }
            set
            {
                _strSimplifiedMessage = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("SimplifiedMessage"));
            }
        }
        public int InitialStack
        {
            get 
            { 
                return _iInitialStack;
            }
            set
            {
                if (value < 0)
                    value = 0;
                _iInitialStack = value;
            }
        }
        public int AmountInPot
        {
            get 
            { 
                return _iAmountInPot;
            }
            set
            {
                if (value < 0)
                    value = 0;
                _iAmountInPot = value;
            }
        }
        public bool Folded
        {
            get
            {
                return _bFolded;
            }
        }
        public Hand Hand
        {
            get
            {
                return _hand;
            }
        }
        #endregion

        #region Поля
        protected Hand _hand = new Hand();
        protected string _strName;
        protected int _iChipStack;
        protected int _iAmountInPot;
        protected bool _bFolded;
        protected int _iAmountContributed;
        protected int _iInitialStack;
        protected string _strMessage;
        protected string _strSimplifiedMessage;
        public bool _bBusted;
        #endregion
    }
}