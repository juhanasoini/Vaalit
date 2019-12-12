using System.Collections.Generic;

namespace Vaalit
{
    class Puolue
    {
        public string PartyName { get; set; }
        public int TotalVouteCount { get; set; }

        public List<Ehdokas> ehdokkaat = new List<Ehdokas>();

        public Puolue(string name, int votes = 0)
        {
            PartyName = name;
            TotalVouteCount = votes;
        }

        public override string ToString()
        {
            return PartyName + " " + TotalVouteCount;
        }

        public int CompareTo(Puolue other)
        {
            if (other == null) return 1;
            if (this.TotalVouteCount > other.TotalVouteCount)
                return -1;
            else if (this.TotalVouteCount < other.TotalVouteCount)
                return 1;

            return 0;
        }

        //Järjestää ehdokaslistan äänimäärän mukaan laskevasti
        public List<Ehdokas> orderByVoteCount()
        {
            var comparer = new SortVoteCountDescendingHelper();
            var temp = ehdokkaat;
            temp.Sort(comparer);

            return temp;
        }

        //Jakaa vertailuluvut puolueen äänimäärän mukaan
        public List<Ehdokas> jaaVertailuluvut()
        {
            var temp = orderByVoteCount();

            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].VertailuLuku = TotalVouteCount / (i + 1);

            }
            return null;
        }
    }
}
