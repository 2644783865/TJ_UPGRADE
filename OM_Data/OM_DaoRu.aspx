<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_DaoRu.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_DaoRu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="导 入" />&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btn" Text="考勤导入" OnClick="btn_Click" />
        <asp:Button runat="server" ID="btn1" Text="固定工资导入" OnClick="btn_Click" />
        <asp:Button runat="server" ID="btn2" Text="公积金导入" OnClick="btn_Click" />
        <asp:Button runat="server" ID="btn3" Text="社会保险导入" OnClick="btn_Click" />
        <asp:Button runat="server" ID="btn4" Text="劳动保险导入" OnClick="btn1_Click" />
        <asp:Button runat="server" ID="btn5" Text="生产部门操作" OnClick="btn_Click" />
        <asp:Button runat="server" ID="btn6" Text="各部门绩效" OnClick="btn_Click" />
    </div>
    </form>
</body>
</html>
