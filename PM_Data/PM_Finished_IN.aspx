<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_IN.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_IN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    成品入库查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td>
                            成品入库管理
                        </td>
                        <td width="40%">
                            <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            按类查询：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_query" runat="server" AutoPostBack="true">
                                <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="1"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="3"></asp:ListItem>
                                <asp:ListItem Text="图号" Value="4"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;
                            <asp:Button ID="btndaochu" runat="server" Text="导出" OnClick="btndaochu_OnClick" />&nbsp;
                        </td>
                        <td runat="server" id="add">
                            <asp:HyperLink ID="addinbill" runat="server" ForeColor="Red" NavigateUrl="~/PM_Data/PM_Finished_INBILL.aspx?action=add">
                                <asp:Label ID="Label" runat="server" Text="成品入库"> </asp:Label></asp:HyperLink>
                        </td>
                        <td runat="server">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~\QR_Interface\QRIn_Finished_List.aspx">
                                <asp:Label ID="Label1" runat="server" Text="到扫码入库管理"> </asp:Label></asp:HyperLink>
                        </td>
                        <td runat="server">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PM_Data/Finished_QRExport.aspx">
                                <asp:Label ID="Label2" runat="server" Text="导出二维码信息"> </asp:Label></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 100%; height: auto; overflow: scroll;
                display: block;">
            <table width="98%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle">
                            <td>
                                <strong>序号</strong>
                            </td>
                            <td>
                                <strong>入库单号</strong>
                            </td>
                            <td>
                                <strong>项目名称</strong>
                            </td>
                            <td>
                                <strong>任务单号</strong>
                            </td>
                            <td>
                                <strong>图号</strong>
                            </td>
                            <td>
                                <strong>总序</strong>
                            </td>
                            <td>
                                <strong>设备名称</strong>
                            </td>
                            <td>
                                <strong>入库数量</strong>
                            </td>
                            <td>
                                <strong>总数</strong>
                            </td>
                            <td>
                                <strong>单重</strong>
                            </td>
                            <td>
                                <strong>制单时间</strong>
                            </td>
                            <td>
                                <strong>入库时间</strong>
                            </td>
                            <td>
                                <strong>备注</strong>
                            </td>
                            <td>
                                <strong>审批状态</strong>
                            </td>
                            <td runat="server" id="hlookup">
                                <strong>查看</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center">
                            <td>
                                <%#Container.ItemIndex+1%>
                                <%--<asp:Label ID="Label1" Visible="false" runat="server" Text='<%#Eval("INCODE")%>'></asp:Label>--%>
                            </td>
                            <td>
                                <asp:Label ID="docnum" runat="server" Text='<%#Eval("TFI_DOCNUM")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("TFI_PROJ")%>
                            </td>
                            <td>
                                <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("TFI_MAP")%>
                            </td>
                            <td>
                                <%#Eval("TFI_ZONGXU")%>
                            </td>
                            <td>
                                <%#Eval("TFI_NAME")%>
                            </td>
                            <td>
                                <%#Eval("TFI_RKNUM")%>
                            </td>
                            <td>
                                <%#Eval("TFI_NUMBER")%>
                            </td>
                            <td>
                                <%#Eval("TFI_WGHT")%>
                            </td>
                            <td>
                                <asp:Label ID="lblInDate" runat="server" Text='<%#Eval("INDATE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSpDate" runat="server" Text='<%#Eval("SPSJ")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("NOTE")%>
                            </td>
                            <td>
                                <asp:Label ID="SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                <asp:Label ID="PZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                            </td>
                            <td runat="server" id="blookup">
                                <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                    <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                没有相关成品入库信息!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
