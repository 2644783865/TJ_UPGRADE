<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_sdpriceedit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_sdpriceedit" Title="水电费价格修改" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
   水电费价格修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

 <div class="box-wrapper1">
        <div class="box-outer" style="text-align: center">
            <table width="98%">
                <tr>
                    <td style="width: 20%">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_OnClick" />
                    </td>
                    <td style="width: 78%" align="right">
                        上次修改时间<asp:Label ID="lbxgtime" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <table id="tab" border="1" cellspacing="0" cellpadding="0" runat="server">
                <tr style="width:100%">
                    <td style="width:100px">
                        电费单价
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="dianprice" BorderStyle="None" runat="server"></asp:TextBox>元/度
                    </td>
                    <td style="width:100px">
                        水费单价
                    </td>
                    <td style="width:200px">
                        <asp:TextBox ID="shuiprice" runat="server" BorderStyle="None"></asp:TextBox>元/吨
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
