<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MT_Menu.aspx.cs" Inherits="ZCZJ_DPF.MT_Data.MT_Menu1" %>

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

    <script type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 150; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    
    </script>

</head>
<body id="leftMenu" class="easyui-layout">
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="30000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <%--<asp:TreeView ID="tvOM" runat="server" Target="right" ShowLines="true" ExpandDepth="0"
            SelectedNodeStyle-ForeColor="#33cc33">
            <Nodes>
            </Nodes>
        </asp:TreeView>--%>
        <asp:HyperLink ID="HyperLink1" onClick="SelectMenu(1);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ӫ�ƻ���<asp:Label runat="server" ID="jyjhd" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink80" onClick="SelectMenu(80);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel65" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ����Ԥ�����<asp:Label runat="server" ID="lbTaskBudgetNum" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink3" onClick="SelectMenu(3);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ����֪ͨ<asp:Label runat="server" ID="fhtz" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" Visible="false" onClick="SelectMenu(4);" Target="right"
            CssClass="LeftMenuNoSelected" runat="server">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ա��Ϣ����<asp:Label ID="lblPeopleInfo" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink8" onClick="SelectMenu(8);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ������������
                        <asp:Label ID="task" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink9" onClick="SelectMenu(9);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ȼ۵�����<asp:Label ID="lb_bjdsh" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink10" onClick="SelectMenu(10);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ������Э����<asp:Label ID="lbl_wxsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink11" onClick="SelectMenu(11);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Э�ȼ�����<asp:Label ID="lbl_wxbjsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink12" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server">
            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���˱ȼ�����<asp:Label ID="lbl_fysp" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink27" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server">
            <asp:UpdatePanel ID="UpdatePanel24" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ʒ�������<asp:Label ID="lbl_finish" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink21" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server">
            <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ʒ��������<asp:Label ID="lbl_out" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink13" onClick="SelectMenu(13);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ɹ���������<asp:Label ID="lbl_cgsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink22" onClick="SelectMenu(22);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��������<asp:Label ID="CUSUP_REVIEW" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink23" onClick="SelectMenu(23);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���ϼ���/�������<asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label><asp:Label
                            ID="Label5" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink14" onClick="SelectMenu(14);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �豸�ɹ�����<asp:Label ID="lblEquNeed" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink55" runat="server" onClick="SelectMenu(55);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/ESM_Data/EQU_GXHT_GL.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel50" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �豸��ͬ����<asp:Label ID="lbl_ssp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink15" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_Kaipiao_List.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ʊ����<asp:Label runat="server" ID="lblKaiPiao" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink16" onClick="SelectMenu(16);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ҵı�������<asp:Label ID="lb_baojian" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink17" onClick="SelectMenu(17);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���ϸ�Ʒ֪ͨ��<asp:Label ID="lb_rejectPro" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink18" Visible="false" onClick="SelectMenu(18);" Target="right"
            CssClass="LeftMenuNoSelected" runat="server">
            <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ����ռ�ù���<%--<asp:Label ID="lb_wlzygl" runat="server" Text="" ForeColor="Red">--%></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink19" onClick="SelectMenu(19);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���ϴ��ù���<asp:Label ID="lb_dydsh" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_ChangePS.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel150" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ƻ����������<asp:Label runat="server" ID="bgtz" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink34" runat="server" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_ZengBuPs.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ƻ�����������<asp:Label runat="server" ID="zbtz" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink40" onClick="SelectMenu(40);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_JHDQXSP.aspx" Target="right" runat="server">
            <p>
                �ƻ���ȡ������
                <asp:Label runat="server" ID="lbQXPS" ForeColor="Red"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink25" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHLXDGL.aspx" Target="right">
            <p>
                ������ϵ��
                <asp:Label runat="server" ID="lbLXD" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink26" runat="server" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLDGL.aspx" Target="right" Visible="false">
            <p>
                �������⴦��
                <asp:Label runat="server" ID="lbCLD" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink57" runat="server" onClick="SelectMenu(57);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_CX.aspx" Target="right">
            <p>
                ����������ѯ
                <asp:Label runat="server" ID="Label4" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink58" runat="server" onClick="SelectMenu(58);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_ADD.aspx" Target="right">
            <p>
                ������������
                <asp:Label runat="server" ID="Label2" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink59" runat="server" onClick="SelectMenu(59);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_SP.aspx" Target="right">
            <p>
                ������������
                <asp:Label runat="server" ID="lbCLD_SP" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink60" runat="server" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_FG.aspx" Target="right">
            <p>
                ���������ֹ�
                <asp:Label runat="server" ID="lbCLD_FG" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink61" runat="server" onClick="SelectMenu(61);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_YYFX.aspx" Target="right">
            <p>
                ��дԭ�����
                <asp:Label runat="server" ID="lbCLD_YYFX" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink62" runat="server" onClick="SelectMenu(62);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_TXCLYJ.aspx" Target="right">
            <p>
                ��д�������
                <asp:Label runat="server" ID="lbCLD_CLYJ" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink63" runat="server" onClick="SelectMenu(63);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_TXCLFA.aspx" Target="right">
            <p>
                ��д������
                <asp:Label runat="server" ID="lbCLD_CLFA" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink65" runat="server" onClick="SelectMenu(65);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_TXCLJG.aspx" Target="right">
            <p>
                ��д������̼����
                <asp:Label runat="server" ID="lbCLD_CLJG" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink66" runat="server" onClick="SelectMenu(66);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_SHCLD_TXFWFY.aspx" Target="right">
            <p>
                ͳ�Ʒ������
                <asp:Label runat="server" ID="lbTJ" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink33" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/CM_TZTHTZDGL.aspx" Target="right" runat="server">
            <asp:UpdatePanel ID="UpdatePanel30" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ͼֽ�滻֪ͨ��<asp:Label ID="lbTZTHTZD" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink35" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server" NavigateUrl="~/PC_Data/PC_CGHTGL.aspx">
            <asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ɹ���ͬ
                        <asp:Label ID="lbCGHTGL" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink24" runat="server" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/CM_Data/PD_DocManage.aspx" Target="right" Visible="false">
            <p>
                Ͷ������<asp:Label runat="server" ID="lbTBPS" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink20" onClick="SelectMenu(20);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��ͬ��������<asp:Label ID="lbHTSP" runat="server" ForeColor="Red"></asp:Label>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink29" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server" NavigateUrl="~/TM_Data/TM_MP_Back.aspx">
            <asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���üƻ�����
                        <asp:Label ID="lblBack" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink5" onClick="SelectMenu(5);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ó���������<asp:Label ID="lblCars" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink50" onClick="SelectMenu(50);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server" NavigateUrl="~/OM_Data/OM_CarWeixiuShenqing.aspx">
            <asp:UpdatePanel ID="UpdatePanel45" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ά��/��������<asp:Label ID="lbWXSQ" runat="server" Text="" ForeColor="Red"></asp:Label><asp:Label
                            ID="lbBYSQ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink6" onClick="SelectMenu(6);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �̶��ʲ��ɹ�<asp:Label ID="lblFixedAssets" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink82" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GdzcBaofeiSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel54" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �̶��ʲ���������<asp:Label runat="server" ID="lbGDZCBF" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink73" runat="server" onClick="SelectMenu(73);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_FGdzcPcPlan.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel73" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫�豸�ʲ��ɹ�<asp:Label ID="lblFGDZCpc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink83" runat="server" onClick="SelectMenu(83);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_FGdzcBaofeiSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel55" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫�豸�ʲ���������<asp:Label runat="server" ID="lbFGDZCBF" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink28" onClick="SelectMenu(28);" Target="right" CssClass="LeftMenuNoSelected"
            runat="server">
            <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ò�����<asp:Label ID="lbsqycnew" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink44" runat="server" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_BgypApplyMain.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel37" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫��Ʒʹ������<asp:Label ID="lblBGYPSQ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink45" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_BgypPcApplyMain.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel34" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫��Ʒ�ɹ�����<asp:Label ID="lblBGYPPC" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink68" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_BGYP_PCHZ.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel56" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫��Ʒ��������<asp:Label ID="lblBGYPHZSP" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_BGYP_In_List.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel38" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫��Ʒ������<asp:Label ID="lblBGYPIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink93" runat="server" onClick="SelectMenu(93);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_TravelApply.aspx" Target="right">
            <p>
                ��������
                <asp:Label ID="lbTravelApply" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink69" runat="server" onClick="SelectMenu(69);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_TravelDelay.aspx" Target="right">
            <p>
                ��������
                <asp:Label ID="lbTravelDelay" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_YongYinList.aspx" Target="right">
            <p>
                ��ӡ����<asp:Label ID="lblYY" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink64" runat="server" onClick="SelectMenu(64);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_ComputerLIst.aspx" Target="right">
            <p>
                �����豸����<asp:Label ID="lblComputer" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink98" runat="server" onClick="SelectMenu(98);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_ExpressManage.aspx" Target="right">
            <p>
                ��ݹ���<asp:Label ID="lbExpress" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink102" runat="server" onClick="SelectMenu(102);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/PowerAuditManagement.aspx" Target="right">
            <p>
                &nbsp &nbsp Ȩ����������<asp:Label ID="lbpower" runat="server" Text="" ForeColor="Red"></asp:Label></p>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink36" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
            Target="right" runat="server" NavigateUrl="~/OM_Data/OM_KaoHeAudit.aspx">
            <asp:UpdatePanel ID="UpdatePanel33" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ������������
                        <asp:Label ID="lblKaohe" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink37" runat="server" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_bzAverageList.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel39" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        һ�߰���ƽ������<asp:Label ID="lblBZEverage" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink38" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_KaoHeList_DepartMonth.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel40" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��֯��Ч�¶ȳɼ�<asp:Label ID="lblDepartMonth" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink39" runat="server" onClick="SelectMenu(39);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_KaoHe_JXGZ_List.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel41" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ְ�ܲ��ż�Ч����<asp:Label ID="lblDepJXGZ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink41" runat="server" onClick="SelectMenu(41);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_SCCZSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ���鼨Ч����<asp:Label ID="labelczgw" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink42" runat="server" onClick="SelectMenu(42);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GZYDSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel35" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        н���춯����<asp:Label ID="lbgzydsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink51" runat="server" onClick="SelectMenu(51);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_JXADDSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel46" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ա���Ӽ�Ч����<asp:Label ID="lbjxaddsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink52" runat="server" onClick="SelectMenu(52);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_JXGZYESP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel47" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ч���ʽ�������<asp:Label ID="lbjxgzyesp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink56" runat="server" onClick="SelectMenu(56);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_JXGZSYSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel51" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ч����ʹ������<asp:Label ID="lbjxgzsysp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink79" runat="server" onClick="SelectMenu(79);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_SHUIDFSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel52" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ס��ˮ�������<asp:Label runat="server" ID="lbZSSDFSP" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink53" runat="server" onClick="SelectMenu(53);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_CanBuSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel48" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �Ͳ���������<asp:Label ID="lbcanbusp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink54" runat="server" onClick="SelectMenu(54);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GDGZSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel49" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��λ��������<asp:Label ID="lbgdgzsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink67" runat="server" onClick="SelectMenu(67);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GDGZSCSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel53" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��λ����ɾ������<asp:Label ID="lbgdgzscsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink72" runat="server" onClick="SelectMenu(72);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_SALARYBASEDATASP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel59" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        н���������<asp:Label ID="lbxcxssp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink43" runat="server" onClick="SelectMenu(43);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GZQDSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel36" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��������<asp:Label ID="lbgzqd" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink47" runat="server" onClick="SelectMenu(47);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_NDPXJH_GL.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel42" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �����ѵ�ƻ�����
                        <asp:Label runat="server" ID="lbNDPX_SQ" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink48" runat="server" onClick="SelectMenu(48);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_NDPXJJH_HZSP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel43" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �����ѵ�ƻ�����<asp:Label runat="server" ID="lbNDPX_HZ" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink135" runat="server" onClick="SelectMenu(135);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_LZSQJSX.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��ְ��������
                        <asp:Label runat="server" ID="lbLZSX" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink70" runat="server" onClick="SelectMenu(70);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_RenYuanDiaoDongMain.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel57" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��Ա����������
                        <asp:Label runat="server" ID="lblMove" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink49" runat="server" onClick="SelectMenu(49);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_LSPX_GL.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel44" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��ʱ��ѵ�ƻ�<asp:Label runat="server" ID="lbLSPX" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink71" runat="server" onClick="SelectMenu(71);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/QC_Data/QC_TargetAnalyze_List.aspx">
            <asp:UpdatePanel ID="UpdatePanel58" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        Ŀ��ֽ�<asp:Label ID="lblTargetAnalyze" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink30" runat="server" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/QC_Data/QC_Internal_Audit.aspx?type=inner">
            <asp:UpdatePanel ID="UpdatePanel27" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ڲ����<asp:Label ID="lblInnerAudit" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink31" runat="server" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/QC_Data/QC_Internal_Audit.aspx?type=group">
            <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        ��������<asp:Label ID="lblGroupAudit" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink32" runat="server" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/QC_Data/QC_External_Audit.aspx">
            <asp:UpdatePanel ID="UpdatePanel29" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ⲿ���<asp:Label ID="lblExterAudit" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink74" runat="server" onClick="SelectMenu(74);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/OM_Data/OM_FGDZCRK_SP.aspx">
            <asp:UpdatePanel ID="UpdatePanel60" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �칫�豸�ʲ����<asp:Label ID="lblnofixfuAudit" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink75" runat="server" onClick="SelectMenu(75);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/OM_Data/OM_GDZCRK_SP.aspx">
            <asp:UpdatePanel ID="UpdatePanel61" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �̶��ʲ����<asp:Label ID="lblfixmoAudit" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink76" runat="server" onClick="SelectMenu(76);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/OM_Data/OM_CanBuYDSP.aspx">
            <asp:UpdatePanel ID="UpdatePanel62" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �Ͳ��춯����<asp:Label ID="lbcanbuydsp" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink77" runat="server" onClick="SelectMenu(77);" CssClass="LeftMenuNoSelected"
            Target="right" NavigateUrl="~/PC_Data/PC_Pur_inform_commit.aspx">
            <asp:UpdatePanel ID="UpdatePanel63" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �ɹ���Ϣ����<asp:Label ID="lbpurinform" runat="server" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
        <asp:HyperLink ID="HyperLink78" runat="server" onClick="SelectMenu(78);" CssClass="LeftMenuNoSelected"
            NavigateUrl="~/OM_Data/OM_GDZCZY_SP.aspx" Target="right">
            <asp:UpdatePanel ID="UpdatePanel64" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        �̶��ʲ�ת������<asp:Label runat="server" ID="lbGDZCZY" ForeColor="Red"></asp:Label></p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:HyperLink>
    </div>
    </form>
</body>
</html>
