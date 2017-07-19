<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Update_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Update_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript">
    window.onload=function()
    {
        var table=document.getElementById("<%=GridView1.ClientID%>");
        if(table)
        {
            var tr=table.getElementsByTagName("tr");
            for(var i=1;i<tr.length;i++)
            {
                var obj=tr[i].getElementsByTagName("td")[1].innerHTML;
                var hdobj=tr[i].getElementsByTagName("td")[8].innerHTML;
                if(hdobj=="0")
                {
                    tr[i].style.display="none";
                }
                if(obj.indexOf("BG")>0)
                {
                    tr[i].style.background="#FF9966";
                }
            }
        }
    }
</script>

    <div class="box-wrapper">
        <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width:96%">
                    <tr><td style="width:10%">最新版材料计划</td></tr>
                </table>
            </div>
        </div>
    </div>
        <div class="box-outer">
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
            没有记录!</asp:Panel>
            <asp:Panel ID="Panel1" runat="server" Style="height: 520px; width: 100%; position: static">
            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true"
                OnRowDataBound="GridView1_RowDataBound">
                <%--<asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" 
                            runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true" 
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
                    <%--<asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="MP_PID" runat="server" style="border-style:none;" type="text" value='<%#Eval("MP_PID") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="MP_PID" HeaderText="批号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_MARID" HeaderText="材料ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NAME" HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_UNIT" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_WEIGHT" HeaderText="重量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_STANDARD" HeaderText="标准" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_USAGE" HeaderText="用途" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_KEYCOMS" HeaderText="关键部件" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TYPE" HeaderText="材料类别" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_TIMERQ" HeaderText="时间要求" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_ENVREFFCT" HeaderText="环保影响" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header,Pager" TableHeight="520px" TableWidth="100%" />
                <%--</asp:GridView>--%>
            </yyc:SmartGridView>
            </asp:Panel>
            <asp:Panel ID="NoDataPanel" runat="server" Font-Size="Large" ForeColor="Red" BackColor="#B3CDE8">
            备注：红色标志为发生变更过的数据!</asp:Panel>
        </div>
    </div>
</asp:Content>

