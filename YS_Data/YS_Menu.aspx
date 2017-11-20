<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YS_Menu.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu">
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 10; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="60000">
    </asp:Timer>
    <div id="menu">
        <div id="menuTitle">
            ����ѡ��<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label></div>
        <div id="menuContent">
            <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>����Ԥ�����<asp:Label ID="lb_num" runat="server"  ForeColor="Red"></asp:Label></p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��ͬԤ��鿴</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ԥ��ʵ�ʷ�����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ԥ��ʵʱ���</p></asp:HyperLink>
            <%--<asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��������</p></asp:HyperLink>--%>
            <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ԥ��ɱ�����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>Ԥ���¶�ͳ��</p></asp:HyperLink>
        </div>
    </div>
    </form>
</body>
</html>
