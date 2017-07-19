<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM_Menu.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
</head>
<body id="leftMenu"class="easyui-layout" >
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
    function SelectMenu(num)
    {
    for(var i=1;i<=26;i++)
    {
        if(document.getElementById("HyperLink"+i)!=null)
        document.getElementById("HyperLink"+i).className='LeftMenuNoSelected';
    }
        if(document.getElementById("HyperLink"+num)!=null)
        document.getElementById("HyperLink"+num).className='LeftMenuSelected';
    }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="60000" runat="server">
    </asp:Timer>
   <div  region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��"  data-options="collapsible:false"  >
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion"  fit="true"` >
        <div title="�ֿ����" style="overflow:auto;background-color: #E3F1FA;">
            <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ���üƻ���ѯ<asp:Label ID="lb_XYplan1" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink26" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ���üƻ�����<asp:Label ID="lb_XYplan" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false"><p>����ִ�в�ѯ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>����ѯ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>������</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�������</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�ֿ����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>MTO����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                Visible="false" Target="right" runat="server"><p>MTO����֪ͨ</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�ֿ��̵�</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�ֿ��λά��</p></asp:HyperLink>
           <%-- <asp:HyperLink ID="HyperLink18" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �������<asp:Label ID="LabelBG" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>--%>
            <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ռ�ù���<asp:Label ID="lb_MyTask" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ���ù���<asp:Label ID="lb_dydsh" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false"><p>���˹���</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink19" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            ��ȫ���<asp:Label ID="lbl_safe" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>��ȫ���ά��</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>�����ɹ�����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            �ɹ���������<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false"><p>��Ŀ�깤</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server" Visible="false">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                        <p visible="false">
                            ��Ŀ��ת����<asp:Label ID="LabelProjTemp" runat="server" ForeColor="Red"></asp:Label></p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���Ͽ�����</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink24" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>����������</p></asp:HyperLink>
            <asp:HyperLink ID="HyperLink25" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                Target="right" runat="server"><p>���ϳ������</p></asp:HyperLink>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
