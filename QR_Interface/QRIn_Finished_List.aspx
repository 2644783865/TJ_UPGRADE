<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="QRIn_Finished_List.aspx.cs" Inherits="ZCZJ_DPF.QR_Interface.FInished_QRInOutManage.QRIn_Finished_List" Title="扫码入库管理" %>
<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
</cc1:ToolkitScriptManager>
<script type="text/javascript">
    function PushConfirm() 
    {
        var retVal = confirm("确定将所选定项目下推生成入库单？");
        return retVal;
    }
</script>
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td style="width:50px" align="right">
                            入库状态：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblInState" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblInState_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="已入库" Value="1"></asp:ListItem>
                            <asp:ListItem Text="未入库" Value="0" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            日期从：<asp:TextBox ID="txtStartYearMonth" Width="100px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>到：
                              <asp:TextBox ID="txtEndYearMonth" Width="100px" runat="server" class="easyui-datebox" onClick="setday(this)"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="right" colspan="3">
                            任务号：
                            <asp:TextBox ID="tbCPQRIn_TaskID" runat="server" style="width:80px"></asp:TextBox>
                            总序：
                            <asp:TextBox ID="tbCPQRIn_Zongxu" runat="server" style="width:50px"></asp:TextBox>
                            项目名称：
                            <asp:TextBox ID="tbEngName" runat="server" style="width:80px"></asp:TextBox>
                            产品名称：
                            <asp:TextBox ID="tbProdName" runat="server" style="width:80px"></asp:TextBox>
                            <asp:Button ID="btnShowPopup" runat="server" Text="查询" OnClick="Query_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Push" runat="server" Text="下推" OnClick="Push_Click" OnClientClick="return PushConfirm()" />
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
                                            项目名称
                                        </td>
                                        <td>
                                            任务号
                                        </td>
                                        <td>
                                            总序
                                        </td>
                                        <td>
                                            产品名称
                                        </td>
                                        <td>
                                            图号
                                        </td>
                                        <td>
                                            入库数量
                                        </td>
                                        <td>
                                            扫码时间
                                        </td>
                                        <td>
                                            扫码人
                                        </td>
                                        <td>
                                            入库状态
                                        </td>
                                        <td>
                                            扫码备注
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                            <asp:Label ID="lbCPQRIn_ID" runat="server" Text='<%#Eval("CPQRIn_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCPQRIn_TaskID" runat="server" Text='<%#Eval("CPQRIn_TaskID")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCPQRIn_Zongxu" runat="server" Text='<%#Eval("CPQRIn_Zongxu")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbBM_CHANAME" runat="server" Text='<%#Eval("BM_CHANAME")%>'></asp:Label>                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbBM_TUHAO" runat="server" Text='<%#Eval("BM_TUHAO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCPQRIn_Num" runat="server" Text='<%#Eval("CPQRIn_Num")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCPQRIn_Time" runat="server" Text='<%#Eval("CPQRIn_Time")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbCPQRIn_Person" runat="server" Text='<%#Eval("CPQRIn_Person")%>'></asp:Label>
                                        </td>
                                        <td>
                                          <asp:Label runat="server" ID="lbCPQRIn_State"  Text='<%#Eval("CPQRIn_State").ToString()=="0"?"未入库":Eval("CPQRIn_State").ToString()=="1"?"已入库":"其它"%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="lbCPQRIn_Note" runat="server" ForeColor="#1A438E" Style="background-color: Transparent;text-align: center" Width="150px" Text='<%#Eval("CPQRIn_Note")%>' ToolTip='<%#Eval("CPQRIn_Note")%>'></asp:TextBox>
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