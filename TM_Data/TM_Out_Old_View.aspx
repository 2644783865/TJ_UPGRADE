<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Old_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Old_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>


<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    外协计划
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-wrapper">
        <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width:96%">
                    <tr><td style="width:10%">变更的外协</td></tr>
                </table>
            </div>
        </div>
    </div>
        <div class="box-outer">
            <asp:Panel ID="Panel1" runat="server" Style="height: 480px; width: 100%; position: static">
            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true"
                OnRowDataBound="GridView1_RowDataBound">
                <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" 
                        onrowdatabound="GridView1_RowDataBound">--%>
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:BoundField DataField="OSL_MARID" HeaderText="物料编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px"/>
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="48px"/>
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"/>
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                    <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                    <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" />
                    <%--<asp:BoundField DataField="MP_USAGE" HeaderText="用途" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响" ItemStyle-HorizontalAlign="Center" />--%>
                    <%--<asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />--%>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="480px" TableWidth="100%" />
                <%--</asp:GridView>--%>
            </yyc:SmartGridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
