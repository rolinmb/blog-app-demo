using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Posts
{
    public class EditModel : PageModel
    {
        public Post curPost = new Post();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;User Id=sa;Password=22721937;TrustServerCertificate=true;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "SELECT * FROM Posts WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                curPost.id = "" + reader.GetInt32(0);
                                curPost.creatorName = reader.GetString(1);
                                curPost.postedTime = reader.GetDateTime(2).ToString();
                                curPost.textContent = reader.GetString(3);
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
        }
        public void OnPost()
        {
            curPost.textContent = Request.Form["text content"];

            if (curPost.id.Length == 0 || curPost.textContent.Length == 0)
            {
                errorMsg = "All fields are required";
                return;
            }

            try {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;User Id=sa;Password=22721937;TrustServerCertificate=true;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "UPDATE Posts "+
                        "SET TextContent=@textcontent" +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@textcontent", curPost.textContent);
                        command.Parameters.AddWithValue("@id", curPost.id);
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                errorMsg += ex.Message;
                return;
            }

            successMsg = "Post Updated Successfully";
            Response.Redirect("/Posts/Index");
        }
    }
}
