<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PC_Menu.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Menu" %>

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
<body id="leftMenu" class="easyui-layout">
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 150; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="30000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div title="�ɹ�����" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���üƻ�����<asp:Label ID="lb_XYplan" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ����ռ�ù���<asp:Label ID="lb_wlzygl" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ����ֹ�<asp:Label ID="lb_rwfg" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ɹ��ƻ�����
                                <asp:Label ID="lb_chjhgl" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink19" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ServiceList.aspx?Dep=05" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �˿ͷ���֪ͨ<asp:Label runat="server" ID="lblFWTZ" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                 <asp:HyperLink ID="HyperLink32" runat="server" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PM_Data/PM_FaHuoNotice.aspx" Target="right">
                    <p>
                        ����֪ͨ<asp:Label runat="server" ID="fhtz" ForeColor="Red" Visible="false"></asp:Label></p>
                </asp:HyperLink>
               <asp:HyperLink ID="HyperLink35" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/PC_Data/PC_ZHAOBIAOQUERY.aspx">
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �б����Ϲ���</p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ȼ۵�����<asp:Label ID="lb_bjdsh" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ��������<asp:Label ID="lb_ddgl" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                
                <asp:HyperLink ID="HyperLink31" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ����/�Ǽ��ɲ�ѯ<asp:Label ID="Label4" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                
                
                <asp:HyperLink runat="server" ID="HyperLink39" onClick="SelectMenu(39);" CssClass="LeftMenuNoSelected"
                    Target="right">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ɹ���ͬ����<asp:Label ID="lbCGHTGL" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink runat="server" ID="HyperLink34" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                    Target="right">
                    <asp:UpdatePanel ID="UpdatePanel34" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ɹ���Ʊ����<asp:Label ID="lbfpwwj" runat="server" Text="(*)" ToolTip="δ����Ʊ����" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ҵı�������<asp:Label ID="lb_baojian" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���õ�����<asp:Label ID="lb_dydsh" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �������ϱ��(�����鿴��
                                <asp:Label ID="lb_biangeng" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���ϼ���/�������<asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><asp:Label ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                
                <asp:HyperLink ID="HyperLink11" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ƻ������<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/QC_Data/QC_Reject_Product.aspx" Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���ϸ�Ʒ֪ͨ��<asp:Label ID="lb_rejectPro" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�۸����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ɹ�ͳ��</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink15" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ɹ��ƻ���ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink30" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����������ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ��������<asp:Label ID="CUSUP_REVIEW" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink17" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ��ͬ����<asp:Label ID="MyViewTask" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink33" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel33" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ����������<asp:Label ID="lbotherin" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                  <asp:HyperLink ID="HyperLink36" runat="server" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/PC_Data/PC_Pur_inform_commit.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ɹ���Ϣ����<asp:Label runat="server" ID="lbpurinform" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                
                
            </div>
            <div title="�ֿ����" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink126" onClick="SelectMenu(126);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���üƻ�����<asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink28" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>����ִ�в�ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����ѯ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink24" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ֿ����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink25" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>MTO����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink26" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                    Visible="false" Target="right" runat="server"><p>MTO����֪ͨ</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink27" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�ֿ��̵�</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink29" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
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
                <asp:HyperLink ID="HyperLink112" onClick="SelectMenu(112);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ռ�ù���<asp:Label ID="lb_MyTask" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink113" onClick="SelectMenu(113);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ���ù���<asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink110" onClick="SelectMenu(110);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>���˹���</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink119" onClick="SelectMenu(119);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ��ȫ���<asp:Label ID="lbl_safe" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink120" onClick="SelectMenu(120);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>��ȫ���ά��</p></asp:HyperLink>
                <asp:HyperLink runat="server" ID="HyperLink18" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                    Target="right">
                    <p>
                        �˿ͲƲ�<asp:Label runat="server" ID="lbNumber" ForeColor="Red"></asp:Label></p>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink114" onClick="SelectMenu(114);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>�����ɹ�����</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink115" onClick="SelectMenu(115);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                �ɹ���������<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                  <asp:HyperLink ID="HyperLink13" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                    NavigateUrl="~/CM_Data/CM_Kaipiao_List.aspx" Target="right">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                ��Ʊ����<asp:Label runat="server" ID="lblKaiPiao" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink121" onClick="SelectMenu(121);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false"><p>��Ŀ�깤</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink122" onClick="SelectMenu(122);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" Visible="false">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <p visible="false">
                                ��Ŀ��ת����<asp:Label ID="LabelProjTemp" runat="server" ForeColor="Red"></asp:Label></p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink123" onClick="SelectMenu(123);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>���Ͽ�����</p></asp:HyperLink>
              <%--  <asp:HyperLink ID="HyperLink124" onClick="SelectMenu(124);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>����������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink125" onClick="SelectMenu(125);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server"><p>���ϳ������</p></asp:HyperLink>--%>

            </div>
            <div title="ɨ�����ģ��" style="overflow: auto; background-color: #E3F1FA;">
                <asp:HyperLink ID="HyperLink125" onClick="SelectMenu(125);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/QR_Interface/QRIn_List.aspx"><p>ɨ���������</p></asp:HyperLink>
                <asp:HyperLink ID="HyperLink127" onClick="SelectMenu(127);" CssClass="LeftMenuNoSelected"
                    Target="right" runat="server" NavigateUrl="~/QR_Interface/QROut_List.aspx"><p>ɨ���������</p></asp:HyperLink>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
