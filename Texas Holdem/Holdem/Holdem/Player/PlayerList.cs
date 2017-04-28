using System.Collections.Generic;
using System.Linq;

namespace TexasHoldem.Logic
{
    public class PlayerList:IList<Player>
    {
        #region Конструкторы
        public PlayerList()
        {
        }
        public PlayerList(PlayerList playerList)
        {
            this._list.AddRange(playerList._list);
        }
        #endregion

        #region Методы
        public int IndexOf(Player item)
        {
            return _list.IndexOf(item);
        }
        public void Insert(int index, Player item)
        {
            _list.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }
        public Player GetPlayer(ref int index)
        {
            while (index > _list.Count() - 1)
            { 
                index -= _list.Count();
            }
            while (index < 0)
            { 
                index += _list.Count();
            }
            return _list[index];
        }
        public void Add(Player item)
        {
            _list.Add(item);
        }
        public void AddRange(PlayerList players)
        {
            _list.AddRange(players);
        }
        public void Clear()
        {
            _list.Clear();
        }
        public bool Contains(Player item)
        {
            return _list.Contains(item);
        }
        public void CopyTo(Player[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }
        public bool Remove(Player item)
        {
            return _list.Remove(item);
        }
        public IEnumerator<Player> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        public void ResetPlayers()
        {
            foreach (Player player in this)
            { 
                player.Reset();
            }
        }
        public void Sort()
        {
            _list = (from player in _list orderby player.AmountInPot descending select player).ToList();
        }
        #endregion

        #region Свойства
        public Player this[int index]
        {
            get
            {
                while (index > _list.Count() - 1)
                { 
                    index -= _list.Count();
                }
                while (index < 0)
                { 
                    index += _list.Count();
                }
                return _list[index];
            }
            set
            {
                while (index > _list.Count() - 1)
                { 
                    index -= _list.Count();
                }
                while (index < 0)
                { 
                    index += _list.Count();
                }
                _list[index] = value;
            }
        }
        public int Count
        {
            get { return _list.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Поля
        List<Player> _list = new List<Player>();
        #endregion
    }
}