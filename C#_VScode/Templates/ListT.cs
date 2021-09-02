using System;

namespace Templates
{
    class ListT<T>
    {
        static private ListT<T> _Instance;
        public static ListT<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ListT<T>();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public T[] list { get; set; }
        public int n { get; set; }
        public ListT()
        {
            n = 0;
        }
        public void Add(T s)
        {
            if (n == 0)
            {
                list = new T[1];
                list[0] = s;
                n = 1;
            }
            else
            {
                T[] temp = new T[n];
                for (int i = 0; i < n; i++)
                {
                    temp[i] = list[i];
                }
                //list = null;
                list = new T[n + 1];
                for (int i = 0; i < n; i++) list[i] = temp[i];
                list[n] = s;
                n++;
            }
        }
        public void Insert(T s, int index)
        {
            if (index >= n - 1) Add(s);
            else
            {
                if (index == 0)
                {
                    T[] temp = new T[n];
                    for (int i = 0; i < n; i++) temp[i] = list[i];
                    list = new T[n + 1];
                    list[0] = s;
                    for (int i = 1; i < n + 1; i++) list[i] = temp[i - 1];
                    n++;
                }
                else
                {
                    T[] temp = new T[n];
                    for (int i = 0; i < n; i++) temp[i] = list[i];
                    list = new T[n + 1];
                    for (int i = 0; i < index; i++) list[i] = temp[i];
                    list[index] = s;
                    for (int i = index; i < n; i++) list[i + 1] = temp[i];
                    n++;
                }
            }
        }
        public static bool DeepEquals(T a, T b)
        {
            foreach (var property in a.GetType().GetProperties())
            {
                var aValue = property.GetValue(a);
                var bValue = property.GetValue(b);
                if (!aValue.Equals(bValue)) return false;
            }
            return true;
        }
        public int IndexOf(T s)
        {
            int index = -1;
            for (int i = 0; i < n; i++)
            {
                if (DeepEquals(s, list[i]))
                    return i;
            }
            return index;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index > n)
                Console.WriteLine("Vi tri xoa khong hop le.");
            else
            {
                int i;
                T[] temp = new T[n - 1];
                if (index == n - 1)
                {
                    for (i = 0; i < n - 1; i++) temp[i] = list[i];
                    list = new T[n - 1];
                    for (i = 0; i < n - 1; i++) list[i] = temp[i];
                }
                else
                {
                    for (i = 0; i < index; i++) temp[i] = list[i];
                    for (i = index + 1; i < n; i++) temp[i - 1] = list[i];
                    list = new T[n - 1];
                    for (i = 0; i < n - 1; i++) list[i] = temp[i];
                }
                n--;
            }
        }
        public void Remove(T s)
        {
            int index = IndexOf(s);
            if (index == -1)
                Console.WriteLine("Khong tim thay sinh vien can xoa.");
            else
                RemoveAt(index);
        }
        public void Sort()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (true)
                    {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
    }
}
