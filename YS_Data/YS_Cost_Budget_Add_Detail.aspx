<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="YS_Cost_Budget_Add_Detail.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_Add_Detail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算详情
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style>
        #tb_baseInfo
        {
            border: 0;
            width: 90%;
            margin: 0 auto;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--头部--%>
    <table width="100%">
        <tr>
            <td colspan="5" align="right" style="padding-top: 15px">
                <asp:Button ID="btn_Save" Visible="false" runat="server" Text="保存" OnClick="btn_Save_Click" />&nbsp;
                <asp:Button ID="btn_PushDown" Visible="false" runat="server" Text="下推至部门反馈" />&nbsp;
            </td>
        </tr>
    </table>
    <asp:TabContainer runat="server" ID="tab_Detail" Width="100%" ActiveTabIndex="0">
        <%--预算汇总--%>
        <asp:TabPanel runat="server" HeaderText="预算汇总" ID="TabPanel10">
            <ContentTemplate>
                <div style="height: 470px; overflow: auto; background-color: #EEF7FD; border: 5px solid #C4D9EB;">
                    <table id="tb_baseInfo" style="" cellpadding="6" cellspacing="10" class="grid" border="0"
                        frame="border">
                        <tr>
                            <th colspan="6">
                                <h3>
                                    预算基本信息</h3>
                            </th>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                任务号：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_TSA_ID" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                任务号收入：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_BUDGET_INCOME" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                任务号重量：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_WEIGHT" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                kg
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                合同号：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_CONTRACT_NO" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                项目名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PROJECTNAME" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                设备名称：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_ENGINEERNAME" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                财务制单人：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_ADDNAME" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                提交时间：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_ADDTIME" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                备注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_NOTE" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th colspan="6">
                                <hr />
                                <h3>
                                    预算费用分配</h3>
                            </th>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                材料费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_MATERIAL_COST" runat="server" Style="text-align: right" oninput="Calculate()"
                                    onkeypress="InputNumberOnly()" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                人工费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_LABOUR_COST" runat="server" Style="text-align: right" oninput="Calculate()"
                                    onkeypress="InputNumberOnly()" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                运费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_TRANS_COST" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                预算总额：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_TOTALCOST_ALL" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                毛利润：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PROFIT" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                毛利率：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PROFIT_RATE" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td class="red" style="text-align: right;">
                                自动计算：
                            </td>
                            <td class="red">
                                材料费+人工费+运费
                            </td>
                            <td>
                            </td>
                            <td class="red">
                                任务号收入 - 预算总额
                            </td>
                            <td>
                            </td>
                            <td class="red">
                                毛利润 / 任务号收入
                            </td>
                        </tr>
                        <tr>
                            <th colspan="6">
                                <hr />
                                <h3>
                                    材料费参考</h3>
                            </th>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                黑色金属：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_FERROUS_METAL" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                外购件：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PURCHASE_PART" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                油漆涂料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PAINT_COATING" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                电气电料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_ELECTRICAL" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                铸锻件：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_CASTING_FORGING_COST" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                其他材料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_OTHERMAT_COST" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                材料费小计：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_materil_history_reference" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <th colspan="6">
                                <hr />
                                <h3>
                                    部门反馈参考</h3>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <strong>材料费反馈：</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                黑色金属反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_FERROUS_METAL_FB" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                外购件反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PURCHASE_PART_FB" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                油漆涂料反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_PAINT_COATING_FB" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                电气电料反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_ELECTRICAL_FB" runat="server" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                铸锻件反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_CASTING_FORGING_COST_FB" Style="text-align: right" runat="server"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                            <td style="text-align: right">
                                其他材料反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_OTHERMAT_COST_FB" Style="text-align: right" runat="server"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                材料费反馈小计：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_materil_dispart_reference" Style="text-align: right" runat="server"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>人工费费反馈：</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                单位人工费反馈：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_YS_UNIT_LABOUR_COST_FB" runat="server" Style="text-align: right"
                                    onkeypress="InputNumberOnly()" oninput="CalculateLabourCostFB()" ReadOnly="false"></asp:TextBox>
                                元/kg
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                人工费反馈小计：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_labour_dispart_reference" runat="server" Style="text-align: right"
                                    ReadOnly="True"></asp:TextBox>
                                元
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--黑色金属--%>
        <asp:TabPanel runat="server" HeaderText="黑色金属" ID="TabPanel1">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_FERROUS_METAL" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码（01.07）
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量（吨）
                                        </th>
                                        <th>
                                            预算单价（元/吨）
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价（元/吨）
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_FERROUS_METAL_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_FERROUS_METAL_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_FERROUS_METAL_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_FERROUS_METAL_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_FERROUS_METAL_info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label16" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label17" runat="server"></asp:Label>
                                </th>
                                <th>
                                    黑色金属预算总价合计：<asp:Label ID="lb_YS_FERROUS_METAL_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    黑色金属反馈总价合计：<asp:Label ID="lb_YS_FERROUS_METAL_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_FERROUS_METAL" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--外购件--%>
        <asp:TabPanel runat="server" HeaderText="外 购 件" ID="TabPanel2">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_PURCHASE_PART" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码（01.11）
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量（台、套）
                                        </th>
                                        <th>
                                            预算单价（元/台、套）
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价（元/台、套）
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_PURCHASE_PART_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_PURCHASE_PART_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_PURCHASE_PART_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_PURCHASE_PART_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_PURCHASE_PART_Info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label18" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label19" runat="server"></asp:Label>
                                </th>
                                <th>
                                    外购件预算总价合计：<asp:Label ID="lb_YS_PURCHASE_PART_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    外购件反馈总价合计：<asp:Label ID="lb_YS_PURCHASE_PART_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_PURCHASE_PART" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--油漆涂料--%>
        <asp:TabPanel runat="server" HeaderText="油漆涂料" ID="TabPanel4">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_PAINT_COATING" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码（01.15）
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量
                                        </th>
                                        <th>
                                            预算单价
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_PAINT_COATING_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_PAINT_COATING_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_PAINT_COATING_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_PAINT_COATING_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_PAINT_COATING_Info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label22" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label23" runat="server"></asp:Label>
                                </th>
                                <th>
                                    油漆涂料预算总价合计：<asp:Label ID="lb_YS_PAINT_COATING_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    油漆涂料反馈总价合计：<asp:Label ID="lb_YS_PAINT_COATING_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_PAINT_COATING" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--电气电料--%>
        <asp:TabPanel runat="server" HeaderText="电气电料" ID="TabPanel5">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_ELECTRICAL" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码（01.03）
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量
                                        </th>
                                        <th>
                                            预算单价
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_ELECTRICAL_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_ELECTRICAL_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_ELECTRICAL_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_ELECTRICAL_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_ELECTRICAL_Info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label26" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label27" runat="server"></asp:Label>
                                </th>
                                <th>
                                    电气电料预算总价合计：<asp:Label ID="lb_YS_ELECTRICAL_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    电气电料反馈总价合计：<asp:Label ID="lb_YS_ELECTRICAL_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_ELECTRICAL" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--铸锻件--%>
        <asp:TabPanel runat="server" HeaderText="铸锻件" ID="TabPanel7">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_CASTING_FORGING_COST" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码（01.08、01.09）
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量（个）
                                        </th>
                                        <th>
                                            理论重量（kg/个）
                                        </th>
                                        <th>
                                            预算单价（元/kg）
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价（元/kg）
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_WEIGHT" runat="server" Text='<%# Eval("YS_WEIGHT") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_CASTING_FORGING_COST_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_CASTING_FORGING_COST_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_WEIGHT").ToString(),Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_CASTING_FORGING_COST_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_CASTING_FORGING_COST_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_WEIGHT").ToString(),Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_CASTING_FORGING_COST_Info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label30" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label31" runat="server"></asp:Label>
                                </th>
                                <th>
                                    铸锻件预算总价合计：<asp:Label ID="lb_YS_CASTING_FORGING_COST_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    铸锻件反馈总价合计：<asp:Label ID="lb_YS_CASTING_FORGING_COST_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_CASTING_FORGING_COST" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--其他材料--%>
        <asp:TabPanel runat="server" HeaderText="其他材料" ID="TabPanel6">
            <ContentTemplate>
                <div style="overflow: auto; height: 470px;">
                    <div style="overflow: auto; height: 430px;">
                        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="nowrap toptable grid"
                            border="1" frame="border">
                            <asp:Repeater ID="rpt_YS_OTHERMAT_COST" runat="server">
                                <HeaderTemplate>
                                    <tr class="tableTitle" style="background-color: #B9D3EE">
                                        <th>
                                            序号
                                        </th>
                                        <th>
                                            物料代码
                                        </th>
                                        <th>
                                            物料名称
                                        </th>
                                        <th>
                                            数量
                                        </th>
                                        <th>
                                            预算单价
                                        </th>
                                        <th>
                                            预算总价（元）
                                        </th>
                                        <th>
                                            反馈单价
                                        </th>
                                        <th>
                                            反馈总价（元）
                                        </th>
                                        <th>
                                            说明
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label1" runat="server" Text="<%#Container.ItemIndex+1%>"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_CODE" runat="server" Text='<% #Eval("YS_CODE") %>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lb_YS_NAME" runat="server" Text='<% #Eval("YS_NAME") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_Union_Amount" runat="server" Text='<%# Eval("YS_Union_Amount") %>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_OTHERMAT_COST_Average_Price" runat="server" Text='<%# Eval("YS_Average_Price","{0:f4}")%>'></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lb_YS_OTHERMAT_COST_SUBTOTAL_Price" runat="server" Text='<%# GetProduct(Eval("YS_Average_Price").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_OTHERMAT_COST_Average_Price_FB"
                                                onkeypress="InputNumberOnly()" Text='<%# Eval("YS_Average_Price_FB") %>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_OTHERMAT_COST_SUBTOTAL_Price_FB"
                                                Text='<%# GetProduct(Eval("YS_Average_Price_FB").ToString(),Eval("YS_Union_Amount").ToString())%>'></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox Style="text-align: right" runat="server" ID="txt_YS_NOTE" Text='<%# Eval("YS_NOTE") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <asp:Panel ID="pal_YS_OTHERMAT_COST_Info" runat="server">
                        <table width="100%" style="background-color: #EEF7FD; margin-top: 8px;">
                            <tr>
                                <th>
                                    反馈人：<asp:Label ID="Label2" runat="server"></asp:Label>
                                </th>
                                <th>
                                    反馈时间：<asp:Label ID="Label3" runat="server"></asp:Label>
                                </th>
                                <th>
                                    其他材料预算总价合计：<asp:Label ID="lb_YS_OTHERMAT_COST_TOTAL_Price" runat="server"></asp:Label>
                                    元
                                </th>
                                <th>
                                    其他材料反馈总价合计：<asp:Label ID="lb_YS_OTHERMAT_COST_TOTAL_Price_FB" runat="server"></asp:Label>
                                    元
                                </th>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pal_No_YS_OTHERMAT_COST" runat="server" Visible="false">
                        该任务号无该类物料!</asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--审核详情--%>
        <asp:TabPanel runat="server" HeaderText="审核与反馈" ID="TabPanel11">
            <ContentTemplate>
                <div style="overflow: auto; height: 450px; background-color: #EEF7FD; border: 5px solid #C4D9EB;
                    padding-top: 10px;">
                    <div style="text-align: right">
                        <asp:Button ID="btn_RebutToCaiWu" Visible="False" runat="server" Text="驳回至财务填写" />
                        <asp:Button ID="btn_RebutToCaiGou" Visible="False" runat="server" Text="驳回至采购反馈" />
                        <asp:Button ID="btn_RebutToShengChan" Visible="False" runat="server" Text="驳回至生产反馈" />
                        <asp:Button ID="btn_Submit" Visible="False" runat="server" Text="提交审批" />&nbsp;&nbsp;&nbsp;
                    </div>
                    <div style="width: 90%; margin: 0 auto;">
                        <asp:Panel ID="Panel3" runat="server">
                            <h3 style="text-align: center">
                                采购部</h3>
                            <table width="100%">
                                <tr>
                                    <td>
                                        反馈人：<asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        反馈结论：<asp:RadioButtonList ID="rdl_CaiGouCheck" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="rdl_CaiGouCheck_SelectedIndexChanged">
                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        反馈时间：<asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox31" runat="server" TextMode="MultiLine" Width="99.5%" Height="50px"
                                            Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel4" runat="server">
                            <h3 style="text-align: center">
                                生产部</h3>
                            <table width="100%">
                                <tr>
                                    <td>
                                        反馈人：<asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        反馈结论：<asp:RadioButtonList ID="rdl_ShengChanCheck" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="rdl_ShengChanCheck_SelectedIndexChanged">
                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        反馈时间：<asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox32" runat="server" TextMode="MultiLine" Width="99.5%" Height="50px"
                                            Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                            <h3 style="text-align: center">
                                财务部</h3>
                            <table width="100%">
                                <tr>
                                    <td>
                                        审批人：<asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        审核结论：<asp:RadioButtonList ID="rdl_CaiWuCheck" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="rdl_CaiWuCheck_SelectedIndexChanged">
                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        审核时间：<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Width="99.5%" Height="50px"
                                            Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server">
                            <h3 style="text-align: center">
                                领导审核</h3>
                            <table width="100%">
                                <tr>
                                    <td>
                                        审批人：<asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        审核结论：<asp:RadioButtonList ID="rdl_YiJiCheck" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="rdl_YiJiCheck_SelectedIndexChanged">
                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        审核时间：<asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine" Width="99.5%" Height="50px"
                                            Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" style="margin-top: 10px;">
                                <tr>
                                    <td>
                                        审批人：<asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td>
                                        审核结论：<asp:RadioButtonList ID="rdl_ErJiCheck" runat="server" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="rdl_ErJiCheck_SelectedIndexChanged">
                                            <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>
                                        审核时间：<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="TextBox6" runat="server" TextMode="MultiLine" Width="99.5%" Height="50px"
                                            Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

    <script src="../JS/jquery/jquery-3.2.1.min.js" type="text/javascript"></script>

    <script src="YS_Data_P.js" type="text/javascript"></script>

    <script type="text/javascript">
        
        //材料费或人工费更改时，计算预算总额、毛利润、毛利率，绑定到oninput事件
        function Calculate() {
            var a = parseFloat($('#<%=txt_YS_MATERIAL_COST.ClientID %>').val()) + parseFloat($('#<%=txt_YS_LABOUR_COST.ClientID %>').val()) + parseFloat($('#<%=txt_YS_TRANS_COST.ClientID %>').val());
            if (!isNaN(a)) {
                $('#<%=txt_YS_TOTALCOST_ALL.ClientID %>').val((parseFloat($('#<%=txt_YS_MATERIAL_COST.ClientID %>').val()) + parseFloat($('#<%=txt_YS_LABOUR_COST.ClientID %>').val()) + parseFloat($('#<%=txt_YS_TRANS_COST.ClientID %>').val())).toFixed(4));
                $('#<%=txt_YS_PROFIT.ClientID %>').val((parseFloat($('#<%=txt_YS_BUDGET_INCOME.ClientID %>').val()) - parseFloat($('#<%=txt_YS_TOTALCOST_ALL.ClientID %>').val())).toFixed(4));
                if (parseFloat($('#<%=txt_YS_BUDGET_INCOME.ClientID %>').val()) != 0) {
                    $('#<%=txt_YS_PROFIT_RATE.ClientID %>').val((parseFloat($('#<%=txt_YS_PROFIT.ClientID %>').val()) * 100 / parseFloat($('#<%=txt_YS_BUDGET_INCOME.ClientID %>').val())).toFixed(2));

                } else {
                    $('#<%=txt_YS_PROFIT_RATE.ClientID %>').val("任务号收入为0")

                }
            }
        }


        //单位人工费反馈更改时，计算人工费小计（=单位人工费*任务号重量），绑定到oninput事件
        function CalculateLabourCostFB() {
            $('#<%=txt_labour_dispart_reference.ClientID %>').val((parseFloat($('#<%=txt_YS_WEIGHT.ClientID %>').val()) * parseFloat($('#<%=txt_YS_UNIT_LABOUR_COST_FB.ClientID %>').val())).toFixed(4));
        }
    </script>

</asp:Content>
