using System;

namespace QLSV
{
    class QLSV
    {
        static private QLSV _Instance;
        public static QLSV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QLSV();
                }
                return _Instance;
            }
            private set
            {
                
            }
        }
        public SV[] list { get; set; }
        public int n { get; set; }
        public QLSV()
        {
            list = null;
            n = 0;
        }
        public void Add(SV s)
        {
            if (n == 0)
            {
                list = new SV[1];
                list[0] = s;
                n = 1;
            }
            else
            {
                SV[] temp = new SV[n];
                for (int i = 0; i < n; i++)
                {
                    temp[i] = list[i];
                }
                //list = null;
                list = new SV[n + 1];
                for (int i = 0; i < n; i++) list[i] = temp[i];
                list[n] = s;
                n++;
            }
        }
        public void Insert(SV s, int index)
        {
            if (index >= n - 1) Add(s);
            else
            {
                if (index == 0)
                {
                    SV[] temp = new SV[n];
                    for (int i = 0; i < n; i++) temp[i] = list[i];
                    list = new SV[n + 1];
                    list[0] = s;
                    for (int i = 1; i < n + 1; i++) list[i] = temp[i - 1];
                    n++;
                }
                else
                {
                    SV[] temp = new SV[n];
                    for (int i = 0; i < n; i++) temp[i] = list[i];
                    list = new SV[n + 1];
                    for (int i = 0; i < index; i++) list[i] = temp[i];
                    list[index] = s;
                    for (int i = index; i < n; i++) list[i + 1] = temp[i];
                    n++;
                }
            }
        }
        public int IndexOf(SV s)
        {
            int index = -1;
            for (int i = 0; i < n; i++)
            {
                if (s.MSV == list[i].MSV && s.NameSV == list[i].NameSV && s.DTB == list[i].DTB)
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
                SV[] temp = new SV[n-1];
                if (index == n-1)
                {
                    for (i = 0; i < n-1; i++) temp[i] = list[i];
                    list = new SV[n-1];
                    for (i = 0; i < n-1; i++) list[i] = temp[i];
                }
                else 
                {
                    for (i = 0; i < index; i++) temp[i] = list[i];
                    for (i = index + 1; i < n; i++) temp[i - 1] = list[i];
                    list = new SV[n-1];
                    for (i = 0; i < n-1; i++) list[i] = temp[i];
                }
                n--;
            }
        }
        public void Remove(SV s)
        {
            int index = IndexOf(s);
            if (index == -1)
                Console.WriteLine("Khong tim thay sinh vien can xoa.");
            else
                RemoveAt(index);
        }
        public void Edit(int _MSV)
        {
            for (int i = 0; i < n; i++)
            {
                if (list[i].MSV == _MSV)
                {
                    Console.Write("Nhap MSV: ");
                    list[i].MSV = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Nhap ten SV: ");
                    list[i].NameSV = Console.ReadLine();
                    Console.Write("Nhap DTB: ");
                    list[i].DTB = Convert.ToDouble(Console.ReadLine());
                    return;
                }
            }
            Console.WriteLine("Khong tim thay ma sinh vien!");
        }
        public void Sort()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i+1; j < n; j++)
                {
                    if (string.Compare(list[i].NameSV, list[j].NameSV) > 0)
                    {
                        SV temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
        public override string ToString()
        {
            string r = "Si so: " + n + "\n";
            int j = 0;
            foreach (SV i in list)
            {
                r += j++ + ": " + i.ToString() + "\n";
            }
            return r;
        }
    }
}