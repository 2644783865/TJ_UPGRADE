<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="FM_Period_Begin_Adjust.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_Period_Begin_Adjust"
    Title="Untitled Page" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    期初调整

    <script language="javascript" type="text/javascript">
      function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 98%; height: 24px">
                    <tr>
                        <td style="width: 40%;">
                            <font style="color: #FF3300">出库核算之前，请您对期初异常数据进行调整;</font>
                        </td>
                        <td align="right" style="width: 40%;">
                        
                        </td>
                        <td align="right" style="width: 30%;">
                            <asp:Button ID="btn_adjust" runat="server" Text="期初调整 " OnClick="btn_adjust_Click"
                                OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />&nbsp;|
                            <asp:Button ID="btn_antiadjust" runat="server" Text="反调整 " OnClick="btn_antiadjust_Click"
                                OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
        
        <asp:HiddenField ID="SUMAMOUNT" runat="server" />
        
        <asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                <ItemTemplate>
                    <%# Container.DataItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SI_MARID" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MNAME" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GUIGE" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CAIZHI" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="GB" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SI_BEGBAL" HeaderText="异常金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SI_YEAR" HeaderText="年" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="SI_PERIOD" HeaderText="月" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <AlternatingRowStyle BackColor="White" />
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    </asp:GridView>
    <asp:Panel ID="NoDataPanel" runat="server">
        没有任务!</asp:Panel>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
        
        </div>
    </div>
</asp:Content>
