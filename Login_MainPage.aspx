    <%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/Main_MasterPage.master" AutoEventWireup="true" CodeFile="Login_MainPage.aspx.cs" Inherits="Main_Code_Login_MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    아이디 :
                </td>
                <td>
                    <asp:TextBox ID="UserID" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserIDRequired" runat="server" ErrorMessage="아이디를 입력해주세요." ToolTip="아이디를 입력해주세요." ControlToValidate="UserID" ValidationGroup="Login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    비밀번호 :
                </td>
                <td>
                    <asp:TextBox ID="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="비밀번호를 입력해주세요." ToolTip="비밀번호를 입력해주세요" ControlToValidate="Password" ValidationGroup="Login">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div><br />
    <div>
        <asp:Button ID="Login" runat="server" Text="로그인" Height="45px" Width="100px" OnClick="Login_Click"/> 
    </div><br />
    <div">
        <asp:Label ID="Error" runat="server" Text=""></asp:Label>
    </div>
    </asp:Content>