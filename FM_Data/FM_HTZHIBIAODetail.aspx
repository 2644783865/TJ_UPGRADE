<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_HTZHIBIAODetail.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_HTZHIBIAODetail" Title="合同预算添加/修改" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
合同预算添加/修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <script src="../JS/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../JS/EasyUI/easyui-lang-zh_CN.js" type="text/javascript"></script>
    
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="width: 100%">
              <table width="100%">
                <tr>
                    <td align="right">
                        <a id="btnsave" class="easyui-linkbutton" runat="server" onserverclick="btnsave_click">保存</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
              </table>
     </div>
     <div>
         <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <tr>
                    <td align="center">
                        年度：
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="dplYear" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                        添加人：
                    </td>
                    <td align="center">
                        <asp:Label ID="ht_addname" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        添加时间：
                    </td>
                    <td align="center">
                        <asp:Label ID="ht_addtime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        预计新签合同额：
                    </td>
                    <td align="center">
                        <asp:TextBox ID="ht_yusuanhte" runat="server"></asp:TextBox>&nbsp;万元
                    </td>
                    <td align="center">
                        备注：
                    </td>
                    <td align="center" colspan="5">
                        <asp:TextBox ID="ht_note" runat="server" Width="90%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
      </div>
</asp:Content>
