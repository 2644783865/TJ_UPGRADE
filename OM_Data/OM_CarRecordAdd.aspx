<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="OM_CarRecordAdd.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarRecordAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增车辆记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate>
      <div class="box-inner">
          <div class="box_right">
              <div class="box-title">
                <table width="98%">
                <tr>
                    <td>
                        (带<span class="red">*</span>号的为必填项)
                    </td>
                    <td align="right">                    
                        <asp:Button ID="btnConfirm" runat="server" Text="确定" OnClick="btnConfirm_OnClick" /> &nbsp;&nbsp;
                        <asp:Button ID="btnReturn" runat="server" Text="返回" OnClick="btnReturn_OnClick" CausesValidation="False" /> &nbsp;&nbsp;
                    </td>
                </tr>
                </table>
              </div>
          </div>
      </div>
      
      <div class="box-wrapper">
        <div class="box-outer">
          <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top" ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="新增用车记录" TabIndex="0">
              <ContentTemplate>
                  <table width="100%">
                      <tr align="center">
                          <td align="right">
                              <asp:Label ID="Label1" runat="server" Text="登记时间：" /></td>
                          <td align="left">
                              <asp:DropDownList ID="ddl_Year" runat="server" />&nbsp;年&nbsp;
                              <asp:DropDownList ID="ddl_Month" runat="server" />&nbsp;月&nbsp;</td>
                      </tr>
                  </table>
                  <table width="98%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                      <tr>
                          <td align="right">车牌号码：</td>
                          <td>
                              <asp:TextBox ID="txtCarNum" runat="server"></asp:TextBox></td>
                          <td align="right">时间段：</td>
                          <td>
                              <asp:TextBox ID="txtTime" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right">始发地：</td>
                          <td>
                              <asp:TextBox ID="txtOrigin" runat="server"></asp:TextBox></td>
                          <td align="right">目的地：</td>
                          <td>
                              <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right">始发里程：</td>
                          <td>
                              <asp:TextBox ID="txtStartMile" runat="server"></asp:TextBox></td>
                          <td align="right">终止里程：</td>
                          <td>
                              <asp:TextBox ID="txtFinalMile" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right">司机：</td>
                          <td>
                              <asp:TextBox ID="txtDriver" runat="server"></asp:TextBox></td>
                          <td align="right">调度员：</td>
                          <td>
                              <asp:TextBox ID="txtCotroller" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right">油单价：</td>
                          <td>
                              <asp:TextBox ID="txtUprice" runat="server"></asp:TextBox></td>
                          <td align="right">用油量：</td>
                          <td>
                              <asp:TextBox ID="txtUoil" runat="server"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right">金额：</td>
                          <td>
                              <asp:TextBox ID="txtMoney" runat="server"></asp:TextBox></td>
                          <td align="right">备注：</td>
                          <td>
                              <asp:TextBox ID="txtNote" runat="server"></asp:TextBox></td>
                      </tr>
                  </table>
              </ContentTemplate>
            </cc1:TabPanel>
          </cc1:TabContainer>
        </div>
      </div>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
