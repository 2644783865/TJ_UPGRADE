<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QX_Menu.aspx.cs" Inherits="ZCZJ_DPF.Systems.QX_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu">
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
    
   function SelectMenu(num)
   {
    for(var i=1;i<=2;i++)
    {
        document.getElementById("HyperLink"+i).className='LeftMenu'+i+'NoSelected';
    }
        document.getElementById("HyperLink"+num).className='LeftMenu'+num+'Selected';
    }

    </script>

    <div id="menu">
        <div id="menuTitle">
            功能选项<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label></div>
        <div id="menuContent">
            <p>
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenu1NoSelected"
                    Target="right" runat="server">配置权限</asp:HyperLink></p>
            <p>
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenu2NoSelected"
                    Target="right" runat="server">配置角色</asp:HyperLink></p>
            <p>
                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenu2NoSelected"
                    Target="right" runat="server">AJAX测试</asp:HyperLink></p>
        </div>
        <div>
            <asp:Image ID="Image1" ImageUrl="../Assets/images/menu_bg_middle.gif" runat="server" /></div>
    </div>
    </form>
</body>
</html>
