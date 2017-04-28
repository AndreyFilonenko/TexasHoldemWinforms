using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem.Logic
{
    public class Table
    {
        #region Конструкторы
        public Table(PlayerList players, Random rnd)
        {
            this._players = players;
            _deck = Deck.GetNewDeck();
            _rnd = rnd;
            _mainPot = new Pot();
            _sidePots = new List<Pot>();
            _smallBlind = new Blind();
            _bigBlind = new Blind();
            _iRoundCounter = 0;
            _iTurnCount = 0;
            _iDealerPosition = _rnd.Next(players.Count);
            _smallBlind.Amount = 500;
            _bigBlind.Amount = 1000;
            _mainPot.SmallBlind = 500;
            _mainPot.BigBlind = 1000;
            _smallBlind.position = _iDealerPosition + 1;
            _bigBlind.position = _iDealerPosition + 2;
            _iCurrentIndex = _iDealerPosition;
        }        
        #endregion

        #region Методы
        public bool BeginNextTurn()
        {
            _iTurnCount++;
            while (_players[_mainPot.AgressorIndex].Folded && _iCurrentIndex != _mainPot.AgressorIndex)
            {
                _mainPot.AgressorIndex = DecrementIndex(_mainPot.AgressorIndex);
            }
            if (_iCurrentIndex == _mainPot.AgressorIndex && _iTurnCount > 1)
            {
                return false;
            }
            else if (EveryoneAllIn())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool EveryoneAllIn()
        {
            int zeroCount = 0;
            int totalCount = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                if (this[i]._bBusted || this[i].Folded)
                {
                    continue;
                }
                if (this[i].ChipStack == 0)
                {
                    zeroCount++;
                }
                totalCount++;
            }
            if (zeroCount != 0 && totalCount == zeroCount)
            {
                return true;
            }
            else if (totalCount - zeroCount == 1)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (this[i]._bBusted || this[i].Folded)
                    {
                        continue;
                    }
                    if (this[i].ChipStack != 0 && this[i].GetAmountToCall(_mainPot) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int IncrementIndex(int currentIndex)
        {
            currentIndex++;
            while (_players.GetPlayer(ref currentIndex).Folded || _players.GetPlayer(ref currentIndex)._bBusted
                || _players.GetPlayer(ref currentIndex).ChipStack == 0)
            {
                currentIndex++;
            }
            _players.GetPlayer(ref currentIndex);
            return currentIndex;
        }
        public int IncrementIndexShowdown(int currentIndex)
        {
            currentIndex++;
            while (_players.GetPlayer(ref currentIndex).Folded || _players.GetPlayer(ref currentIndex)._bBusted)
            {
                currentIndex++;
            }
            _players.GetPlayer(ref currentIndex);
            return currentIndex;
        }
        public int DecrementIndex(int currentIndex)
        {
            currentIndex--;
            while (_players.GetPlayer(ref currentIndex).Folded || _players.GetPlayer(ref currentIndex)._bBusted
                || _players.GetPlayer(ref currentIndex).ChipStack == 0)
            {
                currentIndex--;
            }
            _players.GetPlayer(ref currentIndex);
            return currentIndex;
        }
        public void DealHoleCards()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (i == 0)
                {
                    _players[i].AddToHand(_deck.Deal(true));
                    _players[i].AddToHand(_deck.Deal(true));
                }
                else
                {
                    _players[i].AddToHand(_deck.Deal(false));
                    _players[i].AddToHand(_deck.Deal(false));
                }
            }
        }
        public void PaySmallBlind()
        {
            _players.GetPlayer(ref _smallBlind.position).PaySmallBlind(_smallBlind.Amount, _mainPot, _iCurrentIndex);
            _iCurrentIndex = _smallBlind.position;
        }
        public void PayBigBlind()
        {
            _players.GetPlayer(ref _bigBlind.position).PayBigBlind(_bigBlind.Amount, _mainPot, _iCurrentIndex);
            _iCurrentIndex = _bigBlind.position;
            _iTurnCount = 0;
        }
        public void DealFlop()
        {
            _tableHand.Add(_deck.Deal(true));
            _tableHand.Add(_deck.Deal(true));
            _tableHand.Add(_deck.Deal(true));
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].AddToHand(_tableHand);
            }
        }
        public void DealTurn()
        {
            Card turn = _deck.Deal(true);
            _tableHand.Add(turn);
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].AddToHand(turn);
            }
        }
        public void DealRiver()
        {
            Card river = _deck.Deal(true);
            _tableHand.Add(river);
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].AddToHand(river);
            }
        }
        public void ShowDown()
        {
            if (CreateSidePots())
            {
                _mainPot.PlayersInPot.Sort();
                for (int i = 0; i < _mainPot.PlayersInPot.Count - 1; i++)
                {
                    if (_mainPot.PlayersInPot[i].AmountInPot != _mainPot.PlayersInPot[i + 1].AmountInPot)
                    {
                        PlayerList tempPlayers = new PlayerList();
                        for (int j = _mainPot.PlayersInPot.Count - 1; j > i; j--)
                        {
                            tempPlayers.Add(_mainPot.PlayersInPot[j]);
                        }
                        int potSize = (_mainPot.PlayersInPot[i + 1].AmountInPot - _mainPot.PlayersInPot[i].AmountInPot) * tempPlayers.Count;
                        _mainPot.Amount -= potSize;
                        _sidePots.Add(new Pot(potSize, tempPlayers));
                    }
                }
            }
            PlayerList bestHandList = new PlayerList();
            List<int> Winners = new List<int>();
            bestHandList = SortBestHand(new PlayerList(_mainPot.PlayersInPot));
            for (int i = 0; i < bestHandList.Count; i++)
            {
                for (int j = 0; j < this.Players.Count; j++)
                {
                    if (_players[j] == bestHandList[i])
                    {
                        Winners.Add(j);
                    }
                }
                if (HandCombination.EvaluateHand(new Hand(bestHandList[i].Hand))
                    != HandCombination.EvaluateHand(new Hand(bestHandList[i + 1].Hand)))
                {
                    break;
                }
            }
            _mainPot.Amount /= Winners.Count;
            if (Winners.Count > 1)
            {
                for (int i = 0; i < this.Players.Count; i++)
                {
                    if (Winners.Contains(i))
                    {
                        _iCurrentIndex = i;
                        _players[i].CollectMoney(_mainPot);
                        _strWinnerMessage += _players[i].Name + ", ";
                    }
                }
                _strWinnerMessage += Environment.NewLine + " split the pot.";
            }
            else
            {
                _iCurrentIndex = Winners[0];
                _players[_iCurrentIndex].CollectMoney(_mainPot);
                _strWinnerMessage = _players[_iCurrentIndex].Message;
            }
            for (int i = 0; i < _sidePots.Count; i++)
            {
                List<int> sidePotWinners = new List<int>();
                for (int x = 0; x < bestHandList.Count; x++)
                {
                    for (int j = 0; j < this.Players.Count; j++)
                    {
                        if (_players[j] == bestHandList[x] && _sidePots[i].PlayersInPot.Contains(bestHandList[x]))
                        {
                            sidePotWinners.Add(j);
                        }
                    }
                    if (HandCombination.EvaluateHand(new Hand(bestHandList[x].Hand))
                        != HandCombination.EvaluateHand(new Hand(bestHandList[x + 1].Hand))
                        && sidePotWinners.Count != 0)
                    {
                        break;
                    }
                }
                _sidePots[i].Amount /= sidePotWinners.Count;
                for (int j = 0; j < this.Players.Count; j++)
                {
                    if (sidePotWinners.Contains(j))
                    {
                        _iCurrentIndex = j;
                        _players[j].CollectMoney(_sidePots[i]);
                    }
                }
            }
        }
        private bool CreateSidePots()
        {
            for (int i = 0; i < _mainPot.PlayersInPot.Count() - 1; i++)
            {
                if (_mainPot.PlayersInPot[i].AmountInPot != _mainPot.PlayersInPot[i + 1].AmountInPot)
                {
                    return true;
                }
            }
            return false;
        }
        private PlayerList SortBestHand(PlayerList myPlayers)
        {
            Player pivot;
            if (myPlayers.Count() <= 1)
            {
                return myPlayers;
            }
            pivot = myPlayers[_rnd.Next(myPlayers.Count())];
            myPlayers.Remove(pivot);
            var less = new PlayerList();
            var greater = new PlayerList();
            foreach (Player player in myPlayers)
            {
                if (HandCombination.EvaluateHand(new Hand(player.Hand)) > HandCombination.EvaluateHand(new Hand(pivot.Hand)))
                {
                    greater.Add(player);
                }
                else if (HandCombination.EvaluateHand(new Hand(player.Hand)) <= HandCombination.EvaluateHand(new Hand(pivot.Hand)))
                {
                    less.Add(player);
                }
            }
            var list = new PlayerList();
            list.AddRange(SortBestHand(greater));
            list.Add(pivot);
            list.AddRange(SortBestHand(less));
            return list;
        }
        public bool PlayerWon()
        {
            if (_mainPot.PlayersInPot.Count == 1)
            {
                foreach (Player player in this)
                {
                    if (player._bBusted)
                    {
                        continue;
                    }
                    if (player.Folded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public IEnumerator<Player> GetEnumerator()
        {
            return _players.GetEnumerator();
        }
        public void StartNextRound()
        {
            _players.ResetPlayers();
            _deck = Deck.GetNewDeck();
            if (_iRoundCounter == 10)
            {
                _iRoundCounter = 0;
                _smallBlind.Amount *= 2;
                _bigBlind.Amount = _smallBlind.Amount * 2;
                _mainPot.SmallBlind = SmallBlind;
                _mainPot.BigBlind = BigBlind;
            }
            if (_iRoundCounter != 0)
            {
                _iDealerPosition = IncrementIndex(_iDealerPosition);
                _smallBlind.position = IncrementIndex(_iDealerPosition);
                _bigBlind.position = IncrementIndex(_smallBlind.position);
            }
            _iRoundCounter++;
            _mainPot.Amount = 0;
            _mainPot.AgressorIndex = -1;
            _mainPot.MinimumRaise = _bigBlind.Amount;
            _tableHand.Clear();
            _iCurrentIndex = _iDealerPosition;
            _strWinnerMessage = null;
            _mainPot.PlayersInPot.Clear();
            _sidePots.Clear();
        }
        #endregion

        #region Свойства
        public string WinnerMessage
        {
            get
            {
                return _strWinnerMessage;
            }
        }
        public int TurnCount
        {
            get 
            { 
                return _iTurnCount;
            }
            set 
            { 
                _iTurnCount = value;
            }
        }
        public int SmallBlind
        {
            get 
            { 
                return _smallBlind.Amount;
            }
        }
        public int BigBlind
        {
            get 
            { 
                return _bigBlind.Amount;
            }
        }
        public int RoundCount
        {
            get 
            { 
                return _iRoundCounter;
            }
            set 
            { 
                _iRoundCounter = value;
            }
        }
        public Player this[int index]
        {
            get
            {
                return _players.GetPlayer(ref index);
            }
            set
            {
                _players[index] = value;
            }
        }
        public int CurrentIndex
        {
            get
            {
                return _iCurrentIndex;
            }
            set
            {
                _iCurrentIndex = value;
            }
        }
        public PlayerList Players
        {
            get
            {
                return _players;
            }
        }
        public Pot Pot
        { 
            get
            {
                return _mainPot;
            }
        }
        public int DealerPosition
        {
            get
            {
                return _iDealerPosition;
            }
        }
        public Hand CommunityCards
        {
            get
            {
                return _tableHand;
            }
        }
        #endregion

        #region Поля
        private PlayerList _players = new PlayerList();
        private Deck _deck;
        private Hand _tableHand = new Hand();
        private int _iRoundCounter;
        private Pot _mainPot;
        private List<Pot> _sidePots;
        private Random _rnd;
        private int _iTurnCount;
        private string _strWinnerMessage;
        private Blind _smallBlind;
        private Blind _bigBlind;
        private int _iDealerPosition;
        private int _iCurrentIndex;
        #endregion
        private struct Blind
        {
            private int _amount;
            public int position;
            public int Amount
            {
                get
                {
                    return _amount;
                }
                set
                {
                    _amount = value;
                }
            }
        }
    }
}