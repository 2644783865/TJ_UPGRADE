<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_Pur_inform_commit.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_Pur_inform_commit" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购信息交流&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table  width="100%">
                    <tr>
                        <td style="width:330px" >
                            <asp:RadioButtonList ID="rblZT" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                AutoPostBack="true" OnSelectedIndexChanged="rblZT_OnSelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <%--                                <asp:ListItem Text="最近审批" Value="1"></asp:ListItem>--%>
                                <asp:ListItem Text="待审批" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="审批通过" Value="5"></asp:ListItem>
                                <asp:ListItem Text="驳回" Value="4"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" style="width:200px">
                            编号&nbsp;：<asp:TextBox ID="txtpc_infor_id" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left" style="width:200px">
                            制单人：<asp:TextBox ID="txtpc_infor_zdr" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left" style="width:200px">
                            <asp:Button ID="btn_search" runat="server" Text="查 询" OnClick="btn_search_Click" />
                        </td>
                        <td runat="server" align="right">
                            <asp:HyperLink ID="HyperLink2" NavigateUrl='/PC_Data/PC_Pur_inform_commit_detial.aspx?action=Add'
                                runat="server">
                                <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                添加 &nbsp; &nbsp;&nbsp; &nbsp;
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <asp:Repeater ID="pc_purinform_repeater" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                            <td>
                                <strong>编号</strong>
                            </td>
                            <td>
                                <strong>制单人</strong>
                            </td>
                            <td>
                                <strong>制单时间</strong>
                            </td>
                            <td>
                                <strong>查看</strong>
                            </td>
                            <td>
                                <strong>审核</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:Label ID="MS_ID" runat="server" Text='<%#Eval("PC_PFT_ID")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("PC_PFT_ZDR_NAME")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("PC_PFT_TIME")%>'></asp:Label>
                            </td>
                            <td id="tdView" runat="server">
                                <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# viewSp(Eval("PC_PFT_ID").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />查看
                                </asp:HyperLink>
                            </td>
                            <td id="tdReview" runat="server">
                                <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# ReviewSp(Eval("PC_PFT_ID").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />审核
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="float: right">
            <table>
                <tr>
                    <td>
                        <asp:UCPaging ID="UCPaging1" runat="server" />
                    </td>
                    <td>
                        每页：<asp:DropDownList ID="ddl_pageno_change" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_Click">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;行
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
            没有记录!</asp:Panel>
    </div>
</asp:Content>
