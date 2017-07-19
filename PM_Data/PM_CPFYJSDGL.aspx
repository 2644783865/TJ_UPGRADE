<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PM_CPFYJSDGL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CPFYJSDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品发运比价单均摊
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
        //        function trClick(obj) {
        //            var length = obj.toString().length;
        //            var SHEETNO = obj.toString();
        //            alert(SHEETNO);
        //            alert(length);
        //            if (length < 8) {
        //                for (var i = 0; i < 8 - length; i++) {
        //                    SHEETNO = "0" + SHEETNO;
        //                }
        //            }
        //            alert(SHEETNO);
        //            window.open("PM_CPFYJSD.aspx?action=read&SHEETNO=" + SHEETNO);
        //        }

        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var SHEETNO = $(this).find("input[name*=JS_FATHERID]").val();
                var length = SHEETNO.toString().length;
                if (length < 8) {
                    for (var i = 0; i < 8 - length; i++) {
                        SHEETNO = "0" + SHEETNO;
                    }
                }
                window.open("PM_CPFYJSD.aspx?action=read&SHEETNO=" + SHEETNO);
            });
        })
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                按制单日期查询：从：<asp:TextBox runat="server" ID="txtZDRQ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                到：<asp:TextBox runat="server" ID="txtJZRQ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                按单据编号查询：<asp:TextBox runat="server" Width="100px" ID="txtDJBH"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearch" Text="查看" OnClick="btnSearch_btnSearch" />
                <asp:Button ID="btnShowPopup" runat="server" Text="其他筛选" OnClientClick="viewCondition()" />&nbsp;
                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                    CancelControlID="btnClose" Y="80">
                </asp:ModalPopupExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="~/PM_Data/PM_CPFYJSDOTHER.aspx?action=add"
                    Target="_blank" runat="server">
                    <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                        align="absmiddle" runat="server" />
                    添加其他运费数据</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnxtred" runat="server" Text="下推红字单据" OnClick="btnxtred_click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnexport" runat="server" Text="勾选导出" OnClick="btnexport_OnClick" />
            </div>
            <div class="box-title">
                勾稽状态：
                <asp:RadioButton ID="radio1_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radiogjstate_CheckedChanged"
                    AutoPostBack="True" Checked="true" />
                <asp:RadioButton ID="radio1_gouji" runat="server" Text="已勾稽" GroupName="shenhe" OnCheckedChanged="radiogjstate_CheckedChanged"
                    AutoPostBack="True" />
                <asp:RadioButton ID="radio1_weigouji" runat="server" Text="未勾稽" GroupName="shenhe"
                    OnCheckedChanged="radiogjstate_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 单据类型：
                <asp:DropDownList ID="drp_type" runat="server" OnSelectedIndexChanged="drp_type_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="0" Selected="True">蓝字</asp:ListItem>
                    <asp:ListItem Value="1">红字</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每页：
                <asp:DropDownList ID="ddl_pagesize" runat="server" OnSelectedIndexChanged="ddl_pagesize_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="0" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="1">50</asp:ListItem>
                    <asp:ListItem Value="2">100</asp:ListItem>
                    <asp:ListItem Value="3">200</asp:ListItem>
                </asp:DropDownList>
                行
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelCondition" runat="server" Width="75%" Style="display: none">
        <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
            <tr>
                <td colspan="7" align="center">
                    <asp:Button ID="QueryButton" runat="server" OnClick="btnSearch_btnSearch" Text="查询" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_OnClick" />&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <input type="button" id="btnClose" value="关闭" />
                </td>
            </tr>
            <tr>
                <td>
                    按任务号查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRWH"></asp:TextBox>
                </td>
                <td>
                    按合同号查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHTH"></asp:TextBox>
                </td>
                <td>
                    按收货单位查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSHDW"></asp:TextBox>
                </td>
                <td>
                    按发货商查询：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtFHS"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater runat="server" ID="rptJSDGL" OnItemDataBound="rptJSDGL_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #48D1CC; height: 30px">
                                    <th>
                                        <strong>序号</strong>
                                    </th>
                                    <th>
                                        <strong>编号</strong>
                                    </th>
                                    <th>
                                        <strong>制单人</strong>
                                    </th>
                                    <th>
                                        <strong>制单日期</strong>
                                    </th>
                                    <th>
                                        <strong>计划跟踪号</strong>
                                    </th>
                                    <th>
                                        <strong>合同号</strong>
                                    </th>
                                    <th>
                                        <strong>任务号</strong>
                                    </th>
                                    <th>
                                        <strong>总序</strong>
                                    </th>
                                    <th>
                                        <strong>发货商</strong>
                                    </th>
                                    <th>
                                        <strong>交货期</strong>
                                    </th>
                                    <th>
                                        <strong>收货单位</strong>
                                    </th>
                                    <th>
                                        <strong>发货数量</strong>
                                    </th>
                                    <th>
                                        <strong>单重</strong>
                                    </th>
                                    <th>
                                        <strong>税率</strong>
                                    </th>
                                    <th>
                                        <strong>金额（含税）</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="chk" />
                                        <asp:Label runat="server" ID="XUHAO" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:Label runat="server" ID="JS_FATHERID" Text='<%#Eval("JS_FATHERID")%>' Visible="false"></asp:Label>
                                        <input type="hidden" runat="server" id="hidJS_FATHERID" name="JS_FATHERID" value='<%#Eval("JS_FATHERID")%>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_BH" Text='<%#Eval("JS_BH")%>'></asp:Label><%--结算单号--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_ZDR" Text='<%#Eval("JS_ZDR")%>'></asp:Label><%--制单人--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_RQ" Text='<%#Eval("JS_RQ")%>'></asp:Label><%--制单时间--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_JHGZH" Text='<%#Eval("JS_JHGZH")%>'></asp:Label><%--计划跟踪号--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_HTH" Text='<%#Eval("JS_HTH")%>'></asp:Label><%--合同号--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_RWH" Text='<%#Eval("JS_RWH")%>'></asp:Label><%--任务号--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_ZX" Text='<%#Eval("JS_ZX")%>'></asp:Label><%--总序--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_GYS" Text='<%#Eval("JS_GYS")%>'></asp:Label><%--供应商--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_JHQ" Text='<%#Eval("JS_JHQ")%>'></asp:Label><%--交货期--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_SHDW" Text='<%#Eval("JS_SHDW")%>'></asp:Label><%--收货单位 --%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_BJSL" Text='<%#Eval("JS_BJSL")%>'></asp:Label><%--发货数量--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_DANZ" Text='<%#Eval("JS_DANZ")%>'></asp:Label><%--单重--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_SHUIL" Text='<%#Eval("JS_SHUIL")%>'></asp:Label><%--税率--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JS_HSJE" Text='<%#Eval("JS_HSJE")%>'></asp:Label><%--金额（含税）--%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Panel ID="NoDataPane" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <table width="100%">
                <tr>
                    <td align="center">
                        合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
