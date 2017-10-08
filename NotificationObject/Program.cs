using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NotificationObject.Helper;

namespace NotificationObject
{
    public class Player : NotificationObject
    {

        public int HP
        {
            get
            {
                return _HP;
            }
            set
            {
                SetProperty(ref _HP, value);
            }
        }
        int _HP;
    }

    public class PlayerProj : NotificationObject
    {
        Player p;

        public PlayerProj(Player player)
        {
            p = player;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<Player> Players = new ObservableCollection<Player>();
            ObservableCollection<PlayerProj> Result = new ObservableCollection<PlayerProj>();

            Result.CollectionChanged += (sender, e) => { Console.WriteLine("Changed"); };
            Func<Player, PlayerProj> converter = (pl) => { return new PlayerProj(pl); };
            Players.CollectionChanged += (sender, e)=> {
                switch(e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            Result.Insert(e.NewStartingIndex, converter((Player)e.NewItems[0]));
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        {
                            Result.Move(e.OldStartingIndex, e.NewStartingIndex);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            Result.RemoveAt(e.OldStartingIndex);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        {
                            Result[e.NewStartingIndex] = converter((Player)e.NewItems[0]);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        {
                            Result.Clear();
                        }
                        break;
                }
            };

            var player = new Player();
            var player2 = new Player();

            ReadOnlyCollection<Player> Reader = new ReadOnlyCollection<Player>(Players);
            Players.Add(player);
            Players.Add(player2);

            var readonlyDispatcherCollection = CreateReadonlyDispatcherCollection<PlayerProj, Player>(Players, (p) =>
            {
                return new PlayerProj(p);
            });
            Console.WriteLine(readonlyDispatcherCollection.Count);
            Players.Clear();
            Console.WriteLine(readonlyDispatcherCollection.Count);
            Players.Add(player);
            Console.WriteLine(readonlyDispatcherCollection.Count);

            Console.ReadKey();
        }
    }
}