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
    <style type="text/css">
        table
        {
            width: 100%;
        }
        .number
        {
            text-align: right;
        }
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td>
                任务号：<asp:Label ID="lb_task_code" runat="server"></asp:Label>
            </td>
            <td>
                合同号：<asp:Label ID="lb_contract_code" runat="server"></asp:Label>
            </td>
            <td>
                项目名称：<asp:Label ID="lb_project_name" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                任务号图纸总重：kg
            </td>
        </tr>
    </table>
    <asp:TabContainer runat="server" ID="tab_Detail" Width="100%" ActiveTabIndex="1">
        <%--预算汇总--%>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="预算汇总信息">
            <ContentTemplate>
                <table>
                    <tr>
                        <th colspan='10'>
                            任务号预算分配
                        </th>
                    </tr>
                    <tr>
                        <td>
                            总预算：
                        </td>
                        <td>
                            <asp:Label ID="lb_c_total_task_budget" runat="server"></asp:Label>
                        </td>
                        <td>
                            材料费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_total_material_budget" runat="server" class='number'></asp:TextBox>
                            元
                        </td>
                        <td>
                            人工费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_labour_budget" runat="server" class='number'></asp:TextBox>
                            元
                        </td>
                        <td>
                            分包费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_teamwork_budget" runat="server" class='number'></asp:TextBox>
                            元
                        </td>
                        <td>
                            外协费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_coopreative_budget" runat="server" class='number'></asp:TextBox>
                            元
                        </td>
                    </tr>
                    <tr>
                        <th colspan='10'>
                            <hr />
                            同类型任务号预算信息
                        </th>
                    </tr>
                    <tr>
                        <th colspan='10'>
                            <div style="height: 350px; overflow: auto">
                                <asp:Repeater ID='rpt_type' runat="Server">
                                    <HeaderTemplate>
                                        <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                                            <tr class="tableTitle">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    任务号
                                                </th>
                                                <th>
                                                    合同号
                                                </th>
                                                <th>
                                                    项目名称
                                                </th>
                                                <th>
                                                    设备名称
                                                </th>
                                                <th>
                                                    预算总额
                                                </th>
                                                <th>
                                                    材料费
                                                </th>
                                                <th>
                                                    人工费
                                                </th>
                                                <th>
                                                    分包费
                                                </th>
                                                <th>
                                                    外协费
                                                </th>
                                                <th>
                                                    操作
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
                                                <%#Eval("task_code").ToString()%>
                                            </td>
                                            <td>
                                                <%#Eval("contract_code").ToString()%>
                                            </td>
                                            <td>
                                                <%#Eval("project_name").ToString()%>
                                            </td>
                                            <td>
                                                <%#Eval("equipment_name").ToString()%>
                                            </td>
                                            <td>
                                                <%#Eval("c_total_task_budget").ToString()%>
                                            </td>
                                            <td class='number'>
                                                <%#Eval("total_material_budget").ToString()%>
                                            </td>
                                            <td class='number'>
                                                <%#Eval("direct_labour_budget").ToString()%>
                                            </td>
                                            <td class='number'>
                                                <%#Eval("sub_teamwork_budget").ToString()%>
                                            </td>
                                            <td class='number'>
                                                <%#Eval("cooperative_product_budget").ToString()%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ys_task_budget_detail.aspx?tsak_code="+Eval("task_code").ToString()%>'
                                                    Target="_blank" runat="server">
                                                    <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    查看</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Panel ID="pal_no_type" runat="server" Visible="false">
                                    <div style="margin-top: 150px; color: red; font-size: large">
                                        暂无同类型且已完结的任务号</div>
                                </asp:Panel>
                            </div>
                        </th>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--分工与反馈--%>
        <asp:TabPanel ID="TabPanel8" runat="server" HeaderText="分工与反馈">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr>
                            <th colspan='8'>
                                <h2>
                                    生产部反馈</h2>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                人 &nbsp;工 费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_labour_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td colspan='2'>
                                <asp:TextBox ID="txt_node_labour_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_labour_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_labour_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button1" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                分 &nbsp;包 费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_teamwork_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td colspan='2'>
                                <asp:TextBox ID="txt_node_teamwork_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_teamwork_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_teamwork_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button2" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                外 &nbsp;协 费：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_cooperative_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td colspan='2'>
                                <asp:TextBox ID="txt_node_cooperative_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_cooperative_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_cooperative_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button3" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <th colspan='8'>
                                <h2>
                                    采购部反馈</h2>
                            </th>
                        </tr>
                         <tr>
                            <td>
                                材料费合计：
                            </td>
                            <td>
                                <asp:Label ID="lb_c_total_material_dep" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                材料费参考值合计：
                            </td>
                            <td>
                                <asp:Label ID="lb_c_total_material_his" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                黑色金属：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_ferrous_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_ferrous_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_ferrous_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_ferrous_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_ferrous_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button4" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                外 &nbsp;购 件：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_purchasepart_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_purchasepart_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_purchasepart_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_purchasepart_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_purchasepart_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button5" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                油漆涂料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_paint_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_paint_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_paint_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_paint_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_paint_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button6" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                电器电料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_electrical_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_electrical_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_electrical_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_electrical_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_electrical_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button7" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                铸 &nbsp;锻 件：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_casting_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_casting_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_casting_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_casting_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_casting_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button8" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td>
                                其他材料：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_othermat_dep" runat="server" class='number'></asp:TextBox>
                                元
                            </td>
                            <td>
                                备 注：
                            </td>
                            <td>
                                <asp:TextBox ID="txt_node_othermat_dep_note" runat="server" Width='300px'></asp:TextBox>
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_othermat_his" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_othermat_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：
                                <asp:DropDownList ID="ddl_node_othermat_dep_user_name" runat="server">
                                </asp:DropDownList>
                            </td>
                            <th>
                                <asp:Button ID="Button9" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                       
                    </table>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
        <%--黑色金属明细--%>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="黑色金属明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td class='number'>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_ferrous" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--外购件明细--%>
        <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="外购件明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_purchase" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--油漆涂料明细--%>
        <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="油漆涂料明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td class='number'>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_paint" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--电器电料明细--%>
        <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="电器电料明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td class='number'>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_electrical" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--铸锻件明细--%>
        <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="铸锻件明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元/kg）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td class='number'>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("weight").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_casting" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--其他材料明细--%>
        <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="其他材料明细">
            <ContentTemplate>
                <div style="height: 450px; overflow: auto">
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
                                        参考单价（元）
                                    </th>
                                    <th>
                                        参考总价（元）
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
                                <td class='number'>
                                    <%#Eval("amount").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("unit_price").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("c_total_cost").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pal_no_other" runat="server" Visible="false">
                        <div style="margin: 150px 450px 200px; color: red; font-size: large">
                            该任务号目前没有此类物料，采购反馈请填写 0</div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
