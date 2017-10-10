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

    public class PlayerWatcher : NotificationObject
    {
        Player p;

        public PlayerWatcher(Player player)
        {
            p = player;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<Player> Players = new ObservableCollection<Player>();
            var player = new Player();
            var player2 = new Player();

            var readonlyDispatcherCollection = CreateReadonlyDispatcherCollection<PlayerWatcher, Player>(Players, (p) =>
            {
                return new PlayerWatcher(p);
            });
            Console.WriteLine(readonlyDispatcherCollection.Count);

            Players.Add(player);
            Players.Add(player2);
            Console.WriteLine(readonlyDispatcherCollection.Count);

            ReadonlyDispatcherCollection<PlayerWatcher> projected;
            projected = CreateReadonlyDispatcherCollection(Players, (p) => {
                return new PlayerWatcher(p);
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