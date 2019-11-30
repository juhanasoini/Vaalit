using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vaalit
{
    class Program
    {
        static void tallennaEhdokkaat(List<Ehdokas> ehdokkaat)
        {
            JsonSerializerSettings loJsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
            String json = JsonConvert.SerializeObject(ehdokkaat, loJsonSerializerSettings);
            File.WriteAllText("ehdokkaat.json", json);
        }

        static void Main(string[] args)
        {
            MysqlConn mysql = new MysqlConn();
            //mysql.CreateTable();
            //Luodaan tyhjä lista ehdokkaista
            List<Ehdokas> ehdokkaat = new List<Ehdokas>();
            //Ladataan tiedosto jossa ehdokkaat
            StreamReader sr = new StreamReader(@"ehdokkaat.txt", Encoding.UTF8);
            //Luodaan tyhjä lista puolueista
            Dictionary<string, Puolue> puolueet = new Dictionary<string, Puolue>();

            // erotimerkki jolla splitataan rivi
            string sep = "\t";

            Ehdokas tempEhd;

            //Käydään tiedosto läpi rivi kerrallaan
            while (sr.Peek() >= 0)
            {
                string str;
                string[] strArr;

                str = sr.ReadLine();
                strArr = str.Split(sep.ToCharArray());

                if( puolueet.ContainsKey(strArr[2]) == false )
                {
                    //Luodaan puolue ja lisätään se puoluelistaan
                    Puolue puolue = new Puolue(strArr[2]);
                    puolueet.Add(strArr[2], puolue);
                }

                //Luodaan ehdokas
                Ehdokas ehdokas = new Ehdokas(strArr[0], strArr[1], strArr[2], int.Parse(strArr[3]));

                //Kasvatetaan puolueen kokonaisäänimäärää
                puolueet[strArr[2]].TotalVouteCount += int.Parse(strArr[3]);
                //Lisätään ehdokas puolueen listalle
                puolueet[strArr[2]].ehdokkaat.Add(ehdokas);

                //Lisätään ehdokas paikalliseen ehdokaslistaan
                ehdokkaat.Add(ehdokas);
                tempEhd = ehdokas;
                //ehdokas.Upsert(mysql);
            }
            sr.Close();

            //Generoidaan ehdokkaille vertailuluvut
            foreach (KeyValuePair<string, Puolue> item in puolueet.OrderByDescending(x => x.Value.TotalVouteCount))
            {
                item.Value.jaaVertailuluvut();
            }

            //Järjestetään ehdokkaat vertailuluvun mukaan laskevaanjärjestykseen
            var comparer = new SortVertailulukuDescendingHelper();
            ehdokkaat.Sort(comparer);

            //Napataan läpi päässeet ehdokkaat
            List<Ehdokas> valtuusto = ehdokkaat.GetRange(0, 51);

            //Järjestää ehdokkaat puolueittain + äänimäärän ja nimen mukaan
            valtuusto.Sort();

            //Tulostetaan valtuusto
            for (int i = 0; i < valtuusto.Count; i++)
            {
                //valtuusto[i].Upsert(mysql);
                Console.WriteLine("{0}: {1}", (i + 1), valtuusto[i].ToString());
            }

            //Tallennetaan valtuusto JSON tiedostoon
            tallennaEhdokkaat(valtuusto);

            Console.ReadLine();
        }
    }
}
