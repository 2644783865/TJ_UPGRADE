<%@ Control Language="C#" AutoEventWireup="true" 
CodeBehind="ListPager.ascx.cs" Inherits="ZCZJ_DPF.ListPager"%>

<div class="pager">
  <asp:Repeater ID="PageRepeater" runat="server">
    <ItemTemplate>
      <asp:LinkButton CssClass='<%#String.Format("{0}{1}", Eval("CssClass"), ((int)Eval("PageNumber")==CurrentPage)?(" selected"):(""))%>'
        Text='<%#Eval("PageName")%>' CommandName="SetPage" CommandArgument='<%#Eval("PageNumber")%>' CausesValidation="False" runat="server" />
    </ItemTemplate>
  </asp:Repeater>
</div>
