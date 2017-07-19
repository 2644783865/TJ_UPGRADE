<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CB_MonthSummary_Detail1.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.FM_Data.CB_MonthSummary_Detail1" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    成本统计—跨期查询
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <link href="FixTable.css" rel="stylesheet" type="text/css" />
    <script src="StyleFile/superTables_compressed.js" type="text/javascript"></script>
    <link href="StyleFile/superTables_compressed.css" rel="stylesheet" type="text/css" />
<asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "sDefault",
                headerRows: 2,
                fixedCols: 2,
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
    <div class="box-wrapper" style="width:100%;">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <span><strong>查询区间：</strong></span>
                                <asp:Label ID="tbstart" runat="server"></asp:Label>至
                                <asp:Label ID="tbend" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn" runat="server" Text="导出到EXCEL" OnClick="btn_export_Click" />
                            </td>
                            <td>
                            <asp:Button ID="btn_pl" runat="server" Text="批量导出" OnClick="btn_plexport_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="height: 405px; overflow: auto;width:100%">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center">
                                <th rowspan="2">
                                    序号
                                </th>
                                <th rowspan="2">
                                    任务号
                                </th>
                                <%--<th rowspan="2">
                                    工资
                                </th>--%>
                                <th colspan="3">
                                    直接人工费
                                </th>
                                <th colspan="10">
                                    直接材料费
                                </th>
                                <th colspan="3">
                                    制造费用
                                </th>
                                <th rowspan="2">
                                    外协费用
                                </th>
                                <th rowspan="2">
                                    厂内分包
                                </th>
                                <th rowspan="2">
                                    运费
                                </th>
                                <th rowspan="2">
                                    分交成本(油漆)
                                </th>
                                <th rowspan="2">
                                    其他
                                </th>
                            </tr>
                            <tr>
                                <th>
                                 机加费用
                                </th>
                                <th>
                                 厂内结构费用
                                </th>
                                <th>
                                 小计
                                </th>
                                
                                <th>
                                    外购件
                                </th>
                                <th>
                                    黑色金属
                                </th>
                                <th>
                                    焊材类
                                </th>
                                
                                <th>
                                    铸件
                                </th>
                                <th>
                                    锻件
                                </th>
                                <th>
                                    轴承
                                </th>
                                <th>
                                    标准件
                                </th>
                                <th>
                                    油漆涂料
                                </th>
                                <th>
                                    其他类
                                </th>
                                <th>
                                    材料小计
                                </th>
                                <th>
                                    固定制造费用
                                </th>
                                <th>
                                    可变制造费用
                                </th>
                                <th>
                                    期间制造费用小计
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr id="row" class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="cbxSelect"
                                        runat="server" />
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjrwh" runat="server" Text='<%#Eval("QJTSAID")%>'></asp:Label>
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbqjgz" runat="server" Width="90px" Text='<%#Eval("QJGZ")%>'></asp:Label>
                                </td>--%>
                                <td align="center">
                                    <asp:Label ID="lbqjjjfy" runat="server" Width="90px" Text='<%#Eval("QJJJFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjjgyzfy" runat="server" Width="90px" Text='<%#Eval("QJJGYZFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzjrgf" runat="server" Width="90px" Text='<%#Eval("QJZJRGFXJ")%>'></asp:Label>
                                </td>
                                
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbqjwgj" runat="server" Width="90px" align="center" Text='<%#Eval("QJWGJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjhsjs" runat="server" Width="90px" align="center" Text='<%#Eval("QJHSJS")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjhcl" runat="server" Width="90px" align="center" Text='<%#Eval("QJHCL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzj" runat="server" Width="90px" align="center" Text='<%#Eval("QJZJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjdj" runat="server" Width="90px" align="center" Text='<%#Eval("QJDJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzc" runat="server" Width="90px" align="center" Text='<%#Eval("QJZC")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjbzj" runat="server" Width="90px" align="center" Text='<%#Eval("QJBZJ")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjyqtl" runat="server" Width="90px" align="center" Text='<%#Eval("QJYQTL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjqtcl" runat="server" Width="90px" align="center" Text='<%#Eval("QJQTCL")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjclxj" runat="server" Width="90px" align="center" Text='<%#Eval("QJCLXJ")%>'></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbqjgdzzfy" runat="server" Width="90px" align="center" Text='<%#Eval("QJGDZZFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjkbzzfy" runat="server" Width="90px" align="center" Text='<%#Eval("QJKBZZFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzzfyxj" runat="server" Width="90px" align="center" Text='<%#Eval("QJZZFYXJ")%>'></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbqjwxfy" runat="server" Width="90px" align="center" Text='<%#Eval("QJWXFY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjcnfb" runat="server" Width="90px" align="center" Text='<%#Eval("QJCNFB")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjyf" runat="server" Width="90px" align="center" Text='<%#Eval("QJYF")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjfjcb" runat="server" Width="90px" align="center" Text='<%#Eval("QJFJCB")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjqt" runat="server" Width="90px" align="center" Text='<%#Eval("QJQT")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="2" align="right">
                                    合计：
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbqjgzhj" runat="server"></asp:Label>
                                </td>--%>
                                <td align="center">
                                    <asp:Label ID="lbqjjjfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjjgyzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzjrgfhj" runat="server"></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbqjwgjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjhsjshj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjhclhj" runat="server"></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbqjzjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjdjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzchj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjbzjhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjyqtlhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjqtclhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjclhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjgdzzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjkbzzfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjzzfyhj" runat="server"></asp:Label>
                                </td>
                                
                                <td align="center">
                                    <asp:Label ID="lbqjwxfyhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjcnfbhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjyfhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjfjcbhj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbqjqthj" runat="server"></asp:Label>
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
