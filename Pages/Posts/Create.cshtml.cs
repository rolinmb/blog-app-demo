using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogApp.Pages.Posts
{
    public class CreateModel : PageModel
    {
        public Post newPost = new Post();
        public String errorMsg = "";
        public String successMsg = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            newPost.id = null;
            newPost.creatorName = Request.Form["creator name"];
            newPost.postedTime = null;
            newPost.textContent = Request.Form["text content"];

            if (newPost.creatorName.Length == 0 || newPost.textContent.Length == 0)
            {
                errorMsg = "All fields are required.";
                return;
            }

            try {
                String dbConString = "Data Source=RMB_VICTUS;Initial Catalog=BlogDb;User Id=sa;Password=1234;TrustServerCertificate=true;MultipleActiveResultSets=true";
                using (SqlConnection connection = new SqlConnection(dbConString))
                {
                    connection.Open();
                    String query = "INSERT INTO Posts " +
                        "(CreatorName, TextContent) VALUES" +
                        "(@creatorname, @textcontent)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@creatorname", newPost.creatorName);
                        command.Parameters.AddWithValue("@textcontent", newPost.textContent);
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                errorMsg = ex.Message;
                return;
            }

            newPost.creatorName = "";
            newPost.textContent = "";
            successMsg = "New Post Created Successfully";
            Response.Redirect("/Posts/Index");
        }
    }
}
