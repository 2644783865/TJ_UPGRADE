<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_KAOQINADD.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KAOQINADD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="~/assets/main.css" rel="stylesheet" type="text/css" />
    <title>����-��Ա���</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 23%;">
                <strong>ʱ�䣺</strong>
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
                &nbsp;��
                <asp:Label ID="lbNian" runat="server" Text="*" ForeColor="Red"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddlMonth" runat="server">
                </asp:DropDownList>
                &nbsp;��
                <asp:Label ID="lbYue" runat="server" Text="*" ForeColor="Red"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                ����<asp:Label runat="server" Text="*" ForeColor="Red"></asp:Label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ����<asp:TextBox ID="txtGongHao" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="����" OnClick="btnSave_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
