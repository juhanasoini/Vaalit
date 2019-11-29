using System;
using System.Collections.Generic;

namespace Vaalit
{
    class Ehdokas : IComparable<Ehdokas>
    {
        private string _etunimi;
        private string _sukunimi;
        public string Etunimi
        {
            get
            {
                return _etunimi;
            }
            set
            {
                _etunimi = value;
            }
        }
        public string Sukunimi
        {
            get
            {
                return _sukunimi;
            }
            set
            {
                _sukunimi = value;
            }
        }
        public string Puolue { get; set; }
        public int Aanimaara { get; set; }
        public double VertailuLuku { get; set; }

        public Ehdokas(string fn, string ln, string party, int votes)
        {
            Etunimi = fn;
            Sukunimi = ln;
            Puolue = party;
            Aanimaara = votes;
        }

        /// <summary>
        /// Palauttaa henkilön nimen [Sukunimi Etunimi]
        /// </summary>
        public string fullName()
        {
            return Sukunimi + " " + Etunimi;
        }

        public override string ToString()
        {
            return Sukunimi + " " + Etunimi + " " + Puolue + " " + Aanimaara + " " + VertailuLuku;
        }

        /// <summary>
        /// Vertailee eghdokkaita. Järjestää: Puolue->Äänimäärä->Koko nimi
        /// </summary>
        /// <param name="other"></param>
        public int CompareTo(Ehdokas other)
        {

            int result = Puolue.CompareTo(other.Puolue);
            if (result == 0)
                result = other.Aanimaara.CompareTo(Aanimaara);
            if (result == 0)
                result = fullName().CompareTo(other.fullName());
            return result;
        }
    }
    /// <summary>
    /// Järjestää äänimäärän mukaan laskevaan järjetykseen
    /// </summary>
    public class SortVoteCountDescendingHelper : IComparer<Ehdokas>
    {
        int IComparer<Ehdokas>.Compare(Ehdokas x, Ehdokas y)
        {
            return y.Aanimaara.CompareTo(x.Aanimaara);
        }
    }
    /// <summary>
    /// Järjestää vertailuluvun mukaan laskevaan järjestykseen
    /// </summary>
    public class SortVertailulukuDescendingHelper : IComparer<Ehdokas>
    {
        int IComparer<Ehdokas>.Compare(Ehdokas x, Ehdokas y)
        {
            return y.VertailuLuku.CompareTo(x.VertailuLuku);
        }
    }
}
