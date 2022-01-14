using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryLab5
{
    /// <summary>
	/// Name: Hy Vo
	/// Project: Lab 5 Library
	/// </summary>
    /// 
    public class Movie
    {
        public int ID { get; }
        public string Title { get; set; }
        public string YearReleased { get; set; }
        public int ReviewStars { get; set; }

        public Movie(int id)
        {
            ID = id;
        }

        public static List<Movie> AllRecords(out string output)
        {
            List<Movie> movies = new List<Movie>();
            output = "No movie found";

            SqlCommand command = new SqlCommand();
            command.Connection = Data.connect;
            command.CommandText = "Select * from Movie";

            try
            {
                Data.connect.Open();
                SqlDataReader movieList = command.ExecuteReader();
                while (movieList.Read())
                {
                    Movie movie = new Movie(Convert.ToInt32(movieList[0]));
                    movie.Title = movieList[1].ToString();
                    movies.Add(movie);
                }
                output = "Found movies";
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            finally
            {
                if(Data.connect.State == ConnectionState.Open)
                {
                    Data.connect.Close();
                }
            }

            return movies;
        }

        public static List<Movie> SpecificRecord(int number, out string output)
        {
            List<Movie> movies = new List<Movie>();
            output = "No data";

            SqlCommand command = new SqlCommand();
            command.Connection = Data.connect;
            command.CommandText = "Select * from Movie Where ID = " + number.ToString();

            try
            {
                Data.connect.Open();
                SqlDataReader movieList = command.ExecuteReader();
                while (movieList.Read())
                {
                    Movie movie = new Movie(Convert.ToInt32(movieList[0]));
                    movie.Title = movieList[1].ToString();
                    movie.YearReleased = movieList[2].ToString();
                    movie.ReviewStars = Convert.ToInt32(movieList[3]);
                    movies.Add(movie);
                }
                output = "Found information";
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            finally
            {
                if (Data.connect.State == ConnectionState.Open)
                {
                    Data.connect.Close();
                }
            }

            return movies;
        }
    }

    static internal class Data
    {
        internal static SqlConnection connect = new SqlConnection();
        static Data()
        {
            connect.ConnectionString = global::LibraryLab5.Properties.Settings.Default.MoviesConnectionString;
        }
    }
}
