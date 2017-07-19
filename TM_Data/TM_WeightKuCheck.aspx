<%@ Page Language="C#"  MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_WeightKuCheck.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_WeightKuCheck" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="RightContentTitlePlace">
<table width="100%">
<tr>
<td align="left">库重量核对</td>
<td>核对原则:某序号及其下级在含“库”的情况下，该序号重量应与该序号下所有“库”重量之和相等</td>
</tr>
</table>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title" style="text-align:center;">
            请输入要检验的序号:<asp:TextBox ID="txtXuhao" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            显示级数:<asp:DropDownList ID="ddlJishu" runat="server">
                 <asp:ListItem Text="1级" Value="0"></asp:ListItem>
                 <asp:ListItem Text="2级" Value="1" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="3级" Value="2"></asp:ListItem>
                 <asp:ListItem Text="4级" Value="3"></asp:ListItem>
                 <asp:ListItem Text="5级" Value="4"></asp:ListItem>
                 <asp:ListItem Text="6级" Value="5"></asp:ListItem>
                 <asp:ListItem Text="7级" Value="6"></asp:ListItem>
                 <asp:ListItem Text="8级" Value="7"></asp:ListItem>
                 <asp:ListItem Text="9级" Value="8"></asp:ListItem>
                 <asp:ListItem Text="10级" Value="9"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnQuery"  runat="server" OnClick="btnQuery_OnClick" Text="执行核对" />
            </div>
        </div>
     </div>   
     <div class="box-wrapper">
        <div class="box-outer">
        <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
         <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>  
              <asp:BoundField DataField="BM_XUHAO" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="BM_KU" HeaderText="是否含库" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="BM_WgKu" HeaderText="库重量" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="BM_WgXuhao" HeaderText="序号重量" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Center" />
              <asp:TemplateField HeaderText="库填写是否有误" ItemStyle-HorizontalAlign="Center" >
               <ItemTemplate>
                   <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor='<%#Eval("WT").ToString()=="否"?System.Drawing.Color.Black:System.Drawing.Color.Red %>' Text='<%#Eval("WT").ToString() %>'></asp:Label>
               </ItemTemplate>
              </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </div>
     </div>             
</asp:Content>
