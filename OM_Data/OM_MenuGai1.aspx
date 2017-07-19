<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_MenuGai1.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_MenuGai1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />

    <script src="../JS/EasyUI/jquery.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Assets/OM_menu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .back
        {
            font-size: 12px;
            display: block;
            background: url(images/menunoselected.gif) no-repeat;
            cursor: pointer;
            margin-top: 10px;
            margin-bottom: 10px;
        }
    </style>
</head>
<body id="leftMenu" class="easyui-layout">

    <script language="javascript" type="text/javascript">
        function SelectMenu(num) {
            for (var i = 1; i <= 200; i++) {
                if (document.getElementById("HyperLink" + i) != null)
                    document.getElementById("HyperLink" + i).className = 'LeftMenuNoSelected';
            }
            if (document.getElementById("HyperLink" + num) != null)
                document.getElementById("HyperLink" + num).className = 'LeftMenuSelected';
        }
    </script>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" Interval="3000" runat="server">
    </asp:Timer>
    <div region="west" title=" &nbsp; &nbsp; &nbsp; &nbsp;����ѡ��" data-options="collapsible:false">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div id="menuContent" class="easyui-accordion" fit="true">
            <div id="div_rl" title="������Դ����" runat="server" style="overflow: auto; background-color: #E3F1FA;
                border-width: 2px;">
                <div id="menuContent1" class="easyui-accordion" fit="true">
                    <div title="&nbsp ��Ա������Ϣ����" selected="false" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink1" runat="server" onClick="SelectMenu(1);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/sta_detail.aspx" Target="right">
                         <p>&nbsp ������Ϣ�������ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink136" runat="server" onClick="SelectMenu(136);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/staff_record.aspx" Target="right">
                            <p>&nbsp ��ʷ��Ϣ��ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink94" runat="server" onClick="SelectMenu(94);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ContractRecord.aspx" Target="right">
                            <p>&nbsp ��ͬǩ����¼</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink95" runat="server" onClick="SelectMenu(95);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_StaffChange.aspx" Target="right">
                            <p>&nbsp ��Ա����ͳ��</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ���ڹ���" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink32" runat="server" onClick="SelectMenu(32);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTJ.aspx" Target="right">
                        <p style="width:180px">&nbsp &nbsp ���ڻ������ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink110" runat="server" onClick="SelectMenu(110);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTJdetail.aspx" Target="right">
                        <p style="width:180px">&nbsp &nbsp ���¿�����ϸ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink112" runat="server" onClick="SelectMenu(112)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ZYKQTJdetail.aspx" Target="right">
                            <p>&nbsp &nbsp ���¿�����ϸ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink33" runat="server" onClick="SelectMenu(33);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KQTOTAL.aspx" Target="right">
                    <p>&nbsp &nbsp  ��ʷ����ͳ��</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink106" runat="server" onClick="SelectMenu(106);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Person_Hol.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��н��ٹ���<asp:Label ID="lbbingjiatx" runat="server" ForeColor="Red" ToolTip="�����ۼƲ��¼��Ѵﵽ20���������������鿴��"></asp:Label></asp:Label><asp:Label
                                    ID="lbqingltx" runat="server" ForeColor="Red" ToolTip="�����Ҫ���㣡"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div id="div1" runat="server" title="&nbsp &nbsp н�����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink5" runat="server" onClick="SelectMenu(5);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZQD.aspx" Target="right">
                    <p>&nbsp &nbsp  ���ʱ�</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink79" runat="server" onClick="SelectMenu(79);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/MULTIHZEXPORT.aspx" Target="right">
                    <p>&nbsp &nbsp ���������ۺϲ�ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink92" runat="server" onClick="SelectMenu(92);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Salarysearch.aspx" Target="right">
                    <p>&nbsp &nbsp ���ʻ��ܲ�ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink26" runat="server" onClick="SelectMenu(26);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHe_JXGZ_List.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ְ�ܲ��ż�Ч����<asp:Label ID="lblDepJXGZ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink23" runat="server" onClick="SelectMenu(23);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_bzAverageList.aspx" Target="right">
                            <p>
                                &nbsp &nbsp һ�߰���ƽ������<asp:Label ID="lblBZEverage" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink12" runat="server" onClick="SelectMenu(12);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SCCZSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ���鼨Ч����<asp:Label ID="labelczgw" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink66" runat="server" onClick="SelectMenu(66);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXADDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ա��Ч����<asp:Label ID="lbjxaddsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink72" runat="server" onClick="SelectMenu(72);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXGZYESP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ч���ʽ�������<asp:Label ID="lbjxgzyesp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink77" runat="server" onClick="SelectMenu(77);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JXGZSYSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ч����ʹ������<asp:Label ID="lbjxgzsysp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink7" runat="server" onClick="SelectMenu(7);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZ.aspx" Target="right">
                    <p>&nbsp &nbsp  ��λ����̨��</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink76" runat="server" onClick="SelectMenu(76);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��λ��������<asp:Label ID="lbgdgzsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink82" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDGZSCSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��λ����ɾ������<asp:Label ID="lbgdgzscsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        <asp:HyperLink ID="HyperLink99" runat="server" onClick="SelectMenu(99);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SALARYBASEDATA.aspx" Target="right">
                            <p>
                                &nbsp &nbsp н�����̨��</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink100" runat="server" onClick="SelectMenu(100);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SALARYBASEDATASP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp н���������<asp:Label ID="lbxcxssp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        
                        <asp:HyperLink ID="HyperLink59" runat="server" onClick="SelectMenu(59);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZYDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp н���춯����<asp:Label ID="lbgzydsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink24" runat="server" onClick="SelectMenu(24);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZQDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��������<asp:Label ID="lbgzqd" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink34" runat="server" onClick="SelectMenu(34);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GZT.aspx" Target="right" Visible="false">
                    <p>&nbsp &nbsp  ������</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp �Ͳ�����" selected="false" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink111" runat="server" onClick="SelectMenu(111)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBu.aspx" Target="right">
                             <p>&nbsp &nbsp �Ͳ����ݹ��� </p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink75" runat="server" onClick="SelectMenu(75)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBuSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �Ͳ���������
                                <asp:Label ID="lbcanbusp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink91" runat="server" onClick="SelectMenu(91)" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CanBuYDSP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �Ͳ��춯����
                                <asp:Label ID="lbcanbuydsp" runat="server" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp Ա����ϵ����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink2" runat="server" onClick="SelectMenu(2);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHBX.aspx" Target="right">
                    <p>&nbsp &nbsp  Ա�����չ���</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink3" runat="server" onClick="SelectMenu(3);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GJJ.aspx" Target="right">
                    <p>&nbsp &nbsp  Ա�����������</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink4" runat="server" onClick="SelectMenu(4);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LDBX.aspx" Target="right">
                    <p> &nbsp &nbsp ��ǲԱ�����չ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink62" runat="server" onClick="SelectMenu(62);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_XUNISHBX.aspx" Target="right">
                    <p> &nbsp &nbsp �ػ����⻧����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink63" runat="server" onClick="SelectMenu(63);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_XUNIGJJ.aspx" Target="right">
                    <p> &nbsp &nbsp �ػ����⻧������</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink8" runat="server" onClick="SelectMenu(8);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RenYuanDiaoDongMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ա����������<asp:Label ID="lblMove" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink135" runat="server" onClick="SelectMenu(135);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LZSQJSX.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp ��ְ��������
                                        <asp:Label runat="server" ID="lbLZSX" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ��Ч����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink11" runat="server" onClick="SelectMenu(11);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeMuBList.aspx" Target="right">
                    <p>&nbsp &nbsp ��Чģ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink13" runat="server" onClick="SelectMenu(13);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeList.aspx" Target="right">
                    <p>&nbsp &nbsp ����������</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink14" runat="server" onClick="SelectMenu(14);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeAudit.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ������������<asp:Label ID="lblKaohe" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink6" runat="server" onClick="SelectMenu(6);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoheRecord.aspx" Target="right">
                    <p>&nbsp &nbsp Ա����Ч�¶ȳɼ�</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink25" runat="server" onClick="SelectMenu(25);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_KaoHeList_DepartMonth.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��֯��Ч�¶ȳɼ�<asp:Label ID="lblDepartMonth" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ��ѵ����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink42" runat="server" onClick="SelectMenu(42);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDPXJH_GL.aspx" Target="right">
                            <p>
                                &nbsp �����ѵ�ƻ�����
                                <asp:Label runat="server" ID="lbNDPX_SQ" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink15" runat="server" onClick="SelectMenu(15);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDPXJJH_HZSP.aspx" Target="right">
                            <p>
                                &nbsp �����ѵ�ƻ�����<asp:Label runat="server" ID="lbNDPX_HZ" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink29" runat="server" onClick="SelectMenu(29);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSPX_GL.aspx" Target="right">
                            <p>
                                &nbsp ��ʱ��ѵ�ƻ�<asp:Label runat="server" ID="lbLSPX" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink22" runat="server" onClick="SelectMenu(22);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXBM_GL.aspx" Target="right">
                    <p>&nbsp &nbsp ��ѵ����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink21" runat="server" onClick="SelectMenu(21);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_JDPXJH.aspx" Target="right">
                    <p>&nbsp &nbsp ��ѵʵʩ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink44" runat="server" onClick="SelectMenu(44);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXDA.aspx" Target="right">
                    <p> &nbsp &nbsp  ������ѵ����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink45" runat="server" onClick="SelectMenu(45);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXJSDA.aspx" Target="right">
                    <p>&nbsp &nbsp  ��ѵ��ʦ����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink43" runat="server" onClick="SelectMenu(43);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXXQ_GL.aspx" Target="right">
                    <p>&nbsp &nbsp ��ѵ�������</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink78" runat="server" onClick="SelectMenu(78);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_PXJBXS.aspx" Target="right">
                    <p>&nbsp &nbsp ��ѵѧʱͳ��</p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ��Ƹ���ù���" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink39" runat="server" onClick="SelectMenu(39);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_GL.aspx" Target="right">
                    <p>&nbsp &nbsp  �����Ƹ�ƻ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink65" runat="server" onClick="SelectMenu(65);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_NDZPJH_HZGL.aspx" Target="right">
                    <p>&nbsp &nbsp  �����Ƹ�ƻ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink20" runat="server" onClick="SelectMenu(20);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �����Ƹ�ƻ�����<asp:Label runat="server" ID="lbZPJH" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink67" runat="server" onClick="SelectMenu(67);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_GL.aspx" Target="right">
                    <p>&nbsp &nbsp  ��ʱ��Ƹ�ƻ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink68" runat="server" onClick="SelectMenu(68);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_HZGL.aspx" Target="right">
                    <p>&nbsp &nbsp  ��ʱ��Ƹ�ƻ�����</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink69" runat="server" onClick="SelectMenu(69);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_LSZPJH_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp ��ʱ��Ƹ�ƻ�����<asp:Label runat="server" ID="lbLSZPJH" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink36" runat="server" onClick="SelectMenu(36);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYXQJH_CL.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �����Ƹ�ƻ�����<asp:Label runat="server" ID="Label1" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink40" runat="server" onClick="SelectMenu(40);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYPZQD.aspx" Target="right">
                    <p>&nbsp &nbsp ��Ա�����嵥</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink37" runat="server" onClick="SelectMenu(37);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_RYPZQD_JL.aspx" Target="right">
                    <p>&nbsp &nbsp ��Ա������ʷ��ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink41" runat="server" onClick="SelectMenu(41);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="" Target="right" Visible="false">
                    <p> &nbsp &nbsp  �������</p>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
            <div title="��������" style="overflow: auto; background-color: #E3F1FA; border-width: 1px;">
                <div id="menuContent7" class="easyui-accordion" fit="true">
                    <div title="&nbsp &nbsp ��������" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink56" runat="server" onClick="SelectMenu(56);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarApplyAudit.aspx" Target="right"><p>&nbsp &nbsp  �ó����</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink48" runat="server" onClick="SelectMenu(48);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarApply.aspx" Target="right"><p>&nbsp &nbsp  �ó�����</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink46" runat="server" onClick="SelectMenu(46);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Car.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��������<asp:Label ID="lblCarBX" runat="server" Text="" ForeColor="Red"></asp:Label><asp:Label
                                    ID="lblCarWH" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink47" runat="server" onClick="SelectMenu(47);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarRecord.aspx" Target="right"><p>&nbsp &nbsp  �г���¼��ѯ</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink57" runat="server" onClick="SelectMenu(57);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarWeixiuShenqing.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ά��/��������<asp:Label runat="server" Text="" ForeColor="Red" ID="lbWXSQ"></asp:Label><asp:Label
                                    runat="server" Text="" ForeColor="Red" ID="lbBYSQ"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink54" runat="server" onClick="SelectMenu(54);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarWeihu.aspx" Target="right"><p>&nbsp &nbsp  ά��/����/����</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink55" runat="server" onClick="SelectMenu(55);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHENHE.aspx" Target="right"><p>&nbsp &nbsp  �����������</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink30" runat="server" onClick="SelectMenu(30);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Driver.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ˾������<asp:Label ID="lblSJ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink85" runat="server" onClick="SelectMenu(78);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CarKuCun.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʒ���<asp:Label ID="lbclkc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink86" runat="server" onClick="SelectMenu(79);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARRK.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʒ���<asp:Label ID="lbclrk" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink87" runat="server" onClick="SelectMenu(80);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARRK_SP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʒ�������<asp:Label ID="lbclrksp" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink88" runat="server" onClick="SelectMenu(81);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARCK.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʒ����<asp:Label ID="lbclck" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink89" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_CARCK_SP.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʒ��������<asp:Label ID="lbclcksp" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp �̶��ʲ�����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink49" runat="server" onClick="SelectMenu(49);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcPcPlan.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �̶��ʲ��ɹ�<asp:Label ID="lblGDZCpc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink50" runat="server" onClick="SelectMenu(50);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcStore.aspx" Target="right"><p>&nbsp &nbsp  �̶��ʲ����</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink51" runat="server" onClick="SelectMenu(51);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcIn.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �̶��ʲ����<asp:Label ID="lblGDZCIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink70" runat="server" onClick="SelectMenu(70);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDZCRK_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �̶��ʲ��������<asp:Label runat="server" ID="lbGDZCRK" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink52" runat="server" onClick="SelectMenu(52);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcTransSum.aspx" Target="right"><p>&nbsp &nbsp  �̶��ʲ�ת�Ƽ�¼</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink71" runat="server" onClick="SelectMenu(71);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GDZCZY_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �̶��ʲ�ת������<asp:Label runat="server" ID="lbGDZCZY" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink83" runat="server" onClick="SelectMenu(82);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_GdzcBaofeiSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �̶��ʲ���������<asp:Label runat="server" ID="lbGDZCBF" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink90" runat="server" onClick="SelectMenu(90);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ZCKX.aspx" Target="right"><p>&nbsp &nbsp  �ʲ���ѯ</p></asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp �칫�豸�ʲ�����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink35" runat="server" onClick="SelectMenu(35);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcPcPlan.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �칫�豸�ʲ��ɹ�<asp:Label ID="lblFGDZCpc" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink38" runat="server" onClick="SelectMenu(38);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcStore.aspx" Target="right"><p>&nbsp &nbsp  �칫�豸�ʲ����</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink58" runat="server" onClick="SelectMenu(58);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcIn.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �칫�豸�ʲ����<asp:Label ID="lblFGDZCIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink73" runat="server" onClick="SelectMenu(73);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGDZCRK_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �칫�豸�ʲ��������<asp:Label runat="server" ID="lbFGDZCRK" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink60" runat="server" onClick="SelectMenu(60);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcTransSum.aspx" Target="right"><p>&nbsp &nbsp  �칫�豸�ʲ�ת��</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink74" runat="server" onClick="SelectMenu(74);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGDZCZY_SP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �칫�豸�ʲ�ת������<asp:Label runat="server" ID="lbFGDZCZY" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink84" runat="server" onClick="SelectMenu(84);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_FGdzcBaofeiSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp �칫�豸�ʲ���������<asp:Label runat="server" ID="lbFGDZCBF" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp �칫��Ʒ����" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink18" runat="server" onClick="SelectMenu(18);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypApplyMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ʹ������<asp:Label ID="lblBGYPSQ" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink19" runat="server" onClick="SelectMenu(19);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypPcApplyMain.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �ɹ�����<asp:Label ID="lblBGYPPC" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink96" runat="server" onClick="SelectMenu(96);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BgypChangeRecord.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �Ծɻ��²�ѯ</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink81" runat="server" onClick="SelectMenu(81);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_PCHZ.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �칫��Ʒ��������<asp:Label ID="lblBGYPHZSP" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink16" runat="server" onClick="SelectMenu(16);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_In_List.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ������<asp:Label ID="lblBGYPIN" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink17" runat="server" onClick="SelectMenu(17);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_Store.aspx" Target="right"><p>&nbsp &nbsp ����ѯ</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink27" runat="server" onClick="SelectMenu(27);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Bgyp_DingE.aspx" Target="right"><p>&nbsp &nbsp  ���������ѯ</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink10" runat="server" onClick="SelectMenu(10);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_BGYP_SafeStore.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �칫��Ʒ��ȫ���<asp:Label ID="lblSafe" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ס�޹���" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink53" runat="server" onClick="SelectMenu(53);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SUSHE.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ס�޹���</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink61" runat="server" onClick="SelectMenu(61);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHUIDFLIST.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ס��ˮ��ѹ���</p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink80" runat="server" onClick="SelectMenu(80);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_SHUIDFSP.aspx" Target="right">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                                <ContentTemplate>
                                    <p>
                                        &nbsp &nbsp ס��ˮ�������<asp:Label runat="server" ID="lbZSSDFSP" ForeColor="Red"></asp:Label></p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ʳ�ù���" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink9" runat="server" onClick="SelectMenu(9);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_Eat.aspx" Target="right"><p>&nbsp &nbsp �����ò�(�����鿴)</p></asp:HyperLink>
                        <asp:HyperLink ID="HyperLink31" runat="server" onClick="SelectMenu(31);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_EATNEWLIST.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �����ò�(��)<asp:Label ID="lbsqycnew" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ���ù���" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink93" runat="server" onClick="SelectMenu(93);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_TravelApply.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��������
                                <asp:Label ID="lbTravelApply" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink97" runat="server" onClick="SelectMenu(97);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_TravelDelay.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��������
                                <asp:Label ID="lbTravelDelay" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                    <div title="&nbsp &nbsp ������������" style="overflow: auto; background-color: #E3F1FA;">
                        <asp:HyperLink ID="HyperLink28" runat="server" onClick="SelectMenu(28);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_YongYinList.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��ӡ����<asp:Label ID="lblYY" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink64" runat="server" onClick="SelectMenu(64);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ComputerLIst.aspx" Target="right">
                            <p>
                                &nbsp &nbsp �����豸����<asp:Label ID="lblComputer" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink98" runat="server" onClick="SelectMenu(98);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/OM_ExpressManage.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��ݹ���<asp:Label ID="lbExpress" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        
                        <asp:HyperLink ID="HyperLink101" runat="server" onClick="SelectMenu(101);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/jiPiaomanagementaspx.aspx" Target="right">
                            <p>
                                &nbsp &nbsp ��Ʊ����<asp:Label ID="lbjipiao" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink102" runat="server" onClick="SelectMenu(102);" CssClass="LeftMenuNoSelected"
                            NavigateUrl="~/OM_Data/PowerAuditManagement.aspx" Target="right">
                            <p>
                                &nbsp &nbsp Ȩ����������<asp:Label ID="lbpower" runat="server" Text="" ForeColor="Red"></asp:Label></p>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
