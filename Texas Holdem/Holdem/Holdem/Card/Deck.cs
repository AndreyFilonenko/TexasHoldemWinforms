using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem.Logic
{
    public class Deck
    {
        #region Конструкторы
        private Deck()
        {
            for (RANK r = RANK.TWO; r <= RANK.ACE; ++r)
            {
                for (SUIT s = SUIT.DIAMONDS; s <= SUIT.SPADES; ++s)
                {
                    _deck.Add(new Card(r, s, false));
                }
            }
            _deck.TrimExcess();
        }
        #endregion

        #region Методы
        public static Deck GetNewDeck()
        {
            Deck deck = new Deck();
            deck.Shuffle();
            return deck;
        }       
        public Card Deal(bool faceUp)
        {
            Card dealCard = _deck.ElementAt(_deck.Count - 1);
            dealCard.FaceUp = faceUp;
            _deck.RemoveAt(_deck.Count - 1);
            return dealCard;
        }
        private void Shuffle()
        {
            _deck = _deck.OrderBy(c => Guid.NewGuid()).ToList();
        } 
        #endregion

        #region Поля
        private List<Card> _deck = new List<Card>();
        #endregion
    }
}