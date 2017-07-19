<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true" CodeBehind="FM_YueMo_ChuKu_Adjust_Accounts_Error.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.FM_YueMo_ChuKu_Adjust_Accounts_Error" Title="无标题页" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

   <link href="StyleFile/model.css" rel="stylesheet" type="text/css" />

   <script language="javascript" type="text/javascript">
   function cancel() 
   {
//     window.parent.document.getElementById('ButtonViewCancel').click();
       history.back();
   }
   </script>

    <div class="popup_Container">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <div class="TitlebarRight" onclick="cancel();">
                    </div>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div class="popup_Body">
                
                    <asp:Label ID="LabelErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                    
                    <div style="height: 400px;">
                    <asp:Button ID="btnExport" runat="server" Text="导出" onclick="btnExport_Click" />
                        <yyc:SmartGridView ID="GridViewNoVerityIn"  CellPadding="4" ForeColor="#333333" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="WG_CODE" HeaderText="入库单号" />
                                <asp:BoundField DataField="DocName" HeaderText="制单人" />
                                <asp:BoundField DataField="WG_DATE" HeaderText="制单日期" />
                                <asp:BoundField DataField="VerfierName" HeaderText="审核人" />
                                <asp:BoundField DataField="WG_VERIFYDATE" HeaderText="审核日期" />
                            </Columns>
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#ffffff" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <FixRowColumn FixRowType="Header,Pager" />
                        </yyc:SmartGridView>
                        
                          <yyc:SmartGridView ID="GridViewNoVerityOut"  CellPadding="4" ForeColor="#333333" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="OutCode" HeaderText="出库单号" />
                                <asp:BoundField DataField="Doc" HeaderText="制单人" />
                                <asp:BoundField DataField="Date" HeaderText="制单日期" />
                                <asp:BoundField DataField="Verifier" HeaderText="审核人" />
                                <asp:BoundField DataField="ApprovedDate" HeaderText="审核日期" />
                            </Columns>
                            <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <RowStyle BackColor="#ffffff" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <FixRowColumn FixRowType="Header,Pager" />
                        </yyc:SmartGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
