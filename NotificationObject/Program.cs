﻿using System;
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

    public class PlayerWatcher : IDisposable 
    {
        Player p;
        PropertyChangedEventListener listener;

        public PlayerWatcher(Player player)
        {
            p = player;
            listener = new PropertyChangedEventListener(player, (sender, e) => { });
        }

        public void Dispose()
        {
            if (p != null && listener != null)
            {
                listener.Dispose();
                Console.WriteLine("Disposed");
            }
        }
    }

    public class HPGauge : IDisposable
    {
        IDisposable listener;

        public HPGauge(Player pl)
        {
            listener = new PropertyChangedEventListener(pl, (sender, e)
                =>
            {
                if (e.PropertyName == "HP")
                {
                    Console.WriteLine("HP changed");
                }
            });
        }

        public void Dispose()
        {
            listener?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<Player> Players = new ObservableCollection<Player>();
            var player = new Player();
            var player2 = new Player();

            var gauge = new HPGauge(player);
            // gauge listens player's HP
            player.HP = 400;

            var readonlySyncedCollection = CreateReadonlySyncedCollection<PlayerWatcher, Player>(Players, (p) =>
            {
                return new PlayerWatcher(p);
            });
            Console.WriteLine(readonlySyncedCollection.Count);

            // readonlyDi
            Players.Add(player);
            Players.Add(player2);
            Console.WriteLine(readonlySyncedCollection.Count);

            ReadonlySyncedCollection<PlayerWatcher> projected;
            projected = CreateReadonlySyncedCollection(Players, (p) => {
                return new PlayerWatcher(p);
            });

            Console.WriteLine(readonlySyncedCollection.Count);
            Players.Clear();
            Console.WriteLine(readonlySyncedCollection.Count);
            Players.Add(player);
            Console.WriteLine(readonlySyncedCollection.Count);

            Console.ReadKey();
        }
    }
}