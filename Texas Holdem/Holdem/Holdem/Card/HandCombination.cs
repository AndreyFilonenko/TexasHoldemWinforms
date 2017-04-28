namespace TexasHoldem.Logic
{
    public static class HandCombination
    {
        #region Публичные методы
        public static Hand EvaluateHand(Hand hand)
        {
            if (hand.Count < 5)
            {
                hand.Clear();
                return hand;
            }
            if (IsRoyalFlush(hand))
            { 
                return GetRoyalFlush(hand);
            }
            if (IsStraightFlush(hand))
            { 
                return GetStraightFlush(hand);
            }
            if (IsFourOfAKind(hand))
            { 
                return GetFourOfAKind(hand);
            }
            if (IsFullHouse(hand))
            { 
                return GetFullHouse(hand);
            }
            if (IsFlush(hand))
            { 
                return GetFlush(hand);
            }
            if (IsStraight(hand))
            { 
                return GetStraight(hand);
            }
            if (IsThreeOfAKind(hand))
            { 
                return GetThreeOfAKind(hand);
            }
            if (IsTwoPair(hand))
            { 
                return GetTwoPair(hand);
            }
            if (IsOnePair(hand))
            { 
                return GetOnePair(hand);
            }
            return GetHighCard(hand);
        }        
        #endregion

        #region Приватные методы
        private static bool IsRoyalFlush(Hand hand)
        {
            hand.SortByRank();
            Hand simplifiedhand1;
            Hand simplifiedhand2;
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                {
                    simplifiedhand1 = new Hand(hand);
                    simplifiedhand1.Remove(i);
                    simplifiedhand2 = new Hand(hand);
                    simplifiedhand2.Remove(i + 1);
                    if (IsRoyalFlush(simplifiedhand1))
                    { 
                        return true;
                    }
                    if (IsRoyalFlush(simplifiedhand2))
                    { 
                        return true;
                    }
                }
            }
            SUIT currentsuit = hand[0].Suit;
            if (hand[0].Rank == RANK.ACE && hand[1].Rank == RANK.KING
                && hand[2].Rank == RANK.QUEEN && hand[3].Rank == RANK.JACK
                && hand[4].Rank == RANK.TEN && hand[1].Suit == currentsuit
                && hand[2].Suit == currentsuit && hand[3].Suit == currentsuit
                && hand[4].Suit == currentsuit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static Hand GetRoyalFlush(Hand hand)
        {
            hand.SortByRank();
            Hand straightflush = new Hand(GetStraightFlush(hand));
            straightflush.AddValue((int)COMBINATION.ROYALFLUSH);
            if (straightflush[0].Rank == RANK.ACE)
            {
                return straightflush;
            }
            else
            {
                straightflush.Clear();
                return straightflush;
            }
        }
        private static bool IsStraightFlush(Hand hand)
        {
            hand.SortByRank();
            Hand simplifiedhand1;
            Hand simplifiedhand2;
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                {
                    simplifiedhand1 = new Hand(hand);
                    simplifiedhand1.Remove(i);
                    simplifiedhand2 = new Hand(hand);
                    simplifiedhand2.Remove(i + 1);
                    if (IsStraightFlush(simplifiedhand1))
                    { 
                        return true;
                    }
                    if (IsStraightFlush(simplifiedhand2))
                    { 
                        return true;
                    }
                }
            }
            for (int i = 0; i <= hand.Count - 5; i++)
            {
                RANK currentrank = hand[i].Rank;
                SUIT currentsuit = hand[i].Suit;
                if (currentrank == hand[i + 1].Rank + 1 && currentrank == hand[i + 2].Rank + 2
                    && currentrank == hand[i + 3].Rank + 3 && currentrank == hand[i + 4].Rank + 4
                    && currentsuit == hand[i + 1].Suit && currentsuit == hand[i + 2].Suit
                    && currentsuit == hand[i + 3].Suit && currentsuit == hand[i + 4].Suit)
                {
                    return true;
                }
            }
            for (int i = 0; i <= hand.Count - 4; i++)
            {
                RANK currentrank = hand[i].Rank;
                SUIT currentsuit = hand[i].Suit;
                if (currentrank == RANK.FIVE && hand[i + 1].Rank == RANK.FOUR
                    && hand[i + 2].Rank == RANK.THREE && hand[i + 3].Rank == RANK.TWO
                    && hand[0].Rank == RANK.ACE && currentsuit == hand[i + 1].Suit
                    && currentsuit == hand[i + 2].Suit && currentsuit == hand[i + 3].Suit
                    && currentsuit == hand[0].Suit)
                { 
                    return true;
                }
            }
            return false;
        }
        private static Hand GetStraightFlush(Hand hand)
        {
            hand.SortByRank();
            Hand straightflush = new Hand();
            straightflush.AddValue((int)COMBINATION.STRAIGHTFLUSH);
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Add(new Card(RANK.ACE, hand[0].Suit));
            }
            straightflush.Add(hand[0]);
            int ptr1 = 0;
            int ptr2 = 1;
            while (ptr1 < hand.Count - 2 || ptr2 < hand.Count)
            {
                if (straightflush.Count >= 5)
                { 
                    break;
                }
                RANK rank1 = hand[ptr1].Rank;
                RANK rank2 = hand[ptr2].Rank;
                SUIT suit1 = hand[ptr1].Suit;
                SUIT suit2 = hand[ptr2].Suit;
                if (rank1 - rank2 == 1 && suit1 == suit2)
                {
                    straightflush.Add(hand[ptr2]);
                    ptr1 = ptr2;
                    ptr2++;
                }
                else if (rank1 == RANK.TWO && rank2 == RANK.ACE && suit1 == suit2)
                {
                    straightflush.Add(hand[ptr2]);
                    ptr1 = ptr2;
                    ptr2++;
                }
                else
                {
                    if (rank1 - rank2 <= 1)
                    {
                        ptr2++;
                    }
                    else
                    {
                        straightflush.Clear();
                        straightflush.AddValue((int)COMBINATION.STRAIGHTFLUSH);
                        ptr1++;
                        ptr2 = ptr1 + 1;
                        straightflush.Add(hand[ptr1]);
                    }
                }
            }
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Remove(hand.Count - 1);
            }
            straightflush.AddValue((int)straightflush[0].Rank);
            if (straightflush.Count < 5)
            { 
                straightflush.Clear();
            }
            return straightflush;
        }
        private static bool IsFourOfAKind(Hand hand)
        {
            hand.SortByRank();
            for (int i = 0; i <= hand.Count - 4; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2] && hand[i] == hand[i + 3])
                { 
                    return true;
                }
            }
            return false;
        }
        private static Hand GetFourOfAKind(Hand hand)
        {
            Hand fourofakind = new Hand();
            fourofakind.AddValue((int)COMBINATION.QUADS);
            hand.SortByRank();
            for (int i = 0; i <= hand.Count - 4; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2] && hand[i] == hand[i + 3])
                {
                    fourofakind.Add(hand[i]);
                    fourofakind.Add(hand[i + 1]);
                    fourofakind.Add(hand[i + 2]);
                    fourofakind.Add(hand[i + 3]);
                    fourofakind.AddValue((int)hand[i].Rank);
                    break;
                }
            }
            return GetKickers(hand, fourofakind);
        }
        private static bool IsFullHouse(Hand hand)
        {
            hand.SortByRank();
            bool threeofakind = false;
            bool pair = false;
            RANK threeofakindRank = 0;
            for (int i = 0; i <= hand.Count - 3; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2])
                {
                    threeofakind = true;
                    threeofakindRank = hand[i].Rank;
                    break;
                }
            }
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i].Rank != threeofakindRank)
                {
                    pair = true;
                    break;
                }
            }
            if (threeofakind == true && pair == true)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        private static Hand GetFullHouse(Hand hand)
        {
            hand.SortByRank();
            Hand fullhouse = new Hand();
            fullhouse.AddValue((int)COMBINATION.FULLHOUSE);
            bool threeofakind = false;
            bool pair = false;
            RANK threeofakindRank = 0;
            for (int i = 0; i <= hand.Count - 3; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2])
                {
                    threeofakind = true;
                    threeofakindRank = hand[i].Rank;
                    fullhouse.Add(hand[i]);
                    fullhouse.Add(hand[i + 1]);
                    fullhouse.Add(hand[i + 2]);
                    fullhouse.AddValue((int)hand[i].Rank);
                    break;
                }
            }
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i].Rank != threeofakindRank)
                {
                    pair = true;
                    fullhouse.Add(hand[i]);
                    fullhouse.Add(hand[i + 1]);
                    fullhouse.AddValue((int)hand[i].Rank);
                    break;
                }
            }
            if (threeofakind == true && pair == true)
            {
                return fullhouse;
            }
            else
            {
                fullhouse.Clear();
                return fullhouse;
            }
        }
        private static bool IsFlush(Hand hand)
        {
            hand.SortByRank();
            int diamondCount = 0;
            int clubCount = 0;
            int heartCount = 0;
            int spadeCount = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Suit == SUIT.DIAMONDS)
                {
                    diamondCount++;
                }
                else if (hand[i].Suit == SUIT.CLUBS)
                {
                    clubCount++;
                }
                else if (hand[i].Suit == SUIT.HEARTS)
                {
                    heartCount++;
                }
                else if (hand[i].Suit == SUIT.SPADES)
                { 
                    spadeCount++;
                }
            }
            if (diamondCount >= 5)
            {
                return true;
            }
            else if (clubCount >= 5)
            {
                return true;
            }
            else if (heartCount >= 5)
            {
                return true;
            }
            else if (spadeCount >= 5)
            { 
                return true;
            }
            return false;
        }
        private static Hand GetFlush(Hand hand)
        {
            hand.SortByRank();
            Hand flush = new Hand();
            flush.AddValue((int)COMBINATION.FLUSH);
            int diamondCount = 0;
            int clubCount = 0;
            int heartCount = 0;
            int spadeCount = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Suit == SUIT.DIAMONDS)
                {
                    diamondCount++;
                }
                else if (hand[i].Suit == SUIT.CLUBS)
                {
                    clubCount++;
                }
                else if (hand[i].Suit == SUIT.HEARTS)
                {
                    heartCount++;
                }
                else if (hand[i].Suit == SUIT.SPADES)
                { 
                    spadeCount++;
                }
            }
            if (diamondCount >= 5)
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    if (hand[i].Suit == SUIT.DIAMONDS)
                    {
                        flush.Add(hand[i]);
                        flush.AddValue((int)hand[i].Rank);
                    }
                    if (flush.Count == 5)
                    { 
                        break;
                    }
                }
            }
            else if (clubCount >= 5)
            {
                for (int i = 0; i <= hand.Count; i++)
                {
                    if (hand[i].Suit == SUIT.CLUBS)
                    {
                        flush.Add(hand[i]);
                        flush.AddValue((int)hand[i].Rank);
                    }
                    if (flush.Count == 5)
                    { 
                        break;
                    }
                }
            }
            else if (heartCount >= 5)
            {
                for (int i = 0; i <= hand.Count; i++)
                {
                    if (hand[i].Suit == SUIT.HEARTS)
                    {
                        flush.Add(hand[i]);
                        flush.AddValue((int)hand[i].Rank);
                    }
                    if (flush.Count == 5)
                    { 
                        break;
                    }
                }
            }
            else if (spadeCount >= 5)
            {
                for (int i = 0; i <= hand.Count; i++)
                {
                    if (hand[i].Suit == SUIT.SPADES)
                    {
                        flush.Add(hand[i]);
                        flush.AddValue((int)hand[i].Rank);
                    }
                    if (flush.Count == 5)
                    { 
                        break;
                    }
                }
            }
            return flush;
        }
        private static bool IsStraight(Hand hand)
        {
            hand.SortByRank();
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Add(new Card(RANK.ACE, hand[0].Suit));
            } 
            int straightCount = 1;
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (straightCount == 5)
                { 
                    break;
                }
                RANK currentrank = hand[i].Rank;
                if (currentrank - hand[i + 1].Rank == 1)
                {
                    straightCount++;
                }
                else if (currentrank == RANK.TWO && hand[i + 1].Rank == RANK.ACE)
                {
                    straightCount++;
                }
                else if (currentrank - hand[i + 1].Rank > 1)
                { 
                    straightCount = 1;
                }
            }
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Remove(hand.Count - 1);
            }
            if (straightCount == 5)
            { 
                return true;
            }
            return false;
        }
        private static Hand GetStraight(Hand hand)
        {
            hand.SortByRank();
            Hand straight = new Hand();
            straight.AddValue((int)COMBINATION.STRAIGHT);
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Add(new Card(RANK.ACE, hand[0].Suit));
            }
            int straightCount = 1;
            straight.Add(hand[0]);
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (straightCount == 5)
                { 
                    break;
                }
                RANK currentrank = hand[i].Rank;
                if (currentrank - hand[i + 1].Rank == 1)
                {
                    straightCount++;
                    straight.Add(hand[i + 1]);
                }
                else if (currentrank == RANK.TWO && hand[i + 1].Rank == RANK.ACE)
                {
                    straightCount++;
                    straight.Add(hand[i + 1]);
                }
                else if (currentrank - hand[i + 1].Rank > 1)
                {
                    straightCount = 1;
                    straight.Clear();
                    straight.AddValue((int)COMBINATION.STRAIGHT);
                    straight.Add(hand[i + 1]);
                }
            }
            if (hand[0].Rank == RANK.ACE)
            { 
                hand.Remove(hand.Count - 1);
            }
            if (straightCount != 5)
            { 
                straight.Clear();
            }
            straight.AddValue((int)straight[0].Rank);
            return straight;
        }
        private static bool IsThreeOfAKind(Hand hand)
        {
            hand.SortByRank();
            for (int i = 0; i <= hand.Count - 3; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2])
                { 
                    return true;
                }
            }
            return false;
        }
        private static Hand GetThreeOfAKind(Hand hand)
        {
            hand.SortByRank();
            Hand threeofakind = new Hand();
            threeofakind.AddValue((int)COMBINATION.THREE);
            for (int i = 0; i <= hand.Count - 3; i++)
            {
                if (hand[i] == hand[i + 1] && hand[i] == hand[i + 2])
                {
                    threeofakind.AddValue((int)hand[i].Rank);
                    threeofakind.Add(hand[i]);
                    threeofakind.Add(hand[i + 1]);
                    threeofakind.Add(hand[i + 2]);
                    break;
                }
            }
            return GetKickers(hand, threeofakind);
        }
        private static bool IsTwoPair(Hand hand)
        {
            hand.SortByRank();
            int pairCount = 0;
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                {
                    pairCount++;
                    i++;
                }
            }
            if (pairCount >= 2)
            {
                return true;
            }
            else
            { 
                return false;
            } 
        }
        private static Hand GetTwoPair(Hand hand)
        {
            hand.SortByRank();
            Hand twopair = new Hand();
            twopair.AddValue((int)COMBINATION.TWOPAIRS);
            int pairCount = 0;
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                {
                    twopair.AddValue((int)hand[i].Rank);
                    twopair.Add(hand[i]);
                    twopair.Add(hand[i + 1]);
                    pairCount++;
                    if (pairCount == 2)
                    { 
                        break;
                    }
                    i++;
                }
            }
            if (pairCount == 2)
            {
                return GetKickers(hand, twopair);
            }
            else
            {
                twopair.Clear();
            }
            return twopair;
        }
        private static bool IsOnePair(Hand hand)
        {
            hand.SortByRank();
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                { 
                    return true;
                }
            }
            return false;
        }
        private static Hand GetOnePair(Hand hand)
        {
            hand.SortByRank();
            Hand onepair = new Hand();
            onepair.AddValue((int)COMBINATION.PAIR);
            for (int i = 0; i <= hand.Count - 2; i++)
            {
                if (hand[i] == hand[i + 1])
                {
                    onepair.AddValue((int)hand[i].Rank);
                    onepair.Add(hand[i]);
                    onepair.Add(hand[i + 1]);
                    break;
                }
            }
            return GetKickers(hand, onepair);
        }
        private static Hand GetHighCard(Hand hand)
        {
            hand.SortByRank();
            Hand highcard = new Hand();
            highcard.AddValue((int)COMBINATION.HIGHCARD);
            highcard.Add(hand[0]);
            highcard.AddValue((int)hand[0].Rank);
            return GetKickers(hand, highcard);
        }
        private static Hand GetKickers(Hand hand, Hand specialCards)
        {
            if (specialCards.Count == 0)
            { 
                return specialCards;
            }
            for (int i = 0; i < specialCards.Count; i++)
            {
                hand.Remove(specialCards[i]);
            }
            for (int i = 0; i < hand.Count;i++)
            {
                if (specialCards.Count >= 5)
                { 
                    break;
                }                    
                specialCards.Add(hand[i]);
                specialCards.AddValue((int)hand[i].Rank);
            }
            return specialCards;
        }
        #endregion
    }
}