<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_ProNum_ProfitLoss.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.CM_ProNum_ProfitLoss" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    统计分析
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="StyleFile/FixedTables.css" rel="stylesheet" type="text/css" />
    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>

    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

    <script type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 2,
                fixedCols: 4,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       $(function() {
            sTable();
        });
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper" style="width: 100%;">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="hesuanbz" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                    OnSelectedIndexChanged="hesuanbz_SelectedIndexChanged">
                                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="待核算" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已核算" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;任务号：<asp:TextBox ID="txtrwh"
                                    ForeColor="Gray" runat="server" Width="90px"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;合同号：<asp:TextBox ID="txthth" ForeColor="Gray"
                                    runat="server" Width="90px"></asp:TextBox>
                                &nbsp;&nbsp;项目名称：<asp:TextBox ID="txtxmmc" ForeColor="Gray" runat="server"
                                    Width="90px"></asp:TextBox>
                                &nbsp;&nbsp; 从：<asp:TextBox ID="tb_CXstarttime" Width="80px" runat="server"></asp:TextBox>
                                <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年,dd日" TodaysDateFormat="yyyy年MM月dd日"
                                    ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_CXstarttime">
                                </asp:CalendarExtender>
                                &nbsp;&nbsp; 到：<asp:TextBox ID="tb_CXendtime" Width="80px" runat="server"></asp:TextBox>
                                <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年,dd日" TodaysDateFormat="yyyy年MM月dd日"
                                    ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_CXendtime">
                                </asp:CalendarExtender>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCx" OnClick="btnCx_OnClick" runat="server" Text="查询"></asp:Button>
                                &nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="btn" runat="server" Text="导出" OnClick="btn_export_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btn_pl" runat="server" Text="批量导出" OnClick="btn_plexport_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="height: 405px; overflow: auto; width: 100%">
            <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center">
                            <td rowspan="2">
                                <strong>序号</strong>
                            </td>
                            <td rowspan="2">
                                <strong>任务号</strong>
                            </td>
                            <td rowspan="2">
                                <strong>合同号</strong>
                            </td>
                            <td rowspan="2">
                                <strong>项目名称</strong>
                            </td>
                            <td rowspan="2">
                                <strong>直接人工费</strong>
                            </td>
                            <td colspan="10">
                                <strong>直接材料费</strong>
                            </td>
                            <td rowspan="2">
                                <strong>制造费用</strong>
                            </td>
                            <td rowspan="2">
                                <strong>外协费用</strong>
                            </td>
                            <td rowspan="2">
                                <strong>厂内分包</strong>
                            </td>
                            <td rowspan="2">
                                <strong>运费</strong>
                            </td>
                            <td rowspan="2">
                                <strong>分交成本</strong>
                            </td>
                            <td rowspan="2">
                                <strong>成本总计</strong>
                            </td>
                            <td rowspan="2">
                                <strong>任务号开票金额</strong>
                            </td>
                            <td rowspan="2">
                                <strong>核算标志</strong>
                            </td>
                            <td rowspan="2">
                                <strong>核算时间</strong>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <strong>外购件</strong>
                            </td>
                            <td>
                                <strong>黑色金属</strong>
                            </td>
                            <td>
                                <strong>焊材类</strong>
                            </td>
                            <td>
                                <strong>铸件</strong>
                            </td>
                            <td>
                                <strong>锻件</strong>
                            </td>
                            <td>
                                <strong>轴承</strong>
                            </td>
                            <td>
                                <strong>标准件</strong>
                            </td>
                            <td>
                                <strong>油漆涂料</strong>
                            </td>
                            <td>
                                <strong>其他类</strong>
                            </td>
                            <td>
                                <strong>材料小计</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr id="row" class="baseGadget" align="center" onmouseover="this.className='highlight'"
                            onclick="javascript:change(this);" ondblclick="javascript:changeback(this);"
                            onmouseout="this.className='baseGadget'">
                            <td>
                                <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="cbxSelect"
                                    runat="server" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("PMS_TSAID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhth" runat="server" Text='<%#Eval("TSA_PJID")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbxmmc" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzjrg" runat="server" Text='<%#Eval("RWHCB_ZJRG")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwgj" runat="server" align="center" Text='<%#Eval("RWHCB_WGJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsjs" runat="server" align="center" Text='<%#Eval("RWHCB_HSJS")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhcl" runat="server" align="center" Text='<%#Eval("RWHCB_HCL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzj" runat="server" align="center" Text='<%#Eval("RWHCB_ZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbdj" runat="server" align="center" Text='<%#Eval("RWHCB_DJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzc" runat="server" align="center" Text='<%#Eval("RWHCB_ZC")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbbzj" runat="server" align="center" Text='<%#Eval("RWHCB_BZJ")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyqtl" runat="server" align="center" Text='<%#Eval("RWHCB_YQTL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbqtcl" runat="server" align="center" Text='<%#Eval("RWHCB_QTCL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbclxj" runat="server" align="center" Text='<%#Eval("RWHCB_CL")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzzfy" runat="server" align="center" Text='<%#Eval("RWHCB_ZZFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwxfy" runat="server" align="center" Text='<%#Eval("RWHCB_WXFY")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbcnfb" runat="server" align="center" Text='<%#Eval("RWHCB_CNFB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyf" runat="server" align="center" Text='<%#Eval("RWHCB_YF")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbfjcb" runat="server" align="center" Text='<%#Eval("RWHCB_FJCB")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbcbzj" runat="server" align="center" Text='<%#Eval("RWHCB_CBZJ")%>'></asp:Label>
                            </td>
                                                        <td align="center">
                                <asp:Label ID="Label2" runat="server" align="center" Text='<%#Eval("kp_money_total")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsbz" runat="server" align="center" Text='<%#Eval("CWCB_STATE")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsdate" runat="server" align="center" Text='<%#Eval("CWCB_HSDATE")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <td colspan="4" align="right">
                                合计：
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzjrghj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwgjhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhsjshj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbhclhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzjhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbdjhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzchj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbbzjhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyqtlhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbqtclhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbclhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbzzfyhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbwxfyhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbcnfbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbyfhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbfjcbhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lbcbhj" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                没有记录!</asp:Panel>
        </div>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
    </div>
</asp:Content>
