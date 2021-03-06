﻿<%@ Page Language="C#" MasterPageFile="~/Masters/NoScollRightCotentMaster.Master" AutoEventWireup="true" CodeBehind="ys_contract_budget_list.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.ys_contract_budget_list" Title="无标题页" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    合同预算列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        th
        {
            border-bottom: 0;
            border-style: dotted;
        }
        .top-table
        {
            width: 100%;
        }
        .number{
        text-align:right;
        }
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-outer">
        <table class="top-table">
            <tr>               
                <td>
                    合同号： <asp:TextBox ID="txt_contract_code" class="txtbox" runat="server"></asp:TextBox>
                </td>
                <td>
                    项目名称： <asp:TextBox ID="txt_project_name" class="txtbox" runat="server"></asp:TextBox>
                </td>               
                <td>
                    <asp:Button ID="btn_search" runat="server" Text=" 查 询 " OnClick="btn_search_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="pal_container" runat="server">
            <div style="height: 350px;">
                <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1" class="toptable grid">
                    <asp:Repeater ID="rpt_contract_list" runat="server">
                        <HeaderTemplate>
                            <tr class="tableTitle">
                                <th>
                                    序号
                                </th>                                
                                <th>
                                    合同号
                                </th>
                                <th>
                                业主名称
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <th>
                                    合同收入（元）
                                </th>
                                <th>
                                    任务号预算（元）
                                </th>
                                <th>
                                    运费预算（元）
                                </th>
                                
                                <th>
                                    利润（元）
                                </th>
                                <th>
                                    利率（%）
                                </th>
                                <th>
                                    查看
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr align="center" class="baseGadget" id="row" runat="Server" onmouseover="this.className='highlight'"
                                onmouseout="this.className='baseGadget'">
                                <td>
                                    <%#Container.ItemIndex + 1%>
                                </td>                                
                                <td>
                                    <%#Eval("contract_code").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("contract_customer_name").ToString()%>                                
                                </td>
                                <td>
                                    <%#Eval("contract_project_name").ToString()%>
                                </td>
                                <td>
                                    <%#Eval("contract_equipment_name").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("contract_income").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("contract_task_budget").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("contract_trans_budget").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("contract_profit").ToString()%>
                                </td>
                                <td class='number'>
                                    <%#Eval("contract_profit_rate").ToString()%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ys_task_budget_list.aspx?contract_code="+Eval("contract_code").ToString()%>'
                                        runat="server">&nbsp;
                                        <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />
                                          任务号 &nbsp;</asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <uc1:UCPaging ID="UCPaging" runat="server" />
        </asp:Panel>
        <asp:Panel ID="NoDataPanel" runat="server">
            没有记录!
        </asp:Panel>
    </div>
    <script src="../JS/jquery/jquery-3.2.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        
    </script>

</asp:Content>

