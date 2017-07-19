<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FM_Menu.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu" class="easyui-layout">
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
    function SelectMenu(num)
    {
    for(var i=1;i<=38;i++)
    {
        if(document.getElementById("HyperLink"+i)!=null)
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
    }
     if(document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>

    <%--<div id="menu">--%>
    <%--<div id="menuTitle">
            ����ѡ��<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label></div>--%>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="��Ʊ����" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ɹ���ⷢƱ����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Э��Ʊ����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink26" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�˷ѷ�Ʊ����</p></asp:HyperLink>
            </div>
            <div title="�ɱ�����" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>���������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ݹ������ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink18" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>����ڳ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>���Ԥ�������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ݹ����ݲ�ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ݹ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink17" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ɱ�������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>���������ϸ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����շ������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>����ͳ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>�������۳ɱ�</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Э����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink24" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Э����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Э�����Ϣ����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink27" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�˷Ѻ���</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink28" onClick="SelectMenu(28);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�˷ѻ���</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink29" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�˷Ѳ����Ϣ����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink37" onClick="SelectMenu(37);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ְ��ݹ���ѯ</p></asp:HyperLink>
            </div>
            <div title="�ɱ�ͳ��" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ͳ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ɱ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ɱ�ͳ�Ʒ���</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink32" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��ͬ�ɱ�ͳ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Ŀ�깤��ת</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink30" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��Ʊ����</p></asp:HyperLink>
            </div>
            <div title="�������ݷ���" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�������ָ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink35" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��ָͬ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink36" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ָ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink31" Visible="false" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ָ��ͳ��</p></asp:HyperLink>
            </div>
            <div title="�����±�" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink19" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ʲ���ծ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>������������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ֽ�������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink33" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��ָͬ���������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink34" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ָ���������</p></asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
