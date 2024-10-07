<%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/User_MasterPage.master" AutoEventWireup="true" CodeFile="Ranking_UserPage.aspx.cs" Inherits="Main_Code_Ranking_UserPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LifeConnectionString %>" SelectCommand="SELECT NickName, Lv, Exp, Quest, UserName 
FROM [dbo].[User] 
WHERE [Manager] = 'false'
ORDER BY [Lv] DESC, [Exp] DESC"></asp:SqlDataSource>
        <br />
        사용자 랭킹<br /><br />
        <asp:BulletedList ID="RankingList" runat="server" DataSourceID="SqlDataSource1" DataTextField="NickName" DataValueField="UserName" BulletStyle="Numbered"></asp:BulletedList>
    </div>
    <div>
        커뮤니티<br /><br />
        <asp:TextBox ID="Forum" runat="server" Height="58px" Width="509px"></asp:TextBox>
        <br /><asp:Button ID="Send" runat="server" Text="게시글 작성" Width="513px" OnClick="Send_Click" />
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LifeConnectionString3 %>" SelectCommand="SELECT [Nickname],[Forum] FROM [Forum]"></asp:SqlDataSource>
        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        <asp:DetailsView ID="DetailsView" runat="server" Height="90px" Width="700px" DataSourceID="SqlDataSource2" AutoGenerateRows="False" AllowPaging="True">
            <Fields>
                <asp:BoundField DataField="Nickname" HeaderText="Nickname" SortExpression="Nickname" />
                <asp:BoundField DataField="Forum" HeaderText="Forum" SortExpression="Forum" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>

