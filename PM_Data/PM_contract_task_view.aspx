<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_contract_task_view.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_contract_task_view" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    项目综合信息查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            合 同 号&nbsp;：<asp:TextBox ID="txtHTH" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            项目名称：<asp:TextBox ID="txt_PJNAME" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 450px; overflow: auto; width: 100%">
                <div class="cpbox2 xscroll">
                    <table id="tab" align="center" class="nowrap cptable fullwidth">
                        <asp:Repeater ID="details_repeater" runat="server" OnItemDataBound="details_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>合 同 号</strong>
                                    </td>
                                    <td>
                                        <strong>项目名称</strong>
                                    </td>
                                    <td>
                                        <strong>合同信息</strong>
                                    </td>
                                    <td>
                                        <strong>采购计划</strong>
                                    </td>
                                    <td>
                                        <strong>物料出库管理</strong>
                                    </td>
                                    <td>
                                        <strong>成品质检管理</strong>
                                    </td>
                                    <td>
                                        <strong>生产外协汇总</strong>
                                    </td>
                                    <td>
                                        <strong>生产外协进度管理</strong>
                                    </td>
                                    <td>
                                        <strong>成品入库管理</strong>
                                    </td>
                                    <td>
                                        <strong>成品出库管理</strong>
                                    </td>
                                    <td>
                                        <strong>开票管理</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" style="height:35px" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("PCON_BCODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("PCON_ENGNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink8" NavigateUrl='<%#"/Contract_Data/CM_Contract_SW.aspx?PCON_BCODE=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image8" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />合同信息
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink7" NavigateUrl='<%#"/PC_Data/PC_TBPC_PurQuery.aspx?PCON_BCODE=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image7" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />采购计划
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"/SM_Data/SM_WarehouseOUT_LL_Manage.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />物料出库
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink4" NavigateUrl='<%#"/QC_Data/QC_Inspection_Manage.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image4" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />成品质检</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink3" NavigateUrl='<%#"PM_Xie_union.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />生产外协汇总
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink9" NavigateUrl='<%#"/PM_Data/PM_Xie_Plan.aspx?PCON_BCODE=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image9" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />生产外协进度
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"PM_Finished_IN.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />成品入库</asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink6" NavigateUrl='<%#"PM_Finished_OUT.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image6" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />成品出库</asp:HyperLink>
                                    </td>
                                    
                                    
                                    <td>
                                        <asp:HyperLink ID="HyperLink5" NavigateUrl='<%#"../CM_Data/CM_Kaipiao_List.aspx?contract_task_view=" + Eval("PCON_BCODE").ToString().Trim().Substring(5)%> '
                                            runat="server" Target="_blank">
                                            <asp:Image ID="Image5" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                runat="server" />开票信息
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <table>
                    <tr>
                        <td>
                            <asp:UCPaging ID="UCPaging1" runat="server" />
                        </td>
                        <td>
                            每页：<asp:DropDownList ID="ddl_pageno_change" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="20" Value="30"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;行
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
