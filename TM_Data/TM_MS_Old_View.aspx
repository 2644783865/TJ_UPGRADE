<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MS_Old_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_Old_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
 制作明细
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="box-wrapper">
        <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width:96%">
                    <tr><td style="width:10%">与变更相关的所有明细</td></tr>
                </table>
            </div>
        </div>
    </div>
        <div class="box-outer">
            <asp:Panel ID="Panel1" runat="server" Style="height: 480px; width: 100%; position: static">
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" Font-Size="Large">
                没有记录!</asp:Panel>
            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333">
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center"  ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                <asp:BoundField DataField="MS_MSXUHAO" HeaderText="明细序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="MS_NEWINDEX" HeaderText="序号" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_TUHAO" HeaderText="图号" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_ZONGXU" HeaderText="总序" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_NAME" HeaderText="名称" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_GUIGE" HeaderText="规格" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_CAIZHI" HeaderText="材质" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_UNUM" HeaderText="数量" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
               <asp:TemplateField HeaderText="单重" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <asp:Label ID="lblUnitWeight" runat="server" Text='<%# String.Format("{0:F2}",Convert.ToDouble(Eval("MS_UWGHT").ToString())) =="0.00"?String.Format("{0:F4}",Convert.ToDouble(Eval("MS_UWGHT").ToString())):String.Format("{0:F2}",Convert.ToDouble(Eval("MS_UWGHT").ToString())) %>'></asp:Label>                  
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总重" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                        <asp:Label ID="lblTotalWeight" runat="server" Text='<%# Eval("MS_TLWGHT").ToString()==""?"":String.Format("{0:F2}",Convert.ToDouble(Eval("MS_TLWGHT").ToString())) =="0.00"?String.Format("{0:F4}",Convert.ToDouble(Eval("MS_TLWGHT").ToString())):String.Format("{0:F2}",Convert.ToDouble(Eval("MS_TLWGHT").ToString())) %>'></asp:Label>                  
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MS_MASHAPE" HeaderText="毛坯" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_MASTATE" HeaderText="状态" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_STANDARD" HeaderText="标准" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_KU" HeaderText="库"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MS_PROCESS" HeaderText="工艺流程" ItemStyle-Wrap="false" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="MS_NOTE" HeaderText="备注"  ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>   
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="480px" TableWidth="100%" />
            </yyc:SmartGridView>
            <uc1:UCPaging ID="UCPagingMS" runat="server" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>

