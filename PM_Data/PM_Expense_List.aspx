<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Expense_List.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Expense_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        月成本统计管理
                                    </td>
                                   <%-- <td width="40%">
                                        <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                            AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                        </asp:RadioButtonList>
                                    </td>--%>
                                    <td>
                                        <asp:Label ID="expenseyear" runat="server">年份：</asp:Label>
                                        <asp:DropDownList ID="ddlexpenseyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexpenseyear_SelectedIndexChanged">
                                            <asp:ListItem  Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    <asp:Label ID="expensemonth" runat="server">月份：</asp:Label>
                                        <asp:DropDownList ID="ddlexpensemonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlexpensemonth_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <%--<td align="right">
                                        <asp:Button ID="delete" runat="server" Text="删除" OnClick="delete_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td runat="server" id="add">
                                        <asp:HyperLink ID="addexpensebill" runat="server" ForeColor="Red" NavigateUrl="~/PM_Data/PM_Expense_edit.aspx?action=add">
                                            <asp:Label ID="Label" runat="server" Text="新增成本记录"> </asp:Label></asp:HyperLink>
                                    </td>--%>
                                </tr>
                            </table>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox2 xscroll">
                                            <table id="tab" align="center" class="nowrap cptable fullwidth">
                                               <%-- <asp:Repeater ID="PM_Expense_List_Repeater" runat="server" OnItemDataBound="PM_Expense_List_Repeater_ItemDataBound">--%>
                                             <asp:Repeater ID="PM_Expense_List_Repeater" runat="server" >
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>统计年月</strong>
                                                            </td>
                                                           <%-- <td>
                                                                <strong>项目名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>合同号</strong>
                                                            </td>--%>
                                                            <td>
                                                                <strong>任务单号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>外协费</strong>
                                                            </td>
                                                            <%-- <td>
                                                                <strong>图名</strong>
                                                            </td>--%>
                                                            <td>
                                                                <strong>厂内分包</strong>
                                                            </td>
                                                            <td>
                                                                <strong>运费</strong>
                                                            </td>
                                                            <td>
                                                                <strong>分交成本</strong>
                                                            </td>
                                                            <%--<td runat="server" id="hedit">
                                                                <strong>修改</strong>
                                                            </td>
                                                            <td runat="server" id="hlookup">
                                                                <strong>查看</strong>
                                                            </td>--%>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                            <td>
                                                                <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                                    onclick="checkme(this)" Checked="false"></asp:CheckBox>&nbsp;
                                                                  <asp:Label ID="DOCUNUM" runat="server" Text='<%#Eval("DOCUNUM")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="yyyy" runat="server" Text='<%#Eval("FM_YEAR")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="mm" runat="server" Text='<%#Eval("FM_MONTH")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="yyyymm" runat="server" Text='<%#get_yyyymm(Convert.ToString(Eval("FM_YEAR")),Convert.ToString(Eval("FM_MONTH")))%>'></asp:Label>
                                                            </td>
                                                            <%--<td>
                                                                <asp:Label ID="CM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>'></asp:Label>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="FM_WX" runat="server" Text='<%#Eval("FM_WX")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="FM_CN" runat="server" Text='<%#Eval("FM_CN")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="FM_YF" runat="server" Text='<%#Eval("FM_YF")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="FM_FJ" runat="server" Text='<%#Eval("FM_FJ")%>'></asp:Label>
                                                            </td>
                                                            <%--<td>
                                                                <asp:Label ID="SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="PZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                                                            </td>
                                                            <td runat="server" id="bedit">
                                                                <asp:HyperLink ID="hyp_edit" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="修改"></asp:Label></asp:HyperLink>
                                                            </td>
                                                            <td runat="server" id="blookup">
                                                                <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                                                    <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="11" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
