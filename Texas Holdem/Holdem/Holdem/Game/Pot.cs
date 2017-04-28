namespace TexasHoldem.Logic
{
    public class Pot
    {
        #region Конструкторы
        public Pot()
        {
            _iAmountInPot = 0;
            _iMinimumRaise = 0;
            _iMaximumAmountPutIn = 0;
            _iMinimumAllInAmount = 0;
            _iAmountInPotBeforeAllIn = 0;
            _iAgressorIndex = -1;
        }
        public Pot(int amount, PlayerList playersInPot)
        {
            this.Amount = amount;
            this._playersInPot = playersInPot;
            _iMinimumAllInAmount = 0;
            _iAmountInPotBeforeAllIn = 0;
            _iAgressorIndex = -1;
        }
        #endregion

        #region Методы
        public void AddPlayer(Player player)
        {
            if (!_playersInPot.Contains(player))
            { 
                _playersInPot.Add(player);
            }  
        }
        public void Add(int amount)
        {
            if (amount < 0)
            { 
                return;
            }
            _iAmountInPot += amount;
        }
        #endregion

        #region Свойства
        public int SmallBlind
        {
            get 
            {
                return _iSmallBlind;
            }
            set 
            { 
                _iSmallBlind = value;
            }
        }
        public int BigBlind
        {
            get 
            { 
                return _iBigBlind;
            }
            set 
            { 
                _iBigBlind = value;
            }
        }
        public int MinimumRaise
        {
            get 
            { 
                return _iMinimumRaise;
            }
            set
            {
                _iMinimumRaise = value;
            }
        }
        public int Amount
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
        public int MinimumAllInAmount
        {
            get 
            { 
                return _iMinimumAllInAmount;
            }
            set
            {
                if (value < 0)
                    value = 0;
                _iMinimumAllInAmount = value;
            }
        }
        public int AmountInPotBeforeAllIn
        {
            get 
            { 
                return _iAmountInPotBeforeAllIn;
            }
            set
            {
                if (value < 0)
                    value = 0;
                _iAmountInPotBeforeAllIn = value;
            }
        }
        public int AgressorIndex
        {
            get 
            { 
                return _iAgressorIndex;
            }
            set 
            { 
                _iAgressorIndex = value;
            }
        }
        public int MaximumAmount
        {
            get
            {
                return _iMaximumAmountPutIn;
            }
            set
            {
                _iMaximumAmountPutIn = value;
            }
        }
        public PlayerList PlayersInPot
        {
            get
            {
                return _playersInPot;
            }
        }
        #endregion

        #region Поля
        private PlayerList _playersInPot = new PlayerList();
        private int _iAmountInPot;
        private int _iMinimumRaise;
        private int _iMaximumAmountPutIn;
        private int _iMinimumAllInAmount;
        private int _iAmountInPotBeforeAllIn;
        private int _iAgressorIndex;
        private int _iSmallBlind;
        private int _iBigBlind;
        #endregion
    }
}