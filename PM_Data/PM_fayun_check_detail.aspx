<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_fayun_check_detail.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_fayun_check_detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价单审核
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person2") {
                $("#<%=Tb_shenheren2.ClientID %>").val(r.st_name);
                $("#<%=Tb_shenherencode2.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>

    <%--<asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                询比价单
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_edit" runat="server" Text="编辑" OnClick="btn_edit_Click" Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_confirm" runat="server" Text="确定" OnClick="btn_confirm_Click"
                                    Enabled="false" />
                                &nbsp;&nbsp;
                                <%-- <asp:Button ID="btn_fanshen" runat="server" Text="反审" OnClick="btn_fanshen_Click"
                                    Visible="false" Enabled="false" />--%>
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%">
                    <tr>
                        <td align="left">
                            询比价单标号：<asp:TextBox ID="TextBoxNo" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" visible="false">
                            <asp:TextBox ID="TextBox1" runat="server" Text="3" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                    ActiveTabIndex="0">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="询比价单" TabIndex="0">
                        <ContentTemplate>
                            <div style="border: 1px solid #000000; height: 480px">
                                <div class="cpbox4 xscroll">
                                    <table id="tab" class="nowrap cptable fullwidth">
                                        <asp:Repeater ID="checked_detailRepeater" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" style="background-color: #B9D3EE" id="row">
                                                    <td rowspan="3" id="wlxx" colspan="15" runat="server">
                                                        发运信息
                                                    </td>
                                                    <td rowspan="3" colspan="1">
                                                        比价结果
                                                    </td>
                                                    <td rowspan="1" colspan="18" runat="server" id="GYS">
                                                        运输单位信息
                                                    </td>
                                                </tr>
                                                <tr align="center" style="background-color: #B9D3EE">
                                                    <td rowspan="1" colspan="3" runat="server" id="gys1">
                                                        运输单位1
                                                    </td>
                                                    <td rowspan="1" colspan="3" runat="server" id="gys2">
                                                        运输单位2
                                                    </td>
                                                    <td rowspan="1" colspan="3" runat="server" id="gys3">
                                                        运输单位3
                                                    </td>
                                                    <td rowspan="1" colspan="3" runat="server" id="gys4">
                                                        运输单位4
                                                    </td>
                                                    <td rowspan="1" colspan="3" runat="server" id="gys5">
                                                        运输单位5
                                                    </td>
                                                    <td rowspan="1" colspan="3" runat="server" id="gys6">
                                                        运输单位6
                                                    </td>
                                                </tr>
                                                <tr align="left" style="background-color: #B9D3EE">
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm1">
                                                        <asp:Label ID="PM_SUPPLIERANAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIERAID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbA_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm2">
                                                        <asp:Label ID="PM_SUPPLIERBNAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIERBID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbB_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm3">
                                                        <asp:Label ID="PM_SUPPLIERCNAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIERCID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbC_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm4">
                                                        <asp:Label ID="PM_SUPPLIERDNAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIERDID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbD_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm5">
                                                        <asp:Label ID="PM_SUPPLIERENAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIEREID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbE_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                    <td rowspan="1" colspan="3" bgcolor="#FFFFCC" width="180px" runat="server" id="gysnm6">
                                                        <asp:Label ID="PM_SUPPLIERFNAME" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="PM_SUPPLIERFID" runat="server" Text="" Visible="false"></asp:Label>
                                                        <asp:Label ID="LbF_lei" runat="server" Text="" Width="20px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" style="background-color: #B9D3EE">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <strong>行号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编号</strong>
                                                    </td>
                                                    <td id="td1" runat="server">
                                                        <strong>合同号</strong>
                                                    </td>
                                                    <td id="td2" runat="server">
                                                        <strong>项目名称</strong>
                                                    </td>
                                                    <td id="td3" runat="server">
                                                        <strong>任务号</strong>
                                                    </td>
                                                    <td id="td7" runat="server">
                                                        <strong>总序</strong>
                                                    </td>
                                                    <td id="td4" runat="server">
                                                        <strong>设备名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>图号</strong>
                                                    </td>
                                                    <td id="td5" runat="server">
                                                        <strong>交货期</strong>
                                                    </td>
                                                    <td>
                                                        <strong>发货数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>吨</strong>
                                                    </td>
                                                    <td>
                                                        <strong>公里</strong>
                                                    </td>
                                                    <td id="td6" runat="server">
                                                        <strong>收货单位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>收获地址</strong>
                                                    </td>
                                                    <%--<td id="td7" runat="server">
                                                                <strong>发货内容</strong>
                                                            </td>--%>
                                                    <td>
                                                        <strong>比价结果</strong>
                                                    </td>
                                                    <td runat="server" id="dyc1">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz1">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg1">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                    <td runat="server" id="dyc2">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz2">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg2">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                    <td runat="server" id="dyc3">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz3">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg3">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                    <td runat="server" id="dyc4">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz4">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg4">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                    <td runat="server" id="dyc5">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz5">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg5">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                    <td runat="server" id="dyc6">
                                                        <strong>到货时间</strong>
                                                    </td>
                                                    <td runat="server" id="zz6">
                                                        <strong>金额（含税）</strong>
                                                    </td>
                                                    <td runat="server" id="tg6">
                                                        <strong>吨/公里</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget">
                                                    <td>
                                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                            Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="CM_FID" runat="server" Text='<%#Eval("CM_FID")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="CM_ID" runat="server" Text='<%#Eval("CM_ID")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="CM_BIANHAO" runat="server" Text='<%#Eval("CM_BIANHAO") %>'></asp:Label>
                                                    </td>
                                                    <td id="tt1" runat="server">
                                                        <asp:Label ID="CM_CONTR" runat="server" Text='<%#Eval("TSA_PJID")%>'></asp:Label>
                                                    </td>
                                                    <td id="tt2" runat="server">
                                                        <asp:Label ID="CM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                    </td>
                                                    <td id="tt3" runat="server">
                                                        <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PM_ZONGXU" runat="server" Text='<%#Eval("PM_ZONGXU")%>'></asp:Label>
                                                    </td>
                                                    <td id="tt4" runat="server">
                                                        <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%#Eval("PM_ENGNAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="TSA_MAP" runat="server" Text='<%#Eval("PM_MAP")%>'></asp:Label>
                                                    </td>
                                                    <td id="tt5" runat="server">
                                                        <asp:Label ID="CM_JHTIME" runat="server" Text='<%#Eval("CM_JHTIME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PM_FYNUM" runat="server" Text='<%#Eval("PM_FYNUM") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PM_WEIGHT" runat="server" Text='<%#Eval("PM_WEIGHT") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PM_LENGTH" runat="server" Text='<%#Eval("PM_LENGTH") %>'></asp:Label>
                                                    </td>
                                                    <td id="tt6" runat="server">
                                                        <asp:Label ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="PM_ADDRESS" runat="server" Text='<%#Eval("PM_ADDRESS") %>'></asp:Label>
                                                    </td>
                                                    <%-- <td id="tt7" runat="server">
                                                                <asp:Label ID="PM_FHDETAIL" runat="server" Text='<%#Eval("PM_FHDETAIL")%>'></asp:Label>
                                                            </td>--%>
                                                    <td id="td_supply" runat="server">
                                                        <%--<asp:Label ID="supplierid" runat="server" Text='<%#Eval("PM_SUPPLIERRESID")%>' Visible="false"></asp:Label>--%>
                                                        <%--<asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>--%>
                                                        <%#Eval("supplierresnm")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj1">
                                                        <%--<asp:Label ID="Label11" runat="server" Text='<%#Eval("PM_QOUTEFSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSA")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj1">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSA")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg11">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGA")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj2">
                                                        <%--<asp:Label ID="Label21" runat="server" Text='<%#Eval("PM_QOUTEFSTSB")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSB")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj2">
                                                        <%--<asp:Label ID="Label23" runat="server" Text='<%#Eval("PM_QOUTELSTSB")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSB")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg22">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGB")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj3">
                                                        <%--<asp:Label ID="Label31" runat="server" Text='<%#Eval("PM_QOUTEFSTSC")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSC")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj3">
                                                        <%--<asp:Label ID="Label33" runat="server" Text='<%#Eval("PM_QOUTELSTSC")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSC")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg33">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGC")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj4">
                                                        <%--<asp:Label ID="Label41" runat="server" Text='<%#Eval("PM_QOUTEFSTSD")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSD")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj4">
                                                        <%--<asp:Label ID="Label43" runat="server" Text='<%#Eval("PM_QOUTELSTSD")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSD")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg44">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGD")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj5">
                                                        <%--<asp:Label ID="Label51" runat="server" Text='<%#Eval("PM_QOUTEFSTSE")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSE")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj5">
                                                        <%--<asp:Label ID="Label53" runat="server" Text='<%#Eval("PM_QOUTELSTSE")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSE")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg55">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGE")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="dycbj6">
                                                        <%--<asp:Label ID="Label61" runat="server" Text='<%#Eval("PM_QOUTEFSTSF")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTEFSTSF")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="zzbj6">
                                                        <%--<asp:Label ID="Label63" runat="server" Text='<%#Eval("PM_QOUTELSTSF")%>'></asp:Label>--%>
                                                        <%#Eval("PM_QOUTELSTSF")%>
                                                    </td>
                                                    <td width="60px" runat="server" id="tg66">
                                                        <%--<asp:Label ID="Label13" runat="server" Text='<%#Eval("PM_QOUTELSTSA")%>'></asp:Label>--%>
                                                        <%#Eval("PM_AVGF")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="评审结果" TabIndex="1">
                        <ContentTemplate>
                            <div style="border: 1px solid #000000; height: 450px; overflow: auto">
                                <asp:Panel ID="Pan_shenheren" runat="server">
                                    <table width="100%">
                                        <asp:Panel ID="Panel_zd" runat="server">
                                            <tr>
                                                <td>
                                                    制单意见:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanyj" Columns="100" Rows="6" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server" visible="false">
                                                <td>
                                                    审核人数:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="tb_pnum" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                    制单人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_zdanren" runat="server" Enabled="False"
                                                        CssClass="text1style"></asp:TextBox>
                                                    <asp:TextBox ID="TB_zdanrenid" runat="server" Visible="false"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;制单时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_tjiaot"
                                                        runat="server" Enabled="False" CssClass="text1style"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Pan_shenheren1" runat="server" Enabled="false">
                                            <tr>
                                                <td>
                                                    市场部意见:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj1" Columns="100" Rows="3" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    审核结论:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="Rad_tongyi1" runat="server" Text="同意" GroupName="shenhe1" TextAlign="Right"
                                                        OnCheckedChanged="Rad_tongyi1_checkedchanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                            ID="Rad_butongyi1" runat="server" Text="拒绝" GroupName="shenhe1" TextAlign="Right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                    审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren1" runat="server" Enabled="false"
                                                        CssClass="text1style"></asp:TextBox>
                                                    <asp:TextBox ID="Tb_shenherencode1" runat="server" Visible="false"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet1"
                                                        runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Pan_shenheren2" runat="server">
                                            <tr>
                                                <td>
                                                    主管领导意见:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj2" Columns="100" Rows="3" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    审核结论:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="Rad_tongyi2" runat="server" Text="同意" GroupName="shenhe2" TextAlign="Right"
                                                        OnCheckedChanged="Rad_tongyi2_checkedchanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                            ID="Rad_butongyi2" runat="server" Text="拒绝" GroupName="shenhe2" TextAlign="Right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                    审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren2" runat="server" onfocus="this.blur()"
                                                        CssClass="text1style"></asp:TextBox>
                                                    <asp:Image runat="server" ID="imgSHR2" ImageUrl="../Assets/images/username_bg.gif"
                                                        onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                                    <%--<asp:TextBox ID="Tb_shenherencode2" runat="server" Visible="false"></asp:TextBox>--%>
                                                    <input type="hidden" runat="server" id="Tb_shenherencode2" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet2"
                                                        runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Pan_shenheren3" runat="server" Visible="false" Enabled="false">
                                            <tr>
                                                <td>
                                                    总经理意见:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheyj3" Columns="100" Rows="3" runat="server"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    审核结论:
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;
                                                    <asp:RadioButton ID="Rad_tongyi3" runat="server" Text="同意" GroupName="shenhe3" TextAlign="Right"
                                                        OnCheckedChanged="Rad_tongyi3_checkedchanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton
                                                            ID="Rad_butongyi3" runat="server" Text="拒绝" GroupName="shenhe3" TextAlign="Right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="border-bottom: solid 1px black;" colspan="2">
                                                    审核人:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenheren3" runat="server" Enabled="false"
                                                        CssClass="text1style"></asp:TextBox>
                                                    <asp:TextBox ID="Tb_shenherencode3" runat="server" Visible="false"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;审核时间:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="Tb_shenhet3"
                                                        runat="server" Enabled="false" CssClass="text1style"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </div>
    </div>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="07">
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
