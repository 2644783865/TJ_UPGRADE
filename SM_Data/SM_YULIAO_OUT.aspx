<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="SM_YULIAO_OUT.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_YULIAO_OUT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    余料出库查询&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToStore() {
            window.open("SM_YULIAO_LIST.aspx?FLAG=ToStore");
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="80%">
                            <tr>
                                <td align="left">
                                    名称：
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                    <td align="left">
                                    材质：
                                    <asp:TextBox ID="txtCAIZHI" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                    <td align="left">
                                    规格：
                                    <asp:TextBox ID="txtGUIGE" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
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
                                        物料编码
                                    </td>
                                    <td>
                                        名称
                                    </td>
                                      <td>
                                        材质
                                    </td>
                                      <td>
                                        规格
                                    </td>
                                      <td>
                                        长度
                                    </td>
                                      <td>
                                        宽度
                                          <td>
                                        图形
                                    </td>
                                      <td>
                                        重量
                                    </td>
                                    </td>
                                    <td>
                                        出库数量
                                    </td>
                                    <td>
                                        任务号
                                    </td>
                                    <td>
                                        领料人
                                    </td>
                                    <td>
                                        发料日期
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
                                        <%#Eval("OUTCODE")%>
                                    </td>
                                    <td>
                                        <%#Eval("Marid")%>
                                    </td> <td>
                                        <%#Eval("Name")%>
                                    </td> <td>
                                        <%#Eval("CAIZHI")%>
                                    </td> <td>
                                        <%#Eval("GUIGE")%>
                                    </td> <td>
                                        <%#Eval("LENGTH")%>
                                    </td> <td>
                                        <%#Eval("WIDTH")%>
                                    </td> <td>
                                        <%#Eval("TUXING")%>
                                    </td> <td>
                                        <%#Eval("WEIGHT")%>
                                    </td> 
                                   
                                    <td>
                                        <%#Eval("OUTNUM")%>
                                    </td>
                                    <td>
                                        <%#Eval("PJNAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTPER")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTDATE")%>
                                    </td>
                                    <td>
                                        <%#Eval("DOCUPER")%>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
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
