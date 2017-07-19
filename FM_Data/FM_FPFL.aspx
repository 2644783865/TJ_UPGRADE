<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_FPFL.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FPFL" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    发票明细信息
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="FM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <table style="width: 100%;">
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
                <td align="center">
                    <strong>物料类型：</strong>
                    <asp:DropDownList ID="dplwltype" runat="server" Width="100px">
                        <asp:ListItem Value="NO" Selected="True" Text=""></asp:ListItem>
                        <asp:ListItem Value="0">钢材</asp:ListItem>
                        <asp:ListItem Value="1">五金库</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <strong>供应商：</strong>
                    <asp:TextBox ID="gys_name" runat="server" Text=""></asp:TextBox>
                </td>
                <td align="center" style="width: 26%">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                </td>
                <td align="center">
                    <asp:Button ID="btn_export" runat="server" Text="供应商汇总导出" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="box-outer">
        <div style="overflow: scroll">
            <table id="table1" cellpadding="2" width="100%" cellspacing="1" class="toptable grid nowrap"
                border="1">
                <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                    <HeaderTemplate>
                        <tr align="center">
                            <td align="center">
                                <strong>序号</strong>
                            </td>
                            <td align="center">
                                <strong>发票编号</strong>
                            </td>
                            <td align="center">
                                <strong>勾稽日期</strong>
                            </td>
                            <td align="center">
                                <strong>物料编码</strong>
                            </td>
                            <td align="center">
                                <strong>物料名称</strong>
                            </td>
                            <td align="center">
                                <strong>规格</strong>
                            </td>
                            <td align="center">
                                <strong>单位</strong>
                            </td>
                            <td align="center">
                                <strong>数量</strong>
                            </td>
                            <td align="center">
                                <strong>单价</strong>
                            </td>
                            <td align="center">
                                <strong>含税单价</strong>
                            </td>
                            <td align="center">
                                <strong>金额</strong>
                            </td>
                            <td align="center">
                                <strong>含税金额</strong>
                            </td>
                            <td align="center">
                                <strong>物料类型</strong>
                            </td>
                            <td align="center">
                                <strong>计划跟踪号</strong>
                            </td>
                            <td align="center">
                                <strong>供应商</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_CODE" runat="server" Text='<%#Eval("GI_CODE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_DATE" runat="server" Text='<%#Eval("GI_DATE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_MATCODE" runat="server" Text='<%#Eval("GI_MATCODE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_NAME" runat="server" Text='<%#Eval("GI_NAME")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_GUIGE" runat="server" Text='<%#Eval("GI_GUIGE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_UNIT" runat="server" Text='<%#Eval("GI_UNIT")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_NUM" runat="server" Text='<%#Eval("GI_NUM")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_UNITPRICE" runat="server" Text='<%#Eval("GI_UNITPRICE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_CTAXUPRICE" runat="server" Text='<%#Eval("GI_CTAXUPRICE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_AMTMNY" runat="server" Text='<%#Eval("GI_AMTMNY")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_CTAMTMNY" runat="server" Text='<%#Eval("GI_CTAMTMNY")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_PMODE" runat="server" Text='<%#Eval("GI_PMODE")%>'></asp:Label>
                            </td>
                            <td runat="server" align="center">
                                <asp:Label ID="GI_PTCODE" runat="server" Text='<%#Eval("GI_PTCODE")%>'></asp:Label>
                            </td>
                            <td id="Td1" runat="server" align="center">
                                <asp:Label ID="GI_SUPPLIERNM" runat="server" Text='<%#Eval("GI_SUPPLIERNM")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr>
                            <th colspan="3" align="right">
                                合计:
                            </th>
                            <th colspan="7">
                            </th>
                            <th align="center">
                                <asp:Label ID="lbjehj" runat="server"></asp:Label>
                            </th>
                            <th align="center">
                                <asp:Label ID="lbhsjehj" runat="server"></asp:Label>
                            </th>
                            <th>
                            </th>
                            <th>
                            </th>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                没有记录！</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
