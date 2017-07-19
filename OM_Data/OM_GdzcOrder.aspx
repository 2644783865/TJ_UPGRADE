<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcOrder.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GdzcOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    固定资产采购订单&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../SM_Data/SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script src="../SM_Data/SM_JS/superTables.js" type="text/javascript"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link href="../SM_Data/StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../SM_Data/StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("确定将所选定项目下推生成入库单？");
            return retVal;
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
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
                            <asp:Button ID="btnQuery" runat="server" Text="查  询" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="重  置" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPush" runat="server" Text="下推" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="GridView1" width="100%" align="center" cellpadding="4" cellspacing="1"
                        class="toptable grid" border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        订单编号
                                    </td>
                                    <td>
                                        名称
                                    </td>
                                    <td>
                                        规格或参数
                                    </td>
                                    <td>
                                        订货数量
                                    </td>
                                    <td>
                                        申请人
                                    </td>
                                    <td>
                                        部门
                                    </td>
                                    <td>
                                        备注
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center" style="cursor: pointer" class="baseGadget" onmouseover="this.className='highlight'"
                                    onmouseout="this.className='baseGadget'" ondblclick="<%#showYg(Eval("CODE").ToString())%>"
                                    title="双击查看详情">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("MODEL")%>
                                    </td>
                                    <td>
                                        <%#Eval("NUM")%>
                                    </td>
                                    <td>
                                        <%Eval("AGENT")%>
                                    </td>
                                    <td>
                                        <%#Eval("DEPARTMENT")%>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                        没有相关记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
