<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZCZJ_DPF._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>ϵͳ��½</title>
    <link href="assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body id="loginPage">
    <form id="form1" runat="server">
    <div>
        <div id="main">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                <tr>
                    <td>
                        <div id="LoginForm">
                            <table id="loginBox">
                                <tr>
                                    <td style="height: 110px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px;">
                                    </td>
                                    <td>
                                        <label for="name">
                                            �û�����</label>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;&nbsp;<asp:TextBox ID="UserName" CssClass="login_input" MaxLength="18" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px;">
                                    </td>
                                    <td>
                                        <label for="password">
                                            ���룺</label>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;&nbsp;<asp:TextBox ID="PassWord" CssClass="login_input" MaxLength="18" runat="server"
                                            TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td style="width: 90px;">
                                    </td>
                                    <td>
                                        <label for="validateCode">
                                            ��֤�룺</label>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;<asp:TextBox ID="txtCheckCode" runat="server" CssClass="login_input"
                                            MaxLength="18"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img id="myImg" src="Systems/ValidateCode.ashx" onclick="ChangeCode(this)" alt="���ִ�Сд�����������֤��!"
                                            style="cursor: pointer" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="5px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="laberror" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 90px;">
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:ImageButton ID="LoginButton" ImageUrl="../Assets/images/loginButton.jpg" runat="server" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ResetButton" ImageUrl="../Assets/images/loginReset.jpg" runat="server" />
           
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                    </td>
                                    <td colspan="4" align="center">
                                        <asp:Label runat="server" ID="lb1" ForeColor="Red" Text="Ϊ��֤ERPϵͳ�������ʹ�ý���ʹ��360��ȫ�����7.1��</br>���ϰ汾�ļ���ģʽ���������������"></asp:Label>
                                        <br />
                                        <asp:HyperLink runat="server" NavigateUrl="http://10.11.11.3/soft/setup.exe" ID="hpl1" ForeColor="Green" Text=""><asp:Image runat="server" ID="img1" ImageUrl="~/Assets/icons/games.gif" />�������
     </asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>

            <script type="text/javascript"> 
     //��½��֤�����
function ChangeCode(obj){
    obj.src = "Systems/ValidateCode.ashx?" + Math.random();
}
            </script>

        </div>
    </div>
    </form>
</body>
</html>
