<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CM_Menu.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        if( document.getElementById("HyperLink"+i)!=null)
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
    }
        if( document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="5000" runat="server">
    </asp:Timer>
    <div id="menu">
        <div id="menuTitle">
            功能选项
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label></div>
        <div id="menuContent">
          <%--  <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenu3NoSelected"
                Target="right" runat="server"><p>投标管理</p></asp:HyperLink>--%>
                
            <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                   <asp:AsyncPostBackTrigger  ControlID="Timer1" EventName="Tick"/>
                </Triggers>
                 <ContentTemplate><p>投标评审<asp:Label ID="lb_toubiao" runat="server" ForeColor="Red"></asp:Label></p>
                 </ContentTemplate>
                </asp:UpdatePanel>
                </asp:HyperLink> 
        <%--    <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenu5NoSelected"
                Target="right" runat="server"><p>合同评审</p></asp:HyperLink>--%>
          <%--      
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenu2NoSelected"
                Target="right" runat="server"><p>工程管理</p></asp:HyperLink>--%>
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>任务单</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false"><p>商务联系单</p></asp:HyperLink>
       <%--      <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenu7NoSelected"
                Target="right" runat="server"><p>联系单审核</p></asp:HyperLink>--%>
         <%--   <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenu5NoSelected"
                Target="right" runat="server"><p>报表管理</p></asp:HyperLink>--%>
          <%--  <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenu3NoSelected"
                Target="right" runat="server"><p>商务合同管理</p></asp:HyperLink>--%>
                
            <%--<asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenu4NoSelected"
                Target="right" runat="server"><p>外贸管理</p></asp:HyperLink>--%>
        </div> <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
