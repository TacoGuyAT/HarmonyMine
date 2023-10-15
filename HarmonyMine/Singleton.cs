using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine {
    public class Singleton<T> where T : class, new() {

        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());
        public static T Instance { get { return lazy.Value; } }

        public Singleton() {
        }
    }
}
