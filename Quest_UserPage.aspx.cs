using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class Main_Code_Quest_UserPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillAllQuestList();
        }

        HttpCookie cookie = Request.Cookies["Login_MainPage"];
        Label UserID = (Label)Master.FindControl("UserID");
        UserID.Text = cookie["UserID"];

        lblStatus.Text = "";

        string selectSQL = "SELECT *";
        selectSQL += "FROM [dbo].[Quest] ";
        selectSQL += "INNER JOIN[dbo].[User] ";
        selectSQL += "ON [dbo].[Quest].[Name] = [dbo].[User].[Quest] ";
        selectSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();
            rd.Read();

            QuestProgress.Text = rd["Name"].ToString();

            ProgressQuestName.Text = rd["Name"].ToString();
            ProgressQuestDescription.Text = rd["Description"].ToString();
            ProgressQuestLv.Text = rd["Lv"].ToString();
            ProgressQuestExp.Text = rd["Exp"].ToString();

            rd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = "데이터베이스 읽기 오류";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    private void FillAllQuestList()
    {
        QuestList.Items.Clear();

        string selectSQL = "SELECT [Name], [Description] ";
        selectSQL += "FROM [dbo].[Quest] ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);
        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ListItem item = new ListItem();
                item.Text = rd["Name"].ToString();
                item.Value = rd["Description"].ToString();
                QuestList.Items.Add(item);
            }

            rd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = "데이터베이스 읽기 오류<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void QuestAbandon_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        HttpCookie cookie = Request.Cookies["Login_MainPage"];
        Label UserID = (Label)Master.FindControl("UserID");
        UserID.Text = cookie["UserID"]; ;

        string updateSQL = "UPDATE [Life].[dbo].[User] ";
        updateSQL += "SET [Quest] = '없음' ";
        updateSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        int updated = 0;

        try
        {
            conn.Open();

            updated = cmd.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 포기 성공";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 포기 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        string updateSQL2 = "UPDATE [Life].[dbo].[Quest] ";
        updateSQL2 += "SET [State] = '없음' ";
        updateSQL2 += "WHERE [Name] = @questname ";

        SqlCommand cmd2 = new SqlCommand(updateSQL2, conn);

        cmd2.Parameters.AddWithValue("@questname", QuestProgress.Text);

        int updated2 = 0;

        try
        {
            conn.Open();

            updated2 = cmd.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 포기 성공";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 포기 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void QuestComplete_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        string updateSQL = "UPDATE [Life].[dbo].[Quest] ";
        updateSQL += "SET [State] = '완료 대기' ";
        updateSQL += "WHERE [Name] = @questname ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@questname", QuestProgress.Text);

        int updated = 0;

        try
        {
            conn.Open();

            updated = cmd.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 완료 요청 성공";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 완료 요청 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void QuestList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        string selectSQL = "SELECT * ";
        selectSQL += "FROM [Life].[dbo].[Quest] ";
        selectSQL += "WHERE [Name] = @questname ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@questname", QuestList.SelectedValue);

        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();
            rd.Read();

            QuestName.Text = rd["Name"].ToString();
            QuestDescription.Text = rd["Description"].ToString();
            QuestLv.Text = rd["Lv"].ToString();
            QuestExp.Text = rd["Exp"].ToString();

            rd.Close();
        }
        catch(Exception error)
        {
            lblStatus.Text = "데이터베이스 읽기 오류";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    protected void QuestAccept_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["Login_MainPage"];
        Label UserID = (Label)Master.FindControl("UserID");
        UserID.Text = cookie["UserID"]; ;

        string updateSQL = "UPDATE [Life].[dbo].[User] ";
        updateSQL += "SET [Quest] = @quest ";
        updateSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@quest", QuestList.Text);
        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        int updated = 0;

        try
        {
            conn.Open();

            updated = cmd.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 수락 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 수락 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        string updateSQL2 = "UPDATE [Life].[dbo].[Quest] ";
        updateSQL2 += "SET [State] = '진행중' ";
        updateSQL2 += "WHERE [Name] = @questID ";

        SqlCommand cmd2 = new SqlCommand(updateSQL2, conn);

        cmd2.Parameters.AddWithValue("@questID", QuestList.Text);

        int updated2 = 0;

        try
        {
            conn.Open();

            updated2 = cmd2.ExecuteNonQuery();
            lblStatus.Text = "퀘스트 수락 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "퀘스트 수락 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }
}