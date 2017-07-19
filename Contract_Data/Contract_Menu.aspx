<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contract_Menu.aspx.cs"
    Inherits="ZCZJ_DPF.Contract_Data.Contract_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu">
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
    function SelectMenu(num)
    {
       for(var i=1;i<=13;i++)
        {
        if(document.getElementById("HyperLink"+i)!=null)
        {
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
        }
        }
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
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
            <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��ͬ����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �ҵ���������<asp:Label ID="MyViewTask" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>���ۺ�ͬ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>ί���ͬ</p></asp:HyperLink>
            <%--                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���ڷְ�</p></asp:HyperLink>--%>
            <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>�ɹ���ͬ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>�칫��ͬ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>������ͬ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �������<asp:Label ID="Undo_QK" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Label ID="Undo_YK" runat="server" ForeColor="Red"></asp:Label>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��/�����¼</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��Ʊ��¼</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(46);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_contract_task_view.aspx" Target="right">
                            <p>��Ŀ�ۺ���Ϣ��ѯ</p>
            </asp:HyperLink>
        </div>
    </div>
    </form>
</body>
</html>
