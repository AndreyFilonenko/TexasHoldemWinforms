using System;

namespace TexasHoldem.Logic
{ 
    public class AIPlayer : Player
    {
        #region Конструкторы
        public AIPlayer(int buyInAmount, Random rnd)
            : base(buyInAmount)
        {            
            this.Name = GetBotName();
            _rnd = rnd;
        }
        #endregion

        #region Методы
        private string GetBotName()
        {
            return "111";
        }
        public void MakeADecision(Pot mainPot, int index)
        {
            int firstPercent = 35;
            int secondPercent = 80;
            int thirdPercent = 98;
            int random = _rnd.Next(100) + 1;
            if (random <= firstPercent)
            {
                if (GetAmountToCall(mainPot) == 0)
                {
                    Check(mainPot);
                }
                else
                { 
                    Fold(mainPot);
                }  
            }
            else if (random > firstPercent && random <= secondPercent)
            {
                if (GetAmountToCall(mainPot) == 0)
                {
                    Check(mainPot);
                }
                else
                { 
                    Call(mainPot);
                }   
            }
            else if (random > secondPercent && random <= thirdPercent)
            {
                if (GetAmountToCall(mainPot) == 0)
                {
                    Bet((((_rnd.Next(15) + 10) * (mainPot.MinimumRaise * 10)) / 100), mainPot, index);
                }
                else
                { 
                    Raise((((_rnd.Next(15) + 10) * (mainPot.MinimumRaise * 10)) / 100), mainPot, index);
                } 
            }
            else if (random > thirdPercent)
            { 
                AllIn(mainPot, index);
            }
        }
        #endregion

        #region Поля
        private Random _rnd;
        #endregion
    }
}