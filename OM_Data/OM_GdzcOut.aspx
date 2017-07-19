<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcOut.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GdzcOut" Title="领料管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产出库查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToStore() {
            window.open("OM_GdzcStore.aspx?FLAG=ToStore");
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    名称：
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    规格或参数：
                                    <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="查  询" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_OnClick" />
                                </td>
                                <td align="center">
                                    <input id="ToStore" type="button" value="到库存" onclick="ToStore()" runat="server" />
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
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        出库单号
                                    </td>
                                    <td>
                                        领料部门
                                    </td>
                                    <td>
                                        名称
                                    </td>
                                    <td>
                                        规格或参数
                                    </td>
                                    <td>
                                        出库数量
                                    </td>
                                    <td>
                                        发料员
                                    </td>
                                    <td>
                                        领料日期
                                    </td>
                                    <td>
                                        制单人
                                    </td>
                                    <td>
                                        备注
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center">
                                    <td>
                                        <%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOutCode" runat="server" Text='<%#Eval("OUTCODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("OUTDEP")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTNAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTMODEL")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTNUM")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTSENDER")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTDATE")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTDOC")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTNOTE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                        没有相关出库信息!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
