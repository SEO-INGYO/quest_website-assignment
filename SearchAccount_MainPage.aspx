<%@ Page Title="" Language="C#" MasterPageFile="~/Main/Master/Main_MasterPage.master" AutoEventWireup="true" CodeFile="SearchAccount_MainPage.aspx.cs" Inherits="Main_Code_SearchAccount_MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage" Runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    이메일 :
                </td>
                <td>
                    <asp:TextBox ID="Email" runat="server" Width="170px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="이메일을 입력해주세요." ToolTip="이메일을 입력해주세요." ValidationGroup="Search">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegular" runat="server" ErrorMessage="이메일 형식이 아닙니다." ToolTip="이메일 형식이 아닙니다." ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Search">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    보안 질문 :
                </td>
                <td>
                    <asp:DropDownList ID="Question" runat="server">
                        <asp:listitem Text="보안 질문을 선택해주세요" Value=""></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 동물은?" Value="animal"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 색깔은?" Value="color"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 음식은?" Value="food"></asp:listitem>
                        <asp:listitem Text="가장 좋아하는 노래는?" Value="music"></asp:listitem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="보안 질문을 선택해주세요." ToolTip="보안 질문을 선택해주세요." ValidationGroup="Search">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    보안 정답 :
                </td>
                <td>
                    <asp:TextBox ID="Answer" runat="server" Width="170px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="보안 정답을 입력해주세요." ToolTip="보안 정답을 입력해주세요." ValidationGroup="Search">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Button ID="SearchAccount" runat="server" Text="계정 찾기" OnClick="SearchAccount_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="SearchLabel" runat="server" Text=""></asp:Label>
    </div>
    
</asp:Content>

