<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="QROut_DzFinished_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.QROut_DzFinished_List" Title="扫码出库管理" %>
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
                            日期从：<asp:TextBox ID="txtStartYearMonth" Width="120px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>到：
                              <asp:TextBox ID="txtEndYearMonth" Width="120px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            原任务号：
                            <asp:TextBox ID="tbOldTaskCode_Out" runat="server" style="width:80px"></asp:TextBox>
                            原项目名称：
                            <asp:TextBox ID="tbOldProjName_Out" runat="server" style="width:80px"></asp:TextBox>
                            产品名称：
                            <asp:TextBox ID="tbProdName_Out" runat="server" style="width:80px"></asp:TextBox>
                            图号：
                            <asp:TextBox ID="tbMapCode_Out" runat="server" style="width:80px"></asp:TextBox>
                            产品编号：
                            <asp:TextBox ID="tbOutProdCode_Out" runat="server" style="width:80px"></asp:TextBox>
                            实际发货地址（项目）：
                            <asp:TextBox ID="tbRealAddrs_Out" runat="server" style="width:80px"></asp:TextBox>
                            实际任务号：
                            <asp:TextBox ID="tbTaskCode_Out" runat="server" style="width:80px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
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
                                            单重
                                        </td>
                                        <td>
                                            出库数量
                                        </td>
                                        <td>
                                            单位
                                        </td>
                                        <td>
                                            出库金额
                                        </td>
                                        <td>
                                            实际发货地址
                                        </td>
                                        <td>
                                            实际任务号
                                        </td>
                                        <td>
                                            扫码人
                                        </td>
                                        <td>
                                            扫码出库时间
                                        </td>
                                        <td>
                                            账内账外
                                        </td>
                                        <td>
                                            是否进入ERP
                                        </td>
                                        <td>
                                            顶发说明
                                        </td>
                                        <td>
                                            备注
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="lbQRDzOutID" runat="server" Text='<%#Eval("QRDzOutID")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lbUniqID_Out" runat="server" Text='<%#Eval("UniqID_Out")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldYzName_Out" runat="server" Text='<%#Eval("OldYzName_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldHtCode_Out" runat="server" Text='<%#Eval("OldHtCode_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldTaskCode_Out" runat="server" Text='<%#Eval("OldTaskCode_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldYzHtCode_Out" runat="server" Text='<%#Eval("OldYzHtCode_Out")%>'></asp:Label>                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOldProjName_Out" runat="server" Text='<%#Eval("OldProjName_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbProdName_Out" runat="server" Text='<%#Eval("ProdName_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbMapCode_Out" runat="server" Text='<%#Eval("MapCode_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCaizhi_Out" runat="server" Text='<%#Eval("Caizhi_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutProdCode_Out" runat="server" Text='<%#Eval("OutProdCode_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbSingleWeight_Out" runat="server" Text='<%#Eval("SingleWeight_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutNum_Out" runat="server" Text='<%#Eval("OutNum_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutUnit_Out" runat="server" Text='<%#Eval("OutUnit_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbMoney_Out" runat="server" Text='<%#Eval("Money_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbRealAddrs_Out" runat="server" Text='<%#Eval("RealAddrs_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbTaskCode_Out" runat="server" Text='<%#Eval("TaskCode_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbSmPerson_Out" runat="server" Text='<%#Eval("SmPerson_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbOutTime_Out" runat="server" Text='<%#Eval("OutTime_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbZnZw_Out" runat="server" Text='<%#Eval("ZnZw_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbIfERP_Out" runat="server" Text='<%#Eval("IfERP_Out")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lbDfReason_Out" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("DfReason_Out")%>' ToolTip='<%#Eval("DfReason_Out")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lbNote_Out" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("Note_Out")%>' ToolTip='<%#Eval("Note_Out")%>'></asp:TextBox>
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
