using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Users
{
    public class CreateModel : PageModel
    {
        public User newUser = new User();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            newUser.id = null;
            newUser.creatorName = Request.Form["creator name"];
            newUser.password = Request.Form["password"];
            newUser.fullName = Request.Form["full name"];
            newUser.dateJoined = null;

            if (newUser.creatorName.Length == 0 || newUser.password.Length == 0
                || newUser.fullName.Length == 0)
            {
                errorMsg = "All fields are required.";
                return;
            }

            try {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;User Id=sa;Password=22721937;TrustServerCertificate=true;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "INSERT INTO Users " +
                        "(CreatorName, Password, FullName) VALUES" +
                        "(@creatorname, @password, @fullname)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@creatorname", newUser.creatorName);
                        command.Parameters.AddWithValue("@password", newUser.password);
                        command.Parameters.AddWithValue("@fullname", newUser.fullName);
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                errorMsg = ex.Message;
                return;
            }

            newUser.creatorName = "";
            newUser.password = "";
            newUser.fullName = "";
            successMsg = "New User Added Successfully";
            Response.Redirect("/Users/Index");
        }
    }
}
