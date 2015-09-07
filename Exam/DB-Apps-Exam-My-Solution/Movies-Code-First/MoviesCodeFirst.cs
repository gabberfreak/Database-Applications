using System;
using System.Linq;
using System.Data.Entity;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Movies_Code_First.Migrations;
using Newtonsoft.Json;


namespace Movies_Code_First
{
    class MoviesCodeFirst
    {
        static void Main()
        {

            var context = new MoviesContext();

            Console.WriteLine(context.Countries.Count());

            var countriesJson = File.ReadAllText(@"..\..\countries.json");
            var countries = JArray.Parse(countriesJson);
            foreach (var country in countries)
            {
                ImportCountries(country);
                Console.WriteLine("Country {0} imported", country["name"]);
            }

            var usersJson = File.ReadAllText(@"..\..\users.json");
            var users = JArray.Parse(usersJson);
            foreach (var user in users)
            {
                ImportUsers(user);
                Console.WriteLine("User {0} imported", user["username"]);
            }

            var moviesJson = File.ReadAllText(@"..\..\movies.json");
            var movies = JArray.Parse(moviesJson);
            foreach (var movie in movies)
            {
                ImportMovies(movie);
                Console.WriteLine("Movie {0} imported", movie["title"]);
            }

            var favouriteMoviesJson = File.ReadAllText(@"..\..\users-and-favourite-movies.json");
            var favouriteMovies = JArray.Parse(favouriteMoviesJson);
            foreach (var favouriteMovie in favouriteMovies)
            {
                ImportFavouriteMovies(favouriteMovie);
                Console.WriteLine();
            }

            var movieRatingsJson = File.ReadAllText(@"..\..\movie-ratings.json");
            var movieRatings = JArray.Parse(movieRatingsJson);
            foreach (var element in movieRatings)
            {
                ImportMovieRatings(element);
                Console.WriteLine("Imported data to database");
            }
           
        }

        private static void ImportMovieRatings(JToken elementObj)
        {
            var context = new MoviesContext();

            string userName = elementObj["user"].Value<string>();
            string movie = elementObj["movie"].Value<string>();
            int stars = int.Parse(elementObj["rating"].ToString());

            context.Ratings.Add(new Rating()
            {
                Stars = stars,
                MovieId = context.Movies.FirstOrDefault(m => m.Isbn == movie).Id,
                UserId = context.Users.FirstOrDefault(u => u.UserName == userName).Id
            });

            context.SaveChanges();
        }

        private static void ImportFavouriteMovies(JToken favouriteMovieObj)
        {
            var context = new MoviesContext();

            string userName = favouriteMovieObj["username"].Value<string>();
            var dbUser = context.Users.FirstOrDefault(u => u.UserName == userName);

            Console.WriteLine("User {0} movies imported: ", favouriteMovieObj["username"].Value<string>());
            var moviesIsbn = JArray.Parse(favouriteMovieObj["favouriteMovies"].ToString());

            foreach (var movie in moviesIsbn)
            {
                string currMovie = movie.ToString();
                Movie newMovie = context.Movies.FirstOrDefault(m => m.Isbn == currMovie);
                dbUser.Movies.Add(newMovie);
                Console.WriteLine(newMovie.Title);
            }

            context.SaveChanges();
        }

        private static void ImportMovies(JToken movieObj)
        {
            var context = new MoviesContext();
            var movie = new Movie();

            movie.Isbn = movieObj["isbn"].Value<string>();
            movie.Title = movieObj["title"].Value<string>();
            movie.AgeRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction),
                movieObj["ageRestriction"].ToString());
            context.Movies.Add(movie);
            context.SaveChanges();
        }

        private static void ImportUsers(JToken userObj)
        {
            var context = new MoviesContext();
            var user = new User();

            user.UserName = userObj["username"].Value<string>();

            if (userObj["age"].Type != JTokenType.Null)
            {
                user.Age = userObj["age"].Value<int>();
            }

            if (userObj["email"].Type != JTokenType.Null)
            {
                user.Email = userObj["email"].Value<string>();
            }

            if (userObj["country"].Type != JTokenType.Null)
            {
                var countryName = userObj["country"].Value<string>();
                user.Country = context.Countries.FirstOrDefault(c => c.Name == countryName);
            }

            context.Users.Add(user);
            context.SaveChanges();
        }

        private static void ImportCountries(JToken countryObj)
        {
            var context = new MoviesContext();
            var country = new Country();

            country.Name = countryObj["name"].Value<string>();

            context.Countries.Add(country);
            context.SaveChanges();
        }
    }
}
