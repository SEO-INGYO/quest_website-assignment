using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Master_User_MasterPage :  System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;

        HttpCookie cookie = Request.Cookies["Login_MainPage"];

        UserID.Text = cookie["UserID"];

        lblCounter0.Text = Application["counter"] + "명";
        lblCounter1.Text = Application["activecounter"] + "명";
    }

    protected void MyPage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/MyPage_UserPage.aspx");
    }

    protected void Quest_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/Quest_UserPage.aspx");
    }

    protected void Ranking_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/Ranking_UserPage.aspx");
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Response.Redirect("~/Main/Code/Login_MainPage.aspx");
    }
}
