<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="QRManage_DzFinished_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.QRManage_DzFinished_List" Title="扫码入库汇总管理" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
</cc1:ToolkitScriptManager>
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td style="width:80px" align="right">
                            账内账外：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblZnZwState" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblZnZwState_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="账内" Value="账内"></asp:ListItem>
                                <asp:ListItem Text="账外" Value="账外"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width:120px" align="right">
                            是否进入ERP：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblIfERPState" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblIfERPState_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="是" Value="是"></asp:ListItem>
                                <asp:ListItem Text="否" Value="否"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            入库日期从：<asp:TextBox ID="txtStartYearMonth" Width="120px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>到：
                              <asp:TextBox ID="txtEndYearMonth" Width="120px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            原任务号：
                            <asp:TextBox ID="tbOldTaskCode" runat="server" style="width:100px"></asp:TextBox>
                            原项目名称：
                            <asp:TextBox ID="tbOldProjName" runat="server" style="width:100px"></asp:TextBox>
                            产品名称：
                            <asp:TextBox ID="tbProdName" runat="server" style="width:100px"></asp:TextBox>
                            图号：
                            <asp:TextBox ID="tbMapCode" runat="server" style="width:100px"></asp:TextBox>
                            产品编号：
                            <asp:TextBox ID="tbInProdCode" runat="server" style="width:100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnShowPopup" runat="server" Text="查询" OnClick="Query_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
  <div class="RightContent">
    <div class="box-wrapper">
        <div class="box-outer">
                        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                            没有相关记录!</asp:Panel>
                   <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="table1" width="1000px" align="center" cellpadding="4" cellspacing="1" class="toptable grid nowrap"
                        border="1">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <HeaderTemplate>
                                    <tr align="center">
                                        <td>
                                            行号
                                        </td>
                                        <td>
                                            原业主
                                        </td>
                                        <td>
                                            原合同号
                                        </td>
                                        <td>
                                            原任务号
                                        </td>
                                        <td>
                                            原业主合同号
                                        </td>
                                        <td>
                                            原项目名称
                                        </td>
                                        <td>
                                            产品名称
                                        </td>
                                        <td>
                                            图号
                                        </td>
                                        <td>
                                            材质
                                        </td>
                                        <td>
                                            产品编号
                                        </td>
                                        <td>
                                            扫码人
                                        </td>
                                        <td>
                                            扫码时间
                                        </td>
                                        <td>
                                            入库数量
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            入库金额
                                        </td>
                                        <td>
                                            单重
                                        </td>
                                        <td>
                                            出库数量
                                        </td>
                                        <td>
                                            出库金额
                                        </td>
                                        <td>
                                            库存数量
                                        </td>
                                        <td>
                                            库存金额
                                        </td>
                                        <td>
                                            账内账外
                                        </td>
                                        <td>
                                            是否进入ERP
                                        </td>
                                        <td>
                                            入库备注
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="lbQRDzID" runat="server" Text='<%#Eval("QRDzID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldYzName" runat="server" Text='<%#Eval("OldYzName")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldHtCode" runat="server" Text='<%#Eval("OldHtCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldTaskCode" runat="server" Text='<%#Eval("OldTaskCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldYzHtCode" runat="server" Text='<%#Eval("OldYzHtCode")%>'></asp:Label>                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldProjName" runat="server" Text='<%#Eval("OldProjName")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbProdName" runat="server" Text='<%#Eval("ProdName")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbMapCode" runat="server" Text='<%#Eval("MapCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCaizhi" runat="server" Text='<%#Eval("Caizhi")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbInProdCode" runat="server" Text='<%#Eval("InProdCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbSmPerson" runat="server" Text='<%#Eval("SmPerson")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbInTime" runat="server" Text='<%#Eval("InTime")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbInNum" runat="server" Text='<%#Eval("InNum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbInUnit" runat="server" Text='<%#Eval("InUnit")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbInMoney" runat="server" Text='<%#Eval("InMoney")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbSingleWeight" runat="server" Text='<%#Eval("SingleWeight")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutNum" runat="server" Text='<%#Eval("OutNum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutMoney" runat="server" Text='<%#Eval("OutMoney")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbStorageNum" runat="server" Text='<%#Eval("StorageNum")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbStorageMoney" runat="server" Text='<%#Eval("StorageMoney")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbZnZw" runat="server" Text='<%#Eval("ZnZw")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbIfERP" runat="server" Text='<%#Eval("IfERP")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lbInNote" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("InNote")%>' ToolTip='<%#Eval("InNote")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
              <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
  </div>
</asp:Content>
