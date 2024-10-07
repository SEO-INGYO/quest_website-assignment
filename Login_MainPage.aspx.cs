using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class Main_Code_Login_MainPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("Login_MainPage");

        cookie["UserID"] = UserID.Text;
        cookie.Expires = DateTime.Now.AddYears(1);

        Response.Cookies.Add(cookie);
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (IsAuthenticated(UserID.Text, Password.Text))
        {
            if(IsManager(UserID.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(UserID.Text, true);

                Response.Cookies["UserID"].Value = UserID.Text;
                Response.Cookies["UserID"].Expires = DateTime.Now.AddSeconds(10);

                Response.Redirect("~/Main/Code/Manager_ManagerPage.aspx");
            }

            else
            {
                FormsAuthentication.RedirectFromLoginPage(UserID.Text, true);

                Response.Cookies["UserID"].Value = UserID.Text;
                Response.Cookies["UserID"].Expires = DateTime.Now.AddSeconds(10);

                Response.Redirect("~/Main/Code/MyPage_UserPage.aspx");
            }
        }
        else
        {
            this.Error.Text = "로그인 정보를 다시 확인해주세요";
        }
    }

    bool IsAuthenticated(string UserID, string Password)
    {
        SqlConnection conn = new SqlConnection(connectionString);

        string selectSQL = "SELECT count(UserName) ";
        selectSQL += "FROM [Life].[dbo].[User] ";
        selectSQL += "WHERE [UserName] = @UserID ";
        selectSQL += "AND [Password] = @Password ";

        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@UserID", UserID);
        cmd.Parameters.AddWithValue("@Password", Password);

        conn.Open();
        int count = (int)cmd.ExecuteScalar();
        conn.Close();

        return count > 0;
    }

    bool IsManager(string UserID)
    {
        SqlConnection conn = new SqlConnection(connectionString);

        string selectSQL = "SELECT count(UserName) ";
        selectSQL += "FROM [Life].[dbo].[User] ";
        selectSQL += "WHERE [UserName] = @UserID ";
        selectSQL += "AND [Manager] = '1' ";

        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@UserID", UserID);

        conn.Open();
        int count = (int)cmd.ExecuteScalar();
        conn.Close();

        return count > 0;
    }
}