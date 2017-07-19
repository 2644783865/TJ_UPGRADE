<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master"  AutoEventWireup="true" CodeBehind="CM_CollectionRecord.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_CollectionRecord" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">要款记录
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
<div class="RightContent">
  <div class="box-wrapper"> 
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
      <table width="100%">
      <tr>
      <td>查询条件</td>
      <td align="right"><strong>按项目名称查询：</strong></td>
      <td>
          <asp:DropDownList ID="dplXMMC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplXMMC_onSelectIndexChanged">
          </asp:DropDownList>
      </td>
      <td align="right"><strong>按合同编号查询：</strong></td>
      <td>
          <asp:TextBox ID="txtHTBH" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnQuery" runat="server" Text="查 询" onclick="btnQuery_Click" />
          </td>
      <td align="right"><strong>是否到款：</strong></td>
      <td>
          <asp:RadioButtonList ID="rblState" runat="server" AutoPostBack="true" RepeatColumns="3" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblState_OnSelectedIndexChanged">
            <asp:ListItem Text="全部" Value="2" Selected="True"></asp:ListItem>
            <asp:ListItem Text="是" Value="1"></asp:ListItem>
            <asp:ListItem Text="否" Value="0"></asp:ListItem>
          </asp:RadioButtonList>
      </td>
      </tr>
      </table>
    </div>
    </div>
    </div>
     <div class="box-wrapper" >
     <div class="box-outer">
         <asp:GridView ID="grvYK" width="100%" CssClass="toptable grid" runat="server" OnRowDataBound="grvYK_RowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  ShowFooter="true"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="BP_ID" HeaderText="要款单号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_HTBH" HeaderText="合同编号" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PCON_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_KXMC" HeaderText="款项名称" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_YKRQ" DataFormatString="{0:d}" HeaderText="要款日期" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_SKRQ" DataFormatString="{0:d}" HeaderText="收款日期" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_JE" HeaderText="要款金额" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="BP_SFJE" HeaderText="实付金额" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="收款方式" ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:Label ID="Label2" runat="server" Text='<%#Eval("BP_SKFS").ToString()=="0"?"现金":Eval("BP_SKFS").ToString()=="1"?"转支":Eval("BP_SKFS").ToString()=="2"?"电汇":Eval("BP_SKFS").ToString()=="3"?"票汇":"其他"%>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="支付状态" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%#Eval("BP_STATE").ToString()=="1"?"已支付":"未支付" %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="市场部备注" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text="备注..." ToolTip='<%#Eval("BP_NOTEFST") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="财务部备注" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text="备注..." ToolTip='<%#Eval("BP_NOTESND") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
            <FooterStyle  BackColor="#FFFFFF" Font-Bold="True" ForeColor="Blue" HorizontalAlign="Center" />                  
        </asp:GridView>
        <asp:Panel ID="palNoData" runat="server" HorizontalAlign="Center">没有记录!</asp:Panel>
     <br />
     </div>
     </div>
 </div>  
</div>    
</asp:Content>