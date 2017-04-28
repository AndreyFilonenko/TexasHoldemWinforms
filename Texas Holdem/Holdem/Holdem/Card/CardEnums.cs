namespace TexasHoldem.Logic
{
    public enum RANK
    {
        TWO = 2,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        ACE
    }
    public enum SUIT
    {
        DIAMONDS = 1,
        CLUBS,
        HEARTS,
        SPADES
    }
    public enum COMBINATION
    {
        HIGHCARD = 1,
        PAIR,
        TWOPAIRS,
        THREE,
        STRAIGHT,
        FLUSH,
        FULLHOUSE,
        QUADS,
        STRAIGHTFLUSH,
        ROYALFLUSH
    }
}