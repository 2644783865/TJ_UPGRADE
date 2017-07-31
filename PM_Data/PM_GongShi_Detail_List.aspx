<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_GongShi_Detail_List.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_GongShi_Detail_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    加工工时明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <style>
        #tab tr
        {
            height: 28px;
        }
    </style>

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label><!--检查用户-->
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td width="25%">
                                        <strong>顾客名称：</strong><asp:Label ID="GS_CUSNAME" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td width="25%">
                                        <strong>合同号：</strong><asp:Label ID="GS_CONTR" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td width="25%">
                                        <strong>任务单号：</strong><asp:Label ID="GS_TSAID" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td width="25%">
                                        <strong>时间：</strong><asp:Label ID="lbYEAR" runat="server" Text="Label"></asp:Label>.
                                        <asp:Label ID="lbMONTH" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <div style="height: 350px; overflow: auto; width: 100%">
                                        <table id="tab" align="center" class="toptable grid fullwidth" border="1">
                                            <asp:Repeater ID="PM_GongShi_Detial_List_Repeater" runat="server">
                                                <HeaderTemplate>
                                                    <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                        <td>
                                                            <strong>序号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>时间</strong>
                                                        </td>
                                                        <td>
                                                            <strong>图号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>图名</strong>
                                                        </td>
                                                        <td>
                                                            <strong>加工设备号</strong>
                                                        </td>
                                                        <td>
                                                            <strong>加工设备名称</strong>
                                                        </td>
                                                        <td>
                                                            <strong>设备系数</strong>
                                                        </td>
                                                        <td>
                                                            <strong>加工工时</strong>
                                                        </td>
                                                        <td>
                                                            <strong>工时费用（元）</strong>
                                                        </td>
                                                        <td>
                                                            <strong>备注</strong>
                                                        </td>
                                                    </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                        ondblclick="<%# showYg(Eval("Id").ToString()) %>">
                                                        <td runat="server" id="num">
                                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="DATEYEAR" runat="server" Text='<%#Eval("DATEYEAR")%>'></asp:Label>.
                                                            <asp:Label ID="DATEMONTH" runat="server" Text='<%#Eval("DATEMONTH")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_TUHAO" runat="server" Text='<%#Eval("GS_TUHAO")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_TUMING" runat="server" Text='<%#Eval("GS_TUMING")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_EQUID" runat="server" Text='<%#Eval("GS_EQUID")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_EQUNAME" runat="server" Text='<%#Eval("GS_EQUNAME")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_EQUFACTOR" runat="server" Text='<%#Eval("GS_EQUFACTOR")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_HOURS" runat="server" Text='<%#Eval("GS_HOURS")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_MONEY" runat="server" Text='<%#Eval("GS_MONEY")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="GS_NOTE" runat="server" Text='<%#Eval("GS_NOTE")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            <strong>没有记录!</strong></asp:Panel>
                                    </div>
                                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
