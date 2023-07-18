using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Users
{
    public class EditModel : PageModel
    {
        public User curUser = new User();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;User Id=sa;Password=1234;TrustServerCertificate=true;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                curUser.id = "" + reader.GetInt32(0);
                                curUser.creatorName = reader.GetString(1);
                                curUser.password = reader.GetString(2);
                                curUser.fullName = reader.GetString(3);
                                curUser.dateJoined = reader.GetDateTime(4).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }
        public void OnPost()
        {
            curUser.creatorName = Request.Form["creator name"];
            curUser.password = Request.Form["password"];
            curUser.fullName = Request.Form["full name"];

            if (curUser.id.Length == 0 || curUser.creatorName.Length == 0 || curUser.password.Length == 0
                || curUser.fullName.Length == 0)
            {
                errorMsg = "All fields are required";
                return;
            }

            try
            {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "UPDATE Users " +
                        "SET CreatorName=@creatorname Password=@password FullName=@fullname" +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@creatorname", curUser.creatorName);
                        command.Parameters.AddWithValue("@password", curUser.password);
                        command.Parameters.AddWithValue("@fullname", curUser.fullName);
                        command.Parameters.AddWithValue("@id", curUser.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message;
                return;
            }

            successMsg = "User Updated Successfully";
            Response.Redirect("/Users/Index");
        }    
    }
}
