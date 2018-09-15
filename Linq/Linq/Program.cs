using System;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var jsonFile = @"C:\Repo\CSharp-Practice\Linq\API_response.json";
            var movieDatas = JsonConvert.DeserializeObject<List<MovieData>>(File.ReadAllText(jsonFile));



            var top3 = (from moviedata in movieDatas
                        from data in moviedata.data
                        where data.days_to_release == -49
                        orderby data.views descending
                        select new { moviedata.film, data.views }).Take(3);

            var bottom3 = (from moviedata in movieDatas
                           from data in moviedata.data
                           where data.days_to_release == -49
                           orderby data.views
                           select new { moviedata.film, data.views }).Take(3);



            var threeDaysAfterReleaseDate_Top3 = (from movieData in movieDatas
                                                  from data in movieData.data
                                                  where data.post_date == movieData.releaseDate.AddDays(3)
                                                  orderby data.views descending
                                                  select new { movieData.film, data.views }).Take(3);

            var threeDaysAfterReleaseDate_Bottom3 = (from movieData in movieDatas
                                                     from data in movieData.data
                                                     where data.post_date == movieData.releaseDate.AddDays(3)
                                                     orderby data.views
                                                     select new { movieData.film, data.views }).Take(3);


            Debug.WriteLine("Top 3 movies at 7 weeks to release");
            foreach (var movie in top3)
            {
                Debug.WriteLine($"\t{movie.film} \t {movie.views}");
            }

            Debug.WriteLine("\nBottom 3 movies at 7 weeks to release");
            foreach (var movie in bottom3)
            {
                Debug.WriteLine($"\t{movie.film} \t {movie.views}");
            }

            Debug.WriteLine("\nTop 3 movies at 3 days after release");
            foreach (var movie in threeDaysAfterReleaseDate_Top3)
            {
                Debug.WriteLine($"\t{movie.film} \t {movie.views}");
            }

            Debug.WriteLine("\nBottom 3 movies at 3 days after release");
            foreach (var movie in threeDaysAfterReleaseDate_Bottom3)
            {
                Debug.WriteLine($"\t{movie.film} \t {movie.views}");
            }


        }
    }

    public class MovieData
    {
        public int total_raw_views { get; set; }
        public string country { get; set; }
        public int total_views { get; set; }
        public string film { get; set; }
        public List<data> data { get; set; }
        public string label { get; set; }
        public DateTime releaseDate
        {
            get
            {
                var splitLabel = label.Split(null);
                return DateTime.Parse(splitLabel[splitLabel.Length - 1]);
            }
        }
    }

    public class data
    {
        public int raw_views { get; set; }
        public int views { get; set; }
        public DateTime post_date { get; set; }
        public int days_to_release { get; set; }
    }
}

