<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_FJCBHZ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FJCBHZ" Title="无标题页" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>分交成本信息</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td style="width: 23%;">
                                <strong>时间：</strong>
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td align="center" style="width:26%">
                                <strong>任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td style="width:20%" align="left">
                                <asp:Button ID="Button1" runat="server" Text="导出数据" OnClick="btn_export_Click" />
                            </td>
                          </tr>  
                    </table>
                </div>
            </div>
        </div>
                <div  style="overflow: scroll;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%" >
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound" >
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <th>
                                    序号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    合计金额（含税）
                                </th>
                                <th>
                                    合计不含税金额
                                </th>
                                <th>
                                    年月份
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="chkDel"
                                        runat="server" />    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("FJCB_TSAID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhjhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJHSJE")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhjbhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJBHSJE")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbny" runat="server" align="center" Text='<%#Eval("FJCB_YEARMONTH")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="2" align="right">
                                    合计：
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbhsjezj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
