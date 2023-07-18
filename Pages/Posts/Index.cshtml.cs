using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        public List<Post> listPosts = new List<Post>();

        public void OnGet()
        {
            try
            {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(dbConString)) 
                {
                    connection.Open();
                    String query = "SELECT * FROM Posts";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Post p = new Post();
                                p.id = "" + reader.GetInt32(0);
                                p.creatorName = reader.GetString(1);
                                p.postedTime = reader.GetDateTime(2).ToString();
                                p.textContent = reader.GetString(3);
                                listPosts.Add(p);
                            }
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class Post
    {
        public String id;
        public String creatorName;
        public String postedTime;
        public String textContent;
    }
}