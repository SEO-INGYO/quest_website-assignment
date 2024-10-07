using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Master_Manager_MasterPage :  System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Login_MainPage"];

        ManagerID.Text = cookie["UserID"];
    }

    protected void QuestManager_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/Quest_ManagerPage.aspx");
    }

    protected void UserManager_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/User_ManagerPage.aspx");
    }

    protected void ManagerPage_Click(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/Main/Code/Manager_ManagerPage.aspx");
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Response.Redirect("~/Main/Code/Login_MainPage.aspx");
    }
}
