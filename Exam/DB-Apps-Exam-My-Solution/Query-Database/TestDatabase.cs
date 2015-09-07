using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_Code_First;
using System.Web.Script.Serialization;
using System.IO;

namespace Query_Database
{
    class TestDatabase
    {
        static void Main()
        {
            var context = new MoviesContext();
            Console.WriteLine(context.Movies.Count());

            //1.Adult Movies
            var adultMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Ratings.Count)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.Ratings.Count
                })
                .ToList();

            var jsonAdultMovies = new JavaScriptSerializer().Serialize(adultMovies);
            File.WriteAllText("../../adult-movies.json", jsonAdultMovies);

            //2.Rated Movies by User
            var ratedMoviesUser = context.Users
                .Where(u => u.UserName == "jmeyery")
                .Select(u => new
                {
                    username = u.UserName,
                    ratedMovies = u.Ratings
                        .OrderBy(r => r.Movie.Title)
                        .Select(r => new
                        {
                            title = r.Movie.Title,
                            userRating = r.Stars,
                            averageRating = r.Movie.Ratings.Average(s => s.Stars)
                        })
                })
                .ToList();

            string ratedMoviesUserJson = new JavaScriptSerializer().Serialize(ratedMoviesUser);
            File.WriteAllText("../../rated-movies-by-jmeyery.json", ratedMoviesUserJson);

            //3.Top 10 Favourite Movies
            var topMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Teen)
                .OrderByDescending(m => m.Users.Count)
                .ThenBy(m => m.Title)
                .Select(m => new
                {
                    isbn = m.Isbn,
                    title = m.Title,
                    favouritedBy = m.Users
                        .Select(u => u.UserName)
                })
                .Take(10)
                .ToList();

            string topFavouriteMoviesJson = new JavaScriptSerializer().Serialize(topMovies);
            File.WriteAllText("../../top-10-favourite-movies.json", topFavouriteMoviesJson);
        }
    }
}
