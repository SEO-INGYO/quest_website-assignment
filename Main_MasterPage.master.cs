using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main_Master_Login_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DateTime.Now.Ticks.ToString();
    }

    protected void CreateAccountButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/CreateAccount_MainPage.aspx");
    }

    protected void LoginAccountButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/Login_MainPage.aspx");
    }

    protected void SearchAccountButton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Main/Code/SearchAccount_MainPage.aspx");
    }
}
