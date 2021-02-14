<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ASassignment.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       
    <script type="text/javascript">
            function validate() {
                var str = document.getElementById('<%=TBPW.ClientID%>').value;

                if (str.length < 8) {
                    document.getElementById("PWcheck").innerHTML = "Password Length must be at least 8 characters";
                    document.getElementById("PWcheck").style.color = "Red";
                    return ("too short");

                } else {
                    document.getElementById("PWcheck").innerHTML = "";
                }
                
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="RegBtn" title="Register">
         
    
        
       
    <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>
    <br />
    <asp:TextBox ID="TBFname" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="FNCheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>
    <br />
    <asp:TextBox ID="TBLN" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="LNCheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Credit card No.:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCCN" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="CreditnoCheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="CVV:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCVV" runat="server" ></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="CVVCheck" runat="server"></asp:Label>
    <br />
   
    <asp:Label ID="Label6" runat="server" Text="Name on Card:"></asp:Label>
    <br />
    <asp:TextBox ID="TBCardName" runat="server" Height="17px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="CardNameCheck" runat="server"></asp:Label>
    <br />
    <asp:Label runat="server" Text="Email:"></asp:Label>
    <br />
    <asp:TextBox ID="TBEMail" TextMode="Email" runat="server" Height="17px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="EmailCheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label8" runat="server" Text="Password:"></asp:Label>
    <br />
    <asp:TextBox ID="TBPW" runat="server" Height="17px" onkeyup="javascript:validate()"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="PWcheck" runat="server"></asp:Label>
    <br />
    <asp:Label ID="Label9" runat="server" Text="Date of Birth:"></asp:Label>
    <br />
    <asp:TextBox ID="TBDOB" TextMode="Date" runat="server" Height="17px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="REGBTN" runat="server" OnClick="REGBTN_Click" Text="Register" />
        </div>
    </form>
</body>
</html>
