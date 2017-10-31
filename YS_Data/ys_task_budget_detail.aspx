<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true"
    CodeBehind="ys_task_budget_detail.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.ys_task_budget_detail"
    Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务预算详情
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <%--<style type="text/css">
        th
        {
            border-bottom: 0;
            border-style: dotted;
        }
        .top-table
        {
            width: 100%;
        }
    </style>--%>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table style="width:100%">
            <tr>
                <td>
                    任务号：
                </td>
                <td>
                    合同号：
                </td>
                <td>
                    项目号：
                </td>
                <td>
                    重 量：
                </td>
            </tr>
            <tr>
                <td>
                材料费预算：
                </td>
                <td>
                人工费预算：
                </td>
                <td>
                厂内分包预算：
                </td>
                <td>
                生产外协预算：
                </td>
            </tr>
        </table>
    <asp:TabContainer runat="server" ID="tab_Detail" Width="100%" ActiveTabIndex="1">
        
        <%--预算汇总--%>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="预算汇总信息">
            <ContentTemplate>
            </ContentTemplate>
        </asp:TabPanel>
        <%--黑色金属明细--%>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="黑色金属明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_ferrous' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.07）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        历史单价（元）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_ferrous" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            黑色金属历史参考：<asp:Label ID="lb_ferrous_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            黑色金属部门反馈：<asp:TextBox ID="txt_ferrous_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_ferrous_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb_ferrous_end_time" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_ferrous_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--外购件明细--%>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="外购件明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_purchase' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.11）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        历史单价（元）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_purchase" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            外购件历史参考：<asp:Label ID="lb_purchase_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            外购件部门反馈：<asp:TextBox ID="txt_purchase_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_purchase_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb_purchase_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_purchase_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--油漆涂料明细--%>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="油漆涂料明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_paint' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.15）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        历史单价（元）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_paint" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            油漆涂料历史参考：<asp:Label ID="lb_paint_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            油漆涂料部门反馈：<asp:TextBox ID="txt_paint_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_paint_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb_paint_edntime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_paint_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--电器电料明细--%>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="电器电料明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_electrical' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.03）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        历史单价（元）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_electrical" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            电器电料历史参考：<asp:Label ID="lb_electrical_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            电器电料部门反馈：<asp:TextBox ID="txt_electrical_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_electrical_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb__electrical_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_electrical_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--铸锻件明细--%>
        <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="铸锻件明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_casting' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.08、01.09）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        单件重量（kg）
                                    </th>
                                    <th>
                                        历史单价（元/kg）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("weight").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_casting" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            铸锻件历史参考：<asp:Label ID="lb_casting_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            铸锻件部门反馈：<asp:TextBox ID="txt_casting_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_casting_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb_casting_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_casting_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--其他材料明细--%>
        <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="其他材料明细">
            <ContentTemplate>
                <div style="height: 400px; overflow: auto">
                    <asp:Repeater ID='rpt_other' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                <tr class="tableTitle">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        物料编码（01.07）
                                    </th>
                                    <th>
                                        物料名称
                                    </th>
                                    <th>
                                        规格
                                    </th>
                                    <th>
                                        材质
                                    </th>
                                    <th>
                                        单位
                                    </th>
                                    <th>
                                        需用数量
                                    </th>
                                    <th>
                                        历史单价（元）
                                    </th>
                                    <th>
                                        预计总价（元）
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'" height="30px">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>
                                <td>
                                    <%#Eval("material_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("standard").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("quality").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("unit").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td align="right">
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_other" runat="server" Visible="false">
                        <div style="margin: 180px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，部门反馈请填写 0</div>
                    </asp:Panel>
                </div>
                <table width="100%" style="margin-top: 8px; font-size: xx-large">
                    <tr>
                        <td>
                            其他材料历史参考：<asp:Label ID="lb_other_his" runat="server"></asp:Label>
                            元
                        </td>
                        <td>
                            其他材料部门反馈：<asp:TextBox ID="txt_other_dep" runat="server"></asp:TextBox>
                            元
                        </td>
                        <td>
                            反馈人：<asp:Label ID="lb_other_user" runat="server"></asp:Label>
                        </td>
                        <td>
                            反馈时间：<asp:Label ID="lb_other_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btn_other_submit" runat="server" Text=" 提 交 " />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
