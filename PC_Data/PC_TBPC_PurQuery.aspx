<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="PC_TBPC_PurQuery.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_PurQuery"
    Title="采购计划查询" %>

<%@ Register TagName="UCPaging" TagPrefix="asp" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购计划查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 2,
                fixedCols: 4,
                onStart: function() {
                },
                onFinish: function() {
                    for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {
                        this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
                            var clicked = false;
                            var dataRow = this.sDataTable.tBodies[0].rows[i];
                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                            return function() {
                                if (clicked) {
                                    dataRow.style.backgroundColor = "#ffffff";
                                    fixedRow.style.backgroundColor = "#ffffff";
                                    clicked = false;
                                }
                                else {
                                    dataRow.style.backgroundColor = "LawnGreen";
                                    fixedRow.style.backgroundColor = "LawnGreen";
                                    clicked = true;
                                }
                            }
                        } .call(this, i);
                    }
                    return this;
                }
            });
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                数据范围：<asp:DropDownList ID="DropDownListrange" runat="server">
                                    <asp:ListItem Text="最近2000批" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="全部" Value="1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btn_ShowAll" Text="清除筛选条件" OnClick="btn_ShowAll_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="btn_Export" Text="导出" OnClick="btn_Export_Click" />
                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkzanting" runat="server" AutoPostBack="true"
                                    OnCheckedChanged="btn_search_click" />暂停物料
                            </td>
                            </td>
                            <td align="right">
                                任务列表：
                            </td>
                            <td align="left">
                                <asp:RadioButtonList runat="server" ID="rbl_xiatui" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="btn_search_click">
                                    <asp:ListItem Text="未下推" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已下推" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                采购状态：
                            </td>
                            <td align="left">
                                <asp:RadioButtonList runat="server" ID="rbl_state" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="btn_search_click">
                                    <asp:ListItem Text="分工" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="比价单" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="下订单" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="已质检" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="已入库" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div>
                    <asp:Panel runat="server" ID="panel_query">
                        <table width="100%">
                            <tr>
                                <td>
                                    计划跟踪号：
                                    <asp:TextBox ID="tb_ptcode" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp; 供应商：
                                    <asp:TextBox ID="tb_suppliernm" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    部件名称：
                                    <asp:TextBox ID="tb_PR_CHILDENGNAME" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    部件图号：
                                    <asp:TextBox ID="tb_PR_MAP" Width="80px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    国标：
                                    <asp:TextBox ID="tb_margb" runat="server" Width="80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    图号：
                                    <asp:TextBox ID="tb_PUR_TUHAO" runat="server" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    批号：
                                    <asp:TextBox ID="tb_planno" runat="server"></asp:TextBox>&nbsp;&nbsp; 物料名称：
                                    <asp:TextBox ID="tb_marnm" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;
                                    规格：
                                    <asp:TextBox ID="tb_margg" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;
                                    采购员：
                                    <asp:DropDownList ID="drp_stu" runat="server" OnSelectedIndexChanged="btn_search_click"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                     逾期未到货查看：
                                    <asp:DropDownList ID="dpldaohuostate" runat="server" OnSelectedIndexChanged="dpldaohuostate_click"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="逾期未到货" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbyqwdh" Text="" runat="server" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;
                                     逾期到货查看：
                                    <asp:DropDownList ID="dpldaohuo_hou" runat="server" OnSelectedIndexChanged="dpldaohuostate_click"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="逾期到货" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label1" Text="" runat="server" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;
                                     未购买项查看：
                                    <asp:DropDownList ID="dplweigoumai" runat="server" OnSelectedIndexChanged="dpldaohuostate_click"
                                        AutoPostBack="true">
                                        <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="未购买" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbwgm" Text="" runat="server" ForeColor="Red"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />&nbsp;&nbsp;
                                    <%--<asp:Button ID="btn_clear" runat="server" Text="清除" OnClick="btn_clear_click" />--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="height: 405px; overflow: auto; width: 100%">
                        <div class="cpbox3 xscroll">
                            <table id="tab" width="100%" align="center" class="nowrap cptable fullwidth" border="1">
                                <asp:Repeater runat="server" ID="Rep" OnItemDataBound="Rep_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                            <td>
                                            </td>
                                            <td colspan="19">
                                                <strong>基本信息</strong>
                                            </td>
                                            <td colspan="2">
                                                <strong>占用信息</strong>
                                            </td>
                                            <td colspan="3">
                                                <strong>分工信息</strong>
                                            </td>
                                            <td colspan="4">
                                                <strong>比价信息</strong>
                                            </td>
                                            <td colspan="6">
                                                <strong>订单信息</strong>
                                            </td>
                                            <td>
                                                <strong>质检信息</strong>
                                            </td>
                                            <td colspan="2">
                                                <strong>入库信息</strong>
                                            </td>
                                            <td colspan="2">
                                                <strong>MTO调整信息</strong>
                                            </td>
                                            <td>
                                                <strong>市场交货时间</strong>
                                            </td>
                                        </tr>
                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                            <td>
                                                <strong>行号</strong>
                                            </td>
                                            <td>
                                                <strong>批号</strong>
                                            </td>
                                            <td>
                                                <strong>计划跟踪号</strong>
                                            </td>
                                            <td>
                                                <strong>物料名称</strong>
                                            </td>
                                            <td>
                                                <strong>部件名称</strong>
                                            </td>
                                            <td>
                                                <strong>部件图号</strong>
                                            </td>
                                            <td>
                                                <strong>国标</strong>
                                            </td>
                                            <td>
                                                <strong>图号</strong>
                                            </td>
                                            <td>
                                                <strong>物料编码</strong>
                                            </td>
                                            <td>
                                                <strong>物料信息</strong>
                                            </td>
                                            <td>
                                                <strong>单位</strong>
                                            </td>
                                            <td>
                                                <strong>采购数量</strong>
                                            </td>
                                            <td>
                                                <strong>辅助单位</strong>
                                            </td>
                                            <td>
                                                <strong>辅助数量</strong>
                                            </td>
                                            <td>
                                                <strong>代用</strong>
                                            </td>
                                            <td>
                                                <strong>是否下推</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                            <td>
                                                <strong>上查计划</strong>
                                            </td>
                                            <td>
                                                <strong>技术员</strong>
                                            </td>
                                            <td>
                                                <strong>申请日期</strong>
                                            </td>
                                            <td>
                                                <strong>占用</strong>
                                            </td>
                                            <td>
                                                <strong>相似占用</strong>
                                            </td>
                                            <td>
                                                <strong>采购员</strong>
                                            </td>
                                            <td>
                                                <strong>是否分工</strong>
                                            </td>
                                            <td>
                                                <strong>分工日期</strong>
                                            </td>
                                            <td>
                                                <strong>是否生成比价单</strong>
                                            </td>
                                            <td>
                                                <strong>比价单日期</strong>
                                            </td>
                                            <td>
                                                <strong>比价单号</strong>
                                            </td>
                                            <td>
                                                <strong>供应商</strong>
                                            </td>
                                            <%--<td>
                                                <strong>单价</strong>
                                            </td>--%>
                                            <td>
                                                <strong>是否生成订单</strong>
                                            </td>
                                            <td>
                                                <strong>订单日期</strong>
                                            </td>
                                            <td>
                                                <strong>订单号</strong>
                                            </td>
                                            <td>
                                                <strong>交货日期</strong>
                                            </td>
                                            <td>
                                                <strong>含税单价</strong>
                                            </td>
                                            <td>
                                                <strong>含税金额</strong>
                                            </td>
                                            <td>
                                                <strong>是否质检</strong>
                                            </td>
                                            <td>
                                                <strong>是否到货</strong>
                                            </td>
                                            <td>
                                                <strong>是否入库</strong>
                                            </td>
                                            <td>
                                                <strong>调出数量</strong>
                                            </td>
                                            <td>
                                                <strong>调出辅助数量</strong>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr runat="server" id="row" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                            <td>
                                                <%#Eval("ID_Num") %>
                                                <asp:HiddenField ID="hid_ptcode" runat="server" Value='<%#Eval("ptcode")%>' />
                                                <asp:HiddenField ID="hid_prstate" runat="server" Value='<%#Eval("prstate")%>' />
                                                <asp:HiddenField ID="hid_purstate" runat="server" Value='<%#Eval("purstate")%>' />
                                                <asp:HiddenField ID="hid_planno" runat="server" Value='<%#Eval("planno")%>' />
                                                <asp:HiddenField ID="hid_cstate" runat="server" Value='<%#Eval("PUR_CSTATE")%>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="planno" runat="server" Width="150px" Text='<%#Eval("planno")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("planno")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ptcode" runat="server" Width="200px" Text='<%#Eval("ptcode")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("ptcode")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="marnm" runat="server" Width="60px" Text='<%#Eval("marnm")%>' BorderStyle="None"
                                                    Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("marnm")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PR_CHILDENGNAME" runat="server" Width="100px" Text='<%#Eval("PR_CHILDENGNAME")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("PR_CHILDENGNAME")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PR_MAP" runat="server" Width="100px" Text='<%#Eval("PR_MAP")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("PR_MAP")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="margb" runat="server" Width="60px" Text='<%#Eval("margb")%>' BorderStyle="None"
                                                    Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("margb")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="PUR_TUHAO" runat="server" Width="60px" Text='<%#Eval("PUR_TUHAO")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("PUR_TUHAO")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="marid" runat="server" Width="100px" Text='<%#Eval("marid")%>' BorderStyle="None"
                                                    Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("marid")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="marinfo" runat="server" Width="150px" Text='<%#"规格："+Eval("margg")+" ,材质："+Eval("marcz")+" ,长度（mm）："+Eval("length")+" ,宽度（mm）："+Eval("width")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#"规格："+Eval("margg")+" ,材质："+Eval("marcz")+" ,长度（mm）："+Eval("length")+" ,宽度（mm）："+Eval("width")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="marunit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="rpnum" runat="server" Text='<%#Eval("rpnum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="marfzunit" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="rpfznum" runat="server" Text='<%#Eval("rpfznum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_dy" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_DY" runat="server"></asp:Label></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lb_xt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="purnote" runat="server" Width="100px" Text='<%#Eval("purnote")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("purnote")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:HyperLink runat="server" ID="Hyp_Look" NavigateUrl='<%#"~/TM_Data/TM_MP_Require_Audit.aspx?mp_audit_id="+Eval("planno") %>'
                                                    Style="font-family: @宋体; color: #336600; font-weight: normal;" Target="_blank"
                                                    CssClass="link" ForeColor="Black">
                                                    <asp:Image ID="image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />&nbsp;上查</asp:HyperLink>
                                            </td>
                                            <td>
                                                <%# Eval("sqrnm")%>
                                            </td>
                                            <td>
                                                <%# Eval("prsqtime")%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_zy" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_ZY" runat="server"></asp:Label></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_xszy" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_XSZY" runat="server"></asp:Label></asp:HyperLink>
                                            </td>
                                            <td>
                                                <%# Eval("cgrnm")%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_fg" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_FG" runat="server" Text='<%#get_pur_fg(Eval("purstate").ToString())%>'></asp:Label></asp:HyperLink>
                                            </td>
                                            <td>
                                                <%# Eval("fgtime")%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_bjd" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_BJD" runat="server" Text='<%#get_pur_bjd(Eval("purstate").ToString())%>'></asp:Label></asp:HyperLink>
                                            </td>
                                            <td>
                                                <%#Eval("ICL_IQRDATE").ToString() == "" ? Eval("ICL_IQRDATE") : Convert.ToDateTime(Eval("ICL_IQRDATE")).ToString("yyyy-MM-dd")%>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="picno" Text='<%#Eval("picno")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="changshang"></asp:Label>
                                            </td>
                                            <%--<td>
                                                <asp:Label runat="server" ID="danjia"></asp:Label>
                                            </td>--%>
                                            <td>
                                                <asp:HyperLink ID="hp_dd" runat="server" Target="_blank">
                                                    <asp:Label ID="PUR_DD" runat="server" Text='<%#get_pur_dd(Eval("purstate").ToString())%>'></asp:Label></asp:HyperLink>
                                                <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("purstate")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("PO_ZDDATE").ToString() == "" ? Eval("PO_ZDDATE") : Convert.ToDateTime(Eval("PO_ZDDATE")).ToString("yyyy-MM-dd")%>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="orderno" Text='<%#Eval("orderno")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="cgtimerq" runat="server" Text='<%#Eval("PO_CGTIMERQ")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="ctprice" Text='<%#Eval("ctprice")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="ctamount" Text='<%#Eval("ctamount")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hp_zlbj" runat="server" Target="_blank" title="点击查看报检信息">
                                                    <asp:Label ID="zlbj" runat="server" Text='<%#get_zlbj(Eval("RESULT").ToString())%>'></asp:Label>
                                                </asp:HyperLink>
                                            </td>
                                            <td id="tdifdaohuo">
                                                <asp:Label ID="daohuoF" runat="server"></asp:Label>
                                                <asp:HiddenField runat="server" ID="PO_STATE" Value='<%#Eval("PO_STATE")%>' />
                                                <asp:HiddenField runat="server" ID="PO_ZXNUM" Value='<%#Eval("PO_ZXNUM")%>' />
                                                <asp:HiddenField runat="server" ID="PO_RECGDFZNUM" Value='<%#Eval("PO_RECGDFZNUM")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="rukuF" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OUTTZNUM" runat="server" Text='<%#Eval("OUTTZNUM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="OUTTZFZNUM" runat="server" Text='<%#Eval("OUTTZFZNUM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="CM_FHDATE" runat="server" Text='<%#Eval("CM_FHDATE")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="32" align="center">
                                        <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                                            没有数据！</asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                    <%--<table width="100%">
                        <tr>
                            <td align="left">
                                
                            </td>
                            <td>
                                筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                                合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                    </table>--%>

                    <script language="javascript" type="text/javascript">
                        var myST = new superTable("tab", {
                            cssSkin: "tDefault",
                            headerRows: 2,
                            fixedCols: 4,
                            onStart: function() {
                            },
                            onFinish: function() {
                                for (var i = 0, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {
                                    this.sDataTable.tBodies[0].rows[i].onclick = this.sFDataTable.tBodies[0].rows[i].onclick = function(i) {
                                        var clicked = false;
                                        var dataRow = this.sDataTable.tBodies[0].rows[i];
                                        var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                                        return function() {
                                            if (clicked) {
                                                dataRow.style.backgroundColor = "";
                                                fixedRow.style.backgroundColor = "";
                                                clicked = false;
                                            }
                                            else {
                                                dataRow.style.backgroundColor = "LawnGreen";
                                                fixedRow.style.backgroundColor = "LawnGreen";
                                                clicked = true;
                                            }
                                        }
                                    } .call(this, i);
                                }
                                return this;
                            }
                        });
                    </script>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
