using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<User> listUsers = new List<User>();

        public void OnGet()
        {
            try
            {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User u = new User();
                                u.id = "" + reader.GetInt32(0);
                                u.creatorName = reader.GetString(1);
                                u.password = reader.GetString(2);
                                u.fullName = reader.GetString(3);
                                u.dateJoined = reader.GetDateTime(4).ToString();
                                listUsers.Add(u);
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

    public class User
    {
        public String id;
        public String creatorName;
        public String password;
        public String fullName;
        public String dateJoined;
    }
}
