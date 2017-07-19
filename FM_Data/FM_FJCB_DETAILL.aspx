<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="FM_FJCB_DETAILL.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_FJCB_DETAILL" Title="无标题页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
修改数据
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:Panel ID="Panel1" runat="server">
<div style=" overflow:scroll">
          <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                border="1" width="100%">
          
                <asp:Repeater ID="rptProNumCost" runat="server">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle headcolor">
                                <th>
                                    任务号
                                </th>
                                <th>
                                    油漆种类
                                </th>
                                <th>
                                    用量（L）
                                </th>
                                <th>
                                    单价（含税）
                                </th>
                                <th>
                                    金额（含税）
                                </th>
                                <th>
                                    稀释剂用量（L）
                                </th>
                                <th>
                                    稀释剂单价（含税）
                                </th>
                                <th>
                                    稀释剂金额（含税）
                                </th>
                                <th>
                                    合计金额（含税）
                                </th>
                                <th>
                                    合计不含税金额
                                </th>
                                <th>
                                    年份
                                </th>
                                <th>
                                    月份
                                </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                            ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <td align="center">
                                    <asp:TextBox ID="tbrwh" runat="server" Text='<%#Eval("FJCB_TSAID")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyqzl" runat="server" Text='<%#Eval("FJCB_YQZL")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyqyl" runat="server" Text='<%#Eval("FJCB_YQYL")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyqhsdj" runat="server" Text='<%#Eval("FJCB_YQHSDJ")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyqhsje" runat="server" Text='<%#Eval("FJCB_YQHSJE")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbxsjyl" runat="server" Text='<%#Eval("FJCB_XSJYL")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbxsjhsdj" runat="server" align="center" Text='<%#Eval("FJCB_XSJHSDJ")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbxsjhsje" runat="server" align="center" Text='<%#Eval("FJCB_XSJHSJE")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbhjhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJHSJE")%>'></asp:TextBox>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:TextBox ID="tbhjbhsje" runat="server" align="center" Text='<%#Eval("FJCB_HJBHSJE")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbnf" runat="server" align="center" Text='<%#Eval("FJCB_YEAR")%>'></asp:TextBox>
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="tbyf" runat="server" align="center" Text='<%#Eval("FJCB_MONTH")%>'></asp:TextBox>
                                </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
            </div>
            <div class="box_right" style="height:20px; padding-top:5px;" align="center" >
                    <asp:Button ID="btnConfirm" runat="server" Text="修改" 
                        onclick="btnConfirm_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small"
                      />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="取消" CausesValidation="false" 
                            onclick="btnCancel_Click" Width="6%" BackColor="White" 
                        BorderStyle="Solid" Font-Bold="True" Font-Size="Small" />
            </div>
         </asp:Panel>
</asp:Content>
