using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibraryLab5;

namespace Lab5_HyVo
{
    /// <summary>
	/// Name: Hy Vo
	/// Project: Lab 5 App
	/// </summary>
    public partial class Main : System.Web.UI.Page
    {
        private static string output = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Movie> movies = Movie.AllRecords(out output);
            this.ddlImport.Items.Add("Select movies...");
            foreach (Movie m in movies)
            {
                this.ddlImport.Items.Add(m.Title);
            }
        }

        protected void ddlImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Movie> movieInfo = Movie.SpecificRecord(ddlImport.SelectedIndex, out output);
            foreach(Movie m in movieInfo)
            {
                lblMovie.Text = m.Title;
                lblYear.Text = m.YearReleased;
                lblScore.Text = m.ReviewStars.ToString();
            }
        }
    }
}