<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true"
    CodeBehind="ys_task_budget_detail.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.ys_task_budget_detail"
    Title="任务号预算详情" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务预算详情
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link href="../JS/EasyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../JS/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .mytable
        {
            width: 100%;
            background-color: #EEF7FD;
        }
        .number
        {
            text-align: right;
        }
        span.number
        {
            display: inline-block;
            padding-right: 6px;
            font-size: 13px;
            width: 123px;
        }
        input.number
        {
            width: 120px;
        }
        .mygrid
        {
            border-collapse: collapse;
        }
        .mygrid th, .mygrid td
        {
            border: solid 1px #B3CDE8;
        }
        .cklContainer
        {
            width: 50%;
            text-align: right;
        }
        .myCkl
        {
            display: inline;
            vertical-align: -11px;
            margin-right: 30px;
        }
        .tab-container
        {
            overflow: auto;
            height: 490px;
        }
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table width="100%">
        <tr>
            <td>
                任务号：<asp:Label ID="lb_task_code" runat="server"></asp:Label>
            </td>
            <td>
                合同号：<asp:Label ID="lb_contract_code" runat="server"></asp:Label>
            </td>
            <td>
                项目名称：<asp:Label ID="lb_project_name" runat="server"></asp:Label>
            </td>
            <td>
                任务号图纸总重：<asp:Label ID="lb_task_weight" runat="server"></asp:Label>
                kg
            </td>
        </tr>
    </table>
    <asp:TabContainer runat="server" ID="tab_Detail" Width="100%" ActiveTabIndex="0">
        <%--预算汇总--%>
        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="预算汇总信息">
            <ContentTemplate>
                <table class="mytable" id="tb_budget">
                    <tr>
                        <th colspan='11'>
                            任务号预算分配
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <b>下 发：</b> 总额
                        </td>
                        <td>
                            <asp:Label ID="lb_c_total_task_budget_pre" runat="server" class='number'></asp:Label>元
                        </td>
                        <td>
                            材料费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_total_material_budget_pre" runat="server" class='number budget_pre'
                                Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            人工费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_labour_budget_pre" runat="server" class='number budget_pre'
                                Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            分包费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_teamwork_budget_pre" runat="server" class='number budget_pre'
                                Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            外协费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_coopreative_budget_pre" runat="server" class='number budget_pre'
                                Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            <asp:Button ID="btn_budget_submit" runat="server" Text=" 提 交 " OnClick="btn_budget_submit_Click"
                                Visible="false" class="myButton" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>调 整：</b> 总额
                        </td>
                        <td>
                            <asp:Label ID="lb_c_total_task_budget" runat="server" class='number'></asp:Label>元
                        </td>
                        <td>
                            材料费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_total_material_budget" runat="server" class='number budget'
                                Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            人工费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_labour_budget" runat="server" class='number budget' Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            分包费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_teamwork_budget" runat="server" class='number budget' Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            外协费：
                        </td>
                        <td>
                            <asp:TextBox ID="txt_coopreative_budget" runat="server" class='number budget' Enabled="false"></asp:TextBox>
                            元
                        </td>
                        <td>
                            <asp:Button ID="btn_budget_adjust" runat="server" Text=" 提 交 " OnClick="btn_budget_adjust_Click"
                                Visible="false" class="myButton" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>差 额：</b> 调整-下发
                        </td>
                        <td>
                            <span id="span_budget_mis" class="number"></span>元
                        </td>
                        <td>
                        </td>
                        <td>
                            <span id="span_material_mis" class="number"></span>元
                        </td>
                        <td>
                        </td>
                        <td>
                            <span id="span_labour_mis" class="number"></span>元
                        </td>
                        <td>
                        </td>
                        <td>
                            <span id="span_teamwork_mis" class="number"></span>元
                        </td>
                        <td>
                        </td>
                        <td>
                            <span id="span_cooperative_mis" class="number"></span>元
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <th colspan='11'>
                            <hr />
                            同类型任务号预算信息
                        </th>
                    </tr>
                    <tr>
                        <th colspan='11'>
                            <div style="height: 410px; overflow: auto">
                                <asp:Repeater ID='rpt_type' runat="server">
                                    <HeaderTemplate>
                                        <table align="center" cellpadding="4" cellspacing="1" border="1" class="mytable toptable mygrid">
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
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"ys_task_budget_detail.aspx?tsak_code="+Eval("task_code").ToString()%>'
                                                    Target="_blank">
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
                                <asp:Panel ID="pal_no_type" runat="server" Visible="False">
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
                <div class="tab-container">
                    <table class="mytable">
                        <tr>
                            <th colspan='6'>
                                <h2>
                                    生产部反馈</h2>
                            </th>
                        </tr>
                        <tr>
                            <td colspan='3'>
                                <b>人 &nbsp;工 费：</b><asp:TextBox ID="txt_labour_dep" runat="server" class='number'
                                    Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_labour_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_labour_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="proName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_labour_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_labour"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_labour_dep" runat="server" Text=" 确 定 " OnClick="btn_labour_dep_Click"
                                    class="myButton" />
                                <asp:Button Visible="false" ID="btn_production_divide" runat="server" Text=" 确认分工 "
                                    OnClientClick="return btn_production_divide_click();" OnClick="btn_production_divide_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_labour_dep_note" runat="server" TextMode="MultiLine" Width="99.5%"
                                    Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan='3'>
                                <b>分 &nbsp;包 费：</b><asp:TextBox ID="txt_teamwork_dep" runat="server" class='number'
                                    Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_teamwork_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_teamwork_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="proName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_teamwork_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_teamwork"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_teamwork_dep" runat="server" Text=" 确 定 " OnClick="btn_teamwork_dep_Click"
                                    class="myButton" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_teamwork_dep_note" runat="server" TextMode="MultiLine"
                                    Width="99.5%" Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan='3'>
                                <b>外 &nbsp;协 费：</b><asp:TextBox ID="txt_cooperative_dep" runat="server" class='number'
                                    Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_cooperative_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_cooperative_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="proName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_cooperative_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_cooperative"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_cooperative_dep" runat="server" Text=" 确 定 "
                                    class="myButton" OnClick="btn_cooperative_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_cooperative_dep_note" runat="server" TextMode="MultiLine"
                                    Width="99.5%" Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th colspan='6'>
                                <h2>
                                    采购部反馈</h2>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <b>黑色金属：</b><asp:TextBox ID="txt_ferrous_dep" runat="server" class='number' Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_ferrous_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_ferrous_dep_endtime" runat="server"></asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_ferrous_dep_user_name" runat="server" onfocus="this.blur()"
                                    Enabled="false" Width="80px" class="purName"></asp:TextBox><asp:TextBox ID="txt_node_ferrous_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_ferrous"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_ferrous_dep" runat="server" Text=" 确 定 " OnClick="btn_ferrous_dep_Click"
                                    class="myButton" />
                                <asp:Button Visible="false" ID="btn_purchase_divide" runat="server" Text=" 确认分工 "
                                    OnClientClick="return btn_purchase_divide_click();" OnClick="btn_purchase_divide_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_ferrous_dep_note" runat="server" TextMode="MultiLine" Width="99.5%"
                                    Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>外 &nbsp;购 件：</b><asp:TextBox ID="txt_purchasepart_dep" runat="server" class='number'
                                    Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_purchasepart_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_purchasepart_dep_endtime" runat="server"></asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_purchasepart_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="purName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_purchasepart_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_purchasepart"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_purchasepart_dep" runat="server" Text=" 确 定 "
                                    class="myButton" OnClick="btn_purchasepart_dep_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_purchasepart_dep_note" runat="server" TextMode="MultiLine"
                                    Width="99.5%" Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>油漆涂料：</b><asp:TextBox ID="txt_paint_dep" runat="server" class='number' Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_paint_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_paint_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_paint_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="purName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_paint_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_paint"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_paint_dep" runat="server" Text=" 确 定 " class="myButton"
                                    OnClick="btn_paint_dep_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_paint_dep_note" runat="server" TextMode="MultiLine" Width="99.5%"
                                    Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>电器电料：</b><asp:TextBox ID="txt_electrical_dep" runat="server" class='number' Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_electrical_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_electrical_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_electrical_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="purName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_electrical_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_electrical"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_electrical_dep" runat="server" Text=" 确 定 " class="myButton"
                                    OnClick="btn_electrical_dep_Click" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_electrical_dep_note" runat="server" TextMode="MultiLine"
                                    Width="99.5%" Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>铸 &nbsp;锻 件：</b><asp:TextBox ID="txt_casting_dep" runat="server" class='number'
                                    Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_casting_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_casting_dep_endtime" runat="server">dd</asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_casting_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="purName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_casting_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_casting"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_casting_dep" OnClick="btn_casting_dep_Click"
                                    class="myButton" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_casting_dep_note" runat="server" TextMode="MultiLine" Width="99.5%"
                                    Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>其他材料：</b><asp:TextBox ID="txt_othermat_dep" runat="server" class='number' Enabled="false"></asp:TextBox>
                                元
                            </td>
                            <td>
                                参考值：
                                <asp:Label ID="lb_othermat_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="" class="puchaseFB_mis"></span> 元
                            </td>
                            <td>
                                反馈时间：
                                <asp:Label ID="lb_node_othermat_dep_endtime" runat="server"></asp:Label>
                            </td>
                            <td>
                                反馈人：<asp:TextBox ID="txt_node_othermat_dep_user_name" runat="server" onfocus="this.blur()"
                                    class="purName" Enabled="false" Width="80px"></asp:TextBox><asp:TextBox ID="txt_node_othermat_dep_user_id"
                                        runat="server" Style="display: none;"></asp:TextBox><asp:Image Visible="false" ID="img_othermat"
                                            runat="server" ImageUrl="../Assets/images/username_bg.gif" onclick="choosePerson(this)"
                                            align="middle" Style="cursor: pointer" title="选择" />
                            </td>
                            <th>
                                <asp:Button Visible="false" ID="btn_othermat_dep" OnClick="btn_othermat_dep_Click"
                                    class="myButton" runat="server" Text=" 确 定 " />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txt_node_othermat_dep_note" runat="server" TextMode="MultiLine"
                                    Width="99.5%" Rows="3" Style="resize: none" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>材料费反馈合计：</b><asp:Label ID="lb_c_total_material_dep" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                <b>材料费参考值合计：</b><asp:Label ID="lb_c_total_material_his" runat="server"></asp:Label>
                                元
                            </td>
                            <td>
                                差 值：<span id="span_puchaseTotalFB_mis"></span> 元
                            </td>
                        </tr>
                    </table>
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
                                    <input id="dep" name="dept" value="05">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="height: 245px">
                        <table id="dg">
                        </table>
                    </div>
                </div>
                <div id="buttons" style="text-align: right" visible="false">
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                        确 定 </a>&nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                            onclick="javascript:$('#win').dialog('close')">取 消 </a>
                    <input id="hidPerson" type="hidden" value="" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <%--审核--%>
        <asp:TabPanel ID="TabPanel9" runat="server" HeaderText=" 审 核 ">
            <ContentTemplate>
                <table class="mytable">
                    <tr>
                        <th colspan='5'>
                            <h2>
                                生产部审核</h2>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            审核人：<asp:Label ID="lb_node_production_check_user_name" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核时间：<asp:Label ID="lb_node_production_check_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核结果：
                            <asp:RadioButtonList ID="rbl_production_check" runat="server" RepeatDirection="Horizontal"
                                Enabled="false" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbl_production_check_SelectedIndexChanged">
                                <asp:ListItem Text="同 意" Value="1"></asp:ListItem>
                                <asp:ListItem Text="驳 回" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="cklContainer">
                            <asp:CheckBoxList ID="ckl_production_check" runat="server" RepeatDirection="Horizontal"
                                Visible="false" class="myCkl">
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Button Visible="false" ID="btn_production_check" runat="server" Text=" 确 定 "
                                class="myButton" OnClick="btn_production_check_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txt_node_production_check_note" runat="server" TextMode="MultiLine"
                                Width="99.5%" Rows="5" Style="resize: none" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th colspan='5'>
                            <h2>
                                采购部审核</h2>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            审核人：<asp:Label ID="lb_node_purchase_check_user_name" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核时间：<asp:Label ID="lb_node_purchase_check_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核结果：
                            <asp:RadioButtonList ID="rbl_purchase_check" runat="server" RepeatDirection="Horizontal"
                                Enabled="false" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbl_purchase_check_SelectedIndexChanged">
                                <asp:ListItem Text="同 意" Value="1"></asp:ListItem>
                                <asp:ListItem Text="驳 回" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="cklContainer">
                            <asp:CheckBoxList ID="ckl_purchase_check" runat="server" RepeatDirection="Horizontal"
                                Visible="false" class="myCkl">
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Button Visible="false" ID="btn_purchase_check" runat="server" Text=" 确 定 " class="myButton"
                                OnClick="btn_purchase_check_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txt_node_purchase_check_note" runat="server" TextMode="MultiLine"
                                Width="99.5%" Rows="5" Style="resize: none" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th colspan='5'>
                            <h2>
                                财务部审核</h2>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            审核人：<asp:Label ID="lb_node_budget_check_user_name" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核时间：<asp:Label ID="lb_node_budget_check_endtime" runat="server"></asp:Label>
                        </td>
                        <td>
                            审核结果：
                            <asp:RadioButtonList ID="rbl_budget_check" runat="server" RepeatDirection="Horizontal"
                                Enabled="false" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="rbl_budget_check_SelectedIndexChanged">
                                <asp:ListItem Text="同 意" Value="1"></asp:ListItem>
                                <asp:ListItem Text="驳 回" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="cklContainer">
                            <asp:CheckBoxList ID="ckl_budget_check" runat="server" RepeatDirection="Horizontal"
                                Visible="false" class="myCkl">
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Button Visible="false" ID="btn_budget_check" runat="server" Text=" 确 定 " class="myButton"
                                OnClick="btn_budget_check_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txt_node_budget_check_note" runat="server" TextMode="MultiLine"
                                Width="99.5%" Rows="5" Style="resize: none" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:TabPanel>
        <%--黑色金属明细--%>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="黑色金属明细">
            <ContentTemplate>
                <div class="tab-container">
                    <asp:Repeater ID='rpt_ferrous' runat="Server">
                        <HeaderTemplate>
                            <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class=" mytable toptable mygrid">
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
                <div class="tab-container">
                    <asp:Repeater ID='rpt_purchase' runat="Server">
                        <HeaderTemplate>
                            <table align="center" cellpadding="4" cellspacing="1" border="1" class="mytable toptable mygrid">
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
                <div class="tab-container">
                    <asp:Repeater ID='rpt_paint' runat="Server">
                        <HeaderTemplate>
                            <table align="center" cellpadding="4" cellspacing="1" border="1" class="mytable toptable mygrid">
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
                <div class="tab-container">
                    <asp:Repeater ID='rpt_electrical' runat="Server">
                        <HeaderTemplate>
                            <table align="center" cellpadding="4" cellspacing="1" border="1" class="toptable mygrid">
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
                <div class="tab-container">
                    <asp:Repeater ID='rpt_casting' runat="Server">
                        <HeaderTemplate>
                            <table align="center" cellpadding="4" cellspacing="1" border="1" class=" mytable toptable mygrid">
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
                <div class="tab-container">
                    <asp:Repeater ID='rpt_other' runat="Server">
                        <HeaderTemplate>
                            <table align="center" cellpadding="4" cellspacing="1" border="1" class=" mytable toptable mygrid">
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

    <script src="../JS/jquery/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>

    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript" src="YS_Data_P.js"></script>

    <script type="text/javascript">


        $(function() {

            $('.myButton').bind('click', btn_confirm);
            $('.budget').bind('input', ccltTotalBudget);
            $('.budget_pre').bind('input', ccltTotalBudget_pre);
            $('input.number').bind('blur', validateNumber);
            cclMisBudget();
            cclMisPurchaseFB();
        });


        
        
        
        
             
    </script>

</asp:Content>
