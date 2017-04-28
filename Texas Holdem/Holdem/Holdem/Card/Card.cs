using System.Drawing;
namespace TexasHoldem.Logic
{    
    public class Card
    {
        #region Конструкторы
        public Card()
        {
            _rank = RANK.TWO;
            _suit = SUIT.DIAMONDS;
            _bFaceUp = false;
        }
        public Card(RANK rank,SUIT suit)
        {
            this._rank = rank;
            this._suit = suit;
            _bFaceUp = false;
        }
        public Card(RANK rank, SUIT suit,bool faceUp)
        {
            this._rank = rank;
            this._suit = suit;
            this._bFaceUp = faceUp;
        }
        #endregion

        #region Методы
        public static string rankToString(RANK rank)
        {
            string strOut = "";
            switch (rank)
            {
                case RANK.JACK:
                    strOut = "Jack";
                    break;
                case RANK.QUEEN:
                    strOut = "Queen";
                    break;
                case RANK.KING:
                    strOut = "King";
                    break;
                case RANK.ACE:
                    strOut = "Ace";
                    break;
                default:
                    strOut = rank.ToString();
                    break;
            }
            return strOut;
        }
        public static string suitToString(SUIT suit)
        {
            string strOut = "";
            switch (suit)
            {
                case SUIT.DIAMONDS:
                    strOut = "Diamonds";
                    break;
                case SUIT.CLUBS:
                    strOut = "Clubs";
                    break;
                case SUIT.HEARTS:
                    strOut = "Hearts";
                    break;
                case SUIT.SPADES:
                    strOut = "Spades";
                    break;
                default:
                    break;
            }
            return strOut;
        }
        private void getImageFromFile()
        {
            if (_bFaceUp)
            {
                this.Image = new Bitmap("Cards\\" + (int)_suit + "-" + (int)_rank + ".png");
            }
            else
            { 
                this.Image = new Bitmap("Cards\\sb.bmp");
            } 
        }
        public Bitmap getImage()
        {
            if (Image == null)
            { 
                getImageFromFile();
            }  
            return this.Image;
        }
        public override string ToString()
        {
            if (_bFaceUp == true)
            { 
                return rankToString(_rank) + " of " + suitToString(_suit);
            }
            return "The card is facedown, you cannot see it!";
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void Highlight()
        {
            if (_bFaceUp == false)
            { 
                return;
            }
            if (this.Image == null)
            { 
                getImageFromFile();
            }
            Bitmap HighlightedBitmap = new Bitmap(Image.Width, Image.Height);
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    int green = Image.GetPixel(i, j).G;
                    HighlightedBitmap.SetPixel(i, j, Color.FromArgb(255, 0, green, 0));
                }
            }
            Image = new Bitmap(HighlightedBitmap);
        }
        public void UnHighlight()
        {
            if (_bFaceUp == false)
            { 
                return;
            }
            getImageFromFile();
        }
        #endregion

        #region Перегруженные операторы
        public static bool operator ==(Card a, Card b)
        {
            if (a._rank == b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Card a, Card b)
        {
            if (a._rank != b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <(Card a, Card b)
        {
            if (a._rank < b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator >(Card a, Card b)
        {
            if (a._rank > b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator <=(Card a, Card b)
        {
            if (a._rank <= b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator >=(Card a, Card b)
        {
            if (a._rank >= b._rank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Свойства
        public bool FaceUp
        {
            get 
            { 
                return _bFaceUp;
            }
            set 
            { 
                _bFaceUp = value;
                getImageFromFile();
            }
        }
        public RANK Rank
        {
            get 
            {
                return _rank;
            }
        }
        public SUIT Suit
        {
            get
            {
                return _suit;
            }
        }
        #endregion

        #region Поля
        private RANK _rank;
        private SUIT _suit;
        private Bitmap Image;
        private bool _bFaceUp;
        #endregion
    }
}