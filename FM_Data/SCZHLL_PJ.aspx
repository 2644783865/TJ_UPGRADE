<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true" CodeBehind="SCZHLL_PJ.aspx.cs" Inherits="ZCZJ_DPF.FM_Data.SCZHLL_PJ" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

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
                    <div style="font-size: x-large; font-weight: bold; color: #000000;">
                        生产制号领料明细</div>
                    <div style="height: 400px;">
                        <yyc:SmartGridView ID="GridViewIn" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                            <Columns>
                                <asp:BoundField DataField="Row_Num" HeaderText="序号" />
                                <asp:BoundField DataField="ApprovedDate" HeaderText="日期" />
                                <asp:BoundField DataField="Dep" HeaderText="领料部门" />
                                <asp:BoundField DataField="OutCode" HeaderText="单据编号" />
                                <asp:BoundField DataField="Warehouse" HeaderText="发料仓库" />
                                <asp:BoundField DataField="MaterialCode" HeaderText="物料长代码" />
                                <asp:BoundField DataField="MaterialName" HeaderText="物料名称" />
                                <asp:BoundField DataField="Standard" HeaderText="规格型号" />
                                <asp:BoundField DataField="Unit" HeaderText="单位" />
                                <asp:BoundField DataField="RealNumber" HeaderText="实发数量" />
                                <asp:BoundField DataField="UnitPrice" HeaderText="单价" />
                                <asp:BoundField DataField="Amount" HeaderText="金额" />
                                <asp:BoundField DataField="Sender" HeaderText="制单人" />
                                <asp:BoundField DataField="TSAID" HeaderText="生产制号" />
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