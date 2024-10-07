using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

public partial class Main_Code_User_ManagerPage : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["Life_ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillAllUserList();
        }
    }

    private void FillAllUserList()
    {
        SeleteUser.Items.Clear();

        string selectSQL = "SELECT [UserName], [Password] ";
        selectSQL += "FROM [Life].[dbo].[User] ";

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
                item.Text = rd["UserName"].ToString();
                item.Value = rd["Password"].ToString();
                SeleteUser.Items.Add(item);
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

    protected void SeleteUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblStatus.Text = "";

        string selectSQL = "SELECT *";
        selectSQL += "FROM [dbo].[Quest] ";
        selectSQL += "INNER JOIN[dbo].[User] ";
        selectSQL += "ON [dbo].[Quest].[Name] = [dbo].[User].[Quest] ";
        selectSQL += "WHERE [UserName] = @username ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@username", SeleteUser.SelectedValue);

        SqlDataReader rd;

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();
            rd.Read();

            UserQuest.Text = rd["Name"].ToString();
            QuestState.Text = rd["State"].ToString();

            UserID.Text = rd["UserName"].ToString();
            Password.Text = rd["Password"].ToString();
            ConfirmPassword.Text = rd["Password"].ToString();
            Email.Text = rd["Email"].ToString();
            NickName.Text = rd["NickName"].ToString();
            Question.Text = rd["Question"].ToString();
            Answer.Text = rd["Answer"].ToString();

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

    protected void ChangeAccount_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid == true)
        {
            string updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Password] = @userpw, [Email] = @usermail, [NickName] = @usernick, [Question] = @userquestion, [Answer] = @useranswer ";
            updateSQL += "WHERE [UserName] = @userID ";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateSQL, conn);

            cmd.Parameters.AddWithValue("@userpw", Password.Text);
            cmd.Parameters.AddWithValue("@usermail", Email.Text);
            cmd.Parameters.AddWithValue("@usernick", NickName.Text);
            cmd.Parameters.AddWithValue("@userquestion", Question.Text);
            cmd.Parameters.AddWithValue("@useranswer", Answer.Text);
            cmd.Parameters.AddWithValue("@userID", UserID.Text);

            int updated = 0;

            try
            {
                conn.Open();
                updated = cmd.ExecuteNonQuery();
                lblStatus.Text = "계정 정보 변경 완료";
            }
            catch (Exception error)
            {
                lblStatus.Text = "계정 정보 변경 실패<br />";
                lblStatus.Text += error.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            lblStatus.Text = "계정 정보 변경 실패<br />";
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void DeleteAccount_Click(object sender, EventArgs e)
    {
        string deleteSQL = "DELETE FROM [Life].[dbo].[User] ";
        deleteSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(deleteSQL, conn);

        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        int deleted = 0;

        try
        {
            conn.Open();
            deleted = cmd.ExecuteNonQuery();
            lblStatus.Text = "계정 정보 삭제 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "계정 정보 삭제 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void UpgradeAccount_Click(object sender, EventArgs e)
    {
        string updateSQL = "UPDATE [Life].[dbo].[User] ";
        updateSQL += "SET [Manager] = '1' ";
        updateSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        int updated = 0;

        try
        {
            conn.Open();
            updated = cmd.ExecuteNonQuery();
            lblStatus.Text = "계정 정보 변경 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "계정 정보 변경 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void DowngradeAccount_Click(object sender, EventArgs e)
    {
        string updateSQL = "UPDATE [Life].[dbo].[User] ";
        updateSQL += "SET [Manager] = '0' ";
        updateSQL += "WHERE [UserName] = @userID ";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(updateSQL, conn);

        cmd.Parameters.AddWithValue("@userID", UserID.Text);

        int updated = 0;

        try
        {
            conn.Open();
            updated = cmd.ExecuteNonQuery();
            lblStatus.Text = "계정 정보 변경 완료";
        }
        catch (Exception error)
        {
            lblStatus.Text = "계정 정보 변경 실패<br />";
            lblStatus.Text += error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }

    protected void CompleteQuest_Click(object sender, EventArgs e)
    {
        string selectSQL = "SELECT * ";
        selectSQL += "FROM [Life].[dbo].[Quest] ";
        selectSQL += "WHERE [Name] = @questname";

        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, conn);

        cmd.Parameters.AddWithValue("@questname", UserQuest.Text);

        SqlDataReader rd;

        string questexp = "";

        try
        {
            conn.Open();
            rd = cmd.ExecuteReader();

            rd.Read();

            questexp = rd["Exp"].ToString();

            rd.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = error.Message;
        }
        finally
        {
            conn.Close();
        }

        string selectSQL2 = "SELECT * ";
        selectSQL2 += "FROM [Life].[dbo].[User] ";
        selectSQL2 += "WHERE [UserName] = @questname";

        SqlCommand cmd2 = new SqlCommand(selectSQL2, conn);

        cmd2.Parameters.AddWithValue("@questname", SeleteUser.Text);

        SqlDataReader rd2;

        string userlv = "";
        string userexp = "";

        try
        {
            conn.Open();
            rd2 = cmd2.ExecuteReader();

            rd2.Read();

            userlv = rd2["Lv"].ToString();
            userexp = rd2["Exp"].ToString();

            rd2.Close();
        }
        catch (Exception error)
        {
            lblStatus.Text = error.Message;
        }
        finally
        {
            conn.Close();
        }

        int questexp_i = 0;
        int userexp_i = 0;
        int userlv_i = 0;

        try
        {
            questexp_i = Int32.Parse(questexp);
            userexp_i = Int32.Parse(userexp);
            userlv_i = Int32.Parse(userlv);
        }
        catch(FormatException)
        {

        }

        int result = userlv_i * userlv_i * 100 + 100;
        int resultexp = userexp_i + questexp_i;

        string updateSQL;

        SqlCommand cmd3;

        int updated = 0;

        if (resultexp > result)
        {
            userlv_i = userlv_i + 1;

            updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Lv] = @userlv , [Exp] = 0 , [Quest] = NULL ";
            updateSQL += "WHERE [UserName] = @userID ";

            cmd3 = new SqlCommand(updateSQL, conn);

            cmd3.Parameters.AddWithValue("@userlv", userlv_i);
            cmd3.Parameters.AddWithValue("@userID", SeleteUser.SelectedValue);

            try
            {
                conn.Open();
                updated = cmd3.ExecuteNonQuery();
                lblStatus.Text = "계정 정보 변경 완료";
            }
            catch (Exception error)
            {
                lblStatus.Text = error.Message;
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            updateSQL = "UPDATE [Life].[dbo].[User] ";
            updateSQL += "SET [Exp] = @userexp , [Quest] = NULL ";
            updateSQL += "WHERE [UserName] = @userID ";

            cmd3 = new SqlCommand(updateSQL, conn);

            cmd3.Parameters.AddWithValue("@userexp", resultexp);
            cmd3.Parameters.AddWithValue("@userID", SeleteUser.SelectedValue);

            try
            {
                conn.Open();
                updated = cmd3.ExecuteNonQuery();
                lblStatus.Text = "계정 정보 변경 완료";
            }
            catch (Exception error)
            {
                lblStatus.Text = error.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        string deleteSQL = "DELETE FROM [Life].[dbo].[Quest] ";
        deleteSQL += "WHERE [Name] = @questID ";

        SqlCommand cmd4 = new SqlCommand(deleteSQL, conn);

        cmd4.Parameters.AddWithValue("@questID", UserQuest.Text);

        int deleted = 0;

        try
        {
            conn.Open();
            deleted = cmd4.ExecuteNonQuery();
        }
        catch (Exception error)
        {
            lblStatus.Text = error.Message;
        }
        finally
        {
            conn.Close();
        }

        Response.Redirect(Request.RawUrl);
    }
}