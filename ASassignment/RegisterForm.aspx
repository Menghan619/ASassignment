<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="ASassignment.RegisterForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <header>
        <script type="text/javascript">
            function validate() {
                var str = document.getElementById('<%=TBPW.ClientID%>').value;

                if (str.length < 8) {
                    document.getElementById("PWcheck").innerHTML = "Password Length must be at least 8 characters";
                    document.getElementById("PWcheck").style.color = "Red";
                    return ("too short");
                }
        }
    </header>
    
        
    </script>
    <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>
    <br />
    <asp:TextBox ID="TBFname" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>
    <br />
    <asp:TextBox ID="TBLN" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Credit card No.:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCCN" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label4" runat="server" Text="CVV:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCVV" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Card expiry date:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCED" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Name on Card:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCardName" runat="server" Height="17px"></asp:TextBox>
    <br />
    <asp:Label runat="server" Text="Email:"></asp:Label>
    <br />
    <asp:TextBox ID="TBEMail" runat="server" Height="17px"></asp:TextBox>
    <br />
    <asp:Label ID="Label8" runat="server" Text="Password:"></asp:Label>
    <br />
    <asp:TextBox ID="TBPW" runat="server" Height="17px" onkeyup="javascript:validate()"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="PWcheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label9" runat="server" Text="Date of Birth:"></asp:Label>
    <br />
    <asp:TextBox ID="TBDOB" runat="server" Height="17px"></asp:TextBox>
</asp:Content>
