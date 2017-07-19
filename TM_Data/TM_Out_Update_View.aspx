<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_Out_Update_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Out_Update_View" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术外协
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
                if(obj.indexOf("BG")>0)
                {
                    tr[i].style.background="#FF9966";
                    for(var j=i+1;j<tr.length;j++)//多次变更且原来在原始数据中不存在的
                    {
                        if(tr[j].getElementsByTagName("td")[4].innerHTML==tr[i].getElementsByTagName("td")[4].innerHTML)
                        {
                            tr[i].style.display="none";
                        }
                    }
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
                    <tr>
                    <td style="width:10%">最新版技术外协</td>
                    <td style="width:18%">生产制号：
                       <asp:Label ID="tsa_id" runat="server" Width="100px"></asp:Label>
                    </td>
                    <td style="width:18%">项目名称：
                        <asp:Label ID="proname" runat="server" Width="100px"></asp:Label>
                        <input id="pro_id" type="text" value="" readonly="readonly" runat="server" style="display:none"/>
                    </td>
                    <td style="width:18%">工程名称：
                        <asp:Label ID="engname" runat="server" Width="100px"></asp:Label>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
        <div id="Div1" class="box-outer" runat="server">
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Font-Size="Large">
            没有记录!</asp:Panel>
            <asp:Panel ID="Panel1" runat="server" Style="height: 520px; width: 100%; position: static">
            <yyc:SmartGridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
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
                    <asp:BoundField DataField="OSL_OUTSOURCENO" HeaderText="外协单号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_NAME" HeaderText="部件名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_BIAOSHINO" HeaderText="标识" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_ZONGXU" HeaderText="总序" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_GUIGE" HeaderText="规格" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_CAIZHI" HeaderText="材质" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_UNITWGHT" HeaderText="单重(kg)" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_NUMBER" HeaderText="数量" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_TOTALWGHTL" HeaderText="总重(kg)" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_WDEPNAME" HeaderText="外委部门" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_REQUEST" HeaderText="加工要求" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_REQDATE" HeaderText="加工日期" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OSL_DELSITE" HeaderText="交货地点" ItemStyle-HorizontalAlign="Center" />
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
            <asp:Panel ID="NoDataPanel" runat="server" Font-Size="Large" BackColor="#B3CDE8" ForeColor="Red">
            备注：红色标志为发生变更过的数据!</asp:Panel>
        </div>
    </div>
</asp:Content>
