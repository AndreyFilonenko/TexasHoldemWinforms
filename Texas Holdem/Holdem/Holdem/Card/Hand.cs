using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem.Logic
{
    public class Hand
    {
        #region Конструкторы
        public Hand()
        {
            _hand = new List<Card>();
            _handValue = new List<int>();
        }
        public Hand(Hand otherHand)
        {
            _hand = new List<Card>(otherHand._hand);
            _handValue = new List<int>();
        }
        #endregion

        #region Методы
        public void Clear()
        {
            _hand.Clear();
            _handValue.Clear();
        }
        public void Add(Card card)
        {
            _hand.Add(card);
        }
        public void Remove(int index)
        {
            _hand.RemoveAt(index);
        }
        public void Remove(Card card)
        {
            _hand.Remove(card);
        }
        public void AddValue(int value)
        {
            _handValue.Add(value);
        }
        public void SortByRank()
        {
            _hand = (from card in _hand orderby card.Rank descending select card ).ToList();
        }
        public override string ToString()
        {
            string strOut = "";
            if (this._handValue.Count() == 0)
            {
                strOut = "No Poker Hand is Found";
            }
            switch (this._handValue[0])
            {
                case 1:
                    strOut = Card.RankToString((RANK)_handValue[1]) + " High";
                    break;
                case 2:
                    strOut = "Pair of " + Card.RankToString((RANK)_handValue[1]) + "s";
                    break;
                case 3:
                    strOut = "Two Pair: " + Card.RankToString((RANK)_handValue[1]) + "s over " + Card.RankToString((RANK)_handValue[2]) + "s";
                    break;
                case 4:
                    strOut = "Three " + Card.RankToString((RANK)_handValue[1]) + "s";
                    break;
                case 5:
                    strOut = Card.RankToString((RANK)_handValue[1]) + " High Straight";
                    break;
                case 6:
                    strOut = Card.RankToString((RANK)_handValue[1]) + " High Flush";
                    break;
                case 7:
                    strOut = Card.RankToString((RANK)_handValue[1]) + "s Full of " + Card.RankToString((RANK)_handValue[2]) + "s";
                    break;
                case 8:
                    strOut = "Quad " + Card.RankToString((RANK)_handValue[1]) + "s";
                    break;
                case 9:
                    strOut = Card.RankToString((RANK)_handValue[1]) + " High Straight Flush";
                    break;
                case 10:
                    strOut = "Royal Flush";
                    break;
                default:                    
                    break;
            }
            return strOut;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Перегруженные операторы
        public static bool operator ==(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count; i++)
            {
                if (a.Value[i] != b.Value[i])
                {
                    return false;
                }
            }
            return true;
        }        
        public static bool operator !=(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count(); i++)
            {
                if (a.Value[i] != b.Value[i])
                {
                    return true;
                }
            }
            return false;
        }
        public static bool operator <(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count(); i++)
            {
                if (a.Value[i] < b.Value[i])
                {
                    return true;
                }
                if (a.Value[i] > b.Value[i])
                {
                    return false;
                }
            }
            return false;
        }
        public static bool operator >(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count(); i++)
            {
                if (a.Value[i] > b.Value[i])
                {
                    return true;
                }
                if (a.Value[i] < b.Value[i])
                {
                    return false;
                }

            }
            return false;
        }
        public static bool operator <=(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count(); i++)
            {
                if (a.Value[i] < b.Value[i])
                {
                    return true;
                }
                if (a.Value[i] > b.Value[i])
                {
                    return false;
                }

            }
            return true;
        }
        public static bool operator >=(Hand a, Hand b)
        {
            for (int i = 0; i < a.Value.Count(); i++)
            {
                if (a.Value[i] > b.Value[i])
                {
                    return true;
                }
                if (a.Value[i] < b.Value[i])
                {
                    return false;
                }

            }
            return true;
        }
        public static Hand operator +(Hand a, Hand b)
        {
            for (int i = 0; i < b.Count; i++)
            {
                a.Add(b[i]);
            }
            return a;
        }
        #endregion

        #region Свойства
        public List<int> Value
        {
            get
            {
                return _handValue;
            }
        }
        public Card this[int index]
        {
            get
            {
                return _hand[index];
            }
            set
            {
                _hand[index] = value;
            }
        }
        public int Count
        {
            get
            {
                return _hand.Count;
            }
        }
        #endregion

        #region Поля
        private List<Card> _hand;
        private List<int> _handValue;
        #endregion
    }
}