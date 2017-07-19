﻿<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_GDGZSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GDGZSP" Title="固定工资审批管理" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
  固定工资审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
  <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table style="width: 100%;">
                <tr>
                    <td>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="shenhe" OnCheckedChanged="radio_all_CheckedChanged"
                                            AutoPostBack="True" />
                            <asp:RadioButton ID="radio_mytask" runat="server" Text="我的任务" GroupName="shenhe" OnCheckedChanged="radio_mytask_CheckedChanged"
                                            AutoPostBack="True"  Checked="true"/>
                        
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        审批状态：<asp:DropDownList ID="drp_state" runat="server" OnSelectedIndexChanged="drp_state_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                            <asp:ListItem Value="0">初始化</asp:ListItem>
                                            <asp:ListItem Value="1">审核中</asp:ListItem>
                                            <asp:ListItem Value="2">已通过</asp:ListItem>
                                            <asp:ListItem Value="3">已驳回</asp:ListItem>
                                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   </td>
                   <td align="right">
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptscczsp" runat="server">
                                <HeaderTemplate>
                                    <tr style="background-color: #B9D3EE;" height="30px">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            编号
                                        </td>
                                        <td align="center">
                                            制单人
                                        </td>
                                        <td align="center">
                                            制单时间
                                        </td>
                                        <td align="center">
                                            审核状态
                                        </td>
                                        <td align="center">
                                            查看/审核
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" height="30px">
                                        <td>
                                            <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false" Onclick="checkme(this)" />
                                            <asp:Label ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="TOL_BH" runat="server" Text='<%#Eval("TOL_BH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="XGRST_NAME" runat="server" Width="50px" Text='<%#Eval("XGRST_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="XGTIME" runat="server" Width="90px" Text='<%#Eval("XGTIME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="TOL_TOLSTATE" runat="server" Text='<%#Eval("TOL_TOLSTATE").ToString()=="0"?"初始化":Eval("TOL_TOLSTATE").ToString()=="1"?"审核中":Eval("TOL_TOLSTATE").ToString()=="2"?"通过":Eval("TOL_TOLSTATE").ToString()=="3"?"驳回":"出错"%>'></asp:Label>
                                        </td>
                                        <td>
                                        <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="查看/审核" NavigateUrl='<%#"~/OM_Data/OM_GDGZAdd.aspx?FLAG=audit&spid="+Eval("TOL_BH") %>'
                                    CssClass="link" runat="server">
                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                    查看/审核
                                </asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
